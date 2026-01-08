using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations
    {
    //[AutoloadEquip(EquipType.Beard)]
    public class OcularPeaceAgreement : ModItem
        {
        public override void SetDefaults()
            {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(silver: 1000);
            Item.rare = ItemRarityID.LightPurple;
            Item.accessory = true;
            }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
            {
            var line = new TooltipLine(Mod, "Face", "All eyes should be friendly");
            tooltips.Add(line);
            }
        public override void UpdateEquip(Player player)
            {
            player.npcTypeNoAggro[NPCID.EyeofCthulhu] = true;
            player.npcTypeNoAggro[NPCID.WanderingEye] = true;
            player.npcTypeNoAggro[NPCID.DemonEye] = true;
            player.npcTypeNoAggro[NPCID.DemonEyeOwl] = true;
            player.npcTypeNoAggro[NPCID.DemonEyeSpaceship] = true;
            player.npcTypeNoAggro[NPCID.EyeballFlyingFish] = true;
            player.npcTypeNoAggro[NPCID.Eyezor] = true;
            player.npcTypeNoAggro[NPCID.CataractEye] = true;
            player.npcTypeNoAggro[NPCID.DialatedEye] = true;
            player.npcTypeNoAggro[NPCID.GreenEye] = true;
            player.npcTypeNoAggro[NPCID.MoonLordFreeEye] = true;
            player.npcTypeNoAggro[NPCID.PurpleEye] = true;
            player.npcTypeNoAggro[NPCID.SleepyEye] = true;
            }
        public override void AddRecipes()
            {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.EyeofCthulhuTrophy, 1);
            recipe.AddIngredient<FriendCore>(1);
            recipe.AddIngredient<Paper>();
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();
            }
        }
    }