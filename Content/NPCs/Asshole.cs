using HendecamMod.Content.GlobalNPCs;
using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Armor;
using HendecamMod.Content.Items.Weapons;
using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace HendecamMod.Content.NPCs;

// [AutoloadHead] and NPC.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
[AutoloadHead]
public class Asshole : ModNPC
{
    public const string ShopName = "Shop";
    public int NumberOfTimesTalkedTo = 0;

    private static int ShimmerHeadIndex;
    private static Profiles.StackedNPCProfile NPCProfile;

    public override void Load()
    {
        // Adds our Shimmer Head to the NPCHeadLoader.
        ShimmerHeadIndex = Mod.AddNPCHeadTexture(Type, Texture + "_Shimmer_Head");
    }
    public override ITownNPCProfile TownNPCProfile()
    {
        return NPCProfile;
    }
    public override void SetStaticDefaults()
    {
        NPCID.Sets.HatOffsetY[Type] = -40000;
        Main.npcFrameCount[Type] = 25; // The total amount of frames the NPC has

        NPCID.Sets.ExtraFramesCount[Type] = 5; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs. This is the remaining frames after the walking frames.
        NPCID.Sets.AttackFrameCount[Type] = 4; // The amount of frames in the attacking animation.
        NPCID.Sets.DangerDetectRange[Type] = 700; // The amount of pixels away from the center of the NPC that it tries to attack enemies.
        NPCID.Sets.AttackType[Type] = 0; // The type of attack the Town NPC performs. 0 = throwing, 1 = shooting, 2 = magic, 3 = melee
        NPCID.Sets.AttackTime[Type] = 15; // The amount of time it takes for the NPC's attack animation to be over once it starts.
        NPCID.Sets.AttackAverageChance[Type] = 30; // The denominator for the chance for a Town NPC to attack. Lower numbers make the Town NPC appear more aggressive.
        NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset.
        NPCID.Sets.ShimmerTownTransform[NPC.type] = true; // This set says that the Town NPC has a Shimmered form. Otherwise, the Town NPC will become transparent when touching Shimmer like other enemies.

        NPCID.Sets.ShimmerTownTransform[Type] = true; // Allows for this NPC to have a different texture after touching the Shimmer liquid.


        // Influences how the NPC looks in the Bestiary
        NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
        {
            Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            Direction = 1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
                          // Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
                          // If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
        };

        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

        // Set Example Person's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in ExampleMod/Localization/en-US.lang).
        // NOTE: The following code uses chaining - a style that works due to the fact that the SetXAffection methods return the same NPCHappiness instance they're called on.
        NPC.Happiness
            .SetBiomeAffection<DesertBiome>(AffectionLevel.Like) // Example Person prefers the forest.
            .SetBiomeAffection<SnowBiome>(AffectionLevel.Like)
              .SetBiomeAffection<OceanBiome>(AffectionLevel.Dislike) // Example Person prefers the forest.
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Dislike) // Example Person prefers the forest.


            .SetNPCAffection(NPCID.TaxCollector, AffectionLevel.Dislike) // Loves living near the dryad.
             .SetNPCAffection(NPCID.GoblinTinkerer, AffectionLevel.Dislike) // Loves living near the dryad.
            .SetNPCAffection(ModContent.NPCType<NPCs.River>(), AffectionLevel.Love) // Likes living near the guide.
             .SetNPCAffection(ModContent.NPCType<NPCs.Politician>(), AffectionLevel.Hate) // Likes living near the guide.
            .SetNPCAffection(NPCID.Truffle, AffectionLevel.Like) // Dislikes living near the merchant.
        ; // < Mind the semicolon!

        // This creates a "profile" for ExamplePerson, which allows for different textures during a party and/or while the NPC is shimmered.
        NPCProfile = new Profiles.StackedNPCProfile(
            new Profiles.DefaultNPCProfile(Texture, NPCHeadLoader.GetHeadSlot(HeadTexture), Texture + "_Party"),
            new Profiles.DefaultNPCProfile(Texture + "_Shimmer", ShimmerHeadIndex, Texture + "_Shimmer_Party")
        );
    }

    public override void SetDefaults()
    {
        NPC.townNPC = true; // Sets NPC to be a Town NPC
        NPC.friendly = true; // NPC Will not attack player
        NPC.width = 18;
        NPC.height = 37;
        NPC.aiStyle = 7;
        NPC.damage = 6;
        NPC.defense = 17;
        NPC.lifeMax = 775;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.knockBackResist = 0.5f;

        AnimationType = NPCID.Demolitionist;
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("\"Yelmut.\" "),

				// You can add multiple elements if you really wanted to
				// You can also use localization keys (see Localization/en-US.lang)
				new FlavorTextBestiaryInfoElement("")
        });
    }

    // The PreDraw hook is useful for drawing things before our sprite is drawn or running code before the sprite is drawn
    // Returning false will allow you to manually draw your NPC
    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        // This code slowly rotates the NPC in the bestiary
        // (simply checking NPC.IsABestiaryIconDummy and incrementing NPC.Rotation won't work here as it gets overridden by drawModifiers.Rotation each tick)
        if (NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(Type, out NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers))
        {
            drawModifiers.Rotation += 0.001f;

            // Replace the existing NPCBestiaryDrawModifiers with our new one with an adjusted rotation
            NPCID.Sets.NPCBestiaryDrawOffset.Remove(Type);
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        return true;
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        int num = NPC.life > 0 ? 1 : 5;



        // Create gore when the NPC is killed.
        if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
        {
            // Retrieve the gore types. This NPC has shimmer and party variants for head, arm, and leg gore. (12 total gores)
            string variant = "";
            if (NPC.IsShimmerVariant) variant += "_Shimmer";
            if (NPC.altTexture == 1) variant += "_Party";
            int hatGore = NPC.GetPartyHatGore();
            int headGore = Mod.Find<ModGore>($"{Name}_Gore{variant}_Head").Type;
            int armGore = Mod.Find<ModGore>($"{Name}_Gore{variant}_Arm").Type;
            int legGore = Mod.Find<ModGore>($"{Name}_Gore{variant}_Leg").Type;

            // Spawn the gores. The positions of the arms and legs are lowered for a more natural look.
            if (hatGore > 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, hatGore);
            }
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, headGore, 1f);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 20), NPC.velocity, armGore);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 20), NPC.velocity, armGore);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 34), NPC.velocity, legGore);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 34), NPC.velocity, legGore);
        }
    }
    public override List<string> SetNPCNameList()
    {
        return new List<string>() {
            "Yelmut",

        };
    }
    public override void FindFrame(int frameHeight)
    {
        /*npc.frame.Width = 40;
			if (((int)Main.time / 10) % 2 == 0)
			{
				npc.frame.X = 40;
			}
			else
			{
				npc.frame.X = 0;
			}*/
    }
    public override void SetChatButtons(ref string button, ref string button2)
    { // What the chat buttons are when you open up the chat UI
        button = Language.GetTextValue("LegacyInterface.28");

    }

    public override string GetChat()
    {
        WeightedRandom<string> chat = new WeightedRandom<string>();


        // These are things that the NPC has a chance of telling you when you talk to it.

        if (Main.raining)
        {
            chat.Add(Language.GetTextValue("Oh, I love rain! They help my \"herbs\" in the backyard grow."));
            chat.Add(Language.GetTextValue("Both rain AND wind??? What's not to love!"));


        }
        if (Main.raining && Main.WindyEnoughForKiteDrops)
        {
            chat.Add(Language.GetTextValue("Sounds like a perfect time to curl up in my seven hundred blankets."));
            chat.Add(Language.GetTextValue("I don't like wind. It's like a flashbang but for your ears."));


        }
        if (!Main.dayTime)
        {

            chat.Add(Language.GetTextValue("Ugh, I'm tired. Time to sleep under five hundred blankets."));


        }
        if (Main.bloodMoon)
        {
            chat.Add(Language.GetTextValue("It's kind of funny how all the girls are really mad during Blood Moons. It's quite strange, to be frank."));


        }



        chat.Add(Language.GetTextValue("Don't tell nintendo about the meteor sprite."));
        chat.Add(Language.GetTextValue("I don't know why they say I'm a good artist. I thought I was pretty bad at it."));
        chat.Add(Language.GetTextValue("Hello, terrarian! Just doing some doodling, that's what I do most of the time."));



        NumberOfTimesTalkedTo++;


        string chosenChat = chat; // chat is implicitly cast to a string. This is where the random choice is made.



        return chosenChat;
    }

    public override void OnSpawn(IEntitySource source)
    {
        if (source is EntitySource_SpawnNPC)
        {
            // A TownNPC is "unlocked" once it successfully spawns into the world.
            TownNpeeRespawnSystem.unlockedriver = true;
        }
    }

    public override bool CanTownNPCSpawn(int numTownNPCs)
    { // Requirements for the town NPC to spawn.
        if (YelmutRespawnSystem.unlockedyelmut)
        {
            // If Example Person has spawned in this world before, we don't require the user satisfying the ExampleItem/ExampleBlock inventory conditions for a respawn.
            return true;
        }

        foreach (var player in Main.ActivePlayers)
        {
            // Player has to have either an ExampleItem or an ExampleBlock in order for the NPC to spawn
            if (player.inventory.Any(item => item.type == ModContent.ItemType<YelmutsHelmet>()))
            {
                return true;
            }
        }

        return false;
    }


    public override void OnChatButtonClicked(bool firstButton, ref string shop)
    {
        if (firstButton)
        {

            shop = ShopName; // Name of the shop tab we want to open.
        }
    }

    // Not completely finished, but below is what the NPC will sell

    public override void AddShops()
    {
        var npcShop = new NPCShop(Type, ShopName)
             .Add<YelmutsHelmet>()

             .Add<MarbleMask>(Condition.DownedEarlygameBoss)
                  .Add<MarbleChestplate>(Condition.DownedEarlygameBoss)
                   .Add<MarbleGreaves>(Condition.DownedEarlygameBoss)

               .Add(ItemID.HerbBag, Condition.DownedEarlygameBoss)
                .Add(ItemID.FlinxFur, Condition.DownedEarlygameBoss)
                .Add<Rubber>(Condition.DownedEarlygameBoss)

                .Add<PoorMahogany>(Condition.DownedEarlygameBoss)
                  .Add<WeedLeaves>(Condition.DownedEarlygameBoss)
                   .Add<KingslayerBar>(Condition.DownedKingSlime, Condition.DownedEyeOfCthulhu)
                   .Add<CeramicSheet>(Condition.DownedEowOrBoc, Condition.DownedSkeletron)
                    .Add(ItemID.LivingFireBlock, Condition.Hardmode)
               .Add(ItemID.FrostCore, Condition.DownedMechBossAny)
                .Add(ItemID.AncientBattleArmorMaterial, Condition.DownedMechBossAny)



                      .Add<NerfGun>()
                       .Add<TackShooter>()
                       .Add<MandibleStaff>(Condition.DownedEarlygameBoss)
                     .Add<Pyrocannon>(Condition.DownedEowOrBoc, Condition.DownedSkeletron)
                     .Add<ParticleGun>(Condition.DownedEowOrBoc, Condition.DownedSkeletron)
                     .Add<WandOfTheOvercompensator>(Condition.DownedEowOrBoc, Condition.DownedSkeletron)
                       .Add<MintalMachinePistol>(Condition.Hardmode)



        ;


        npcShop.Register(); // Name of this shop tab

        if (ModLoader.TryGetMod("bitsnbobs", out Mod YelMerica) && YelMerica.TryFind<ModItem>("PoorMahogany", out ModItem PoorMahogany)
            && YelMerica.TryFind<ModItem>("Rubber", out ModItem Rubber) && YelMerica.TryFind<ModItem>("PyriteBar", out ModItem PyriteBar))
        {



            npcShop.Add(PyriteBar.Type, Condition.DownedEarlygameBoss);

        }

    }





    public override void ModifyActiveShop(string shopName, Item[] items)
    {
        foreach (Item item in items)
        {
            // Skip 'air' items and null items.
            if (item == null || item.type == ItemID.None)
            {
                continue;
            }

            // If NPC is shimmered then reduce all prices by 50%.
            if (NPC.IsShimmerVariant)
            {
                int value = item.shopCustomPrice ?? item.value;
                item.shopCustomPrice = value / 2;
            }
        }
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<YelmutsHelmet>(), 5));

    }




    public override void TownNPCAttackStrength(ref int damage, ref float knockback)
    {
        damage = 9;
        knockback = 2.25f;
    }

    public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
    {
        cooldown = 1;
        randExtraCooldown = 69;
    }

    public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
    {
        projType = ModContent.ProjectileType<PlasmaLaser>();
        attackDelay = 1;
    }

    public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
    {
        multiplier = 12f;
        randomOffset = 2f;
        // SparklingBall is not affected by gravity, so gravityCorrection is left alone.
    }

    public override void LoadData(TagCompound tag)
    {
        NumberOfTimesTalkedTo = tag.GetInt("numberOfTimesTalkedTo");
    }

    public override void SaveData(TagCompound tag)
    {
        tag["numberOfTimesTalkedTo"] = NumberOfTimesTalkedTo;
    }


}



