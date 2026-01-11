using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Rarities
{
    public class DarkGreen : ModRarity //for icons
    {
        public override Color RarityColor => new Color(0, 89, 8);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset < 0)
            {
                return ItemRarityID.Purple;
            }

            return Type;
        }
    }
}