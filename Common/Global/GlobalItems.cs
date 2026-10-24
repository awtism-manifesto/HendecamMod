using HendecamMod.Common.Systems;
using HendecamMod.Content.Global;

namespace HendecamMod.Common.Global;

public class RodOfHarmonyBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.RodOfHarmony;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.ChaosState] = true;
    }
}

public class ObsidanSkullBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.ObsidianSkull;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.OnFire] = true;
    }
}

public class CobaltShieldBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.CobaltShield;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.ClearBuff(BuffID.Dazed);
    }
}

public class ObsidianShieldBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.ObsidianShield;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 1000);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.OnFire] = true;
        player.ClearBuff(BuffID.Dazed);
    }
}

public class BlindfoldBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.Blindfold;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.Blackout] = true;
    }
}

public class PocketMirrorBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.PocketMirror;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.Horrified] = true;
    }
}

public class ReflectiveShadesBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.ReflectiveShades;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 1000);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.Blackout] = true;
        player.buffImmune[BuffID.Horrified] = true;
    }
}

public class VitaminsBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.Vitamins;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.WitheredWeapon] = true;
    }
}

public class ArmorPolishBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.ArmorPolish;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.WitheredArmor] = true;
    }
}

public class ArmorBracingBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.ArmorBracing;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 1000);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.WitheredWeapon] = true;
        player.buffImmune[BuffID.WitheredArmor] = true;
    }
}

public class AdhesiveBandageBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.AdhesiveBandage;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.Rabies] = true;
    }
}

public class BezoarBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.Bezoar;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.Venom] = true;
    }
}

public class MedicatedBandageBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.MedicatedBandage;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 1000);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.Rabies] = true;
        player.buffImmune[BuffID.Venom] = true;
    }
}

public class NazarBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.Nazar;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.ManaSickness] = true;
    }
}

public class MegaphoneBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.Megaphone;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.Suffocation] = true;
    }
}

public class CounterCurseMantraBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.CountercurseMantra;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 1000);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.ManaSickness] = true;
        player.buffImmune[BuffID.Suffocation] = true;
    }
}

public class TrifoldMapBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.TrifoldMap;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.VortexDebuff] = true;
    }
}

public class FastClockBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.FastClock;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.OgreSpit] = true;
    }
}

public class ThePlanBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.ThePlan;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 1000);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.VortexDebuff] = true;
        player.buffImmune[BuffID.OgreSpit] = true;
    }
}

public class HandWarmerBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.HandWarmer;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.Frostburn2] = true;
    }
}

public class EngineeringHelmetBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.EngineeringHelmet;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.Obstructed] = true;
        player.buffImmune[BuffID.Electrified] = true;
    }
}

public class ChromaticCloakBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.ShimmerCloak;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.Shimmer] = true;
    }
}

public class AnkhShieldBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.AnkhShield;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 8000);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
        ItemID.Sets.ShimmerTransformToItem[ItemID.AnkhShield] = ItemID.AnkhCharm;
        item.defense = 16;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.OnFire] = true;
        player.ClearBuff(BuffID.Dazed);
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
    }
}

public class AnkhCharmBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.AnkhCharm;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 8000);
        item.rare = ItemRarityID.LightRed;
        ItemID.Sets.ShimmerTransformToItem[ItemID.AnkhCharm] = ItemID.AnkhShield;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
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
    }
}

public class FaelingBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.Shimmerfly;
    }

    public override void SetDefaults(Item item)
    {
        item.bait = 50;
    }
}

public class CrabBannerBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.CrabBanner;
    }

    public override void SetDefaults(Item item)
    {
        item.bait = 1200000;
    }
}

public class DirtBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.DirtBlock;
    }

    public override void SetDefaults(Item item)
    {
        item.bait = 1;
    }
}

public class WormNerf : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.Worm;
    }

    public override void SetDefaults(Item item)
    {
        item.value = Item.sellPrice(silver: 0);
    }
}

public class EtherianBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.DD2EnergyCrystal;
    }

    public override void SetDefaults(Item item)
    {
        item.value = 1250;
    }
}

public class FlurryBuff : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.FlurryBoots;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.iceSkate = true;
    }
}

public class SailfishBuff : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.SailfishBoots;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.fishingSkill += 30;
    }
}

public class AmphBuff : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.AmphibianBoots;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.fishingSkill += 30;
        player.rocketBoots = 1;
        player.vanityRocketBoots = 4;
    }
}

public class FrogBuff : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.FrogLeg;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.rocketBoots = 1;
        player.vanityRocketBoots = 4;
    }
}

public class WaterWalkingBuff : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.WaterWalkingBoots;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.moveSpeed += 1f;
        player.accRunSpeed = 6.75f;
    }
}

public class ObsidianWaterWalkingBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.ObsidianWaterWalkingBoots;
    }

    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.moveSpeed += 1f;
        player.accRunSpeed = 6.75f;
    }
}

public class LavaWaderBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.LavaWaders;
    }

    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.moveSpeed += 1f;
        player.accRunSpeed = 6.75f;
    }
}

public class FlameBuff : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.FlameWakerBoots;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.moveSpeed += 1f;
        player.accRunSpeed = 6.75f;
        player.fireWalk = true;
    }
}

public class FlowerBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.FlowerBoots;
    }

    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.moveSpeed += 1f;
        player.accRunSpeed = 6.75f;
    }
}

public class HellfireBuff : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.HellfireTreads;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.fireWalk = true;
        player.lavaRose = true;
        player.waterWalk2 = true;
        player.lavaMax += 210;
    }
}

public class HermesBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.HermesBoots;
    }

    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.rocketBoots = 1;
        player.vanityRocketBoots = 2;
    }
}

public class SpectreBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.SpectreBoots;
    }

    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.iceSkate = true;
        player.desertBoots = true;
    }
}

public class IceBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.IceSkates;
    }

    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.desertBoots = true;
    }
}

public class DuneBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.SandBoots;
    }

    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }
}

public class LightningBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.LightningBoots;
    }

    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.iceSkate = true;
        player.desertBoots = true;
    }
}

public class FrostBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.FrostsparkBoots;
    }

    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.accRunSpeed = 12f;
        player.desertBoots = true;
        player.fishingSkill += 30;
    }
}

public class TerraBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.TerrasparkBoots;
    }

    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Lime;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
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
    }
}

public class RocketBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.RocketBoots;
    }

    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
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
    }
}

public class FairyBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return GetInstance<HendecamConfig>().BootsTreeRework && item.type == ItemID.FairyBoots;
    }

    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Green;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.moveSpeed += 1f;
        player.accRunSpeed = 12f;
    }
}

public class Peebotomy : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.PDA;
    }

    public override void UpdateInfoAccessory(Item item, Player player)
    {
        player.GetModPlayer<IQTestPlayer>().showLobotometer = true;
        player.GetModPlayer<JokePlayer>().showJokes = true;
        player.GetModPlayer<BrainScanPlayer>().showLobotometerDecay = true;
    }
}

public class Cellbotomy : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.CellPhone;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 15;
        item.useAnimation = 15;
    }

    public override void UpdateInfoAccessory(Item item, Player player)
    {
        player.GetModPlayer<IQTestPlayer>().showLobotometer = true;
        player.GetModPlayer<JokePlayer>().showJokes = true;
        player.GetModPlayer<BrainScanPlayer>().showLobotometerDecay = true;
    }
}

public class Shellbotomy : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.Shellphone;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 15;
        item.useAnimation = 15;
    }

    public override void UpdateInfoAccessory(Item item, Player player)
    {
        player.GetModPlayer<IQTestPlayer>().showLobotometer = true;
        player.GetModPlayer<JokePlayer>().showJokes = true;
        player.GetModPlayer<BrainScanPlayer>().showLobotometerDecay = true;
    }
}

public class Shellbotomy2 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.ShellphoneSpawn;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 15;
        item.useAnimation = 15;
    }

    public override void UpdateInfoAccessory(Item item, Player player)
    {
        player.GetModPlayer<IQTestPlayer>().showLobotometer = true;
        player.GetModPlayer<JokePlayer>().showJokes = true;
        player.GetModPlayer<BrainScanPlayer>().showLobotometerDecay = true;
    }
}

public class Shellbotomy3 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.ShellphoneHell;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 15;
        item.useAnimation = 15;
    }

    public override void UpdateInfoAccessory(Item item, Player player)
    {
        player.GetModPlayer<IQTestPlayer>().showLobotometer = true;
        player.GetModPlayer<JokePlayer>().showJokes = true;
        player.GetModPlayer<BrainScanPlayer>().showLobotometerDecay = true;
    }
}

public class Shellbotomy4 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.ShellphoneOcean;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 15;
        item.useAnimation = 15;
    }

    public override void UpdateInfoAccessory(Item item, Player player)
    {
        player.GetModPlayer<IQTestPlayer>().showLobotometer = true;
        player.GetModPlayer<JokePlayer>().showJokes = true;
        player.GetModPlayer<BrainScanPlayer>().showLobotometerDecay = true;
    }
}