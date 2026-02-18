using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class SuperCeramicFedora : ModItem
{
    public static readonly int AdditiveStupidDamageBonus = 13;
    public static readonly int StupidAttackSpeedBonus = 10;
    public static readonly int StupidCritBonus = 9;
    public static LocalizedText SetBonusText { get; private set; }

    public override void SetStaticDefaults()
    {
        ArmorIDs.Head.Sets.IsTallHat[Item.headSlot] = true;
        ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs(AdditiveStupidDamageBonus);
    }

    public override void SetDefaults()
    {
        Item.width = 22; 
        Item.height = 18; 
        Item.value = Item.sellPrice(gold: 13); 
        Item.rare = ItemRarityID.LightRed; 
        Item.defense = 7; 
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "9% increased stupid damage and crit chance");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+45 max Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<SuperCeramicChestplate>() && legs.type == ModContent.ItemType<SuperCeramicLeggings>();
    }

    public override void UpdateEquip(Player player)
    {
       
        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 10f;

        player.GetCritChance<StupidDamage>() += StupidCritBonus;

        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
        loboPlayer.MaxBonus += 45f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<CeramicSheet>(25);
        recipe.AddIngredient<EbonceramicSheet>(10);
        recipe.AddIngredient<PearlceramicSheet>(10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient<CeramicSheet>(25);
        recipe.AddIngredient<CrimceramicSheet>(10);
        recipe.AddIngredient<PearlceramicSheet>(10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "35% increased damage reduction at max HP, getting hit causes ceramic shards to shatter off the player but removes the boost";
        player.GetModPlayer<CeramMultiscale>().Multiscale = true;
    }
}

public class CeramMultiscale : ModPlayer
{
    private const int ShatterCooldownMax = 60 * 10;

    public bool Multiscale;
    private int ShatterCooldown;

    public override void ResetEffects()
    {
        Multiscale = false;
    }

    public override void PostUpdate()
    {
        if (ShatterCooldown > 0)
            ShatterCooldown--;
        if (Multiscale && Player.statLife == Player.statLifeMax2)
        {
            Player.endurance = 1f - 0.65f * (1f - Player.endurance);
        }
    }

    public override void OnHurt(Player.HurtInfo info)
    {
        if (!Multiscale)
            return;
        if (ShatterCooldown > 0)
            return;

        int baseDamage = 35;
        float defenseScale = 0.75f;
        int finalDamage = baseDamage + Player.statDefense * defenseScale;
        Projectile.NewProjectile(
            Player.GetSource_FromThis(),
            Player.Center,
            new Vector2(0f, -5f),
            ModContent.ProjectileType<CeramOmniSpawn>(),
            finalDamage,
            3f,
            Player.whoAmI
        );

        // Start cooldown
        ShatterCooldown = ShatterCooldownMax;
    }
}