using System;
using System.Collections.Generic;
using HendecamMod.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Common.Global
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

            [ItemID.PDA] = new("PDA", Replace: false, Color.DarkViolet),
            [ItemID.CellPhone] = new("CellPhone", Replace: false, Color.DarkViolet),
            [ItemID.Shellphone] = new("Shellphone", Replace: false, Color.DarkViolet),
            [ItemID.ShellphoneSpawn] = new("ShellphoneSpawn", Replace: false, Color.DarkViolet),
            [ItemID.ShellphoneHell] = new("ShellphoneHell", Replace: false, Color.DarkViolet),
            [ItemID.ShellphoneOcean] = new("ShellphoneOcean", Replace: false, Color.DarkViolet)
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