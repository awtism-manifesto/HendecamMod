using HendecamMod.Common.Systems;
using HendecamMod.Content.Global;
using System.Collections.Generic;

namespace HendecamMod.Common.Global;

public class AngelBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().BootsTreeRework == true && (ModLoader.TryGetMod("CalamityMod", out Mod Cal) && Cal.TryFind("AngelTreads", out ModItem AngelTreads)))
        {
            return item.type == AngelTreads.Type;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {

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
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also has enhanced effects of Terraspark Boots") { OverrideColor = Color.DarkViolet });
    }
}
public class MoonBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().BootsTreeRework == true && (ModLoader.TryGetMod("CalamityMod", out Mod Cal) && Cal.TryFind("MoonWalkers", out ModItem MoonWalkers)))
        {
            return item.type == MoonWalkers.Type;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {

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
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also has enhanced effects of Terraspark Boots") { OverrideColor = Color.DarkViolet });
    }
}
public class VoiddBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().BootsTreeRework == true && (ModLoader.TryGetMod("CalamityMod", out Mod Cal) && Cal.TryFind("VoidStriders", out ModItem VoidStriders)))
        {
            return item.type == VoidStriders.Type;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {

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
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also has enhanced effects of Terraspark Boots") { OverrideColor = Color.DarkViolet });
    }
}
public class SeraphBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().BootsTreeRework == true && (ModLoader.TryGetMod("CalamityMod", out Mod Cal) && Cal.TryFind("SeraphTracers", out ModItem SeraphTracers)))
        {
            return item.type == SeraphTracers.Type;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {

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
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also has enhanced effects of Terraspark Boots") { OverrideColor = Color.DarkViolet });
    }
    public class ZephyrBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            if (GetInstance<HendecamConfig>().BootsTreeRework == true && (ModLoader.TryGetMod("FargowiltasSouls", out Mod Farg) && Farg.TryFind("ZephyrBoots", out ModItem ZephyrBoots)))
            {
                return item.type == ZephyrBoots.Type;
            }
            else return false;
        }

        public override void SetDefaults(Item item)
        {

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
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also has enhanced effects of Terraspark Boots") { OverrideColor = Color.DarkViolet });
        }
    }
    public class ParticleSprinterBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            if (GetInstance<HendecamConfig>().BootsTreeRework == true && (ModLoader.TryGetMod("ThoriumMod", out Mod Thor) && Thor.TryFind("TerrariumParticleSprinters", out ModItem TerrariumParticleSprinters)))
            {
                return item.type == TerrariumParticleSprinters.Type;
            }
            else return false;
        }

        public override void SetDefaults(Item item)
        {

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
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also has enhanced effects of Terraspark Boots") { OverrideColor = Color.DarkViolet });
        }
    }
    public class HorizonBuff : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            if (GetInstance<HendecamConfig>().BootsTreeRework == true && (ModLoader.TryGetMod("ContinentOfJourney", out Mod Coj) && Coj.TryFind("Horizon", out ModItem Horizon)))
            {
                return item.type == Horizon.Type;
            }
            else return false;
        }

        public override void SetDefaults(Item item)
        {

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
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also has enhanced effects of Terraspark Boots") { OverrideColor = Color.DarkViolet });
        }
    }
}
