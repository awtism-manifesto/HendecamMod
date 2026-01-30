using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class PacifistsMark : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Grants 200 more Health, at the cost of Combat Buffs");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyHealth>().NastyEffect = true;
        player.buffImmune[BuffID.AmmoReservation] = true;
        player.buffImmune[BuffID.Archery] = true;
        player.buffImmune[BuffID.Endurance] = true;
        player.buffImmune[BuffID.Heartreach] = true;
        player.buffImmune[BuffID.Inferno] = true;
        player.buffImmune[BuffID.Calm] = true;
        player.buffImmune[BuffID.Battle] = true;
        player.buffImmune[BuffID.Ironskin] = true;
        player.buffImmune[BuffID.Lifeforce] = true;
        player.buffImmune[BuffID.MagicPower] = true;
        player.buffImmune[BuffID.ManaRegeneration] = true;
        player.buffImmune[BuffID.Rage] = true;
        player.buffImmune[BuffID.Summoning] = true;
        player.buffImmune[BuffID.Thorns] = true;
        player.buffImmune[BuffID.Titan] = true;
        player.buffImmune[BuffID.Warmth] = true;
        player.buffImmune[BuffID.Wrath] = true;
        player.buffImmune[BuffID.Regeneration] = true;
        player.buffImmune[BuffID.Hunter] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FallenStar, 4);
        recipe.AddIngredient(ItemID.DemonHeart);
        recipe.AddTile(TileID.AdamantiteForge);
        recipe.Register();
    }
}