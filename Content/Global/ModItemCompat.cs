using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;
using Terraria;

namespace HendecamMod.Content.GlobalItems;

public class BowMage1 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModLoader.TryGetMod("CHIRIKSADDITIONCR", out Mod BowMerica) && BowMerica.TryFind("InfernalInvention", out ModItem InfernalInvention))
        {
            return item.type == InfernalInvention.Type;
        }

        return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod Cross-Mod (Merge Bows) - Now deals Ranged AND Magic damage") { OverrideColor = Color.MediumPurple });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<RangedMagicDamage>();
    }
}

public class BowMage12 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModLoader.TryGetMod("CHIRIKSADDITIONCR", out Mod BowMerica) && BowMerica.TryFind("Dawn", out ModItem Dawn))
        {
            return item.type == Dawn.Type;
        }

        return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod Cross-Mod (Merge Bows) - Now deals Ranged AND Magic damage") { OverrideColor = Color.MediumPurple });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<RangedMagicDamage>();
    }
}

public class BowMage123 : GlobalItem
{
    // if (ModLoader.TryGetMod("HendecamMod", out Mod ShitMerica))

    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModLoader.TryGetMod("CHIRIKSADDITIONCR", out Mod BowMerica) && BowMerica.TryFind("Calcium", out ModItem Calcium))
        {
            return item.type == Calcium.Type;
        }

        return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod Cross-Mod (Merge Bows) - Now deals Ranged AND Magic damage") { OverrideColor = Color.MediumPurple });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<RangedMagicDamage>();
    }
}
public class DoorStupid1 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModLoader.TryGetMod("SOTS", out Mod SotsMerica) && SotsMerica.TryFind("DoorPants", out ModItem DoorPants))
        {
            return item.type == DoorPants.Type;
        }

        return false;
    }
    public static readonly int StupidSpeed = 5;
    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.GetAttackSpeed<StupidDamage>() += StupidSpeed / 100f;
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod Cross-Mod (SOTS) - Increases stupid attack speed by 5%") { OverrideColor = Color.OrangeRed});
    }

    
}
public class DoorStupid2 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModLoader.TryGetMod("SOTS", out Mod SotsMerica) && SotsMerica.TryFind("BandOfDoor", out ModItem BandOfDoor))
        {
            return item.type == BandOfDoor.Type;
        }

        return false;
    }
    public static readonly int StupidSpeed = 5;
    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        player.GetAttackSpeed<StupidDamage>() += StupidSpeed / 100f;
        player.GetDamage<StupidDamage>() += StupidSpeed / 100f;
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod Cross-Mod (SOTS) - Increases stupid damage and attack speed by 5%") { OverrideColor = Color.OrangeRed });
    }


}
public class ConsolariaForSomeReason : GlobalItem
{
    // if (ModLoader.TryGetMod("HendecamMod", out Mod ShitMerica))

    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModLoader.TryGetMod("Consolaria", out Mod ConsMerica) && ConsMerica.TryFind("EggCannon", out ModItem EggCannon))
        {
            return item.type == EggCannon.Type;
        }

        return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod Cross-Mod (Consolaria) - Now deals Ranged AND Stupid damage, fires in a two round burst") { OverrideColor = Color.LimeGreen });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<RangedStupidDamage>();
        item.useTime = 10;
        item.useAnimation = 20;
        item.reuseDelay = 15;
    }
}
public class Arse : GlobalItem
{
    // if (ModLoader.TryGetMod("HendecamMod", out Mod ShitMerica))

    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModLoader.TryGetMod("Arsenal_Mod", out Mod Arse) && Arse.TryFind("SlingerShooter", out ModItem SlingerShooter))
        {
            return item.type == SlingerShooter.Type;
        }

        return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod Cross-Mod (Arsenal) - Now deals Ranged AND Stupid damage") { OverrideColor = Color.MediumVioletRed });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<RangedStupidDamage>();
       
    }
}
 
public class Arse2 : GlobalItem
{
    // if (ModLoader.TryGetMod("HendecamMod", out Mod ShitMerica))

    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModLoader.TryGetMod("Arsenal_Mod", out Mod Arse) && Arse.TryFind("Crackshot", out ModItem Crackshot))
        {
            return item.type == Crackshot.Type;
        }

        return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod Cross-Mod (Arsenal) - Now deals Ranged AND Stupid damage") { OverrideColor = Color.MediumVioletRed });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<RangedStupidDamage>();

    }
}
public class StupidSand : GlobalItem
{
    // if (ModLoader.TryGetMod("HendecamMod", out Mod ShitMerica))

    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModLoader.TryGetMod("VitalityMod", out Mod VitalMerica) && VitalMerica.TryFind("PocketSand", out ModItem PocketSand))
        {
            return item.type == PocketSand.Type;
        }

        return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod Cross-Mod (Vitality) - Now deals stupid damage") { OverrideColor = Color.MediumVioletRed });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<StupidDamage>();

    }
}