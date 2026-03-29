using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Body)]
public class AstatineBreastplate : ModItem
{
    public static readonly int AdditiveDamageBonus = 14;
    public static readonly int AttackSpeedBonus = 7;
    public static readonly int RangedCritBonus = 13;
    public static readonly int MeleeArmorPenetration = 12;
    public static LocalizedText SetBonusText { get; private set; }

    public override void SetStaticDefaults()
    {
        SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs();
    }

    public override void SetDefaults()
    {
        Item.width = 32; // Width of the item
        Item.height = 28; // Height of the item
        Item.value = Item.sellPrice(gold: 105); // How many coins the item is worth
        Item.rare = ItemRarityID.Red; // The rarity of the item
        Item.defense = 25; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "14% increased damage and 7% increased attack speed");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "13% increased ranged crit chance and +12 melee armor penetration")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<AstatineHelmet>() && legs.type == ModContent.ItemType<AstatineGreaves>();
    }

    public override void UpdateEquip(Player player)
    {
        player.GetCritChance(DamageClass.Ranged) += RangedCritBonus;
        player.GetAttackSpeed(DamageClass.Generic) += AttackSpeedBonus / 100f;
        player.GetArmorPenetration(DamageClass.Melee) += MeleeArmorPenetration;
        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AstatineBar>(48);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Gives +30% crit chance at max life, Taking damage releases a large nuclear explosion but removes this boost";
        player.GetModPlayer<AstaSetBoom>().AstaBlam = true;
    }
}

public class AstaSetBoom : ModPlayer
{
    public const int CritBonus = 30;
    private const int ExplosionCooldownMax = 60 * 15;

    public bool AstaBlam;
    private int explosionCooldown;

    public override void ResetEffects()
    {
        AstaBlam = false;
    }

    public override void PostUpdate()
    {
        // Cooldown ticking down
        if (explosionCooldown > 0)
            explosionCooldown--;

        // Apply crit bonus only at full health
        if (AstaBlam && Player.statLife == Player.statLifeMax2)
        {
            Player.GetCritChance(DamageClass.Generic) += CritBonus;
        }
    }

    public override void OnHurt(Player.HurtInfo info)
    {
        // Only trigger if set bonus is active
        if (!AstaBlam)
            return;

        // Cooldown check
        if (explosionCooldown > 0)
            return;

        int baseDamage = 925;
        float defenseScale = 3.33f;
        int finalDamage = baseDamage + Player.statDefense * defenseScale;

        // Spawn explosion
        Projectile.NewProjectile(
            Player.GetSource_FromThis(),
            Player.Center,
            new Vector2(0f, -1f),
            ModContent.ProjectileType<AstaBoomOmni>(),
            finalDamage,
            15f,
            Player.whoAmI
        );

        // Start cooldown
        explosionCooldown = ExplosionCooldownMax;
    }
}