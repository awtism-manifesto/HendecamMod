using System.IO;
using Terraria.ModLoader.IO;

namespace HendecamMod.Common.Systems;

public class BossDownedSystem : ModSystem
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
      
        flags[1] = downedApacheElfShip;
      
        writer.Write(flags);
    }

    public override void NetReceive(BinaryReader reader)
    {
        BitsByte flags = reader.ReadByte();
        // reading in the same order as NetSend
      
        downedApacheElfShip = flags[1];
      
    }
}