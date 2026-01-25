using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

public class MechanicalPeaceAgreement : ModItem
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
        var line = new TooltipLine(Mod, "Face", "A certain trio should be friendly");
        tooltips.Add(line);
        }
    public override void UpdateEquip(Player player)
        {
        player.npcTypeNoAggro[NPCID.Spazmatism] = true;
        player.npcTypeNoAggro[NPCID.Retinazer] = true;
        player.npcTypeNoAggro[NPCID.SkeletronPrime] = true;
        player.npcTypeNoAggro[NPCID.PrimeCannon] = true;
        player.npcTypeNoAggro[NPCID.PrimeLaser] = true;
        player.npcTypeNoAggro[NPCID.PrimeSaw] = true;
        player.npcTypeNoAggro[NPCID.PrimeVice] = true;
        }
    public override void AddRecipes()
        {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.RetinazerTrophy, 1);
        recipe.AddIngredient(ItemID.SpazmatismTrophy, 1);
        recipe.AddIngredient(ItemID.SkeletronPrimeTrophy, 1);
        recipe.AddIngredient(ItemID.DestroyerTrophy, 1);
        recipe.AddIngredient<FriendCore>(1);
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
        }
    }
