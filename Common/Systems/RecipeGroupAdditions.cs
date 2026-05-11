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
        // okay this code makes no fucking sense the modloader fucking REFUSES to acknowledge it's fucking existence and i am doing everything EXACTLY the same as examplemod but with different names for stuff and it's REALLY pissing me off
        //  AND YES I REMEMBERED THE FUCKING SYSTEMS USING DIRECTORY IN THE ITEM WHERE I TRIED TO IMPLEMENT THIS
        // SO EITHER, EXAMPLEMOD'S CODE IS ENTIRELY BROKEN AND UNUSABLE, OR IT IS MISSING **CRITICAL** CONTEXT ON HOW TO ACTUALLY IMPLEMENT A FUCKING RECIPE GROUP
        // IF THERE'S SOME OTHER RANDOM BULLSHIT OUTSIDE OF EXAMPLERECIPES.CS THAT I NEED, I WILL BLOW MY GOD DAMN BRAINS OUT BECAUSE THERE ARE **ZERO** FUCKING REFERENCES TO OTHER CODE FILES EXCEPT FOR EXAMPLEITEMS THEMSELVES IN THAT FILE
        public static RecipeGroup StoneGroup;

        public override void Unload()
        {
            StoneGroup = null;
        }
        public override void AddRecipeGroups()
        {
            RecipeGroup StoneGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.StoneBlock)}", ItemID.StoneBlock, ItemID.EbonstoneBlock, ItemID.PearlstoneBlock, ItemID.CrimstoneBlock);
            RecipeGroup.RegisterGroup(nameof(ItemID.StoneBlock), StoneGroup);
        }
    }
}
