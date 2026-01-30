using HendecamMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Buffs;
public class WeaponImbueFusion : ModBuff
{
    public override void SetStaticDefaults()
    {
        BuffID.Sets.IsAFlaskBuff[Type] = true;
        Main.meleeBuff[Type] = true;
        Main.persistentBuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<FusionImbueGlobal>().FusionWeaponImbue = true;
        player.MeleeEnchantActive = true; // MeleeEnchantActive indicates to other mods that a weapon imbue is active.
    }
}
public class FusionImbueGlobal : ModPlayer
{
    public bool FusionWeaponImbue = false;

    public override void ResetEffects()
    {
        FusionWeaponImbue = false;
    }

    public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (FusionWeaponImbue && item.DamageType.CountsAsClass<MeleeDamageClass>())
        {
            target.AddBuff(ModContent.BuffType<RadPoisoning3>(), 60 * Main.rand.Next(3, 7));
        }
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (FusionWeaponImbue && (proj.DamageType.CountsAsClass<MeleeDamageClass>() || ProjectileID.Sets.IsAWhip[proj.type]) && !proj.noEnchantments)
        {
            target.AddBuff(ModContent.BuffType<RadPoisoning3>(), 60 * Main.rand.Next(3, 7));
        }
    }

    // MeleeEffects and EmitEnchantmentVisualsAt apply the visual effects of the weapon imbue to items and projectiles respectively.
    public override void MeleeEffects(Item item, Rectangle hitbox)
    {
        if (FusionWeaponImbue && item.DamageType.CountsAsClass<MeleeDamageClass>() && !item.noMelee && !item.noUseGraphic)
        {
            if (Main.rand.NextBool(5))
            {
                Dust dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<AstatineDust>());
                dust.velocity *= 0.5f;
            }
        }
    }

    public override void EmitEnchantmentVisualsAt(Projectile projectile, Vector2 boxPosition, int boxWidth, int boxHeight)
    {
        if (FusionWeaponImbue && (projectile.DamageType.CountsAsClass<MeleeDamageClass>() || ProjectileID.Sets.IsAWhip[projectile.type]) && !projectile.noEnchantments)
        {
            if (Main.rand.NextBool(5))
            {
                Dust dust = Dust.NewDustDirect(boxPosition, boxWidth, boxHeight, ModContent.DustType<AstatineDust>());
                dust.velocity *= 0.5f;
            }
        }
    }
}