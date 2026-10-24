using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Common.Systems
{
    public class VanillaItemTooltips : GlobalItem
    {
        private readonly record struct TooltipEntry(
            string Key,
            bool Replace = false,
            Color? Color = null,
            Func<bool> Condition = null
        );

        private const string BaseKey = "Mods.HendecamMod.Items.";

        private static bool WeaponBuffsEnabled() => ModContent.GetInstance<HendecamConfig>().VanillaWeaponStatBuffs;
        private static bool BootsReworkEnabled() => ModContent.GetInstance<HendecamConfig>().BootsTreeRework;

        private static readonly Dictionary<int, TooltipEntry> ItemTooltips = new()
        {
            [ItemID.LawnMower] = new("LawnMower", Replace: false, new Color(255, 100, 100)),

            [ItemID.RodOfHarmony] = new("RodOfHarmony", Replace: false, Color.DarkViolet),
            [ItemID.ObsidianSkull] = new("ObsidianSkull", Replace: false, Color.DarkViolet),
            [ItemID.CobaltShield] = new("CobaltShield", Replace: false, Color.DarkViolet),
            [ItemID.ObsidianShield] = new("ObsidianShield", Replace: false, Color.DarkViolet),
            [ItemID.Blindfold] = new("Blindfold", Replace: false, Color.DarkViolet),
            [ItemID.PocketMirror] = new("PocketMirror", Replace: false, Color.DarkViolet),
            [ItemID.ReflectiveShades] = new("ReflectiveShades", Replace: false, Color.DarkViolet),
            [ItemID.Vitamins] = new("Vitamins", Replace: false, Color.DarkViolet),
            [ItemID.ArmorPolish] = new("ArmorPolish", Replace: false, Color.DarkViolet),
            [ItemID.ArmorBracing] = new("ArmorBracing", Replace: false, Color.DarkViolet),
            [ItemID.AdhesiveBandage] = new("AdhesiveBandage", Replace: false, Color.DarkViolet),
            [ItemID.Bezoar] = new("Bezoar", Replace: false, Color.DarkViolet),
            [ItemID.MedicatedBandage] = new("MedicatedBandage", Replace: false, Color.DarkViolet),
            [ItemID.Nazar] = new("Nazar", Replace: false, Color.DarkViolet),
            [ItemID.Megaphone] = new("Megaphone", Replace: false, Color.DarkViolet),
            [ItemID.CountercurseMantra] = new("CountercurseMantra", Replace: false, Color.DarkViolet),
            [ItemID.TrifoldMap] = new("TrifoldMap", Replace: false, Color.DarkViolet),
            [ItemID.FastClock] = new("FastClock", Replace: false, Color.DarkViolet),
            [ItemID.ThePlan] = new("ThePlan", Replace: false, Color.DarkViolet),
            [ItemID.HandWarmer] = new("HandWarmer", Replace: false, Color.DarkViolet),
            [ItemID.EngineeringHelmet] = new("EngineeringHelmet", Replace: false, Color.DarkViolet),
            [ItemID.ShimmerCloak] = new("ShimmerCloak", Replace: false, Color.DarkViolet),
            [ItemID.AnkhShield] = new("AnkhShield", Replace: false, Color.DarkViolet),
            [ItemID.AnkhCharm] = new("AnkhCharm", Replace: false, Color.DarkViolet),

            [ItemID.Shimmerfly] = new("Shimmerfly", Replace: false, Color.DarkViolet),
            [ItemID.EmpressButterfly] = new("EmpressButterfly", Replace: false, Color.DarkViolet),
            [ItemID.CrabBanner] = new("CrabBanner", Replace: false, Color.DarkViolet),
            [ItemID.DirtBlock] = new("DirtBlock", Replace: false, Color.DarkViolet),
            [ItemID.Worm] = new("Worm", Replace: false, Color.DarkViolet),
            [ItemID.DD2EnergyCrystal] = new("DD2EnergyCrystal", Replace: false, Color.DarkViolet),

            [ItemID.FlurryBoots] = new("FlurryBoots", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.SailfishBoots] = new("SailfishBoots", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.AmphibianBoots] = new("AmphibianBoots", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.FrogLeg] = new("FrogLeg", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.WaterWalkingBoots] = new("WaterWalkingBoots", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.ObsidianWaterWalkingBoots] = new("ObsidianWaterWalkingBoots", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.LavaWaders] = new("LavaWaders", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.FlameWakerBoots] = new("FlameWakerBoots", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.FlowerBoots] = new("FlowerBoots", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.HellfireTreads] = new("HellfireTreads", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.HermesBoots] = new("HermesBoots", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.SpectreBoots] = new("SpectreBoots", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.IceSkates] = new("IceSkates", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.SandBoots] = new("SandBoots", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.LightningBoots] = new("LightningBoots", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.FrostsparkBoots] = new("FrostsparkBoots", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.TerrasparkBoots] = new("TerrasparkBoots", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.RocketBoots] = new("RocketBoots", Replace: false, Color.DarkViolet, BootsReworkEnabled),
            [ItemID.FairyBoots] = new("FairyBoots", Replace: false, Color.DarkViolet, BootsReworkEnabled),

            [ItemID.MetalDetector] = new("MetalDetector", Replace: false, Color.DarkViolet),
            [ItemID.MagicMirror] = new("MagicMirror", Replace: false, Color.DarkViolet),
            [ItemID.IceMirror] = new("IceMirror", Replace: false, Color.DarkViolet),
            [ItemID.MagicConch] = new("MagicConch", Replace: false, Color.DarkViolet),
            [ItemID.DemonConch] = new("DemonConch", Replace: false, Color.DarkViolet),
            [ItemID.PDA] = new("PDA", Replace: false, Color.DarkViolet),
            [ItemID.CellPhone] = new("CellPhone", Replace: false, Color.DarkViolet),
            [ItemID.Shellphone] = new("Shellphone", Replace: false, Color.DarkViolet),
            [ItemID.ShellphoneSpawn] = new("ShellphoneSpawn", Replace: false, Color.DarkViolet),
            [ItemID.ShellphoneHell] = new("ShellphoneHell", Replace: false, Color.DarkViolet),
            [ItemID.ShellphoneOcean] = new("ShellphoneOcean", Replace: false, Color.DarkViolet),

            //Proj
            [ItemID.HolyArrow] = new("HolyArrow", Replace: false, Color.DarkViolet),

            //Weaps
            [ItemID.CobaltRepeater] = new("CobaltRepeater", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.PalladiumRepeater] = new("PalladiumRepeater", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.MythrilRepeater] = new("MythrilRepeater", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.OrichalcumRepeater] = new("OrichalcumRepeater", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.AdamantiteRepeater] = new("AdamantiteRepeater", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.TitaniumRepeater] = new("TitaniumRepeater", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.HallowedRepeater] = new("HallowedRepeater", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),

            [ItemID.CobaltDrill] = new("CobaltDrill", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.PalladiumDrill] = new("PalladiumDrill", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.MythrilDrill] = new("MythrilDrill", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.OrichalcumDrill] = new("OrichalcumDrill", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.AdamantiteDrill] = new("AdamantiteDrill", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.TitaniumDrill] = new("TitaniumDrill", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.Drax] = new("Drax", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.ChlorophyteDrill] = new("ChlorophyteDrill", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.LaserDrill] = new("LaserDrill", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.VortexDrill] = new("VortexDrill", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.SolarFlareDrill] = new("SolarFlareDrill", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.NebulaDrill] = new("NebulaDrill", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.StardustDrill] = new("StardustDrill", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),

            [ItemID.CandyCaneSword] = new("CandyCaneSword", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.DarkLance] = new("DarkLance", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.MonkStaffT2] = new("MonkStaffT2", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.BookofSkulls] = new("BookofSkulls", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.Frostbrand] = new("Frostbrand", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.TheHorsemansBlade] = new("TheHorsemansBlade", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.FlyingKnife] = new("FlyingKnife", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.JackOLanternLauncher] = new("JackOLanternLauncher", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.WandofFrosting] = new("WandofFrosting", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.PearlwoodSword] = new("PearlwoodSword", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.NettleBurst] = new("NettleBurst", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.Cutlass] = new("Cutlass", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.Flamethrower] = new("Flamethrower", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.ElfMelter] = new("ElfMelter", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.BoneArrow] = new("BoneArrow", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.BlueMoon] = new("BlueMoon", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.NightsEdge] = new("NightsEdge", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.Excalibur] = new("Excalibur", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.TrueExcalibur] = new("TrueExcalibur", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.TrueNightsEdge] = new("TrueNightsEdge", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.FalconBlade] = new("FalconBlade", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.NorthPole] = new("NorthPole", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.Umbrella] = new("Umbrella", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.TragicUmbrella] = new("TragicUmbrella", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.Megashark] = new("Megashark", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),
            [ItemID.Muramasa] = new("Muramasa", Replace: false, Color.DarkViolet, WeaponBuffsEnabled),

        };

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ItemTooltips.TryGetValue(entity.type, out TooltipEntry entry) &&
                   (entry.Condition is null || entry.Condition());
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ItemTooltips.TryGetValue(item.type, out TooltipEntry entry))
                return;

            if (entry.Condition is not null && !entry.Condition())
                return;

            string text = Language.GetTextValue(BaseKey + entry.Key);
            TooltipLine line = new(Mod, "CustomItemTooltip", text)
            {
                OverrideColor = entry.Color
            };

            if (entry.Replace)
            {
                int firstIndex = tooltips.FindIndex(t => t.Mod == "Terraria" && t.Name.StartsWith("Tooltip"));
                tooltips.RemoveAll(t => t.Mod == "Terraria" && t.Name.StartsWith("Tooltip"));

                if (firstIndex != -1 && firstIndex <= tooltips.Count)
                    tooltips.Insert(firstIndex, line);
                else
                    tooltips.Add(line);
            }
            else
            {
                int lastIndex = tooltips.FindLastIndex(t => t.Mod == "Terraria" && t.Name.StartsWith("Tooltip"));
                if (lastIndex != -1)
                    tooltips.Insert(lastIndex + 1, line);
                else
                    tooltips.Add(line);
            }
        }
    }
}