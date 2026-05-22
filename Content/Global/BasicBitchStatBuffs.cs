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

public class DarkCock : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.DarkLance;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased range, speed and damage") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 41;
        item.useTime = 17;
        item.useAnimation = 17;

        item.shootSpeed = 8.75f;
    }
}

public class AssGlaive : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.MonkStaffT2;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Significantly increased range") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 63;

        item.useTime = 25;
        item.useAnimation = 25;

        item.shootSpeed = 87.5f;
    }
}

public class SkullEmoji : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.BookofSkulls;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Hugely increased projectile velocity, lowered mana cost") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 27;
        item.useAnimation = 27;
        item.mana = 13;
        item.shootSpeed = 11.25f;
    }
}
public class FrostEEEEEE : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.BookofSkulls;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now shoots icy bolts every swing at a much higher velocity") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 49;

        item.useTime = 19;
        item.useAnimation = 19;

        item.shootSpeed = 19.5f;
    }
}
public class OnyxCock : GlobalItem
{

    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.OnyxBlaster;
        }
        else return false;
    }
    public override void SetDefaults(Item item)
    {
        item.damage = 25;
        item.useTime = 46;
        item.useAnimation = 46;

        item.shootSpeed = 9.15f;
    }
}
public class HorsemansBladeBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.TheHorsemansBlade;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 19;
        item.useAnimation = 19;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Significantly increased swing speed") { OverrideColor = Color.DarkViolet });
    }
}
public class FlyingKnifeBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.FlyingKnife;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 119 / 2;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Damage increased by 35%") { OverrideColor = Color.DarkViolet });
    }
}
public class JackingOffRN : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.JackOLanternLauncher;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.shootSpeed = 15.95f;
        item.useTime = 18;
        item.useAnimation = 18;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Massively increased velocity and fire rate") { OverrideColor = Color.DarkViolet });
    }
}
public class FrostyCock : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.WandofFrosting;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 18;
        item.useTime = 21;
        item.useAnimation = 21;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased speed and damage") { OverrideColor = Color.DarkViolet });
    }
}

public class WoodUwU : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.PearlwoodSword;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 45;
        item.useTime = 10;
        item.useAnimation = 10;
        item.scale = 2f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: 1.5x damage, 2x size, and 3x speed ") { OverrideColor = Color.DarkViolet });
    }
}
public class Peanix : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.PhoenixBlaster;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 31;
        item.useTime = 13;
        item.useAnimation = 13;
    }
}


public class NettleAss : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.NettleBurst;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 70;
        item.ArmorPenetration = 20;
        item.shootSpeed = 36f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Damage and armor penetration doubled") { OverrideColor = Color.DarkViolet });
    }
}

public class CutBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.NettleBurst;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased size and speed") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {

        item.scale = 1.25f;
        item.useTime = 11;
        item.useAnimation = 11;
    }
}
public class Flameeeer : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.Flamethrower;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 38;

        item.shootSpeed = 15.1f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Drastically increased range") { OverrideColor = Color.DarkViolet });
    }
}

public class ElfFlameeeer : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.ElfMelter;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 58;

        item.shootSpeed = 18.05f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Drastically increased range") { OverrideColor = Color.DarkViolet });
    }
}
public class Bubbly : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.BubbleGun;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.shootSpeed = 26.5f;
    }
}
public class Gemz1 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.SapphireStaff;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 29;
        item.useAnimation = 29;
    }
}

public class Gemz2 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.EmeraldStaff;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 27;
        item.useAnimation = 27;
    }
}

public class Gemz3 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.RubyStaff;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 23;
        item.useAnimation = 23;
    }
}

public class Gemz4 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.DiamondStaff;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 21;
        item.useAnimation = 21;
    }
}
public class Bonerhahafunnyimlosingmymind : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.BoneArrow;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased damage and velocity") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 10;

        item.shootSpeed = 6.33f;
    }
}
public class Moone : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.BlueMoon;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod); // Notify the game that we've made a functional change to this item.
        item.ArmorPenetration = 25;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now ignores 25 enemy defense") { OverrideColor = Color.DarkViolet });
    }
}

public class NightsEdgeBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.NightsEdge;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: All stats significantly buffed") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 54;
        item.scale = 1.1f;
        item.ArmorPenetration = 15;
        item.useTime = 19;
        item.useAnimation = 19;
    }
}

public class ExcaliburBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.Excalibur;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: All stats significantly buffed") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 100;
        item.scale = 1.1f;

        item.useTime = 17;
        item.useAnimation = 17;
    }
}

public class TrueExcaliburBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.TrueExcalibur;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: All stats significantly buffed") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 102;
        item.scale = 1.15f;

        item.useTime = 15;
        item.useAnimation = 15;
    }
}
public class TrueNightsBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.TrueExcalibur;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: All stats significantly buffed") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 95;
        item.scale = 1.25f;

        item.useTime = 27;
        item.useAnimation = 27;
    }
}

public class TerraBladeBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.TerraBlade;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 111;
        item.scale = 1.11f;
    }
}
public class FalconPUNCH : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.FalconBlade;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Much faster swing speed") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 9;
        item.useAnimation = 9;
    }
}

public class StripperPole : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.NorthPole;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Damage significantly buffed") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 125;
        item.useTime = 24;
        item.useAnimation = 24;
        item.shootSpeed = 8.67f;
    }
}

public class Cummies : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.Umbrella;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now guaranteed to crit") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.crit = 96;
    }
}

public class CummiesYummiesUWU : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.TragicUmbrella;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Guaranteed to crit, massively increased damage") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod);

        item.damage = 48;
        item.crit = 96;
    }
}

public class Icee : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.IceBow;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 51;
    }
}
public class ShadowTheEdgehog : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.ShadowFlameBow;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 69;
    }
}

public class Pee : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.Revolver;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 25;
    }
}


public class MeowMeowUwU : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.PiercingStarlight;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.shootSpeed = 30.25f;
    }
}

public class Darty : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.DartRifle;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 57;
        item.useTime = 35;
        item.useAnimation = 35;
    }
}

public class Peenitz : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.PossessedHatchet;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 111;
        item.useTime = 11;
        item.useAnimation = 11;
    }
}

public class Darty2 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.DartPistol;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 31;
        item.useTime = 17;
        item.useAnimation = 17;
    }
}
public class MegasharkBuff69 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.Megashark;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased damage and velocity") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 33;
        item.knockBack = 3f;
        item.shootSpeed = 12.25f;
    }
}

public class CalamityOverhaulSucksFuckingAss : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().VanillaWeaponStatBuffs == true)
        {
            return item.type == ItemID.Muramasa;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: All stats significantly buffed") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 29;
        item.useTime = 14;
        item.useAnimation = 14;
        item.scale = 1.15f;
        item.ArmorPenetration = 5;
    }
}

