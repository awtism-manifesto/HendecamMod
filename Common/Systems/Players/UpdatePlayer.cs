using HendecamMod.Common.Utilities;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;
using HendecamMod.Networking;
using HendecamMod.Networking.Packets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Common.Systems.Players;

public partial class HendecamPlayer
{
    private void ApplyBuffImmunities(int[] buffs)
    {
        for (int i = 0; i < buffs.Length; i++)
            Player.buffImmune[buffs[i]] = true;
    }

    private void ClearBuffList(int[] buffs)
    {
        for (int i = 0; i < buffs.Length; i++)
            Player.ClearBuff(buffs[i]);
    }

    public override void UpdateBadLifeRegen()
    {
        int totalDoT = 0;
        if (Rad) totalDoT += 16;
        if (Rad2) totalDoT += 30;
        if (Rad3) totalDoT += 48;
        if (Ligma) totalDoT += 1500;
        if (MoonBurnt) totalDoT += 222;
        if (lifeRegenDebuff) totalDoT += 16;
        if (Scald) totalDoT += 15;

        if (totalDoT > 0)
        {
            if (Player.lifeRegen > 0)
                Player.lifeRegen = 0;
            Player.lifeRegenTime = 0;
            Player.lifeRegen -= totalDoT;
        }
    }

    public override void PreUpdate()
    {
        UpdateMouse();
        var config = ModContent.GetInstance<HendecamExperimentalConfig>();
        if (config.DeathEnabled && Main.rand.NextBool(config.DeathChance * 60))
        {
            Player.AddBuff(ModContent.BuffType<OtherworldPoisoning>(), 120);
        }
    }

    public override void UpdateEquips()
    {
        if (isCamo)
        {
            Player.GetAttackSpeed(DamageClass.Generic) += 0.05f;
            Player.GetCritChance(DamageClass.Generic) += 10;
            Player.aggro -= 500;
        }

        if (nastyPatty)
        {
            for (int i = 1; i < BuffID.Count; i++)
            {
                if (!Main.debuff[i] || Main.vanityPet[i] || Main.lightPet[i] || BuffID.Sets.BasicMountData[i] != null)
                {
                    Player.buffImmune[i] = true;
                    Player.ClearBuff(i);
                }
            }
            Player.maxMinions = 0;

            if (Player.mount.Active)
                Player.mount.Dismount(Player);
        }
        if (lunchly)
        {
            junkFood = true;
            moldyCheese = true;
            ceaseDesist = true;
        }
        if (junkFood)
        {
            schoolFood = true;
            slashedJacket = true;
            hateMail = true;
            nostalgicDiamond = true;
        }
        if (moldyCheese)
        {
            schoolFood = true;
            slashedJacket = true;
        }
        if (ceaseDesist)
        {
            hateMail = true;
            nostalgicDiamond = true;
            disenchanter = true;
        }
        if (schoolFood)
        {
            prisonFood = true;
            dustyMedal = true;
        }
        if (prisonFood)
        {
            dietSchedule = true;
            ankleMonitor = true;
        }
        if (dustyMedal)
        {
            oldOnesGlasses = true;
            emptyFishingBucket = true;
        }
        if (slashedJacket)
        {
            burntPan = true;
            tornFilter = true;
        }
        if (burntPan)
        {
            hotWax = true;
            pacifistsMark = true;
        }
        if (tornFilter)
        {
            brokenMouse = true;
            gasMask = true;
        }
        if (hateMail)
        {
            expiredFood = true;
            nintendoPatent = true;
        }
        if (expiredFood)
        {
            petaAutograph = true;
            tornSaddle = true;
        }
        if (petaAutograph)
        {
            poachersContract = true;
            killShelterPermit = true;
        }
        if (disenchanter)
        {
            alphaChecklist = true;
            leadPlating = true;
            grindstone = true;
        }
        if (alphaChecklist)
        {
            ApplyBuffImmunities(AccessoryBuffs);
        }
        if (ankleMonitor)
        {
            ApplyBuffImmunities(ExplorationBuffs);
        }
        if (brokenMouse)
        {
            ApplyBuffImmunities(StationBuffs);
        }
        if (gasMask)
        {
            ApplyBuffImmunities(ArenaBuffs);
        }
        if (hotWax)
        {
            for (int i = 1; i < Player.buffType.Length; i++)
            {
                if (BuffID.Sets.IsAFlaskBuff[i])
                {
                    Player.buffImmune[i] = true;
                    Player.ClearBuff(i);
                }
            }
        }
        if (killShelterPermit)
        {
            for (int i = 1; i < BuffID.Count; i++)
            {
                if (Main.vanityPet[i])
                {
                    Player.buffImmune[i] = true;
                    Player.ClearBuff(i);
                }
            }
        }
        if (leadPlating)
        {
            ApplyBuffImmunities(ArmorBuffs);
        }
        if (nostalgicDiamond)
        {
            for (int i = 1; i < BuffID.Count; i++)
            {
                if (BuffID.Sets.BasicMountData[i] != null)
                {
                    Player.buffImmune[i] = true;
                    Player.ClearBuff(i);
                }
            }

            if (Player.mount.Active && Player.mount.Cart)
                Player.mount.Dismount(Player);
        }
        if (oldOnesGlasses)
        {
            ApplyBuffImmunities(BuldingBuffs);
        }
        if (pacifistsMark)
        {
            ApplyBuffImmunities(CombatBuffs);
        }
        if (grindstone)
        {
            ApplyBuffImmunities(GrindstoneBuffs);
        }
        if (poachersContract)
        {
            for (int i = 1; i < BuffID.Count; i++)
            {
                if (Main.lightPet[i])
                {
                    Player.buffImmune[i] = true;
                    Player.ClearBuff(i);
                }
            }
        }
        if (nintendoPatent)
        {
            Player.maxMinions = 0;
        }
        if (dietSchedule)
        {
            ApplyBuffImmunities(FoodBuffs);
        }
        if (emptyFishingBucket)
        {
            ApplyBuffImmunities(FishingBuffs);
        }
        if (tornSaddle)
        {
            for (int i = 1; i < BuffID.Count; i++)
            {
                if (BuffID.Sets.BasicMountData[i] != null)
                {
                    Player.buffImmune[i] = true;
                    Player.ClearBuff(i);
                }
            }

            if (Player.mount.Active && !Player.mount.Cart)
                Player.mount.Dismount(Player);
        }
    }

    public override void PostUpdateEquips()
    {
        if (soulOfImmunity)
        {
            for (int i = 1; i < Player.buffType.Length; i++)
            {
                Player.buffImmune[i] = true;
                Player.ClearBuff(i);
            }
            if (Player.mount.Active)
                Player.mount.Dismount(Player);
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && !Main.npc[i].boss)
                {
                    Player.npcTypeNoAggro[Main.npc[i].type] = true;

                }
            }
        }
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

    public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
    {
        if (isCamo)
        {
            a = 128;
        }
        if (cubeWeed)
        {
            r = 0.2f;
            g = 0.72f;
            b = 0.1f;
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
            target.AddBuff(BuffID.OnFire3, 600);
    }

    private void ApplyImbueDebuffs(NPC target)
    {
        if (FissionWeaponImbue) target.AddBuff(BuffType<RadPoisoning2>(), 60 * Main.rand.Next(3, 7));
        if (FusionWeaponImbue) target.AddBuff(BuffType<RadPoisoning3>(), 60 * Main.rand.Next(3, 7));
        if (radWeaponImbue) target.AddBuff(BuffType<RadPoisoning>(), 60 * Main.rand.Next(3, 7));
    }

    private void SpawnImbueDusts(Vector2 position, int width, int height)
    {
        if (FissionWeaponImbue && Main.rand.NextBool(5))
            Dust.NewDustDirect(position, width, height, DustType<PlutoniumDust>()).velocity *= 0.5f;
        if (FusionWeaponImbue && Main.rand.NextBool(5))
            Dust.NewDustDirect(position, width, height, DustType<AstatineDust>()).velocity *= 0.5f;
        if (radWeaponImbue && Main.rand.NextBool(5))
            Dust.NewDustDirect(position, width, height, DustType<UraniumDust>()).velocity *= 0.5f;
    }

    private static bool IsValidMeleeProj(Projectile proj) =>
        (proj.DamageType.CountsAsClass<MeleeDamageClass>() || ProjectileID.Sets.IsAWhip[proj.type]) && !proj.noEnchantments;

    public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (item.DamageType.CountsAsClass<MeleeDamageClass>())
            ApplyImbueDebuffs(target);
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (IsValidMeleeProj(proj))
            ApplyImbueDebuffs(target);
    }

    public override void MeleeEffects(Item item, Rectangle hitbox)
    {
        if (item.DamageType.CountsAsClass<MeleeDamageClass>() && !item.noMelee && !item.noUseGraphic)
            SpawnImbueDusts(hitbox.TopLeft(), hitbox.Width, hitbox.Height);
    }

    public override void EmitEnchantmentVisualsAt(Projectile projectile, Vector2 boxPosition, int boxWidth, int boxHeight)
    {
        if (IsValidMeleeProj(projectile))
            SpawnImbueDusts(boxPosition, boxWidth, boxHeight);
    }
}