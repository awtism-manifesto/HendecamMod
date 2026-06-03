using Terraria.ModLoader;
using Terraria;

namespace HendecamMod.Content.Global
{
    public class SantankNoFally : GlobalNPC
    {
        public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
        {
          
            return npc.type == NPCID.SantaNK1;
        }
        public override bool PreAI(NPC npc)
        {
            
            int playerX = (int)(npc.position.X + npc.width / 2) / 16;
            int playerY = (int)(npc.position.Y + npc.height) / 16;

            Tile tile = Framing.GetTileSafely(playerX, playerY);

          
            if (Main.tileSolidTop[tile.TileType] && npc.velocity.Y > 0f)
            {
              
                float platformTop = playerY * 16f;
                if (npc.Bottom.Y >= platformTop && npc.Bottom.Y <= platformTop + 8f)
                {
                    npc.velocity.Y = 0f;
                    npc.position.Y = platformTop - npc.height;
                }
            }

            return true;
        }

    }
}