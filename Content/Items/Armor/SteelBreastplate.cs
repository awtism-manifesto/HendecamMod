using System.Collections.Generic;
using HendecamMod.Content.Items.Placeables;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Body)]
public class SteelBreastplate : ModItem
{
    public static LocalizedText SetBonusText { get; private set; }

    public override void SetStaticDefaults()
    {
        SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs();
    }

    public override void SetDefaults()
    {
        Item.width = 32; 
        Item.height = 28; 
        Item.value = Item.sellPrice(gold: 2); 
        Item.rare = ItemRarityID.Blue; 
        Item.defense = 7; 
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<SteelHelmet>() && legs.type == ModContent.ItemType<SteelLeggings>();
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GoldChainmail);
        recipe.AddIngredient<SteelBar>(4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.PlatinumChainmail);
        recipe.AddIngredient<SteelBar>(4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.statDefense += 5;
        player.setBonus = "+5 defense";
    }
}