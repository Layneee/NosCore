using System;
using System.Collections.Generic;
using System.Text;
using NosCore.Core.Serializing;

namespace NosCore.Packets.ServerPackets
{
    [PacketHeader("dlg")]
    public class DlgPacket : PacketDefinition
    {
        [PacketIndex(0, SerializeToEnd = true)]
        public string PacketContent { get; set; } //TODO: Review this
    }
}
