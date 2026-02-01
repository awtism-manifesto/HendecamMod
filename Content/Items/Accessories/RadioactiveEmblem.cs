using System.Collections.Generic;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class RadioactiveEmblem : ModItem
{
    // By declaring these here, changing the values will alter the effect, and the tooltip

    public static readonly int AdditiveDamageBonus = 21;
    public static readonly int AttackSpeedBonus = 7;
    public static readonly int CritBonus = 7;

    // Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AdditiveDamageBonus);

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Red;
        Item.value = 2500000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "21% increased damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "7% increased crit chance and attack speed")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Causes your attacks to irradiate any enemy you hit")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "These buffs come at the cost of 15% of your max life")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe = CreateRecipe();
        recipe.AddIngredient<AstatineBar>(12);
        recipe.AddIngredient<PlutoniumBar>(12);
        recipe.AddIngredient<UraniumBar>(12);
        recipe.AddIngredient(ItemID.DestroyerEmblem);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        // GetDamage returns a reference to the specified damage class' damage StatModifier.
        // Since it doesn't return a value, but a reference to it, you can freely modify it with mathematics operators (+, -, *, /, etc.).
        // StatModifier is a structure that separately holds float additive and multiplicative modifiers, as well as base damage and flat damage.
        // When StatModifier is applied to a value, its additive modifiers are applied before multiplicative ones.
        // Base damage is added directly to the weapon's base damage and is affected by damage bonuses, while flat damage is applied after all other calculations.
        // In this case, we're doing a number of things:
        // - Adding 25% damage, additively. This is the typical "X% damage increase" that accessories use, use this one.
        // - Adding 12% damage, multiplicatively. This effect is almost never used in Terraria, typically you want to use the additive multiplier above. It is extremely hard to correctly balance the game with multiplicative bonuses.
        // - Adding 4 base damage.
        // - Adding 5 flat damage.
        // Since we're using DamageClass.Generic, these bonuses apply to ALL damage the player deals.
        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;
        player.GetAttackSpeed(DamageClass.Generic) += AttackSpeedBonus / 100f;
        player.GetCritChance(DamageClass.Generic) += CritBonus;
        player.statLifeMax2 = (int)(player.statLifeMax2 * 0.85f);
        player.GetModPlayer<RadApply>().radEffect = true;
    }
}

public class RadApply : ModPlayer
{
    public bool radEffect;

    public override void ResetEffects()
    {
        radEffect = false;
    }

    public override void PostUpdateRunSpeeds()
    {
        if (!Player.GetModPlayer<RadApply>().radEffect)
        {
            return;
        }

        if (Main.rand.NextBool(6)) // 1-in-3 chance every tick
        {
            int dust = Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<UraniumDust>(),
                Player.velocity.X * Main.rand.NextFloat(-1.2f, 2.33f), Player.velocity.Y * Main.rand.NextFloat(-1.2f, 2.33f), 70, default, 0.82f);
            Main.dust[dust].noGravity = true;
        }

        if (Main.rand.NextBool(6)) // 1-in-3 chance every tick
        {
            int dust = Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<PlutoniumDust>(),
                Player.velocity.X * Main.rand.NextFloat(-1.2f, 2.33f), Player.velocity.Y * Main.rand.NextFloat(-1.2f, 2.33f), 70, default, 0.82f);
            Main.dust[dust].noGravity = true;
        }

        if (Main.rand.NextBool(6)) // 1-in-3 chance every tick
        {
            int dust = Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<AstatineDust>(),
                Player.velocity.X * Main.rand.NextFloat(-1.2f, 2.33f), Player.velocity.Y * Main.rand.NextFloat(-1.2f, 2.33f), 70, default, 0.82f);
            Main.dust[dust].noGravity = true;
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!Player.GetModPlayer<RadApply>().radEffect)
        {
        }
        else
        {
            target.AddBuff(ModContent.BuffType<RadPoisoning3>(), 120);
            target.AddBuff(ModContent.BuffType<RadPoisoning2>(), 280);
            target.AddBuff(ModContent.BuffType<RadPoisoning>(), 540);
        }
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!Player.GetModPlayer<RadApply>().radEffect)
        {
        }
        else
        {
            target.AddBuff(ModContent.BuffType<RadPoisoning3>(), 120);
            target.AddBuff(ModContent.BuffType<RadPoisoning2>(), 280);
            target.AddBuff(ModContent.BuffType<RadPoisoning>(), 540);
        }
    }
}