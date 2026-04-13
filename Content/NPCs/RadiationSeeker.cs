using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Global;
using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Items.Placeables;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace HendecamMod.Content.NPCs;

public class RadiationSeeker : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.DemonEye];

        NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.ExplosiveBunny;

        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers
        {
            Velocity = 1f 
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }

    public override void SetDefaults()
    {
        NPC.width = 25;
        NPC.height = 25;
        NPC.damage = 102;
        NPC.defense = 15;
        NPC.lifeMax = 5100;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath39;
        NPC.value = 62500f;
        NPC.knockBackResist = 0f;
        NPC.aiStyle = NPCAIStyleID.DemonEye;
        NPC.noGravity = true;
        NPC.despawnEncouraged = false;

        AIType = NPCID.DemonEye; // Use vanilla zombie's type when executing AI code. (This also means it will try to despawn during daytime)
        AnimationType = NPCID.DemonEye; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
        Banner = Type;
        BannerItem = ItemType<FlyingPigBanner>();
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement("\"They fucking LOOOOOOOOOOVE radiation\" "),
            new FlavorTextBestiaryInfoElement("")
        });
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
        for (int i = 0; i < 7; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustType<UraniumDust>());
            dust.noGravity = true;
            dust.velocity *= 7.5f;
            dust.scale *= 1.25f;
            Dust dust2 = Dust.NewDustDirect(target.position, target.width, target.height, DustType<PlutoniumDust>());
            dust2.noGravity = true;
            dust2.velocity *= 8.5f;
            dust2.scale *= 1.25f;
            Dust dust3 = Dust.NewDustDirect(target.position, target.width, target.height, DustType<AstatineDust>());
            dust3.noGravity = true;
            dust3.velocity *= 9.5f;
            dust3.scale *= 1.25f;
            Dust dust4 = Dust.NewDustDirect(target.position, target.width, target.height, DustType<PromethiumDust>());
            dust4.noGravity = true;
            dust4.velocity *= 11.5f;
            dust4.scale *= 1.25f;
        }

        int buffType = BuffType<RadPoisoning>();
       

        int timeToAdd = (int)(Main.rand.NextFloat(6, 7) * 30); 
        target.AddBuff(buffType, timeToAdd);
        int buffType2 = BuffType<RadPoisoning2>();


        int timeToAdd2 = (int)(Main.rand.NextFloat(6, 7) * 30);
        target.AddBuff(buffType2, timeToAdd2);
        int buffType3 = BuffType<RadPoisoning3>();


        int timeToAdd3 = (int)(Main.rand.NextFloat(6, 7) * 30);
        target.AddBuff(buffType3, timeToAdd3);
        int buffType4 = BuffType<RadPoisoning4>();


        int timeToAdd4 = (int)(Main.rand.NextFloat(6, 7) * 30);
        target.AddBuff(buffType4, timeToAdd4);
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ItemType<UraniumOre>(), 1, 35, 75));
        npcLoot.Add(ItemDropRule.Common(ItemType<PlutoniumOre>(), 1, 25, 60));
        npcLoot.Add(ItemDropRule.Common(ItemType<AstatineOre>(), 1, 20, 50));
        npcLoot.Add(ItemDropRule.Common(ItemType<PromethiumOre>(), 1, 10, 25));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (BossDownedSystem.downedPromethiumPlasmoid)

        {
            return SpawnCondition.OverworldNightMonster.Chance * 0.045f;
        }
        else return SpawnCondition.OverworldNightMonster.Chance * 0f; ;
       
    }
}