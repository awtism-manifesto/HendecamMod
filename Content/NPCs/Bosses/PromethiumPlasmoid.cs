using HendecamMod.Common.Systems;
using HendecamMod.Content.Projectiles.Enemies;
using Terraria;

namespace HendecamMod.Content.NPCs.Bosses;

[AutoloadBossHead]
public class PromethiumPlasmoid : ModNPC
{
    private int nextSpawnTick;
    private int tickCounter;

    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 8;
        NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
    }

    public override void SetDefaults()
    {
        NPC.damage = 225;
        NPC.defense = 15;
        NPC.lifeMax = 250000;
        NPC.width = 60;
        NPC.height = 60;
        NPC.scale = 3f;

        NPC.velocity = NPC.velocity * 1.25f;
        NPC.knockBackResist = 0;

        NPC.HitSound = SoundID.NPCHit44;
        NPC.DeathSound = SoundID.NPCDeath39;

        NPC.noGravity = true;
        NPC.noTileCollide = true;

        NPC.SpawnWithHigherTime(30);
        NPC.boss = true;
        NPC.npcSlots = 10f;

        NPC.aiStyle = NPCAIStyleID.DungeonSpirit;
        if (!Main.dedServ)
        {
            Music = MusicID.OtherworldlyBoss1;
        }
    }

    public override void FindFrame(int frameHeight)
    {
        Main.npcFrameCount[Type] = 8;
        AnimationType = NPCID.Drippler;
    }

    public override void OnKill()
    {
        NPC.SetEventFlagCleared(ref PromethiumPlasmoidDown.downedPromethiumPlasmoid, -1);
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
            nextSpawnTick = Main.rand.Next(300, 300);
        }

        Lighting.AddLight(NPC.Center, 0.25f, 0.8f, 2.5f);
        tickCounter++;

        if (tickCounter == nextSpawnTick)
        {
            NPC.TargetClosest();
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                var source = NPC.GetSource_FromAI();
                Vector2 position = NPC.Center;
                Vector2 targetPosition = Main.player[NPC.target].Center;
                Vector2 direction = targetPosition - position;
                direction.Normalize();
                int randX = 0;
                int randY = 0;
                switch (Main.rand.Next(2))
                {
                    case 0:
                        randX = 3000;
                        break;
                    case 1:
                        randX = -3000;
                        break;
                }

                switch (Main.rand.Next(2))
                {
                    case 0:
                        randY = 3000;
                        break;
                    case 1:
                        randY = -3000;
                        break;
                }

                int damage = NPC.damage;
                NPC.NewNPCDirect(source, (int)NPC.Center.X + randX, (int)NPC.Center.Y + randY, ModContent.NPCType<UnstablePlasmoid>(), NPC.whoAmI);
            }

            tickCounter = 0;
            nextSpawnTick = Main.rand.Next(300, 300);
        }
    }
}