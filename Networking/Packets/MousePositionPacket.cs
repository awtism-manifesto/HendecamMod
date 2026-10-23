using HendecamMod.Common.Systems.Players;
using System.IO;
using Terraria;

namespace HendecamMod.Networking.Packets;

public sealed class MousePositionPacket : HendecamPacket 
{
    public MousePositionPacket(Player player)
    {
        var modPlayer = player.GetModPlayer<HendecamPlayer>();
        Writer.TryWriteSenderPlayer(player);
        Writer.WriteVector2(modPlayer.MousePosition);
    }
    public override void Read(BinaryReader reader, int sender)
    {
        if (!reader.TryReadSenderPlayer(sender, out var player) || !player.TryGetModPlayer(out HendecamPlayer modPlayer))
        {
            return;
        }
        modPlayer.MousePosition = reader.ReadVector2();
        if (Main.dedServ)
        {
            NetSystem.SendPacket(new MousePositionPacket(player), ignoreClient: sender);
        }
    }
}