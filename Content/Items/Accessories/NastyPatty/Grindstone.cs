using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class Grindstone : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Grants double your generic armor penetration, at the cost of Envoirnmental Buffs");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyPenetration>().NastyEffect = true;
        player.buffImmune[BuffID.ParryDamageBuff] = true;
        player.buffImmune[BuffID.SoulDrain] = true;
        player.buffImmune[BuffID.HeartyMeal] = true;
        player.buffImmune[BuffID.CoolWhipPlayerBuff] = true;
        player.buffImmune[BuffID.ScytheWhipPlayerBuff] = true;
        player.buffImmune[BuffID.SwordWhipPlayerBuff] = true;
        player.buffImmune[BuffID.ThornWhipPlayerBuff] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SharpeningStation);
        recipe.AddTile(TileID.SharpeningStation);
        recipe.Register();
    }
}