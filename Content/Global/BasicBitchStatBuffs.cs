using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items;
using HendecamMod.Content.Poop;
using HendecamMod.Content.Projectiles;
using Mono.Cecil;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using static System.Net.Mime.MediaTypeNames;

namespace HendecamMod.Content.Global;

// if you have to read through these unhinged ahh public classes and youre not Autism Manifesto, i apologize.
public class BasicBitchXenopopperBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.Xenopopper;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 54;
        item.useTime = 19;
        item.useAnimation = 19;
    }

   
}
public class BasicBitchIceSickleBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.IceSickle;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 60;

    }


}
public class BasicBitchMedusaBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.MedusaHead;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 85;

    }


}
public class BasicBitchCobaltBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.CobaltRepeater;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 9;
        item.useAnimation = 18;
        item.reuseDelay = 14;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now fires in a two round burst") { OverrideColor = Color.DarkViolet });
    }
}
public class BasicBitchPalladiumBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.PalladiumRepeater;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 9;
        item.useAnimation = 18;
        item.reuseDelay = 12;
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now fires in a two round burst") { OverrideColor = Color.DarkViolet });
    }

}
public class BasicBitchMythrilBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.MythrilRepeater;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 8;
        item.useAnimation = 16;
        item.reuseDelay = 13;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now fires in a two round burst") { OverrideColor = Color.DarkViolet });
    }
}
public class BasicBitchOrichBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.OrichalcumRepeater;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 8;
        item.useAnimation = 16;
        item.reuseDelay = 12;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now fires in a two round burst") { OverrideColor = Color.DarkViolet });
    }
}
public class BasicBitchAdamantiteBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.AdamantiteRepeater;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 7;
        item.useAnimation = 14;
        item.reuseDelay = 12;
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now fires in a two round burst") { OverrideColor = Color.DarkViolet });
    }

}
public class BasicBitchTitaniumBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.TitaniumRepeater;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 7;
        item.useAnimation = 14;
        item.reuseDelay = 11;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now fires in a two round burst") { OverrideColor = Color.DarkViolet });
    }
}
public class BasicBitchHallowBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.HallowedRepeater;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 7;
        item.useAnimation = 14;
        item.reuseDelay = 10;
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now fires in a two round burst") { OverrideColor = Color.DarkViolet });
    }

}
public class ChlorophyteShitbow : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.ChlorophyteShotbow;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 51;
        item.useTime = 18;
        item.useAnimation = 18;
       
    }
   

}
public class PhantomPeenix : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.DD2PhoenixBow;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 43;
        item.useTime = 16;
        item.useAnimation = 16;

    }


}
public class Speccterrrr : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.SpectreStaff;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 86;
        item.useTime = 22;
        item.useAnimation = 22;

    }


}
public class BasicBitchVilethornBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.Vilethorn;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 12;
        item.mana = 8;

    }


}
public class BasicBitchRottedForkBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.TheRottedFork;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 19;
        item.useTime = 25;
        item.useAnimation = 25;
        item.shootSpeed = 5.33f;

    }


}





