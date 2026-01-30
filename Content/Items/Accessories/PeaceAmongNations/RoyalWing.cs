using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Beard)]
public class RoyalWing : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Bats should be friendly");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.npcTypeNoAggro[NPCID.CaveBat] = true;
        player.npcTypeNoAggro[NPCID.SporeBat] = true;
        player.npcTypeNoAggro[NPCID.JungleBat] = true;
        player.npcTypeNoAggro[NPCID.Hellbat] = true;
        player.npcTypeNoAggro[NPCID.IceBat] = true;
        player.npcTypeNoAggro[NPCID.GiantBat] = true;
        player.npcTypeNoAggro[NPCID.IlluminantBat] = true;
        player.npcTypeNoAggro[NPCID.Lavabat] = true;
        player.npcTypeNoAggro[NPCID.Slimer] = true;
        player.npcTypeNoAggro[NPCID.GiantFlyingFox] = true;
        player.npcTypeNoAggro[NPCID.VampireBat] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BatBat);
        recipe.AddIngredient<FriendCore>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}