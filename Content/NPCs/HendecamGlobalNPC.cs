using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace HendecamMod.Content.NPCs;

public class HendecamGlobalNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;

    // Debuff Flags & Counters
    public bool Radded;
    public bool Radded2;
    public bool Radded3;
    public bool MoonCooked;
    public bool DyingOfLigma;
    public bool Purpled;
    public bool gay;
    public bool hasDebuff;
    public bool Splintering;
    public bool Scaldded;
    public int ScaldTimer = 298;
    public bool Microplastic;
    public int MicroplasticStacks = 0;

    public override void ResetEffects(NPC npc)
    {
        Radded = false;
        Radded2 = false;
        Radded3 = false;
        MoonCooked = false;
        DyingOfLigma = false;
        Purpled = false;
        gay = false;
        Scaldded = false;
        Splintering = false;
        hasDebuff = false;

        if (!npc.HasBuff(BuffType<MicroplasticPoison>()))
        {
            Microplastic = false;
            MicroplasticStacks = 0;
        }
    }

    private void DOTDebuff(NPC npc, float damagePerSecond, int multiplier, ref int damage)
    {
        if (npc.lifeRegen > 0)
        {
            npc.lifeRegen = 0;
        }
        npc.lifeRegen -= (int)(damagePerSecond * multiplier);
        if (damage < damagePerSecond)
        {
            damage = (int)damagePerSecond;
        }
    }
    public void SyncData(NPC npc)
    {
        if (Main.netMode == NetmodeID.Server)
        {
            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI);
        }
    }
    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (Radded)
        {
            DOTDebuff(npc, 7, 7, ref damage);
        }
        if (Radded2)
        {
            DOTDebuff(npc, 13, 8, ref damage);
        }
        if (Radded3)
        {
            DOTDebuff(npc, 21, 9, ref damage);
        }
        if (MoonCooked)
        {
            DOTDebuff(npc, 66, 6, ref damage);
        }
        if (DyingOfLigma)
        {
            DOTDebuff(npc, 911, 13, ref damage);
        }
        if (Purpled)
        {
            DOTDebuff(npc, 72, 7, ref damage);
        }
        if (Microplastic)
        {
            DOTDebuff(npc, 1f, MicroplasticStacks, ref damage);
        }
        if (Scaldded)
        {
            DOTDebuff(npc, 8, 7, ref damage);
        }
        if (Splintering)
        {
            DOTDebuff(npc, 2, 7, ref damage);
        }
    }

    public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
        if (Purpled)
        {
            modifiers.Defense *= 0.5f;
        }
        if (gay)
        {
            modifiers.Defense *= Gay.DefenseMultiplier;
        }
    }

    public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
    {
        if (projectile.npcProj || projectile.trap)
            return;
        if (hasDebuff && (projectile.CountsAsClass<StupidDamage>() || projectile.DamageType.CountsAsClass<MeleeDamageClass>()))
        {
            modifiers.SourceDamage *= 1.15f;
        }
        if (!projectile.IsMinionOrSentryRelated)
            return;

        float projTagMultiplier = ProjectileID.Sets.SummonTagDamageMultiplier[projectile.type];

        if (npc.HasBuff<AlpineTagBuff>())
        {
            modifiers.FlatBonusDamage += AlpineTagBuff.TagDamage * projTagMultiplier;
        }
        if (npc.HasBuff<EmblemTag>())
        {
            modifiers.FlatBonusDamage += EmblemTag.TagDamage * projTagMultiplier;
        }
        if (npc.HasBuff<GalaxyTag>())
        {
            modifiers.FlatBonusDamage += GalaxyTag.TagDamage * projTagMultiplier;
        }
        if (npc.HasBuff<JevilTag>())
        {
            modifiers.FlatBonusDamage += JevilTag.TagDamage * projTagMultiplier;
        }
        if (npc.HasBuff<KevlarTag>())
        {
            modifiers.FlatBonusDamage += KevlarTag.TagDamage * projTagMultiplier;
        }
        if (npc.HasBuff<KingTag>())
        {
            modifiers.FlatBonusDamage += KingTag.TagDamage * projTagMultiplier;
        }
        if (npc.HasBuff<LilTMan>())
        {
            modifiers.FlatBonusDamage += LilTMan.TagDamage * projTagMultiplier;
        }
        if (npc.HasBuff<PearlTag>())
        {
            modifiers.FlatBonusDamage += PearlTag.TagDamage * projTagMultiplier;
        }
        if (npc.HasBuff<PlutoWhipBuff>())
        {
            modifiers.FlatBonusDamage += PlutoWhipBuff.TagDamage * projTagMultiplier;
        }
        if (npc.HasBuff<ShinyTag>())
        {
            modifiers.FlatBonusDamage += ShinyTag.TagDamage * projTagMultiplier;
        }
        if (npc.HasBuff<ShinyTag2>())
        {
            modifiers.FlatBonusDamage += ShinyTag2.TagDamage * projTagMultiplier;
        }
        if (npc.HasBuff<SmoothTag>())
        {
            modifiers.FlatBonusDamage += SmoothTag.TagDamage * projTagMultiplier;
        }
        if (npc.HasBuff<SpikyTag>())
        {
            modifiers.FlatBonusDamage += SpikyTag.TagDamage * projTagMultiplier;
        }
    }

    public override void OnKill(NPC npc)
    {
        switch (npc.type)
        {
            case NPCID.EyeofCthulhu:
                if (!NPC.downedBoss1)
                {
                    GetInstance<PyriteSystem>().BlessWorldWithPyrite();
                }
                break;

            case NPCID.SkeletronHead:
                if (!NPC.downedBoss3)
                {
                    GetInstance<AzuriteSystem>().BlessWorldWithAzurite();
                }
                break;

            case NPCID.Deerclops:
                if (!NPC.downedDeerclops)
                {
                    GetInstance<LycopiteSystem>().BlessWorldWithLycopite();
                }
                break;

            case NPCID.WallofFlesh:
                if (!Main.hardMode)
                {
                    GetInstance<MintalSystem>().BlessWorldWithMintal();
                }
                break;

            case NPCID.Plantera:
                if (!NPC.downedPlantBoss)
                {
                    GetInstance<MorbiumSystem>().BlessWorldWithMorbium();
                }
                break;
        }
    }

    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        if (Purpled)
        {
            drawColor.R = 203;
            drawColor.G = 69;
            drawColor.B = 255;
        }

        if (gay)
        {
            drawColor.R = (byte)Main.rand.Next(89, 233);
            drawColor.G = (byte)Main.rand.Next(89, 233);
            drawColor.B = (byte)Main.rand.Next(89, 233);
        }

        if (hasDebuff)
        {
            if (Main.rand.NextBool(7))
            {
                int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustType<LycopiteDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, Color.Orange);
                Main.dust[dust].velocity *= 1.2f;
                Main.dust[dust].velocity.Y -= 0.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale *= 1.2f;
                Main.dust[dust].alpha /= 3;
            }
            if (Main.rand.NextBool(8))
            {
                int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustType<PlutoniumDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, Color.LightPink);
                Main.dust[dust2].velocity *= 1.2f;
                Main.dust[dust2].velocity.Y -= 0.5f;
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].scale *= 1.2f;
                Main.dust[dust2].alpha /= 3;
            }
        }
    }
}