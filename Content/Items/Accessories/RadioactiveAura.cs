using System.Collections.Generic;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class RadioactiveAura : ModItem
{
    // By declaring these here, changing the values will alter the effect, and the tooltip

    public static readonly int AdditiveDamageBonus = 8;
    public static readonly int AttackSpeedBonus = 5;

    public static readonly int CritBonus = 8;

    // Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AdditiveDamageBonus);

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.LightPurple;
        Item.value = 530000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Enemies are much less likely to target you");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "8% increased damage and critical strike chance, and 5% increased attack speed")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Your attacks now inflict Radiation Poisoning")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe
            recipe = CreateRecipe();

        recipe.AddIngredient<PlutoniumBar>(8);

        recipe.AddIngredient(ItemID.PutridScent);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
       
        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;
        player.GetAttackSpeed(DamageClass.Generic) += AttackSpeedBonus / 100f;
        player.GetCritChance(DamageClass.Generic) += CritBonus;
        player.GetModPlayer<Rad2Apply>().rad2Effect = true;
        player.aggro += -660;
    }
}

public class Rad2Apply : ModPlayer
{
    public bool rad2Effect;

    public override void ResetEffects()
    {
        rad2Effect = false;
    }

    public override void PostUpdateRunSpeeds()
    {
        if (!Player.GetModPlayer<Rad2Apply>().rad2Effect)
        {
            return;
        }

        if (Main.rand.NextBool(4))
        {
            int dust = Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<PlutoniumDust>(),
                Player.velocity.X * Main.rand.NextFloat(-1.2f, 2.33f), Player.velocity.Y * Main.rand.NextFloat(-1.2f, 2.33f), 70, default, 0.82f);
            Main.dust[dust].noGravity = true;
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!Player.GetModPlayer<Rad2Apply>().rad2Effect)
        {
        }
        else
        {
            target.AddBuff(ModContent.BuffType<RadPoisoning2>(), 240);
        }
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!Player.GetModPlayer<Rad2Apply>().rad2Effect)
        {
        }
        else
        {
            target.AddBuff(ModContent.BuffType<RadPoisoning2>(), 240);
        }
    }
}