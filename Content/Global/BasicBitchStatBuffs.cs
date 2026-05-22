using HendecamMod.Common.Systems;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Global;

// if you have to read through these unhinged ahh public classes and youre not Autism Manifesto, i apologize.
public class BasicXenopopperBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.Xenopopper;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 54;
        item.useTime = 19;
        item.useAnimation = 19;
    }

   
}
public class BasicIceSickleBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.IceSickle;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 60;
    }
}

public class BasicMedusaBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.MedusaHead;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 85;
    }
}

public class BasicCobaltBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.CobaltRepeater;
        }
        else return false;
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

public class BasicPalladiumBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.PalladiumRepeater;
        }
        else return false;
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

public class BasicMythrilBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.MythrilRepeater;
        }
        else return false;
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

public class BasicOrichBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.OrichalcumRepeater;
        }
        else return false;
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

public class BasicAdamantiteBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.AdamantiteRepeater;
        }
        else return false;
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

public class BasicTitaniumBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.TitaniumRepeater;
        }
        else return false;
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

public class BasicHallowBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.HallowedRepeater;
        }
        else return false;
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

public class BasicShotbowBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.ChlorophyteShotbow;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 46;
        item.useTime = 19;
        item.useAnimation = 19;
    }
}

public class PhantomPhenixBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.DD2PhoenixBow;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 43;
        item.useTime = 17;
        item.useAnimation = 17;
    }
}

public class SpecctreStaffBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.SpectreStaff;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 86;
        item.useTime = 22;
        item.useAnimation = 22;
    }
}

public class BasicVilethornBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.Vilethorn;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 12;
        item.mana = 8;
    }
}

public class BasicRottedForkBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.TheRottedFork;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 19;
        item.useTime = 25;
        item.useAnimation = 25;
        item.shootSpeed = 5.15f;
    }
}
public class DrillBuff1 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.CobaltDrill;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 83;
        item.useAnimation = 10;
        item.useTime = 6;
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item22, player.position);
        return true;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased speed and hugely increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class DrillBuff12 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.PalladiumDrill;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 87;
        item.useAnimation = 10;
        item.useTime = 5;
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item22, player.position);
        return true;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased speed and hugely increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class DrillBuff123 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.MythrilDrill;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 95;
        item.useAnimation = 9;
        item.useTime = 4;
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item22, player.position);
        return true;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased speed and hugely increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class DrillBuff1234 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.OrichalcumDrill;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 102;
        item.useAnimation = 9;
        item.useTime = 4;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased speed and hugely increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class DrillBuff12345 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.AdamantiteDrill;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 108;
        item.useAnimation = 9;
        item.useTime = 3;
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item22, player.position);
        return true;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased speed and hugely increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class DrillBuff123456 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.TitaniumDrill;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 115;
        item.useAnimation = 9;
        item.useTime = 3;
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item22, player.position);
        return true;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased speed and hugely increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class DrillBuff567 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.Drax;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 190;
        item.useAnimation = 8;
        item.useTime = 2;
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item22, player.position);
        return true;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased speed and hugely increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class DrillBuff67 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.ChlorophyteDrill;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 215;
        item.useAnimation = 8;
        item.useTime = 2;
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item22, player.position);
        return true;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased speed and hugely increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class DrillBuffLaser : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.LaserDrill;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 155;
        item.useAnimation = 12;
        item.useTime = 4;
        item.tileBoost = 23;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased speed, range and hugely increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class DrillBuff678 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.VortexDrill;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 325;
        item.useAnimation = 6;
        item.useTime = 1;
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item22, player.position);
        return true;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased speed and hugely increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class DrillBuff6789 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.SolarFlareDrill;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 350;
        item.useAnimation = 6;
        item.useTime = 1;
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item22, player.position);
        return true;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased speed and hugely increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class DrillBuff6781 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.NebulaDrill;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 325;
        item.useAnimation = 6;
        item.useTime = 1;
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item22, player.position);
        return true;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased speed and hugely increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class DrillBuff6782 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.StardustDrill;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 325;
        item.useAnimation = 6;
        item.useTime = 1;
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item22, player.position);
        return true;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased speed and hugely increased damage") { OverrideColor = Color.DarkViolet });
    }
   
}
public class CandyCane696969 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.CandyCaneSword;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Stats buffed all around") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.scale = 1.15f;
        item.useTime = 14;
        item.damage = 21;
        item.useAnimation = 14;
    }
}
public class FuckAntlions : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.AntlionClaw;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Decreased damage and size, but massively increased swing speed") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.scale = 0.95f;
        item.useTime = 7;
        item.damage = 11;
        item.useAnimation = 7;
        item.knockBack = 0.75f;
    }
}
