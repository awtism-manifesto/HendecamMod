using HendecamMod.Common.Systems;

namespace HendecamMod.Content.NPCs.Bosses;

[AutoloadBossHead]
public class HeadOfCthulhu : ModNPC
{
    private int nextSpawnTick;
    private int tickCounter;

    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 4;
        NPCID.Sets.MPAllowedEnemies[Type] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.BoneJavelin] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.TentacleSpike] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.BloodButcherer] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Shimmer] = true;
        if (Main.hardMode)
        {
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
        }
    }

    public override void SetDefaults()
    {
        if (Main.hardMode)
        {
            NPC.damage = 94;
            NPC.defense = 82;
            NPC.lifeMax = 80000;
        }
        else
        {
            NPC.damage = 32;
            NPC.defense = 67;
            NPC.lifeMax = 10050;
        }

        NPC.width = 300;
        NPC.height = 300;

        NPC.knockBackResist = 0;

        NPC.HitSound = SoundID.NPCHit57;
        NPC.DeathSound = SoundID.NPCDeath62;

        NPC.noGravity = true;
        NPC.noTileCollide = true;

        NPC.SpawnWithHigherTime(30);
        NPC.boss = true;
        NPC.npcSlots = 10f;

        NPC.aiStyle = NPCAIStyleID.Flocko;
        if (!Main.dedServ)
        {
            if (Main.hardMode)
            {
                Music = MusicID.OtherworldlyLunarBoss;
            }
            else
            {
                Music = MusicID.OtherworldlyTowers;
            }
        }
    }

    public override void FindFrame(int frameHeight)
    {
        Main.npcFrameCount[Type] = 4;
        AnimationType = NPCID.BlazingWheel;
    }

    public override void OnKill()
    {
        NPC.SetEventFlagCleared(ref BossDownedSystem.downedHeadOfCthulhu, -1);
    }

    public override bool CanHitPlayer(Player target, ref int cooldownSlot)
    {
        cooldownSlot = ImmunityCooldownID.Bosses;
        return true;
    }

    public override void AI()
    {
        if (!NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
        {
            NPC.SetDefaults(0);
            NPC.active = false;
        }

        if (nextSpawnTick == 0)
        {
            nextSpawnTick = Main.rand.Next(24, 26);
        }

        tickCounter++;

        if (tickCounter >= nextSpawnTick)
        {
            NPC.TargetClosest();
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                var source = NPC.GetSource_FromAI();
                Vector2 position = NPC.Center;
                Vector2 targetPosition = Main.player[NPC.target].Center;
                Vector2 direction = targetPosition - position;
                direction.Normalize();
                float speed = 10f;
                int damage = NPC.damage; //If the projectile is hostile, the damage passed into NewProjectile will be applied doubled, and quadrupled if expert mode, so keep that in mind when balancing projectiles if you scale it off NPC.damage (which also increases for expert/master)
                if (Main.hardMode)
                {
                    Projectile.NewProjectile(source, position, direction * speed, ProjectileID.Skull, damage, 0f, Main.myPlayer);
                    Projectile.NewProjectile(source, position, direction * speed, ProjectileID.PhantasmalEye, damage, 0f, Main.myPlayer);
                    Projectile.NewProjectile(source, position, direction * speed, ProjectileID.CursedFlameHostile, damage, 0f, Main.myPlayer);
                }
                else
                {
                    Projectile.NewProjectile(source, position, direction * speed, ProjectileID.Skull, damage, 0f, Main.myPlayer);
                    NPC.NewNPCDirect(source, (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.ServantofCthulhu, NPC.whoAmI);
                    NPC.NewNPCDirect(source, (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.VileSpitEaterOfWorlds, NPC.whoAmI);
                }
            }

            tickCounter = 0;
            nextSpawnTick = Main.rand.Next(24, 26);
        }
    }
}