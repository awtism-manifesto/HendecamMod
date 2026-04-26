using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;

namespace HendecamMod.Common.Systems
{
    public class RecipeGroupAdditions : ModSystem
    {
        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.3")} {Lang.GetItemNameValue(ItemID.StoneBlock)}", ItemID.StoneBlock, ItemID.EbonstoneBlock, ItemID.PearlstoneBlock, ItemID.CrimstoneBlock);
            RecipeGroup.RegisterGroup(nameof(ItemID.StoneBlock), group);
        }
    }
}
