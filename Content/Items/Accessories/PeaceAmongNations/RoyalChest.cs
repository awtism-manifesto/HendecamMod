using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Beard)]
public class RoyalChest : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Mimics should be friendly");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.npcTypeNoAggro[NPCID.Mimic] = true;
        player.npcTypeNoAggro[NPCID.BigMimicCorruption] = true;
        player.npcTypeNoAggro[NPCID.BigMimicCrimson] = true;
        player.npcTypeNoAggro[NPCID.BigMimicHallow] = true;
        player.npcTypeNoAggro[NPCID.BigMimicJungle] = true;
        player.npcTypeNoAggro[NPCID.IceMimic] = true;
        player.npcTypeNoAggro[NPCID.PresentMimic] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.ToySled);
        recipe.AddIngredient<FriendCore>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}