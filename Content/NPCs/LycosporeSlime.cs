using HendecamMod.Content.Global;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Items.Weapons.Summon;
using HendecamMod.Content.Projectiles.Enemies;
using HendecamMod.Content.Projectiles.Enemies.Boss;
using JetBrains.Annotations;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace HendecamMod.Content.NPCs;

// Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
public class LycosporeSlime : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BlueSlime];

        NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.MotherSlime;

        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers
        {
            // Influences how the NPC looks in the Bestiary
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            // Sets your NPC's flavor text in the bestiary.
            new FlavorTextBestiaryInfoElement("\"Be careful, these Lycospore Slimes can give you an explosive surprise when hurt\" "),

            // You can add multiple elements if you really wanted to
            // You can also use localization keys (see Localization/en-US.lang)
            new FlavorTextBestiaryInfoElement("")
        });
    }

    public override void SetDefaults()
    {
        NPC.width = 45;
        NPC.height = 25;
        NPC.damage = 33;
        NPC.defense = 8;
        NPC.lifeMax = 102;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.value = 3330f;
        NPC.knockBackResist = 0.7f;
        NPC.aiStyle = NPCAIStyleID.Slime;

        AIType = NPCID.YellowSlime; // Use vanilla zombie's type when executing AI code. (This also means it will try to despawn during daytime)
        AnimationType = NPCID.BlueSlime; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
        Banner = Type;
        BannerItem = ItemType<LycosporeSlimeBanner>();
    }
  
    public int ShroomCD = 0;

    public override void AI()
    {
        var source = NPC.GetSource_FromAI();
        Vector2 center = NPC.Center;

        ShroomCD--;

        if (NPC.life <= NPC.lifeMax * 0.5f && ShroomCD <= 0 && NPC.HasValidTarget)
        {
           
            float spreadRadius = 150f; 
            Vector2 offset = Main.rand.NextVector2Circular(spreadRadius, spreadRadius);
            Vector2 spawnPos = center + offset;

            // Stationary projectile: zero velocity
            Projectile.NewProjectile(source, spawnPos, Vector2.Zero,
                ProjectileType<BoomShroomHostile>(), 40, 0f, Main.myPlayer);

            ShroomCD = 15;
        }
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        // Since Party Zombie is essentially just another variation of Zombie, we'd like to mimic the Zombie drops.
        // To do this, we can either (1) copy the drops from the Zombie directly or (2) just recreate the drops in our code.
        // (1) Copying the drops directly means that if Terraria updates and changes the Zombie drops, your ModNPC will also inherit the changes automatically.
        // (2) Recreating the drops can give you more control if desired but requires consulting the wiki, bestiary, or source code and then writing drop code.

        // (1) This example shows copying the drops directly. For consistency and mod compatibility, we suggest using the smallest positive NPCID when dealing with npcs with many variants and shared drop pools.
        var zombieDropRules = Main.ItemDropsDB.GetRulesForNPCID(NPCID.GreenSlime, false); // false is important here
        foreach (var zombieDropRule in zombieDropRules)
        {
            // In this foreach loop, we simple add each drop to the PartyZombie drop pool. 
            npcLoot.Add(zombieDropRule);
        }

        // (2) This example shows recreating the drops. This code is commented out because we are using the previous method instead.
        // npcLoot.Add(ItemDropRule.Common(ItemID.Shackle, 50)); // Drop shackles with a 1 out of 50 chance.
        // npcLoot.Add(ItemDropRule.Common(ItemID.ZombieArm, 250)); // Drop zombie arm with a 1 out of 250 chance.
        npcLoot.Add(ItemDropRule.Common(ItemType<LycopiteOre>(), 1, 9, 24));
       
       
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (NPC.downedDeerclops)
        {
            return SpawnCondition.Cavern.Chance * 0.36f;
        }
        else return SpawnCondition.Cavern.Chance * 0f;

    }
}