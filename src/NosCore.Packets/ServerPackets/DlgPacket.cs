using System;
using System.Collections.Generic;
using System.Text;
using NosCore.Core.Serializing;
using NosCore.Packets.ClientPackets;

namespace NosCore.Packets.ServerPackets
{
    [PacketHeader("dlg")]
    public class DlgPacket : PacketDefinition
    {
        [PacketIndex(0, IsReturnPacket = true)]
        public FinsPacket YesPacket { get; set; }

        [PacketIndex(1, IsReturnPacket = true)]
        public FinsPacket NoPacket { get; set; }

        [PacketIndex(2, SerializeToEnd = true)]
        public string Question { get; set; }
    }
}
