using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class SlashedJacket : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 4000);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Grants 200 Health, 30% increased attack speed, double the breath timer, and Hellfire for all attacks"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "No longer gain effects from Flasks, Combat, Activated, or Enviornmental Buffs"));
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyBreath>().NastyEffect = true;
        player.GetModPlayer<NastySpeed>().NastyEffect = true;
        player.GetModPlayer<NastyHealth>().NastyEffect = true;
        player.GetModPlayer<NastyFire>().NastyEffect = true;
        player.buffImmune[BuffID.WeaponImbueConfetti] = true;
        player.buffImmune[BuffID.WeaponImbueCursedFlames] = true;
        player.buffImmune[BuffID.WeaponImbueFire] = true;
        player.buffImmune[BuffID.WeaponImbueGold] = true;
        player.buffImmune[BuffID.WeaponImbueIchor] = true;
        player.buffImmune[BuffID.WeaponImbueNanites] = true;
        player.buffImmune[BuffID.WeaponImbuePoison] = true;
        player.buffImmune[BuffID.WeaponImbueVenom] = true;
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
        player.buffImmune[BuffID.AmmoBox] = true;
        player.buffImmune[BuffID.Bewitched] = true;
        player.buffImmune[BuffID.Clairvoyance] = true;
        player.buffImmune[BuffID.Sharpened] = true;
        player.buffImmune[BuffID.WarTable] = true;
        player.buffImmune[BuffID.SugarRush] = true;
        player.buffImmune[BuffID.Campfire] = true;
        player.buffImmune[BuffID.DryadsWard] = true;
        player.buffImmune[BuffID.Sunflower] = true;
        player.buffImmune[BuffID.HeartLamp] = true;
        player.buffImmune[BuffID.Honey] = true;
        player.buffImmune[BuffID.PeaceCandle] = true;
        player.buffImmune[BuffID.StarInBottle] = true;
        player.buffImmune[BuffID.CatBast] = true;
        player.buffImmune[BuffID.MonsterBanner] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<BurntPan>();
        recipe.AddIngredient<TornFilter>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}