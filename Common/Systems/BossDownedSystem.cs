using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace HendecamMod.Common.Systems
{
    public class HeadOfCthulhuDown : ModSystem
    {
        public static bool downedHeadOfCthulhu = false;
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
        public static bool downedApacheElfShip = false;
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
}