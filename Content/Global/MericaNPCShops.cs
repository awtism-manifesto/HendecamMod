using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Items.Weapons.Ammo;
using HendecamMod.Content.Items.Weapons.Multiclass;
using HendecamMod.Content.Items.Weapons.Ranger;
using HendecamMod.Content.Items.Weapons.Stupid;
using HendecamMod.Content.Items.Weapons.VapeItems;
using HendecamMod.Content.NPCs.Town.Alpine;
using HendecamMod.Content.Poop;

namespace HendecamMod.Content.Global;

public class MericaNPCShops : GlobalNPC
{
    public override void ModifyShop(NPCShop shop)
    {
        if (shop.NpcType == NPCID.ArmsDealer)
        {
            // Adding an item to a vanilla NPC is easy:
            // This item sells for the normal price.
            shop.Add<fivenato>(condition: Condition.DownedPlantera);
            shop.Add<Shitballs>(condition: Condition.DrunkWorld);
            shop.Add<KingslayerBullet>(condition: Condition.ForTheWorthyWorld);
            shop.Add<CeramicDart>(condition: Condition.Hardmode);
            shop.Add<Glock>(condition: Condition.DownedEyeOfCthulhu);
            shop.Add<AK47>(condition: Condition.DownedSkeletron);
            shop.Add(ItemID.Handgun, condition: Condition.DownedSkeletron);
            shop.Add(ItemID.QuadBarrelShotgun, condition: Condition.DownedSkeletron);
            shop.Add<TommyGun>(condition: Condition.Hardmode);
            shop.Add<PoliceBaton>(condition: Condition.NotTenthAnniversaryWorld);
            shop.Add<SacrificialPistol>(condition: Condition.PlayerCarriesItem(ItemType<DiseaseBlaster>()));
            shop.Add<DiseaseBlaster>(condition: Condition.PlayerCarriesItem(ItemType<SacrificialPistol>()));
            shop.Add<Brainderbuss>(condition: Condition.PlayerCarriesItem(ItemType<ToothlessWyrm>()));
            shop.Add<ToothlessWyrm>(condition: Condition.PlayerCarriesItem(ItemType<Brainderbuss>()));
        }

        if (shop.NpcType == NPCID.Merchant)
        {
            // Adding an item to a vanilla NPC is easy:
            // This item sells for the normal price.
            shop.Add<EnfieldRifle>(condition: Condition.NpcIsPresent(NPCID.ArmsDealer));
            shop.Add<M1Garand>(condition: Condition.Hardmode);

            shop.Add(ItemID.Blowpipe);
        }

        if (shop.NpcType == NPCID.Demolitionist)
        {
            // Adding an item to a vanilla NPC is easy:
            // This item sells for the normal price.
            shop.Add<Barbarossa>(condition: Condition.DownedEmpressOfLight);
            shop.Add<RivetGun>(condition: Condition.DownedSkeletron);
            shop.Add(ItemID.Nail, condition: Condition.DownedSkeletron);
        }

        if (shop.NpcType == NPCID.PartyGirl)
        {
            // Adding an item to a vanilla NPC is easy:
            // This item sells for the normal price.
            shop.Add<RectumsRequiem>(condition: Condition.Hardmode);
            shop.Add<WhippetWhip>(condition: Condition.DownedEarlygameBoss);
        }

        if (shop.NpcType == NPCID.Wizard)
        {
            // Adding an item to a vanilla NPC is easy:
            // This item sells for the normal price.
            shop.Add<GayFrogAlchemyGuide>(condition: Condition.DownedMechBossAny);
        }
        if (shop.NpcType == NPCID.Steampunker)
        {
            // Adding an item to a vanilla NPC is easy:
            // This item sells for the normal price.
            shop.Add<GrindingGears>(condition: Condition.DownedMechBossAll);
        }

        if (shop.NpcType == NPCID.SkeletonMerchant)
        {
            // Adding an item to a vanilla NPC is easy:
            // This item sells for the normal price.
            shop.Add<DeadSoldiersRifle>(condition: Condition.InJungle);
            shop.Add<PocketMortar>();
            shop.Add<RocketNeg1>();
        }

        if (shop.NpcType == NPCID.BestiaryGirl)
        {
            // Adding an item to a vanilla NPC is easy:
            // This item sells for the normal price.
            shop.Add<TedGun>(condition: Condition.DownedEmpressOfLight);
            shop.Add<OrcaMask>();
            shop.Add<OrcaSuit>();
            shop.Add<OrcaTail>();
        }

        if (shop.NpcType == NPCID.Pirate)
        {
            // Adding an item to a vanilla NPC is easy:
            // This item sells for the normal price.
            shop.Add<CaptainsCannon>(condition: Condition.MoonPhases26);
            shop.Add<CaptainsCannon>(condition: Condition.MoonPhases37);
            shop.Add<Bundlebuss>(condition: Condition.MoonPhases04);
            shop.Add<BigBuddy>(condition: Condition.MoonPhases15);
        }

        if (shop.NpcType == NPCID.DD2Bartender)
        {
            // Adding an item to a vanilla NPC is easy:
            // This item sells for the normal price.
            shop.Add(ItemID.AleThrowingGlove);
            shop.Add<MagicCue>();
            shop.Add<BoggsGlove>(condition: Condition.DownedOldOnesArmyT3);
        }

        if (shop.NpcType == NPCID.Mechanic)
        {
            // Adding an item to a vanilla NPC is easy:
            // This item sells for the normal price.
            shop.Add(ItemID.Cog, condition: Condition.Hardmode);
        }

        if (shop.NpcType == NPCID.Cyborg)
        {
            // Adding an item to a vanilla NPC is easy:
            // This item sells for the normal price.
            shop.Add<AutismDiagnosis>();
            shop.Add<CyberneticGunParts>(condition: Condition.NpcIsPresent(NPCType<Alpine>()));

            shop.Add<PowerHelmet>();
            shop.Add<PowerChestplate>();
            shop.Add<PowerPants>();
        }

        if (shop.NpcType == NPCID.GoblinTinkerer)
        {
            // Adding an item to a vanilla NPC is easy:
            // This item sells for the normal price.

            if (!ModLoader.TryGetMod("CalamityMod", out Mod CalMerica))
            {
                shop.Add<SkibidiToilet>(condition: Condition.DownedSkeletron);
            }

            shop.Add<LegoBricks>();
            shop.Add<GoodGrades>(condition: Condition.DownedEowOrBoc);
            shop.Add<Polymer>(condition: Condition.DownedEowOrBoc);
            shop.Add<Kevlar>(condition: Condition.Hardmode);
            shop.Add<ShadowflameArrow>(condition: Condition.DownedMechBossAny);
        }
    }
}