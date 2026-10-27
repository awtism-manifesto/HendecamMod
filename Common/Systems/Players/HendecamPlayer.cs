using HendecamMod.Common.Utilities;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Accessories.NastyPatty;
using HendecamMod.Networking;
using HendecamMod.Networking.Packets;
using Microsoft.Xna.Framework;
using MonoMod.Core.Platforms;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace HendecamMod.Common.Systems.Players;

public partial class HendecamPlayer : ModPlayer
{
    //buff/debuff
    public bool radWeaponImbue;
    public bool FusionWeaponImbue;
    public bool FissionWeaponImbue;
    public bool Scald;
    public bool Lucid;
    public bool lifeRegenDebuff;
    public bool Rad;
    public bool Rad2;
    public bool Rad3;
    public bool Rad4;
    public bool Ligma;
    public bool MoonBurnt;
    public bool TermLucid;

    //Accessory
    public bool isCamo;
    #region cube
    public bool cubeAstatine;
    public bool cubeChlorophyte;
    public bool cubeGel;
    public bool cubeIron;
    public bool cubeMeteor;
    public bool cubeMintal;
    public bool cubeMorbium;
    public bool cubePlutonium;
    public bool cubeRiver;
    public bool cubeSilver;
    public bool cubeUranium;
    public bool cubeWeed;
    public bool cubeZinc;
    #endregion
    public bool nastyPatty;
    public bool alphaChecklist;
    public bool ankleMonitor;
    public bool burntPan;
    public bool ceaseDesist;
    public bool disenchanter;
    public bool dustyMedal;
    public bool expiredFood;
    public bool junkFood;
    public bool lunchly;
    public bool moldyCheese;
    public bool petaAutograph;
    public bool prisonFood;
    public bool schoolFood;
    public bool tornFilter;
    public bool hateMail;
    public bool brokenMouse;
    public bool dietSchedule;
    public bool emptyFishingBucket;
    public bool gasMask;
    public bool grindstone;
    public bool hotWax;
    public bool killShelterPermit;
    public bool leadPlating;
    public bool nintendoPatent;
    public bool nostalgicDiamond;
    public bool oldOnesGlasses;
    public bool pacifistsMark;
    public bool poachersContract;
    public bool tornSaddle;
    public bool slashedJacket;

    //
    public bool soulOfImmunity;

    private static readonly int[] ExplorationBuffs =
    [
        BuffID.Featherfall, BuffID.Flipper, BuffID.Gills, BuffID.Mining,
        BuffID.NightOwl, BuffID.ObsidianSkin, BuffID.Shine, BuffID.Spelunker,
        BuffID.Swiftness, BuffID.Dangersense, BuffID.WaterWalking, BuffID.Gravitation
    ];

    private static readonly int[] FoodBuffs =
    [
        BuffID.WellFed, BuffID.WellFed2, BuffID.WellFed3
    ];

    private static readonly int[] BuldingBuffs =
    [
        BuffID.Builder, BuffID.BiomeSight, BuffID.Invisibility
    ];

    private static readonly int[] FishingBuffs =
    [
        BuffID.Crate, BuffID.Fishing, BuffID.Sonar, BuffID.Lucky
    ];

    private static readonly int[] CombatBuffs =
    [
        BuffID.AmmoReservation, BuffID.Archery, BuffID.Endurance, BuffID.Heartreach,
        BuffID.Inferno, BuffID.Calm, BuffID.Battle, BuffID.Ironskin, BuffID.Lifeforce,
        BuffID.MagicPower, BuffID.ManaRegeneration, BuffID.Rage, BuffID.Summoning,
        BuffID.Thorns, BuffID.Titan, BuffID.Warmth, BuffID.Wrath, BuffID.Regeneration,
        BuffID.Hunter
    ];

    private static readonly int[] StationBuffs =
    [
        BuffID.AmmoBox, BuffID.Bewitched, BuffID.Clairvoyance,
        BuffID.Sharpened, BuffID.WarTable, BuffID.SugarRush
    ];

    private static readonly int[] ArenaBuffs =
    [
        BuffID.Campfire, BuffID.DryadsWard, BuffID.Sunflower, BuffID.HeartLamp,
        BuffID.Honey, BuffID.PeaceCandle, BuffID.StarInBottle, BuffID.CatBast,
        BuffID.MonsterBanner
    ];

    private static readonly int[] AccessoryBuffs =
    [
        BuffID.Werewolf, BuffID.Merfolk, BuffID.IceBarrier, BuffID.PaladinsShield, BuffID.Panic
    ];

    private static readonly int[] ArmorBuffs =
    [
        BuffID.TitaniumStorm, BuffID.StardustGuardianMinion, BuffID.ShadowDodge,
        BuffID.BeetleEndurance1, BuffID.BeetleEndurance2, BuffID.BeetleEndurance3,
        BuffID.BeetleMight1, BuffID.BeetleMight2, BuffID.BeetleMight3,
        BuffID.SolarShield1, BuffID.SolarShield2, BuffID.SolarShield3,
        BuffID.LeafCrystal, BuffID.NebulaUpDmg1, BuffID.NebulaUpDmg2, BuffID.NebulaUpDmg3,
        BuffID.NebulaUpLife1, BuffID.NebulaUpLife2, BuffID.NebulaUpLife3,
        BuffID.NebulaUpMana1, BuffID.NebulaUpMana2, BuffID.NebulaUpMana3,
        BuffID.BallistaPanic, BuffID.RapidHealing
    ];

    private static readonly int[] GrindstoneBuffs =
    [
        BuffID.ParryDamageBuff, BuffID.SoulDrain, BuffID.HeartyMeal,
        BuffID.CoolWhipPlayerBuff, BuffID.ScytheWhipPlayerBuff,
        BuffID.SwordWhipPlayerBuff, BuffID.ThornWhipPlayerBuff
    ];
    public int shakeDuration;
    public float shakeIntensityX;
    public float shakeIntensityY;
    public float recoilFront = 0f;
    public float recoilBack = 0f;

    public Vector2 MousePosition { get; set; }
    private Vector2 mousePosPrev;

    public void Screenshake(int dur, Vector2 power, Vector2 anchor)
    {
        if (anchor != Vector2.Zero)
        {
            power *= Terraria.Utils.GetLerpValue(2000f, 0f, anchor.Distance(Player.Center), true);
        }
        shakeDuration = dur;
        shakeIntensityX = power.X;
        shakeIntensityY = power.Y;
    }

    public override void ResetEffects()
    {
        isCamo = false;
        ///
        cubeAstatine = false;
        cubeChlorophyte = false;
        cubeGel = false;
        cubeIron = false;
        cubeMeteor = false;
        cubeMintal = false;
        cubeMorbium = false;
        cubePlutonium = false;
        cubeRiver = false;
        cubeSilver = false;
        cubeUranium = false;
        cubeWeed = false;
        cubeZinc = false;
        ///
        nastyPatty = false;
        alphaChecklist = false;
        ankleMonitor = false;
        burntPan = false;
        ceaseDesist = false;
        disenchanter = false;
        dustyMedal = false;
        junkFood = false;
        expiredFood = false;
        lunchly = false;
        moldyCheese = false;
        petaAutograph = false;
        prisonFood = false;
        schoolFood = false;
        tornFilter = false;
        hateMail = false;
        brokenMouse = false;
        dietSchedule = false;
        emptyFishingBucket = false;
        gasMask = false;
        grindstone = false;
        hotWax = false;
        killShelterPermit = false;
        leadPlating = false;
        nintendoPatent = false;
        nostalgicDiamond = false;
        oldOnesGlasses = false;
        pacifistsMark = false;
        poachersContract = false;
        tornSaddle = false;
        slashedJacket = false;

        ///
        radWeaponImbue = false;
        FusionWeaponImbue = false;
        FissionWeaponImbue = false;
        Scald = false;
        lifeRegenDebuff = false;
        Rad = false;
        Rad2 = false;
        Rad3 = false;
        MoonBurnt = false;
        Ligma = false;
        TermLucid = false;
        if (shakeDuration > 0)
            shakeDuration--;
        if (recoilFront > 0f)
            recoilFront -= 0.02f;
        if (recoilBack > 0f)
            recoilBack -= 0.02f;
    }

    public override void UpdateDead()
    {
        ResetEffects();
    }

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
    }

    public override void ModifyScreenPosition()
    {
        if (shakeDuration > 0)
        {
            Main.screenPosition += Main.rand.NextVector2Circular(shakeIntensityX, shakeIntensityY);
        }
    }
}