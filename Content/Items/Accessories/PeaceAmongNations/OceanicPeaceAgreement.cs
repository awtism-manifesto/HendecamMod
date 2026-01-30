using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Buttplug)]
public class OceanicPeaceAgreement : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(copper: 676767);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "The depths of the ocean should be friendly");
        tooltips.Add(line);
    }
    public override void UpdateEquip(Player player)
    {
        player.npcTypeNoAggro[NPCID.Crab] = true;
        player.npcTypeNoAggro[NPCID.Shark] = true;
        player.npcTypeNoAggro[NPCID.Sharkron] = true;
        player.npcTypeNoAggro[NPCID.Sharkron2] = true;
        player.npcTypeNoAggro[NPCID.Squid] = true;
        player.npcTypeNoAggro[NPCID.SeaSnail] = true;
        player.npcTypeNoAggro[NPCID.BloodNautilus] = true;

    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DukeFishronTrophy);
        recipe.AddIngredient<FriendCore>(1);
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}
