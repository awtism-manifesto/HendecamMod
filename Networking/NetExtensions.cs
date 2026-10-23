using HendecamMod.Common.Systems.DataStructures;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace HendecamMod.Networking;

public static class NetExtensions // from overhaul. credits to Mirsario
{
    public static void WritePoint16(this BinaryWriter writer, Point16 p)
    {
        writer.Write(p.X);
        writer.Write(p.Y);
    }
    public static Point16 ReadPoint16(this BinaryReader reader)
    {
        return new Point16(reader.ReadInt16(), reader.ReadInt16());
    }
    public static void WriteGuid(this BinaryWriter writer, Guid guid)
    {
        writer.Write(guid.ToByteArray());
    }
    public static Guid ReadGuid(this BinaryReader reader)
    {
        return new Guid(reader.ReadBytes(16));
    }
    public static void WritePoint8(this BinaryWriter writer, Point8 point)
    {
        writer.Write(point.X);
        writer.Write(point.Y);
    }
    public static Point8 ReadPoint8(this BinaryReader reader)
    {
        return new Point8(reader.ReadByte(), reader.ReadByte());
    }

    public static void WriteNetworkText(this BinaryWriter writer, NetworkText text)
    {
        text.Serialize(writer);
    }
    public static NetworkText ReadNetworkText(this BinaryReader reader)
    {
        return NetworkText.Deserialize(reader);
    }
    public static void TryWriteSenderPlayer(this BinaryWriter writer, Player player)
    {
        if (Main.dedServ)
        {
            writer.Write((byte)player.whoAmI);
        }
    }
    public static bool TryReadSenderPlayer(this BinaryReader reader, int sender, out Player player)
    {
        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            sender = reader.ReadByte();
        }

        player = Main.player[sender];

        return player != null && player.active;
    }
}