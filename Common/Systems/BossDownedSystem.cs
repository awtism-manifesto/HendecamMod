using System.IO;
using Terraria.ModLoader.IO;

namespace HendecamMod.Common.Systems;

public class BossDownedSystem : ModSystem
{
    public static bool downedHeadOfCthulhu;
    public static bool downedApacheElfShip;
    public static bool downedPromethiumPlasmoid;

    public override void ClearWorld()
    {
        downedHeadOfCthulhu = false;
        downedApacheElfShip = false;
        downedPromethiumPlasmoid = false;
    }

    public override void SaveWorldData(TagCompound tag)
    {
        tag["downedHeadOfCthulhu"] = downedHeadOfCthulhu;
        tag["downedApacheElfShip"] = downedApacheElfShip;
        tag["downedPromethiumPlasmoid"] = downedPromethiumPlasmoid;
    }

    public override void LoadWorldData(TagCompound tag)
    {
        downedHeadOfCthulhu = tag.GetBool("downedHeadofCthulhu");
        downedApacheElfShip = tag.GetBool("downedApacheElfShip");
        downedPromethiumPlasmoid = tag.GetBool("downedPromethiumPlasmoid");
    }

    public override void NetSend(BinaryWriter writer)
    {
        var flags = new BitsByte();
        flags[0] = downedHeadOfCthulhu;
        flags[1] = downedApacheElfShip;
        flags[2] = downedPromethiumPlasmoid;
        writer.Write(flags);
    }

    public override void NetReceive(BinaryReader reader)
    {
        BitsByte flags = reader.ReadByte();
        // reading in the same order as NetSend
        downedHeadOfCthulhu = flags[0];
        downedApacheElfShip = flags[1];
        downedPromethiumPlasmoid = flags[2];
    }
}