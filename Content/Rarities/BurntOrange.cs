using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Rarities
{
    public class BurntOrange : ModRarity //for cross compat items
    {
        public override Color RarityColor => new Color(224, 89, 0);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset < 0)
            { // If the offset is 1 or 2 (a positive modifier).
                return ItemRarityID.Purple;  // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }

            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }
}