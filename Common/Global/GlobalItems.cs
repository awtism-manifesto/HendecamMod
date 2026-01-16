using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Common.Global
{
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Grants immunity to Chaos State when equipped") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to On Fire!") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Stunned") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Stunned and On Fire!") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Blackout") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Horrified") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Blackout and Horrified") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Withered Weapon") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Withered Armor") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Withered Weapon and Withered Armor") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Feral Bite") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Acid Venom") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Feral Bite and Acid Venom") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Mana Sickness") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Suffocation") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Mana Sickness and Suffocation") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Distorted") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Oozed") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Distorted and Oozed") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to Frostbite") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now grants immunity to Obstructed and Electrified") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now grants complete immunity to Shimmered") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to: On Fire!, VANILLA Stunned,") { OverrideColor = Color.DarkViolet });
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Blackout, Horrified, Withered Armor, Withered Weapon, Feral Bite,") { OverrideColor = Color.DarkViolet });
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Acid Venom, Mana Sickness, Suffocation, Distorted, Oozed,") { OverrideColor = Color.DarkViolet });
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Peckish, Hungry, Starving, Frostburn, Shadowflame, Hellfire,") { OverrideColor = Color.DarkViolet });
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Cursed Inferno, Frozen, Frostbite, Obstructed, and Electrified") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also grants immunity to:") { OverrideColor = Color.DarkViolet });
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Blackout, Horrified, Withered Armor, Withered Weapon, Feral Bite,") { OverrideColor = Color.DarkViolet });
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Acid Venom, Mana Sickness, Suffocation, Distorted, Oozed,") { OverrideColor = Color.DarkViolet });
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Peckish, Hungry, Starving, Frostburn, Shadowflame, Hellfire,") { OverrideColor = Color.DarkViolet });
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Cursed Inferno, Frozen, Frostbite, Obstructed, and Electrified") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Can be used as bait, required for fishing in the Shimmer") { OverrideColor = Color.DarkViolet });
        }
    }
    public class EmpressRoseToyMessage : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.EmpressButterfly;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {


            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Can also be used to fish in the Shimmer") { OverrideColor = Color.DarkViolet });

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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Can be used as bait") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Can be used as bait") { OverrideColor = Color.DarkViolet });
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Sell price set to 0 because fuck you") { OverrideColor = Color.DarkViolet });
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
            return item.type == ItemID.FlurryBoots;
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.iceSkate = true;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now naturally has the effects of Ice Skates") { OverrideColor = Color.DarkViolet });
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
            return item.type == ItemID.SailfishBoots;
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.fishingSkill += 30;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now grants 30 fishing power") { OverrideColor = Color.DarkViolet });
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
            return item.type == ItemID.AmphibianBoots;
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.fishingSkill += 30;
            player.rocketBoots = 1;
            player.vanityRocketBoots = 4;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now grants 30 fishing power and rocket flight") { OverrideColor = Color.DarkViolet });
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
            return item.type == ItemID.FrogLeg;
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.rocketBoots = 1;
            player.vanityRocketBoots = 4;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now grants rocket flight") { OverrideColor = Color.DarkViolet });
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
            return item.type == ItemID.WaterWalkingBoots;
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.moveSpeed += 1f;
            player.accRunSpeed = 6.75f;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now lets you run fast") { OverrideColor = Color.DarkViolet });
        }
    }
    public class ObsidianWaterWalkingBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.ObsidianWaterWalkingBoots;
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now lets you run fast") { OverrideColor = Color.DarkViolet });
        }
    }
    public class LavaWaderBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.LavaWaders;
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now lets you run fast") { OverrideColor = Color.DarkViolet });
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
            return item.type == ItemID.FlameWakerBoots;
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.moveSpeed += 1f;
            player.accRunSpeed = 6.75f;
            player.fireWalk = true;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now allows you to run fast, and has the effects of Obsidian Skull") { OverrideColor = Color.DarkViolet });
        }
    }
    public class FlowerBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.FlowerBoots;
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now allows you to run fast") { OverrideColor = Color.DarkViolet });
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
            return item.type == ItemID.HellfireTreads;
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.fireWalk = true;
            player.lavaRose = true;
            player.waterWalk2 = true;
            player.lavaMax += 210;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now allows you to walk on all liquids, grants 7 seconds of lava immunity, immunity to fire blocks, and reduced damage from touching lava") { OverrideColor = Color.DarkViolet });
        }
    }
    public class HermesBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.HermesBoots;
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Allows rocket boot flight") { OverrideColor = Color.DarkViolet });
        }
    }
    public class SpectreBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.SpectreBoots;
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Has the effects of Ice Skates and Dune Riders") { OverrideColor = Color.DarkViolet });
        }
    }
    public class IceBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.IceSkates;
        }
        public override void SetDefaults(Item item)
        {
            item.rare = ItemRarityID.Green;
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.desertBoots = true;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Has the effects Dune Riders") { OverrideColor = Color.DarkViolet });
        }
    }
    public class DuneBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.SandBoots;
        }
        public override void SetDefaults(Item item)
        {
            item.rare = ItemRarityID.Green;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now has green rarity lol") { OverrideColor = Color.DarkViolet });
        }
    }
    public class LightningBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.LightningBoots;
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Has the effects of Ice Skates and Dune Riders") { OverrideColor = Color.DarkViolet });
        }
    }
    public class FrostBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.FrostsparkBoots;
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Has the effects of Dune Riders, grants 30 more fishing power, and has much faster movement speed") { OverrideColor = Color.DarkViolet });
        }
    }
    public class TerraBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.TerrasparkBoots;
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Run faster on sand, negate fall damage, higher jump speed, increased fall distance, auto jump, +30 fishing power, run MUCH faster") { OverrideColor = Color.DarkViolet });
        }
    }
    public class RocketBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.RocketBoots;
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Allows you to run REALLY fast") { OverrideColor = Color.DarkViolet });
        }
    }
    public class FairyBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.FairyBoots;
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
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Allows you to run REALLY fast") { OverrideColor = Color.DarkViolet });
        }
    }
}