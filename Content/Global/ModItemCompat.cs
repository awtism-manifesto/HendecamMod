using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace HendecamMod.Content.GlobalItems;

public class BowMage1 : GlobalItem
{

    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModLoader.TryGetMod("CHIRIKSADDITIONCR", out Mod BowMerica) && BowMerica.TryFind<ModItem>("InfernalInvention", out ModItem InfernalInvention))
        {
            return item.type == InfernalInvention.Type;
        }
        else
        {
            return false;
        }

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
        if (ModLoader.TryGetMod("CHIRIKSADDITIONCR", out Mod BowMerica) && BowMerica.TryFind<ModItem>("Dawn", out ModItem Dawn))
        {
            return item.type == Dawn.Type;
        }
        else
        {
            return false;
        }

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
        if (ModLoader.TryGetMod("CHIRIKSADDITIONCR", out Mod BowMerica) && BowMerica.TryFind<ModItem>("Calcium", out ModItem Calcium))
        {
            return item.type == Calcium.Type;
        }
        else
        {
            return false;
        }

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

public class ConsolariaForSomeReason : GlobalItem
{

    // if (ModLoader.TryGetMod("HendecamMod", out Mod ShitMerica))

    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModLoader.TryGetMod("Consolaria", out Mod ConsMerica) && ConsMerica.TryFind("EggCannon", out ModItem EggCannon))
        {
            return item.type == EggCannon.Type;
        }
        else
        {
            return false;
        }

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