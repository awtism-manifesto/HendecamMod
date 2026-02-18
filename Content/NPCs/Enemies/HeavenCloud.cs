using HendecamMod.Content.NPCs.Bosses;

namespace HendecamMod.Content.NPCs.Enemies;

public class HeavenCloud : ModNPC
    {
    private int nextSpawnTick = 0;
    private int tickCounter = 0;

    public override void SetStaticDefaults()
        {
        if (Main.hardMode)
            {
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
            }
        }
    public override void SetDefaults()
        {
        NPC.damage = 0;
        NPC.defense = 30;
        NPC.lifeMax = 100000;

        NPC.width = 110;
        NPC.scale = 1.5f;
        NPC.height = 110;


        NPC.knockBackResist = 0;

        NPC.HitSound = SoundID.NPCHit44;
        NPC.DeathSound = SoundID.NPCDeath39;

        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.aiStyle = NPCAIStyleID.Butterfly;
        }

    public override void FindFrame(int frameHeight)
        {
        Main.npcFrameCount[Type] = 1;
        //AnimationType = NPCID.BlazingWheel;
        }
    public override void AI()
        {
        NPC.active = true;
        if (nextSpawnTick == 0)
            {
            nextSpawnTick = Main.rand.Next(300, 300);
            }

        Lighting.AddLight(NPC.Center, 0.8f, 0.8f, 2.5f);
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
                if (!NPC.AnyNPCs(ModContent.NPCType<EyesOfGabriel>()))
                    {

                    }
                if (!NPC.AnyNPCs(ModContent.NPCType<EyesOfRaphael>()))
                    {

                    }
                }
            tickCounter = 0;
            nextSpawnTick = Main.rand.Next(300, 300);
            }
        }
    }