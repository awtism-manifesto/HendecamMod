using HendecamMod.Common.Utils;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;
using HendecamMod.Networking;
using HendecamMod.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HendecamMod.Common.Systems.Players
{
    public partial class HendecamPlayer
    {
        public override void UpdateBadLifeRegen()
        {
            if (Rad)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 16;
            }
            if (Rad2)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 30;
            }
            if (Rad3)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= (int)(48.5f);
            }
            if (Ligma)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= (int)(1500f);
            }
            if (MoonBurnt)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= (int)(222f);
            }
            if (lifeRegenDebuff)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                // Player.lifeRegenTime used to increase the speed at which the player reaches its maximum natural life regeneration
                // So we set it to 0, and while this debuff is active, it never reaches it
                Player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second
                Player.lifeRegen -= 16;
            }
            if (Scald)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 15;
            }
        }
        public override void PreUpdate()
        {
            UpdateMouse();
            var config = ModContent.GetInstance<HendecamExperimentalConfig>();
            if (config.DeathEnabled)
            {
                if (Main.rand.NextBool(config.DeathChance * 60))
                {
                    Player.AddBuff(ModContent.BuffType<OtherworldPoisoning>(), 120);
                }
            }
        }
        public override void UpdateEquips()
        {
        }
        public override void PostUpdateEquips()
        {
            base.PostUpdateEquips();
        }
        public void UpdateMouse()
        {
            if (Player.IsLocalPlayer())
            {
                mousePosPrev = MousePosition;
                MousePosition = Main.MouseWorld;
                if (Main.netMode == NetmodeID.MultiplayerClient && mousePosPrev != MousePosition)
                {
                    NetSystem.SendPacket(new MousePositionPacket(Player));
                }
            }
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (FissionWeaponImbue && item.DamageType.CountsAsClass<MeleeDamageClass>())
            {
                target.AddBuff(BuffType<RadPoisoning2>(), 60 * Main.rand.Next(3, 7));
            }
            if (FusionWeaponImbue && item.DamageType.CountsAsClass<MeleeDamageClass>())
            {
                target.AddBuff(BuffType<RadPoisoning3>(), 60 * Main.rand.Next(3, 7));
            }
            if (radWeaponImbue && item.DamageType.CountsAsClass<MeleeDamageClass>())
            {
                target.AddBuff(BuffType<RadPoisoning>(), 60 * Main.rand.Next(3, 7));
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (FissionWeaponImbue && (proj.DamageType.CountsAsClass<MeleeDamageClass>() || ProjectileID.Sets.IsAWhip[proj.type]) && !proj.noEnchantments)
            {
                target.AddBuff(BuffType<RadPoisoning2>(), 60 * Main.rand.Next(3, 7));
            }
            if (FusionWeaponImbue && (proj.DamageType.CountsAsClass<MeleeDamageClass>() || ProjectileID.Sets.IsAWhip[proj.type]) && !proj.noEnchantments)
            {
                target.AddBuff(BuffType<RadPoisoning3>(), 60 * Main.rand.Next(3, 7));
            }
            if (radWeaponImbue && (proj.DamageType.CountsAsClass<MeleeDamageClass>() || ProjectileID.Sets.IsAWhip[proj.type]) && !proj.noEnchantments)
            {
                target.AddBuff(BuffType<RadPoisoning>(), 60 * Main.rand.Next(3, 7));
            }
        }

        public override void MeleeEffects(Item item, Rectangle hitbox)
        {
            if (FissionWeaponImbue && item.DamageType.CountsAsClass<MeleeDamageClass>() && !item.noMelee && !item.noUseGraphic)
            {
                if (Main.rand.NextBool(5))
                {
                    Dust dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustType<PlutoniumDust>());
                    dust.velocity *= 0.5f;
                }
            }
            if (FusionWeaponImbue && item.DamageType.CountsAsClass<MeleeDamageClass>() && !item.noMelee && !item.noUseGraphic)
            {
                if (Main.rand.NextBool(5))
                {
                    Dust dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustType<AstatineDust>());
                    dust.velocity *= 0.5f;
                }
            }
            if (radWeaponImbue && item.DamageType.CountsAsClass<MeleeDamageClass>() && !item.noMelee && !item.noUseGraphic)
            {
                if (Main.rand.NextBool(5))
                {
                    Dust dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustType<UraniumDust>());
                    dust.velocity *= 0.5f;
                }
            }
        }

        public override void EmitEnchantmentVisualsAt(Projectile projectile, Vector2 boxPosition, int boxWidth, int boxHeight)
        {
            if (FissionWeaponImbue && (projectile.DamageType.CountsAsClass<MeleeDamageClass>() || ProjectileID.Sets.IsAWhip[projectile.type]) && !projectile.noEnchantments)
            {
                if (Main.rand.NextBool(5))
                {
                    Dust dust = Dust.NewDustDirect(boxPosition, boxWidth, boxHeight, DustType<PlutoniumDust>());
                    dust.velocity *= 0.5f;
                }
            }
            if (FusionWeaponImbue && (projectile.DamageType.CountsAsClass<MeleeDamageClass>() || ProjectileID.Sets.IsAWhip[projectile.type]) && !projectile.noEnchantments)
            {
                if (Main.rand.NextBool(5))
                {
                    Dust dust = Dust.NewDustDirect(boxPosition, boxWidth, boxHeight, DustType<AstatineDust>());
                    dust.velocity *= 0.5f;
                }
            }
            if (radWeaponImbue && (projectile.DamageType.CountsAsClass<MeleeDamageClass>() || ProjectileID.Sets.IsAWhip[projectile.type]) && !projectile.noEnchantments)
            {
                if (Main.rand.NextBool(5))
                {
                    Dust dust = Dust.NewDustDirect(boxPosition, boxWidth, boxHeight, DustType<UraniumDust>());
                    dust.velocity *= 0.5f;
                }
            }
        }
    }
}
