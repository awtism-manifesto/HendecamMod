using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace HendecamMod.Common.Systems;

public class HeadOfCthulhuDown : ModSystem
{
    public static bool downedHeadOfCthulhu;

    public override void ClearWorld()
    {
        downedHeadOfCthulhu = false;
    }

    public override void SaveWorldData(TagCompound tag)
    {
        tag["downedHeadOfCthulhu"] = downedHeadOfCthulhu;
    }

    public override void LoadWorldData(TagCompound tag)
    {
        downedHeadOfCthulhu = tag.GetBool("downedHeadofCthulhu");
    }

    public override void NetSend(BinaryWriter writer)
    {
        var flags = new BitsByte();
        flags[0] = downedHeadOfCthulhu;
        writer.Write(flags);
    }

    public override void NetReceive(BinaryReader reader)
    {
        BitsByte flags = reader.ReadByte();
        downedHeadOfCthulhu = flags[0];
    }
}

public class ApacheElfShipDown : ModSystem
{
    public static bool downedApacheElfShip;

    public override void ClearWorld()
    {
        downedApacheElfShip = false;
    }

    public override void SaveWorldData(TagCompound tag)
    {
        tag["downedApacheElfShip"] = downedApacheElfShip;
    }

    public override void LoadWorldData(TagCompound tag)
    {
        downedApacheElfShip = tag.GetBool("downedApacheElfShip");
    }

    public override void NetSend(BinaryWriter writer)
    {
        var flags = new BitsByte();
        flags[0] = downedApacheElfShip;
        writer.Write(flags);
    }

    public override void NetReceive(BinaryReader reader)
    {
        BitsByte flags = reader.ReadByte();
        downedApacheElfShip = flags[0];
    }
}

public class PromethiumPlasmoidDown : ModSystem
{
    public static bool downedPromethiumPlasmoid;

    public override void ClearWorld()
    {
        downedPromethiumPlasmoid = false;
    }

    public override void SaveWorldData(TagCompound tag)
    {
        tag["downedPromethiumPlasmoid"] = downedPromethiumPlasmoid;
    }

    public override void LoadWorldData(TagCompound tag)
    {
        downedPromethiumPlasmoid = tag.GetBool("downedPromethiumPlasmoid");
    }

    public override void NetSend(BinaryWriter writer)
    {
        var flags = new BitsByte();
        flags[0] = downedPromethiumPlasmoid;
        writer.Write(flags);
    }

    public override void NetReceive(BinaryReader reader)
    {
        BitsByte flags = reader.ReadByte();
        downedPromethiumPlasmoid = flags[0];
    }
}