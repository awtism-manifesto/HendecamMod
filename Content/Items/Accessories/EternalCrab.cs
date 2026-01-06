using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Rarities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories
{
    public class EternalCrab : ModItem
    {
        // By declaring these here, changing the values will alter the effect, and the tooltip

        
        public static readonly int ArmorPenetration = 1000;
        
        

        // Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
        

        public override void SetDefaults()
        {
            Item.width = 45;
            Item.height = 30;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<Seizure2>();
            Item.value = 99988700;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrabBanner);
            recipe.AddIngredient<TheSecondAmendment>();
            recipe.AddIngredient(ItemID.KingSlimeTrophy);
            recipe.AddIngredient(ItemID.EyeofCthulhuTrophy);
            recipe.AddIngredient(ItemID.EaterofWorldsTrophy);
            recipe.AddIngredient(ItemID.BrainofCthulhuTrophy);
            recipe.AddIngredient(ItemID.BossTrophyDarkmage);
            recipe.AddIngredient(ItemID.DeerclopsTrophy);
            recipe.AddIngredient(ItemID.QueenBeeTrophy);
            recipe.AddIngredient(ItemID.SkeletronTrophy);
            recipe.AddIngredient(ItemID.WallofFleshTrophy);
            recipe.AddIngredient(ItemID.FlyingDutchmanTrophy);
            recipe.AddIngredient(ItemID.QueenSlimeTrophy);
            recipe.AddIngredient(ItemID.DestroyerTrophy);
            recipe.AddIngredient(ItemID.RetinazerTrophy);
            recipe.AddIngredient(ItemID.SpazmatismTrophy);
            recipe.AddIngredient(ItemID.SkeletronPrimeTrophy);
            recipe.AddIngredient(ItemID.BossTrophyOgre);
            recipe.AddIngredient(ItemID.PlanteraTrophy);
            recipe.AddIngredient(ItemID.MourningWoodTrophy);
            recipe.AddIngredient(ItemID.PumpkingTrophy);
            recipe.AddIngredient(ItemID.EverscreamTrophy);
            recipe.AddIngredient(ItemID.SantaNK1Trophy);
            recipe.AddIngredient(ItemID.IceQueenTrophy);
            recipe.AddIngredient(ItemID.GolemTrophy);
            recipe.AddIngredient(ItemID.MartianSaucerTrophy);
            recipe.AddIngredient(ItemID.BossTrophyBetsy);
            recipe.AddIngredient(ItemID.AncientCultistTrophy);
            recipe.AddIngredient(ItemID.MoonLordTrophy);

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "Increases armor penetration by 1000");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);



           
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            

         
            player.GetArmorPenetration(DamageClass.Generic) += ArmorPenetration;
        }
    }
}