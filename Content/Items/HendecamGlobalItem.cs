using HendecamMod.Common.Systems;
using HendecamMod.Common.Utilities;
using HendecamMod.Content.Global;
using HendecamMod.Content.Items.Accessories.Cubes;
using HendecamMod.Content.Items.Weapons.Ranger;
using HendecamMod.Content.Poop;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace HendecamMod.Common.Global;

public class HendecamGlobalItem : GlobalItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ShimmerTransformToItem[ItemID.AnkhShield] = ItemID.AnkhCharm;
        ItemID.Sets.ShimmerTransformToItem[ItemID.AnkhCharm] = ItemID.AnkhShield;
    }

    public override void SetDefaults(Item item)
    {

        switch (item.type)
        {
            case ItemID.RodOfHarmony:
                item.value = Item.sellPrice(silver: 500);
                item.rare = ItemRarityID.LightRed;
                item.accessory = true;
                break;
            case ItemID.ObsidianSkull:
            case ItemID.CobaltShield:
            case ItemID.Blindfold:
            case ItemID.PocketMirror:
            case ItemID.Vitamins:
            case ItemID.ArmorPolish:
            case ItemID.AdhesiveBandage:
            case ItemID.Bezoar:
            case ItemID.Nazar:
            case ItemID.Megaphone:
            case ItemID.TrifoldMap:
            case ItemID.FastClock:
            case ItemID.HandWarmer:
            case ItemID.EngineeringHelmet:
            case ItemID.ShimmerCloak:
                item.value = Item.sellPrice(silver: 500);
                item.rare = ItemRarityID.LightRed;
                item.accessory = true;
                break;

            case ItemID.ObsidianShield:
            case ItemID.ReflectiveShades:
            case ItemID.ArmorBracing:
            case ItemID.MedicatedBandage:
            case ItemID.CountercurseMantra:
            case ItemID.ThePlan:
                item.value = Item.sellPrice(silver: 1000);
                item.rare = ItemRarityID.LightRed;
                item.accessory = true;
                break;

            case ItemID.AnkhShield:
                item.value = Item.sellPrice(silver: 8000);
                item.rare = ItemRarityID.LightRed;
                item.accessory = true;
                item.defense = 16;
                break;

            case ItemID.AnkhCharm:
                item.value = Item.sellPrice(silver: 8000);
                item.rare = ItemRarityID.LightRed;
                break;

            case ItemID.Shimmerfly:
                item.bait = 50;
                break;

            case ItemID.CrabBanner:
                item.bait = 1200000;
                break;

            case ItemID.DirtBlock:
                item.bait = 1;
                break;

            case ItemID.Worm:
                item.value = 0;
                break;

            case ItemID.DD2EnergyCrystal:
                item.value = 1250;
                break;

            case ItemID.DemonHeart:
                item.value = 6750000;
                break;

            case ItemID.EmpressButterfly:
                item.bait = 115;
                break;

            case ItemID.MetalDetector:
                item.scale = 1.025f;
                item.DamageType = DamageClass.Melee;
                item.damage = 15;
                item.useTime = 15;
                item.useAnimation = 15;
                item.knockBack = 1.75f;
                item.useStyle = ItemUseStyleID.Swing;
                break;

            case ItemID.Minishark:
                item.damage = 6;
                item.useTime = 8;
                item.useAnimation = 8;
                break;

            case ItemID.StarCannon:
                item.damage = 55;
                item.useTime = 12;
                item.useAnimation = 12;
                break;

            case ItemID.IceBlade:
                item.useTime = 38;
                break;

            case ItemID.EnchantedSword:
                item.useTime = 40;
                break;

            case ItemID.NightmarePickaxe:
            case ItemID.DeathbringerPickaxe:
                item.pick = 70;
                break;

            case ItemID.CellPhone:
            case ItemID.Shellphone:
            case ItemID.ShellphoneSpawn:
            case ItemID.ShellphoneHell:
            case ItemID.ShellphoneOcean:
            case ItemID.MagicMirror:
            case ItemID.IceMirror:
            case ItemID.MagicConch:
            case ItemID.DemonConch:
                item.useTime = 15;
                item.useAnimation = 15;
                break;

            case ItemID.FlurryBoots:
            case ItemID.SailfishBoots:
            case ItemID.AmphibianBoots:
            case ItemID.FrogLeg:
            case ItemID.WaterWalkingBoots:
            case ItemID.ObsidianWaterWalkingBoots:
            case ItemID.LavaWaders:
            case ItemID.FlameWakerBoots:
            case ItemID.FlowerBoots:
            case ItemID.HellfireTreads:
            case ItemID.HermesBoots:
            case ItemID.SpectreBoots:
            case ItemID.IceSkates:
            case ItemID.SandBoots:
            case ItemID.LightningBoots:
            case ItemID.FrostsparkBoots:
            case ItemID.RocketBoots:
            case ItemID.FairyBoots:
                if (GetInstance<HendecamConfig>().BootsTreeRework)
                    item.rare = ItemRarityID.Green;
                break;

            case ItemID.TerrasparkBoots:
                if (GetInstance<HendecamConfig>().BootsTreeRework)
                    item.rare = ItemRarityID.Lime;
                break;
        }
    }
    public override void UpdateArmorSet(Player player, string set)
    {
        if (player.GetHCPlayer().leadPlating)
        {
            return;
        }
        base.UpdateArmorSet(player, set);
    }
    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        switch (item.type)
        {
            case ItemID.RodOfHarmony:
                player.buffImmune[BuffID.ChaosState] = true;
                break;

            case ItemID.ObsidianSkull:
                player.buffImmune[BuffID.OnFire] = true;
                break;

            case ItemID.CobaltShield:
                player.ClearBuff(BuffID.Dazed);
                break;

            case ItemID.ObsidianShield:
                player.buffImmune[BuffID.OnFire] = true;
                player.ClearBuff(BuffID.Dazed);
                break;

            case ItemID.Blindfold:
                player.buffImmune[BuffID.Blackout] = true;
                break;

            case ItemID.PocketMirror:
                player.buffImmune[BuffID.Horrified] = true;
                break;

            case ItemID.ReflectiveShades:
                player.buffImmune[BuffID.Blackout] = true;
                player.buffImmune[BuffID.Horrified] = true;
                break;

            case ItemID.Vitamins:
                player.buffImmune[BuffID.WitheredWeapon] = true;
                break;

            case ItemID.ArmorPolish:
                player.buffImmune[BuffID.WitheredArmor] = true;
                break;

            case ItemID.ArmorBracing:
                player.buffImmune[BuffID.WitheredWeapon] = true;
                player.buffImmune[BuffID.WitheredArmor] = true;
                break;

            case ItemID.AdhesiveBandage:
                player.buffImmune[BuffID.Rabies] = true;
                break;

            case ItemID.Bezoar:
                player.buffImmune[BuffID.Venom] = true;
                break;

            case ItemID.MedicatedBandage:
                player.buffImmune[BuffID.Rabies] = true;
                player.buffImmune[BuffID.Venom] = true;
                break;

            case ItemID.Nazar:
                player.buffImmune[BuffID.ManaSickness] = true;
                break;

            case ItemID.Megaphone:
                player.buffImmune[BuffID.Suffocation] = true;
                break;

            case ItemID.CountercurseMantra:
                player.buffImmune[BuffID.ManaSickness] = true;
                player.buffImmune[BuffID.Suffocation] = true;
                break;

            case ItemID.TrifoldMap:
                player.buffImmune[BuffID.VortexDebuff] = true;
                break;

            case ItemID.FastClock:
                player.buffImmune[BuffID.OgreSpit] = true;
                break;

            case ItemID.ThePlan:
                player.buffImmune[BuffID.VortexDebuff] = true;
                player.buffImmune[BuffID.OgreSpit] = true;
                break;

            case ItemID.HandWarmer:
                player.buffImmune[BuffID.Frostburn2] = true;
                break;

            case ItemID.EngineeringHelmet:
                player.buffImmune[BuffID.Obstructed] = true;
                player.buffImmune[BuffID.Electrified] = true;
                break;

            case ItemID.ShimmerCloak:
                player.buffImmune[BuffID.Shimmer] = true;
                break;

            case ItemID.AnkhShield:
                player.buffImmune[BuffID.OnFire] = true;
                player.ClearBuff(BuffID.Dazed);
                goto case ItemID.AnkhCharm;

            case ItemID.AnkhCharm:
                player.buffImmune[BuffID.Blackout] = true;
                player.buffImmune[BuffID.Horrified] = true;
                player.buffImmune[BuffID.WitheredArmor] = true;
                player.buffImmune[BuffID.WitheredWeapon] = true;
                player.buffImmune[BuffID.Rabies] = true;
                player.buffImmune[BuffID.Venom] = true;
                player.buffImmune[BuffID.ManaSickness] = true;
                player.buffImmune[BuffID.Suffocation] = true;
                player.buffImmune[BuffID.VortexDebuff] = true;
                player.buffImmune[BuffID.OgreSpit] = true;
                player.buffImmune[BuffID.NeutralHunger] = true;
                player.buffImmune[BuffID.Hunger] = true;
                player.buffImmune[BuffID.Starving] = true;
                player.buffImmune[BuffID.Frostburn] = true;
                player.buffImmune[BuffID.ShadowFlame] = true;
                player.buffImmune[BuffID.OnFire3] = true;
                player.buffImmune[BuffID.CursedInferno] = true;
                player.buffImmune[BuffID.Frozen] = true;
                player.buffImmune[BuffID.Frostburn2] = true;
                player.buffImmune[BuffID.Obstructed] = true;
                player.buffImmune[BuffID.Electrified] = true;
                break;
        }

        if (!GetInstance<HendecamConfig>().BootsTreeRework)
            return;

        switch (item.type)
        {
            case ItemID.FlurryBoots:
                player.iceSkate = true;
                break;

            case ItemID.SailfishBoots:
                player.fishingSkill += 30;
                break;

            case ItemID.AmphibianBoots:
                player.fishingSkill += 30;
                player.rocketBoots = 1;
                player.vanityRocketBoots = 4;
                break;

            case ItemID.FrogLeg:
                player.rocketBoots = 1;
                player.vanityRocketBoots = 4;
                break;

            case ItemID.WaterWalkingBoots:
            case ItemID.ObsidianWaterWalkingBoots:
            case ItemID.LavaWaders:
            case ItemID.FlowerBoots:
                player.moveSpeed += 1f;
                player.accRunSpeed = 6.75f;
                break;

            case ItemID.FlameWakerBoots:
                player.moveSpeed += 1f;
                player.accRunSpeed = 6.75f;
                player.fireWalk = true;
                break;

            case ItemID.HellfireTreads:
                player.fireWalk = true;
                player.lavaRose = true;
                player.waterWalk2 = true;
                player.lavaMax += 210;
                break;

            case ItemID.HermesBoots:
                player.rocketBoots = 1;
                player.vanityRocketBoots = 2;
                break;

            case ItemID.SpectreBoots:
            case ItemID.LightningBoots:
                player.iceSkate = true;
                player.desertBoots = true;
                break;

            case ItemID.IceSkates:
                player.desertBoots = true;
                break;

            case ItemID.FrostsparkBoots:
                player.accRunSpeed = 12f;
                player.desertBoots = true;
                player.fishingSkill += 30;
                break;

            case ItemID.TerrasparkBoots:
                player.accRunSpeed = 12f;
                player.desertBoots = true;
                player.fishingSkill += 30;
                player.flowerBoots = true;
                player.noFallDmg = true;
                player.autoJump = true;
                player.jumpSpeedBoost += 0.6f;
                player.extraFall += 10;
                if (!hideVisual)
                {
                    player.CancelAllBootRunVisualEffects();
                    player.hellfireTreads = true;
                    if (!player.mount.Active || player.mount.Type != MountID.WallOfFleshGoat)
                    {
                        player.DoBootsEffect(player.DoBootsEffect_PlaceFlamesOnTile);
                        player.DoBootsEffect(player.DoBootsEffect_PlaceFlowersOnTile);
                    }
                }
                break;

            case ItemID.RocketBoots:
                player.moveSpeed += 1f;
                player.accRunSpeed = 12f;
                if (!hideVisual)
                {
                    player.CancelAllBootRunVisualEffects();
                    player.hellfireTreads = true;
                    if (!player.mount.Active || player.mount.Type != MountID.WallOfFleshGoat)
                    {
                        player.DoBootsEffect(player.DoBootsEffect_PlaceFlamesOnTile);
                    }
                }
                break;

            case ItemID.FairyBoots:
                player.moveSpeed += 1f;
                player.accRunSpeed = 12f;
                break;
            case (ItemID.CobaltPickaxe):
                item.pick = 125;
                break;
            case (ItemID.PalladiumPickaxe):
                item.pick = 125;
                break;
        }
        #region vanilla buff
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs)
        {
            switch (item.type)
            {
                case ItemID.Xenopopper:
                    item.damage = 54;
                    item.useTime = 19;
                    item.useAnimation = 19;
                    break;

                case ItemID.IceSickle:
                    item.damage = 60;
                    break;

                case ItemID.MedusaHead:
                    item.damage = 85;
                    break;

                case ItemID.CobaltRepeater:
                    item.useTime = 9;
                    item.useAnimation = 18;
                    item.reuseDelay = 14;
                    break;

                case ItemID.PalladiumRepeater:
                    item.useTime = 9;
                    item.useAnimation = 18;
                    item.reuseDelay = 12;
                    break;

                case ItemID.MythrilRepeater:
                    item.useTime = 8;
                    item.useAnimation = 16;
                    item.reuseDelay = 13;
                    break;

                case ItemID.OrichalcumRepeater:
                    item.useTime = 8;
                    item.useAnimation = 16;
                    item.reuseDelay = 12;
                    break;

                case ItemID.AdamantiteRepeater:
                    item.useTime = 7;
                    item.useAnimation = 14;
                    item.reuseDelay = 12;
                    break;

                case ItemID.TitaniumRepeater:
                    item.useTime = 7;
                    item.useAnimation = 14;
                    item.reuseDelay = 11;
                    break;

                case ItemID.HallowedRepeater:
                    item.useTime = 7;
                    item.useAnimation = 14;
                    item.reuseDelay = 10;
                    break;

                case ItemID.ChlorophyteShotbow:
                    item.damage = 46;
                    item.useTime = 19;
                    item.useAnimation = 19;
                    break;

                case ItemID.DD2PhoenixBow:
                    item.damage = 43;
                    item.useTime = 17;
                    item.useAnimation = 17;
                    break;

                case ItemID.SpectreStaff:
                    item.damage = 86;
                    item.useTime = 22;
                    item.useAnimation = 22;
                    break;

                case ItemID.Vilethorn:
                    item.damage = 12;
                    item.mana = 8;
                    break;

                case ItemID.TheRottedFork:
                    item.damage = 19;
                    item.useTime = 25;
                    item.useAnimation = 25;
                    item.shootSpeed = 5.15f;
                    break;

                case ItemID.CobaltDrill:
                    item.damage = 83;
                    item.useAnimation = 10;
                    item.useTime = 6;
                    break;

                case ItemID.PalladiumDrill:
                    item.damage = 87;
                    item.useAnimation = 10;
                    item.useTime = 5;
                    break;

                case ItemID.MythrilDrill:
                    item.damage = 95;
                    item.useAnimation = 9;
                    item.useTime = 4;
                    break;

                case ItemID.OrichalcumDrill:
                    item.damage = 102;
                    item.useAnimation = 9;
                    item.useTime = 4;
                    break;

                case ItemID.AdamantiteDrill:
                    item.damage = 108;
                    item.useAnimation = 9;
                    item.useTime = 3;
                    break;

                case ItemID.TitaniumDrill:
                    item.damage = 115;
                    item.useAnimation = 9;
                    item.useTime = 3;
                    break;

                case ItemID.Drax:
                    item.damage = 190;
                    item.useAnimation = 8;
                    item.useTime = 2;
                    break;

                case ItemID.ChlorophyteDrill:
                    item.damage = 215;
                    item.useAnimation = 8;
                    item.useTime = 2;
                    break;

                case ItemID.LaserDrill:
                    item.damage = 155;
                    item.useAnimation = 12;
                    item.useTime = 4;
                    item.tileBoost = 23;
                    break;

                case ItemID.VortexDrill:
                case ItemID.NebulaDrill:
                case ItemID.StardustDrill:
                    item.damage = 325;
                    item.useAnimation = 6;
                    item.useTime = 1;
                    break;

                case ItemID.SolarFlareDrill:
                    item.damage = 350;
                    item.useAnimation = 6;
                    item.useTime = 1;
                    break;

                case ItemID.CandyCaneSword:
                    item.scale = 1.15f;
                    item.damage = 21;
                    item.useTime = 14;
                    item.useAnimation = 14;
                    break;

                case ItemID.DarkLance:
                    item.damage = 41;
                    item.useTime = 17;
                    item.useAnimation = 17;
                    item.shootSpeed = 8.75f;
                    break;

                case ItemID.MonkStaffT2:
                    item.damage = 63;
                    item.useTime = 25;
                    item.useAnimation = 25;
                    item.shootSpeed = 87.5f;
                    break;

                case ItemID.BookofSkulls:
                    item.useTime = 27;
                    item.useAnimation = 27;
                    item.mana = 13;
                    item.shootSpeed = 11.25f;
                    break;

                case ItemID.Frostbrand:
                    item.damage = 49;
                    item.useTime = 19;
                    item.useAnimation = 19;
                    item.shootSpeed = 19.5f;
                    break;

                case ItemID.OnyxBlaster:
                    item.damage = 25;
                    item.useTime = 46;
                    item.useAnimation = 46;
                    item.shootSpeed = 9.15f;
                    break;

                case ItemID.TheHorsemansBlade:
                    item.useTime = 19;
                    item.useAnimation = 19;
                    break;

                case ItemID.FlyingKnife:
                    item.damage = 119 / 2;
                    break;

                case ItemID.JackOLanternLauncher:
                    item.shootSpeed = 15.95f;
                    item.useTime = 18;
                    item.useAnimation = 18;
                    break;

                case ItemID.WandofFrosting:
                    item.damage = 18;
                    item.useTime = 21;
                    item.useAnimation = 21;
                    break;

                case ItemID.PearlwoodSword:
                    item.damage = 45;
                    item.useTime = 10;
                    item.useAnimation = 10;
                    item.scale = 2f;
                    break;

                case ItemID.PhoenixBlaster:
                    item.damage = 31;
                    item.useTime = 13;
                    item.useAnimation = 13;
                    break;

                case ItemID.NettleBurst:
                    item.damage = 70;
                    item.ArmorPenetration = 20;
                    item.shootSpeed = 36f;
                    break;

                case ItemID.Cutlass:
                    item.scale = 1.25f;
                    item.useTime = 11;
                    item.useAnimation = 11;
                    break;

                case ItemID.Flamethrower:
                    item.damage = 38;
                    item.shootSpeed = 15.1f;
                    break;

                case ItemID.ElfMelter:
                    item.damage = 58;
                    item.shootSpeed = 18.05f;
                    break;

                case ItemID.BubbleGun:
                    item.shootSpeed = 26.5f;
                    break;

                case ItemID.SapphireStaff:
                    item.useTime = 29;
                    item.useAnimation = 29;
                    break;

                case ItemID.EmeraldStaff:
                    item.useTime = 27;
                    item.useAnimation = 27;
                    break;

                case ItemID.RubyStaff:
                    item.useTime = 23;
                    item.useAnimation = 23;
                    break;

                case ItemID.DiamondStaff:
                    item.useTime = 21;
                    item.useAnimation = 21;
                    break;

                case ItemID.BoneArrow:
                    item.damage = 10;
                    item.shootSpeed = 6.33f;
                    break;

                case ItemID.BlueMoon:
                    item.StatsModifiedBy.Add(Mod);
                    item.ArmorPenetration = 25;
                    break;

                case ItemID.NightsEdge:
                    item.damage = 54;
                    item.scale = 1.1f;
                    item.ArmorPenetration = 15;
                    item.useTime = 19;
                    item.useAnimation = 19;
                    break;

                case ItemID.Excalibur:
                    item.damage = 100;
                    item.scale = 1.1f;
                    item.useTime = 17;
                    item.useAnimation = 17;
                    break;

                case ItemID.TrueExcalibur:
                    item.damage = 102;
                    item.scale = 1.15f;
                    item.useTime = 15;
                    item.useAnimation = 15;
                    break;

                case ItemID.TrueNightsEdge:
                    item.damage = 95;
                    item.scale = 1.25f;
                    item.useTime = 27;
                    item.useAnimation = 27;
                    break;

                case ItemID.TerraBlade:
                    item.damage = 111;
                    item.scale = 1.11f;
                    break;

                case ItemID.FalconBlade:
                    item.useTime = 9;
                    item.useAnimation = 9;
                    break;

                case ItemID.NorthPole:
                    item.damage = 125;
                    item.useTime = 24;
                    item.useAnimation = 24;
                    item.shootSpeed = 8.67f;
                    break;

                case ItemID.Umbrella:
                    item.crit = 96;
                    break;

                case ItemID.TragicUmbrella:
                    item.StatsModifiedBy.Add(Mod);
                    item.damage = 48;
                    item.crit = 96;
                    break;

                case ItemID.IceBow:
                    item.damage = 51;
                    break;

                case ItemID.ShadowFlameBow:
                    item.damage = 69;
                    break;

                case ItemID.Revolver:
                    item.damage = 25;
                    break;

                case ItemID.PiercingStarlight:
                    item.shootSpeed = 30.25f;
                    break;

                case ItemID.DartRifle:
                    item.damage = 57;
                    item.useTime = 35;
                    item.useAnimation = 35;
                    break;

                case ItemID.PossessedHatchet:
                    item.damage = 111;
                    item.useTime = 11;
                    item.useAnimation = 11;
                    break;

                case ItemID.DartPistol:
                    item.damage = 31;
                    item.useTime = 17;
                    item.useAnimation = 17;
                    break;

                case ItemID.Megashark:
                    item.damage = 33;
                    item.knockBack = 3f;
                    item.shootSpeed = 12.25f;
                    break;

                case ItemID.Muramasa:
                    item.damage = 29;
                    item.useTime = 14;
                    item.useAnimation = 14;
                    item.scale = 1.15f;
                    item.ArmorPenetration = 5;
                    break;
            }
        }
        #endregion
        #region shimmer
        ItemID.Sets.ShimmerTransformToItem[ItemID.Hammush] = ItemID.MushroomSpear;
        ItemID.Sets.ShimmerTransformToItem[ItemID.SlimeHook] = ItemType<GelCube>();
        #endregion
    }

    public override void UpdateInfoAccessory(Item item, Player player)
    {
        switch (item.type)
        {
            case ItemID.PDA:
            case ItemID.CellPhone:
            case ItemID.Shellphone:
            case ItemID.ShellphoneSpawn:
            case ItemID.ShellphoneHell:
            case ItemID.ShellphoneOcean:
                player.GetModPlayer<IQTestPlayer>().showLobotometer = true;
                player.GetModPlayer<JokePlayer>().showJokes = true;
                player.GetModPlayer<BrainScanPlayer>().showLobotometerDecay = true;
                break;
        }
    }

    public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (item.type == ItemType<AA12>() && (type == ProjectileID.ChlorophyteBullet || type == ProjectileType<ChloroShit>()))
        {
            damage = (int)(damage * 0.5f);
        }
    }
}