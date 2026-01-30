using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class BrokenMouse : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 500);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Grants 30% more Attack Speed, at the cost of Activated Buffs");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastySpeed>().NastyEffect = true;
        player.buffImmune[BuffID.AmmoBox] = true;
        player.buffImmune[BuffID.Bewitched] = true;
        player.buffImmune[BuffID.Clairvoyance] = true;
        player.buffImmune[BuffID.Sharpened] = true;
        player.buffImmune[BuffID.WarTable] = true;
        player.buffImmune[BuffID.SugarRush] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();

        recipe.AddIngredient(ItemID.IronBar, 5);
        recipe.AddIngredient(ItemID.Wire, 10);
        recipe.AddIngredient<ShatteredKeyboard>();
        recipe.AddIngredient(ItemID.TungstenHammer);
        recipe.AddTile(TileID.HeavyWorkBench);
        recipe.Register();
    }
}