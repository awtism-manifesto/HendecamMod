using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class BurntPan : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 1000);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 60;
        Item.useAnimation = 60;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item178;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 100;
        Item.knockBack = 40.0f;
        Item.ChangePlayerDirectionOnShoot = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "When Used:"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Inflicts Hellfire and grants 4hp/s life regen"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "When Equipped:"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Grants 200 Health and Hellfire for all attacks"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "No longer gain effects from Flasks or Combat Buffs"));
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        player.lifeRegen += (int)(8f);
        return true;
    }

    public override void UpdateEquip(Player player)
    {
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
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<PacifistsMark>();
        recipe.AddIngredient<HotWax>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}