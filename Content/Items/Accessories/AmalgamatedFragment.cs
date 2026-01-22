using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Accessories
{
    public class AmalgamatedFragment : ModItem
    {

        public static readonly int AdditiveDamageBonus = 12;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AdditiveDamageBonus);
        public override void SetStaticDefaults()
        {


            ItemID.Sets.ItemIconPulse[Item.type] = true; 
            ItemID.Sets.ItemNoGravity[Item.type] = true; 

           
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.accessory = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 999000;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "Face", "Increases melee, ranged, magic, summon, and stupid damage by 11%");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "Slightly less effective with Omni-class weapons")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);

            if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))


            {

                line = new TooltipLine(Mod, "Face", "Hendecam Mod Cross-Mod (Thorium) - 11% increased throwing damage")
                {
                    OverrideColor = new Color(34, 221, 240)
                };
                tooltips.Add(line);
            }
            foreach (var l in tooltips)
            {
                if (l.Name.EndsWith(":RemoveMe"))
                {
                    l.Hide();
                }
            }
        }
        public override void AddRecipes()
        {


            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentSolar, 15);
            recipe.AddIngredient(ItemID.FragmentVortex, 15);
            recipe.AddIngredient(ItemID.FragmentNebula, 15);
            recipe.AddIngredient(ItemID.FragmentStardust, 15);
            recipe.AddIngredient<FragmentFlatEarth>(15);
           
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica) && ThorMerica.TryFind("WhiteDwarfFragment", out ModItem WhiteDwarfFragment))


            {
                recipe.AddIngredient(WhiteDwarfFragment.Type, 15);

            }

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) += AdditiveDamageBonus / 112f;
            player.GetDamage(DamageClass.Ranged) += AdditiveDamageBonus / 112f;
            player.GetDamage(DamageClass.Magic) += AdditiveDamageBonus / 112f;
            player.GetDamage(DamageClass.Summon) += AdditiveDamageBonus / 112f;
            
            player.GetDamage<StupidDamage>() += AdditiveDamageBonus / 112f;
            player.GetDamage<OmniDamage>() -= AdditiveDamageBonus / 92f;
            if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))


            {
                player.GetDamage(DamageClass.Throwing) += AdditiveDamageBonus / 112f;

            }
        }
    }
}
