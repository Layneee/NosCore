﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NosCore.Core;
using NosCore.Core.Networking;
using NosCore.Data.StaticEntities;
using NosCore.Data.WebApi;
using NosCore.Database.Entities;
using NosCore.DAL;
using NosCore.Shared.Enumerations.Map;
using NosCore.Shared.I18N;

namespace NosCore.GameObject.Networking
{
    public sealed class ServerManager : BroadcastableBase
    {
        private static ServerManager _instance;

        private static readonly ConcurrentDictionary<Guid, MapInstance> Mapinstances =
            new ConcurrentDictionary<Guid, MapInstance>();

        private static readonly List<Map.Map> Maps = new List<Map.Map>();

        public ConcurrentDictionary<long, CharacterRelationDTO> CharacterRelations { get; set; }

        private ServerManager()
        {
        }
        private static int seed = Environment.TickCount;
        private static readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));
        public int RandomNumber(int min = 0, int max = 100)
        {
            return random.Value.Next(min, max);
        }
        public static ServerManager Instance => _instance ?? (_instance = new ServerManager());

        public List<NpcMonsterDTO> NpcMonsters { get; set; }

        public MapInstance GenerateMapInstance(short mapId, MapInstanceType type)
        {
            var map = Maps.Find(m => m.MapId.Equals(mapId));
            if (map == null)
            {
                return null;
            }

            var guid = Guid.NewGuid();
            var mapInstance = new MapInstance(map, guid, false, type);
            mapInstance.LoadPortals();
            mapInstance.LoadMonsters();
            mapInstance.LoadNpcs();
            mapInstance.StartLife();
            Mapinstances.TryAdd(guid, mapInstance);
            return mapInstance;
        }

        public void Initialize()
        {
            // parse rates
            try
            {
                CharacterRelations = new ConcurrentDictionary<long, CharacterRelationDTO>();
                var i = 0;
                var monstercount = 0;
                var npccount = 0;
                NpcMonsters = DAOFactory.NpcMonsterDAO.LoadAll().ToList();
                var mapPartitioner = Partitioner.Create(DAOFactory.MapDAO.LoadAll().Cast<Map.Map>(),
                    EnumerablePartitionerOptions.NoBuffering);
                var mapList = new ConcurrentDictionary<short, Map.Map>();
                Parallel.ForEach(mapPartitioner, new ParallelOptions { MaxDegreeOfParallelism = 8 }, map =>
                  {
                      var guid = Guid.NewGuid();
                      map.Initialize();
                      mapList[map.MapId] = map;
                      var newMap = new MapInstance(map, guid, map.ShopAllowed, MapInstanceType.BaseMapInstance);
                      Mapinstances.TryAdd(guid, newMap);
                      newMap.LoadPortals();
                      newMap.LoadMonsters();
                      newMap.LoadNpcs();
                      newMap.StartLife();
                      monstercount += newMap.Monsters.Count;
                      npccount += newMap.Npcs.Count;
                      i++;
                  });
                Maps.AddRange(mapList.Select(s => s.Value));
                if (i != 0)
                {
                    Logger.Log.Info(string.Format(LogLanguage.Instance.GetMessageFromKey(LanguageKey.MAPS_LOADED), i));
                }
                else
                {
                    Logger.Log.Error(LogLanguage.Instance.GetMessageFromKey(LanguageKey.NO_MAP));
                }
                Logger.Log.Info(string.Format(LogLanguage.Instance.GetMessageFromKey(LanguageKey.MAPNPCS_LOADED),
                    npccount));
                Logger.Log.Info(string.Format(LogLanguage.Instance.GetMessageFromKey(LanguageKey.MAPMONSTERS_LOADED),
                    monstercount));
            }
            catch (Exception ex)
            {
                Logger.Log.Error("General Error", ex);
            }
        }

        public Guid GetBaseMapInstanceIdByMapId(short mapId)
        {
            return Mapinstances.FirstOrDefault(s =>
                s.Value?.Map.MapId == mapId && s.Value.MapInstanceType == MapInstanceType.BaseMapInstance).Key;
        }

        public MapInstance GetMapInstance(Guid id)
        {
            return Mapinstances.ContainsKey(id) ? Mapinstances[id] : null;
        }

        public void BroadcastPacket(PostedPacket postedPacket, int? channelId = null)
        {
            if (channelId == null)
            {
                foreach (var channel in WebApiAccess.Instance.Get<List<WorldServerInfo>>("api/channels"))
                {
                    WebApiAccess.Instance.Post<PostedPacket>("api/packet", postedPacket, channel.WebApi);
                }
            }
            else
            {
                var channel = WebApiAccess.Instance.Get<List<WorldServerInfo>>("api/channels")
                    .FirstOrDefault(s => s.Id == channelId.Value);
                if (channel != null)
                {
                    WebApiAccess.Instance.Post<PostedPacket>("api/packet", postedPacket, channel.WebApi);
                }
            }
        }

        public void BroadcastPackets(List<PostedPacket> packets, int? channelId = null)
        {
            foreach (var packet in packets)
            {
                BroadcastPacket(packet, channelId);
            }
        }
    }
}