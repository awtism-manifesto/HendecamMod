using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class RadioactiveEmblem : ModItem
{

    public static readonly int AdditiveDamageBonus = 21;
    public static readonly int AttackSpeedBonus = 7;
    public static readonly int CritBonus = 7;

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
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
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

        if (Main.rand.NextBool(6)) 
        {
            int dust = Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<UraniumDust>(),
                Player.velocity.X * Main.rand.NextFloat(-1.2f, 2.33f), Player.velocity.Y * Main.rand.NextFloat(-1.2f, 2.33f), 70, default, 0.82f);
            Main.dust[dust].noGravity = true;
        }

        if (Main.rand.NextBool(6)) 
        {
            int dust = Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<PlutoniumDust>(),
                Player.velocity.X * Main.rand.NextFloat(-1.2f, 2.33f), Player.velocity.Y * Main.rand.NextFloat(-1.2f, 2.33f), 70, default, 0.82f);
            Main.dust[dust].noGravity = true;
        }

        if (Main.rand.NextBool(6)) 
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