using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Beard)]
public class RoyalMushroom : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Fungal enemies should be friendly");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.npcTypeNoAggro[NPCID.AnomuraFungus] = true;
        player.npcTypeNoAggro[NPCID.FungiBulb] = true;
        player.npcTypeNoAggro[NPCID.FungiSpore] = true;
        player.npcTypeNoAggro[NPCID.FungoFish] = true;
        player.npcTypeNoAggro[NPCID.GiantFungiBulb] = true;
        player.npcTypeNoAggro[NPCID.SporeBat] = true;
        player.npcTypeNoAggro[NPCID.SporeSkeleton] = true;
        player.npcTypeNoAggro[NPCID.MushiLadybug] = true;
        player.npcTypeNoAggro[NPCID.ZombieMushroom] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Shroomerang);
        recipe.AddIngredient<FriendCore>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}