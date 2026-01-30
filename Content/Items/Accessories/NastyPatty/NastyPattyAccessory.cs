using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class NastyPattyAccessory : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 16000);
        Item.rare = ItemRarityID.Quest;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Grants 200 Health, 30% increased attack speed, double the breath timer, Hellfire for all attacks,"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "50 mana, 4 Luck, 10% Damage Reduction, 25 Safe Fall Distance, 3hp/s, Light, 25% Crit Chance,"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Double Movement Speed, 50 Defense, Doubled Armor Penetraton, 50% more Generic Damage, and much higher jump speed"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "No longer gain effects from any vanilla buff"));
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyRegen>().NastyEffect = true;
        player.GetModPlayer<NastyLight>().NastyEffect = true;
        player.GetModPlayer<NastyCrit>().NastyEffect = true;
        player.GetModPlayer<NastyMovement>().NastyEffect = true;
        player.GetModPlayer<NastyPenetration>().NastyEffect = true;
        player.GetModPlayer<NastyDefense>().NastyEffect = true;
        player.GetModPlayer<NastyDamage>().NastyEffect = true;
        player.GetModPlayer<NastyJump>().NastyEffect = true;
        player.GetModPlayer<NastyBreath>().NastyEffect = true;
        player.GetModPlayer<NastySpeed>().NastyEffect = true;
        player.GetModPlayer<NastyHealth>().NastyEffect = true;
        player.GetModPlayer<NastyFire>().NastyEffect = true;
        player.GetModPlayer<NastyLuck>().NastyEffect = true;
        player.GetModPlayer<NastyMana>().NastyEffect = true;
        player.GetModPlayer<NastyEndurance>().NastyEffect = true;
        player.GetModPlayer<NastyFall>().NastyEffect = true;
        player.buffImmune[BuffID.WeaponImbueConfetti] = true;
        player.buffImmune[BuffID.WeaponImbueCursedFlames] = true;
        player.buffImmune[BuffID.WeaponImbueFire] = true;
        player.buffImmune[BuffID.WeaponImbueGold] = true;
        player.buffImmune[BuffID.WeaponImbueIchor] = true;
        player.buffImmune[BuffID.WeaponImbueNanites] = true;
        player.buffImmune[BuffID.WeaponImbuePoison] = true;
        player.buffImmune[BuffID.WeaponImbueVenom] = true;
        player.buffImmune[BuffID.AmmoReservation] = true;
        player.buffImmune[BuffID.Archery] = true;
        player.buffImmune[BuffID.Endurance] = true;
        player.buffImmune[BuffID.Heartreach] = true;
        player.buffImmune[BuffID.Inferno] = true;
        player.buffImmune[BuffID.Calm] = true;
        player.buffImmune[BuffID.Battle] = true;
        player.buffImmune[BuffID.Ironskin] = true;
        player.buffImmune[BuffID.Lifeforce] = true;
        player.buffImmune[BuffID.MagicPower] = true;
        player.buffImmune[BuffID.ManaRegeneration] = true;
        player.buffImmune[BuffID.Rage] = true;
        player.buffImmune[BuffID.Summoning] = true;
        player.buffImmune[BuffID.Thorns] = true;
        player.buffImmune[BuffID.Titan] = true;
        player.buffImmune[BuffID.Warmth] = true;
        player.buffImmune[BuffID.Wrath] = true;
        player.buffImmune[BuffID.Regeneration] = true;
        player.buffImmune[BuffID.Hunter] = true;
        player.buffImmune[BuffID.AmmoBox] = true;
        player.buffImmune[BuffID.Bewitched] = true;
        player.buffImmune[BuffID.Clairvoyance] = true;
        player.buffImmune[BuffID.Sharpened] = true;
        player.buffImmune[BuffID.WarTable] = true;
        player.buffImmune[BuffID.SugarRush] = true;
        player.buffImmune[BuffID.Campfire] = true;
        player.buffImmune[BuffID.DryadsWard] = true;
        player.buffImmune[BuffID.Sunflower] = true;
        player.buffImmune[BuffID.HeartLamp] = true;
        player.buffImmune[BuffID.Honey] = true;
        player.buffImmune[BuffID.PeaceCandle] = true;
        player.buffImmune[BuffID.StarInBottle] = true;
        player.buffImmune[BuffID.CatBast] = true;
        player.buffImmune[BuffID.MonsterBanner] = true;
        player.buffImmune[BuffID.Crate] = true;
        player.buffImmune[BuffID.Fishing] = true;
        player.buffImmune[BuffID.Sonar] = true;
        player.buffImmune[BuffID.Lucky] = true;
        player.buffImmune[BuffID.Builder] = true;
        player.buffImmune[BuffID.BiomeSight] = true;
        player.buffImmune[BuffID.Invisibility] = true;
        player.buffImmune[BuffID.Featherfall] = true;
        player.buffImmune[BuffID.Flipper] = true;
        player.buffImmune[BuffID.Gills] = true;
        player.buffImmune[BuffID.Mining] = true;
        player.buffImmune[BuffID.NightOwl] = true;
        player.buffImmune[BuffID.ObsidianSkin] = true;
        player.buffImmune[BuffID.Shine] = true;
        player.buffImmune[BuffID.Spelunker] = true;
        player.buffImmune[BuffID.Swiftness] = true;
        player.buffImmune[BuffID.Dangersense] = true;
        player.buffImmune[BuffID.WaterWalking] = true;
        player.buffImmune[BuffID.Gravitation] = true;
        player.buffImmune[BuffID.WellFed] = true;
        player.buffImmune[BuffID.WellFed2] = true;
        player.buffImmune[BuffID.WellFed3] = true;
        player.ClearBuff(BuffID.Flamingo);
        player.ClearBuff(BuffID.Rudolph);
        player.ClearBuff(BuffID.WitchBroom);
        player.ClearBuff(BuffID.BabyBird);
        player.ClearBuff(BuffID.BabySlime);
        player.ClearBuff(BuffID.DeadlySphere);
        player.ClearBuff(BuffID.StormTiger);
        player.ClearBuff(BuffID.Smolstar);
        player.ClearBuff(BuffID.FlinxMinion);
        player.ClearBuff(BuffID.HornetMinion);
        player.ClearBuff(BuffID.ImpMinion);
        player.ClearBuff(BuffID.PirateMinion);
        player.ClearBuff(BuffID.Pygmies);
        player.ClearBuff(BuffID.Ravens);
        player.ClearBuff(BuffID.BatOfLight);
        player.ClearBuff(BuffID.SharknadoMinion);
        player.ClearBuff(BuffID.SpiderMinion);
        player.ClearBuff(BuffID.StardustMinion);
        player.ClearBuff(BuffID.StardustDragonMinion);
        player.ClearBuff(BuffID.EmpressBlade);
        player.ClearBuff(BuffID.TwinEyesMinion);
        player.ClearBuff(BuffID.UFOMinion);
        player.ClearBuff(BuffID.VampireFrog);
        player.ClearBuff(BuffID.BasiliskMount);
        player.ClearBuff(BuffID.BeeMount);
        player.ClearBuff(BuffID.BunnyMount);
        player.ClearBuff(BuffID.CuteFishronMount);
        player.ClearBuff(BuffID.DarkHorseMount);
        player.ClearBuff(BuffID.DarkMageBookMount);
        player.ClearBuff(BuffID.DrillMount);
        player.ClearBuff(BuffID.GolfCartMount);
        player.ClearBuff(BuffID.LavaSharkMount);
        player.ClearBuff(BuffID.MajesticHorseMount);
        player.ClearBuff(BuffID.PaintedHorseMount);
        player.ClearBuff(BuffID.PigronMount);
        player.ClearBuff(BuffID.PirateShipMount);
        player.ClearBuff(BuffID.PogoStickMount);
        player.ClearBuff(BuffID.QueenSlimeMount);
        player.ClearBuff(BuffID.SantankMount);
        player.ClearBuff(BuffID.ScutlixMount);
        player.ClearBuff(BuffID.SlimeMount);
        player.ClearBuff(BuffID.SpookyWoodMount);
        player.ClearBuff(BuffID.TurtleMount);
        player.ClearBuff(BuffID.UFOMount);
        player.ClearBuff(BuffID.UnicornMount);
        player.ClearBuff(BuffID.WallOfFleshGoatMount);
        player.ClearBuff(BuffID.WolfMount);
        player.ClearBuff(BuffID.CrimsonHeart);
        player.ClearBuff(BuffID.FairyBlue);
        player.ClearBuff(BuffID.FairyGreen);
        player.ClearBuff(BuffID.FairyRed);
        player.ClearBuff(BuffID.FairyQueenPet);
        player.ClearBuff(BuffID.PetDD2Ghost);
        player.ClearBuff(BuffID.PumpkingPet);
        player.ClearBuff(BuffID.MagicLantern);
        player.ClearBuff(BuffID.ShadowOrb);
        player.ClearBuff(BuffID.SuspiciousTentacle);
        player.ClearBuff(BuffID.GolemPet);
        player.ClearBuff(BuffID.Wisp);
        player.ClearBuff(BuffID.PetBunny);
        player.ClearBuff(BuffID.PetDD2Dragon);
        player.ClearBuff(BuffID.PetDD2Gato);
        player.ClearBuff(BuffID.PetLizard);
        player.ClearBuff(BuffID.PetParrot);
        player.ClearBuff(BuffID.PetSapling);
        player.ClearBuff(BuffID.PetSpider);
        player.ClearBuff(BuffID.PetTurtle);
        player.ClearBuff(BuffID.BerniePet);
        player.ClearBuff(BuffID.BlueChickenPet);
        player.ClearBuff(BuffID.BrainOfCthulhuPet);
        player.ClearBuff(BuffID.ChesterPet);
        player.ClearBuff(BuffID.DD2BetsyPet);
        player.ClearBuff(BuffID.DD2OgrePet);
        player.ClearBuff(BuffID.DeerclopsPet);
        player.ClearBuff(BuffID.DestroyerPet);
        player.ClearBuff(BuffID.DualSlimePet);
        player.ClearBuff(BuffID.DukeFishronPet);
        player.ClearBuff(BuffID.EaterOfWorldsPet);
        player.ClearBuff(BuffID.EverscreamPet);
        player.ClearBuff(BuffID.EyeOfCthulhuPet);
        player.ClearBuff(BuffID.GlommerPet);
        player.ClearBuff(BuffID.IceQueenPet);
        player.ClearBuff(BuffID.JunimoPet);
        player.ClearBuff(BuffID.KingSlimePet);
        player.ClearBuff(BuffID.LunaticCultistPet);
        player.ClearBuff(BuffID.MartianPet);
        player.ClearBuff(BuffID.MoonLordPet);
        player.ClearBuff(BuffID.PigPet);
        player.ClearBuff(BuffID.PlanteraPet);
        player.ClearBuff(BuffID.QueenSlimePet);
        player.ClearBuff(BuffID.SkeletronPet);
        player.ClearBuff(BuffID.SkeletronPrimePet);
        player.ClearBuff(BuffID.TwinsPet);
        player.ClearBuff(BuffID.BabyDinosaur);
        player.ClearBuff(BuffID.BabyEater);
        player.ClearBuff(BuffID.BabyGrinch);
        player.ClearBuff(BuffID.BabyHornet);
        player.ClearBuff(BuffID.BabyImp);
        player.ClearBuff(BuffID.BabyPenguin);
        player.ClearBuff(BuffID.BabyRedPanda);
        player.ClearBuff(BuffID.BabySkeletronHead);
        player.ClearBuff(BuffID.BabySnowman);
        player.ClearBuff(BuffID.BabyTruffle);
        player.ClearBuff(BuffID.BabyWerewolf);
        player.ClearBuff(BuffID.BlackCat);
        player.ClearBuff(BuffID.CavelingGardener);
        player.ClearBuff(BuffID.CompanionCube);
        player.ClearBuff(BuffID.CursedSapling);
        player.ClearBuff(BuffID.DirtiestBlock);
        player.ClearBuff(BuffID.DynamiteKitten);
        player.ClearBuff(BuffID.UpbeatStar);
        player.ClearBuff(BuffID.EyeballSpring);
        player.ClearBuff(BuffID.FennecFox);
        player.ClearBuff(BuffID.GlitteryButterfly);
        player.ClearBuff(BuffID.LilHarpy);
        player.ClearBuff(BuffID.MiniMinotaur);
        player.ClearBuff(BuffID.Plantero);
        player.ClearBuff(BuffID.Puppy);
        player.ClearBuff(BuffID.ShadowMimic);
        player.ClearBuff(BuffID.SharkPup);
        player.ClearBuff(BuffID.Spiffo);
        player.ClearBuff(BuffID.Squashling);
        player.ClearBuff(BuffID.TikiSpirit);
        player.ClearBuff(BuffID.VoltBunny);
        player.ClearBuff(BuffID.ZephyrFish);
        player.buffImmune[BuffID.TitaniumStorm] = true;
        player.buffImmune[BuffID.StardustGuardianMinion] = true;
        player.buffImmune[BuffID.ShadowDodge] = true;
        player.buffImmune[BuffID.BeetleEndurance1] = true;
        player.buffImmune[BuffID.BeetleEndurance2] = true;
        player.buffImmune[BuffID.BeetleEndurance3] = true;
        player.buffImmune[BuffID.BeetleMight1] = true;
        player.buffImmune[BuffID.BeetleMight2] = true;
        player.buffImmune[BuffID.BeetleMight3] = true;
        player.buffImmune[BuffID.SolarShield1] = true;
        player.buffImmune[BuffID.SolarShield2] = true;
        player.buffImmune[BuffID.SolarShield3] = true;
        player.buffImmune[BuffID.LeafCrystal] = true;
        player.buffImmune[BuffID.NebulaUpDmg1] = true;
        player.buffImmune[BuffID.NebulaUpDmg2] = true;
        player.buffImmune[BuffID.NebulaUpDmg3] = true;
        player.buffImmune[BuffID.NebulaUpLife1] = true;
        player.buffImmune[BuffID.NebulaUpLife2] = true;
        player.buffImmune[BuffID.NebulaUpLife3] = true;
        player.buffImmune[BuffID.NebulaUpMana1] = true;
        player.buffImmune[BuffID.NebulaUpMana2] = true;
        player.buffImmune[BuffID.NebulaUpMana3] = true;
        player.buffImmune[BuffID.BallistaPanic] = true;
        player.buffImmune[BuffID.RapidHealing] = true;
        player.buffImmune[BuffID.ParryDamageBuff] = true;
        player.buffImmune[BuffID.SoulDrain] = true;
        player.buffImmune[BuffID.HeartyMeal] = true;
        player.buffImmune[BuffID.CoolWhipPlayerBuff] = true;
        player.buffImmune[BuffID.ScytheWhipPlayerBuff] = true;
        player.buffImmune[BuffID.SwordWhipPlayerBuff] = true;
        player.buffImmune[BuffID.ThornWhipPlayerBuff] = true;
        player.buffImmune[BuffID.MinecartLeft] = true;
        player.buffImmune[BuffID.MinecartRight] = true;
        player.buffImmune[BuffID.MinecartLeftMech] = true;
        player.buffImmune[BuffID.MinecartRightMech] = true;
        player.buffImmune[BuffID.MinecartLeftWood] = true;
        player.buffImmune[BuffID.MinecartRightWood] = true;
        player.buffImmune[BuffID.AmberMinecartLeft] = true;
        player.buffImmune[BuffID.AmberMinecartRight] = true;
        player.buffImmune[BuffID.AmethystMinecartLeft] = true;
        player.buffImmune[BuffID.AmethystMinecartRight] = true;
        player.buffImmune[BuffID.BeeMinecartLeft] = true;
        player.buffImmune[BuffID.BeeMinecartRight] = true;
        player.buffImmune[BuffID.BeetleMinecartLeft] = true;
        player.buffImmune[BuffID.BeetleMinecartRight] = true;
        player.buffImmune[BuffID.CoffinMinecartLeft] = true;
        player.buffImmune[BuffID.CoffinMinecartRight] = true;
        player.buffImmune[BuffID.DesertMinecartLeft] = true;
        player.buffImmune[BuffID.DesertMinecartRight] = true;
        player.buffImmune[BuffID.DiamondMinecartLeft] = true;
        player.buffImmune[BuffID.DiamondMinecartRight] = true;
        player.buffImmune[BuffID.DiggingMoleMinecartLeft] = true;
        player.buffImmune[BuffID.DiggingMoleMinecartRight] = true;
        player.buffImmune[BuffID.EmeraldMinecartLeft] = true;
        player.buffImmune[BuffID.EmeraldMinecartRight] = true;
        player.buffImmune[BuffID.FartMinecartLeft] = true;
        player.buffImmune[BuffID.FartMinecartRight] = true;
        player.buffImmune[BuffID.FishMinecartLeft] = true;
        player.buffImmune[BuffID.FishMinecartRight] = true;
        player.buffImmune[BuffID.HellMinecartLeft] = true;
        player.buffImmune[BuffID.HellMinecartRight] = true;
        player.buffImmune[BuffID.LadybugMinecartLeft] = true;
        player.buffImmune[BuffID.LadybugMinecartRight] = true;
        player.buffImmune[BuffID.MeowmereMinecartLeft] = true;
        player.buffImmune[BuffID.MeowmereMinecartRight] = true;
        player.buffImmune[BuffID.PartyMinecartLeft] = true;
        player.buffImmune[BuffID.PartyMinecartRight] = true;
        player.buffImmune[BuffID.PigronMinecartLeft] = true;
        player.buffImmune[BuffID.PigronMinecartRight] = true;
        player.buffImmune[BuffID.PirateMinecartLeft] = true;
        player.buffImmune[BuffID.PirateMinecartRight] = true;
        player.buffImmune[BuffID.RubyMinecartLeft] = true;
        player.buffImmune[BuffID.RubyMinecartRight] = true;
        player.buffImmune[BuffID.SapphireMinecartLeft] = true;
        player.buffImmune[BuffID.SapphireMinecartRight] = true;
        player.buffImmune[BuffID.ShroomMinecartLeft] = true;
        player.buffImmune[BuffID.ShroomMinecartRight] = true;
        player.buffImmune[BuffID.SteampunkMinecartLeft] = true;
        player.buffImmune[BuffID.SteampunkMinecartRight] = true;
        player.buffImmune[BuffID.SunflowerMinecartLeft] = true;
        player.buffImmune[BuffID.SunflowerMinecartRight] = true;
        player.buffImmune[BuffID.TerraFartMinecartLeft] = true;
        player.buffImmune[BuffID.TerraFartMinecartRight] = true;
        player.buffImmune[BuffID.TopazMinecartLeft] = true;
        player.buffImmune[BuffID.TopazMinecartRight] = true;
        player.buffImmune[BuffID.Werewolf] = true;
        player.buffImmune[BuffID.Merfolk] = true;
        player.buffImmune[BuffID.IceBarrier] = true;
        player.buffImmune[BuffID.PaladinsShield] = true;
        player.buffImmune[BuffID.Panic] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<ExpiredPetFood>();
        recipe.AddIngredient<JunkFood>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AdamantiteForge);
        recipe.Register();
    }

    public class NastyRegen : ModPlayer
    {
        public bool NastyEffect;

        public override void ResetEffects()
        {
            NastyEffect = false;
        }

        public override void PostUpdateEquips()
        {
            if (!Player.GetModPlayer<NastyHealth>().NastyEffect)
            {
            }
            else
            {
                Player.lifeRegen += 6;
            }
        }
    }

    public class NastyLight : ModPlayer
    {
        public bool NastyEffect;

        public override void ResetEffects()
        {
            NastyEffect = false;
        }

        public override void PostUpdateEquips()
        {
            if (!Player.GetModPlayer<NastyLight>().NastyEffect)
            {
            }
            else
            {
                Lighting.AddLight(Player.Center, 2.25f, 2.25f, 2.25f);
            }
        }
    }

    public class NastyCrit : ModPlayer
    {
        public bool NastyEffect;

        public override void ResetEffects()
        {
            NastyEffect = false;
        }

        public override void PostUpdateEquips()
        {
            if (!Player.GetModPlayer<NastyCrit>().NastyEffect)
            {
            }
            else
            {
                Player.GetCritChance(DamageClass.Generic) += 25f;
            }
        }
    }

    public class NastyMovement : ModPlayer
    {
        public bool NastyEffect;

        public override void ResetEffects()
        {
            NastyEffect = false;
        }

        public override void PostUpdateEquips()
        {
            if (!Player.GetModPlayer<NastyMovement>().NastyEffect)
            {
            }
            else
            {
                Player.moveSpeed += 1f;
            }
        }
    }

    public class NastyPenetration : ModPlayer
    {
        public bool NastyEffect;

        public override void ResetEffects()
        {
            NastyEffect = false;
        }

        public override void PostUpdateEquips()
        {
            if (!Player.GetModPlayer<NastyPenetration>().NastyEffect)
            {
            }
            else
            {
                Player.GetArmorPenetration(DamageClass.Generic) += 200f;
            }
        }
    }

    public class NastyDefense : ModPlayer
    {
        public bool NastyEffect;

        public override void ResetEffects()
        {
            NastyEffect = false;
        }

        public override void PostUpdateEquips()
        {
            if (!Player.GetModPlayer<NastyDefense>().NastyEffect)
            {
            }
            else
            {
                Player.statDefense += 50;
            }
        }
    }

    public class NastyDamage : ModPlayer
    {
        public bool NastyEffect;

        public override void ResetEffects()
        {
            NastyEffect = false;
        }

        public override void PostUpdateEquips()
        {
            if (!Player.GetModPlayer<NastyDamage>().NastyEffect)
            {
            }
            else
            {
                Player.GetDamage(DamageClass.Generic) += 0.5f;
            }
        }
    }

    public class NastyJump : ModPlayer
    {
        public bool NastyEffect;

        public override void ResetEffects()
        {
            NastyEffect = false;
        }

        public override void PostUpdateEquips()
        {
            if (!Player.GetModPlayer<NastyJump>().NastyEffect)
            {
            }
            else
            {
                Player.jumpSpeedBoost += 0.7f;
            }
        }
    }

    public class NastyBreath : ModPlayer
    {
        public bool NastyEffect;

        public override void ResetEffects()
        {
            NastyEffect = false;
        }

        public override void PostUpdateEquips()
        {
            if (!Player.GetModPlayer<NastyBreath>().NastyEffect)
            {
            }
            else
            {
                Player.breathEffectiveness += 2f;
            }
        }
    }

    public class NastySpeed : ModPlayer
    {
        public bool NastyEffect;

        public override void ResetEffects()
        {
            NastyEffect = false;
        }

        public override void PostUpdateEquips()
        {
            if (!Player.GetModPlayer<NastySpeed>().NastyEffect)
            {
            }
            else
            {
                Player.GetAttackSpeed(DamageClass.Generic) += 0.3f;
            }
        }
    }

    public class NastyHealth : ModPlayer
    {
        public bool NastyEffect;

        public override void ResetEffects()
        {
            NastyEffect = false;
        }

        public override void PostUpdateEquips()
        {
            if (!Player.GetModPlayer<NastyHealth>().NastyEffect)
            {
            }
            else
            {
                Player.statLifeMax2 += 200;
            }
        }
    }

    public class NastyFire : ModPlayer
    {
        public bool NastyEffect;

        public override void ResetEffects()
        {
            NastyEffect = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!Player.GetModPlayer<NastyFire>().NastyEffect)
            {
            }
            else
            {
                target.AddBuff(BuffID.OnFire3, 600);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!Player.GetModPlayer<NastyFire>().NastyEffect)
            {
            }
            else
            {
                target.AddBuff(BuffID.OnFire3, 600);
            }
        }
    }

    public class NastyLuck : ModPlayer
    {
        public bool NastyEffect;

        public override void ResetEffects()
        {
            NastyEffect = false;
        }

        public override void PostUpdateEquips()
        {
            if (!Player.GetModPlayer<NastyLuck>().NastyEffect)
            {
            }
            else
            {
                Player.equipmentBasedLuckBonus += 4;
            }
        }
    }

    public class NastyMana : ModPlayer
    {
        public bool NastyEffect;

        public override void ResetEffects()
        {
            NastyEffect = false;
        }

        public override void PostUpdateEquips()
        {
            if (!Player.GetModPlayer<NastyMana>().NastyEffect)
            {
            }
            else
            {
                Player.statManaMax2 += 50;
            }
        }
    }

    public class NastyEndurance : ModPlayer
    {
        public bool NastyEffect;

        public override void ResetEffects()
        {
            NastyEffect = false;
        }

        public override void PostUpdateEquips()
        {
            if (!Player.GetModPlayer<NastyEndurance>().NastyEffect)
            {
            }
            else
            {
                Player.endurance = 1f - 0.9f * (1f - Player.endurance);
            }
        }
    }

    public class NastyFall : ModPlayer
    {
        public bool NastyEffect;

        public override void ResetEffects()
        {
            NastyEffect = false;
        }

        public override void PostUpdateEquips()
        {
            if (!Player.GetModPlayer<NastyFall>().NastyEffect)
            {
            }
            else
            {
                Player.extraFall += 25;
            }
        }
    }
}