using HendecamMod.Common.Systems;
using HendecamMod.Common.Utils;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace HendecamMod.Content.NPCs.Bosses.PromethiumPlasmoid;

// !!!! This boss is split up into different files for organization !!!!
// This file contains common overriden methods for NPCs/Bosses such as defaults, loot, CanX, CheckX...

[AutoloadBossHead]
public partial class PromethiumPlasmoid : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 6;
        NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
        NPCID.Sets.MPAllowedEnemies[Type] = true;
        NPCID.Sets.TrailCacheLength[NPC.type] = 12;
        NPCID.Sets.TrailingMode[NPC.type] = 3;
        NPCID.Sets.CantTakeLunchMoney[Type] = true;
        NPCID.Sets.DontDoHardmodeScaling[Type] = true;
        //NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers() { };
        /*{
            PortraitScale = 0.2f,
            PortraitPositionYOverride = -150
        };*/
        //NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement>
            {
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                //new FlavorTextBestiaryInfoElement(this.GetLocalizedValue("Bestiary")),
            });
    }

    public override void SetDefaults()
    {
        NPC.damage = 225;
        NPC.defense = 15;
        NPC.lifeMax = 225000;
        NPC.width = 210;
        NPC.height = 210;
        NPC.scale = 1f;
        NPC.knockBackResist = 0;
        NPC.HitSound = SoundID.NPCHit44;
        NPC.DeathSound = SoundID.NPCDeath39;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.value = 10000000;
        NPC.SpawnWithHigherTime(30);
        NPC.boss = true;
        NPC.npcSlots = 10f;

        NPC.aiStyle = -1;
        if (!Main.dedServ)
        {
            Music = MusicID.OtherworldlyBoss1;
        }
    }

    int frameDuration = 6;
    int maxFrames = 6;
    public override void FindFrame(int frameHeight)
    {
        NPC.frameCounter += 1;
        if (NPC.frameCounter > frameDuration)
        {
            NPC.frame.Y += frameHeight;
            NPC.frameCounter = 0;
            if (NPC.frame.Y >= maxFrames * frameHeight)
            {
                NPC.frame.Y = 0;
            }
        }
    }

    public override bool CanHitPlayer(Player target, ref int cooldownSlot)
    {
        cooldownSlot = ImmunityCooldownID.Bosses;
        return true;
    }


    public override bool? CanFallThroughPlatforms()
    {
        return true;
    }

    public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
    {
        NPC.damage = (int)(NPC.damage * balance * 0.5f);
        NPC.lifeMax = (int)(NPC.lifeMax * balance * 0.5f);
    }

    // No despawning
    public override bool CheckActive()
    {
        return false;
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (Main.dedServ) return;
        // Gore, Dust, Etc.
    }

    public override void OnKill()
    {
        NPC.SetEventFlagCleared(ref BossDownedSystem.downedPromethiumPlasmoid, -1);
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        // loot
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        LemonUtils.DrawAfterimages(NPC, drawColor, 1);
        return true;
    }
}