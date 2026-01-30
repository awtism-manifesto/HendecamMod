namespace HendecamMod.Content.NPCs.Enemies;

public class ApacheElf : ModNPC
{
    private int nextSpawnTick = 0;
    private int tickCounter = 0;

    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 4;
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
        NPC.damage = 240;
        NPC.defense = 30;
        NPC.lifeMax = 5000;

        NPC.width = 100;
        NPC.height = 100;

        NPC.knockBackResist = 0;

        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;

        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.aiStyle = NPCAIStyleID.ElfCopter;
    }

    public override void FindFrame(int frameHeight)
    {
        Main.npcFrameCount[Type] = 4;
        AnimationType = NPCID.BlazingWheel;
    }
}