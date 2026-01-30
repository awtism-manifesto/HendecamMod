using System.Collections.Generic;
using HendecamMod.Content.Items.Placeables;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class HotWax : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Makes ALL attacks inflict Hellfire, at the cost of Flasks");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyFire>().NastyEffect = true;
        player.buffImmune[BuffID.WeaponImbueConfetti] = true;
        player.buffImmune[BuffID.WeaponImbueCursedFlames] = true;
        player.buffImmune[BuffID.WeaponImbueFire] = true;
        player.buffImmune[BuffID.WeaponImbueGold] = true;
        player.buffImmune[BuffID.WeaponImbueIchor] = true;
        player.buffImmune[BuffID.WeaponImbueNanites] = true;
        player.buffImmune[BuffID.WeaponImbuePoison] = true;
        player.buffImmune[BuffID.WeaponImbueVenom] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<SteelBar>(5);
        recipe.AddIngredient(ItemID.BeeWax, 30);
        recipe.AddIngredient(ItemID.MagmaStone);
        recipe.AddTile(TileID.HoneyDispenser);
        recipe.Register();
    }
}