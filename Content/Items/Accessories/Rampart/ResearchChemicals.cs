
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.Rampart
    {
    //[AutoloadEquip(EquipType.Beard)]
    public class ResearchChemicals : ModItem
        {
        public override void SetDefaults()
            {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(silver: 4000);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
            }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
            {
            tooltips.Add(new TooltipLine(Mod, "Face", "Grants immunity to Chaos State, Potion Sickness, Creative Shock,"));
            tooltips.Add(new TooltipLine(Mod, "Face", "Might Wind, Moon Bite, The Tongue, Shimmer, and the Shadow Candle"));
            }
        public override void UpdateEquip(Player player)
            {
            player.buffImmune[BuffID.ChaosState] = true;
            player.ClearBuff(BuffID.PotionSickness);
            player.buffImmune[BuffID.NoBuilding] = true;
            player.buffImmune[BuffID.WindPushed] = true;
            player.buffImmune[BuffID.MoonLeech] = true;
            player.buffImmune[BuffID.TheTongue] = true;
            player.buffImmune[BuffID.Shimmer] = true;
            player.buffImmune[BuffID.ShadowCandle] = true;
            }
        public override void AddRecipes()
            {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient<LitBong>(1);
            recipe.AddIngredient<LeadBlanket>(1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
            }
        }
    }