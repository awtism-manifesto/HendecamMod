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

public class SpiritFlameBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.SpiritFlame;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MagicSummonDamage>();

        item.useTime = 17;
        item.useAnimation = 17;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals magic AND summon damage, increased fire rate") { OverrideColor = Color.DarkViolet });
    }
}

public class Banana : GlobalItem
{
    public override bool InstancePerEntity => true;
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.Bananarang;
        }
        else return false;
    }

    // Rest of the class remains the same...
    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod); // Notify the game that we've made a functional change to this item.
        item.DamageType = ModContent.GetInstance<StupidDamage>();
        item.damage = 61;
    }
    public float LobotometerCost = 3f;
    public override bool? UseItem(Item item, Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(item, player);
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals Stupid damage, uses 3 Lobotometer") { OverrideColor = Color.DarkViolet });
    }
}

public class RulerStupid : GlobalItem
{
    public override bool InstancePerEntity => true;
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.Ruler;
        }
        else return false;
    }

    public float LobotometerCost = 1f;
    public override bool? UseItem(Item item, Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(item, player);
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals Melee AND Stupid damage, uses 1 Lobotometer") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
        item.damage = 30;
    }
}

public class PewStupid : GlobalItem
{
    public override bool InstancePerEntity => true;
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.PewMaticHorn;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals Ranged AND Stupid damage, uses 2 Lobotometer") { OverrideColor = Color.DarkViolet });
    }
    public float LobotometerCost = 2f;
    public override bool? UseItem(Item item, Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(item, player);
    }
    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<RangedStupidDamage>();
        item.useTime = 12;
        item.useAnimation = 12;
        item.shootSpeed = 16.5f;
    }
}

public class SandStupid : GlobalItem
{
    public override bool InstancePerEntity => true;
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.Sandgun;
        }
        else return false;
    }

    public float LobotometerCost = 3f;
    public override bool? UseItem(Item item, Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(item, player);
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals Ranged AND Stupid damage, uses 3 Lobotometer") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<RangedStupidDamage>();
    }
}

public class YouShouldDriveDrunk : GlobalItem
{
    public override bool InstancePerEntity => true;
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.AleThrowingGlove;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: All stats massively buffed, no longer requires ammo, and now deals Stupid damage, costs 6 Lobotometer") { OverrideColor = Color.DarkViolet });
    }
    public float LobotometerCost = 6f;
    public override bool? UseItem(Item item, Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(item, player);
    }
    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<StupidDamage>();
        item.rare = ItemRarityID.Green;
        item.damage = 54;
        item.useTime = 28;
        item.shootSpeed = 15.75f;
        item.useAnimation = 28;
        item.useStyle = ItemUseStyleID.Swing;
        item.useAmmo = AmmoID.None;
    }
}

public class HamBattt : GlobalItem
{
    public override bool InstancePerEntity => true;
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.HamBat;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: All stats buffed, now deals Melee AND Stupid damage, uses 3 Lobotometer") { OverrideColor = Color.DarkViolet });
    }
    public float LobotometerCost = 3f;
    public override bool? UseItem(Item item, Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(item, player);
    }
    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();

        item.damage = 69;
        item.useTime = 16;

        item.useAnimation = 16;
        item.scale = 1.66f;
    }
}

public class Pirahna : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.PiranhaGun;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<RangedSummonDamage>();

        item.ArmorPenetration = 10;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals Ranged AND summon damage + ignores 10 enemy defense") { OverrideColor = Color.DarkViolet });
    }
}

public class Scourge : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.ScourgeoftheCorruptor;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MeleeRangedDamage>();
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals melee AND ranged damage") { OverrideColor = Color.DarkViolet });
    }
}

public class Vamp : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.VampireKnives;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MeleeMagicDamage>();
        item.damage = 33;
        item.mana = 7;
        item.useTime = 14;
        item.useAnimation = 14;
        item.shootSpeed = 27.75f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals melee AND magic damage, stats adjusted") { OverrideColor = Color.DarkViolet });
    }
}

public class IGobbleAss : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.SkyFracture;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MeleeMagicDamage>();

        item.mana = 14;
        item.reuseDelay = 16;
        item.noMelee = false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals magic AND melee damage, stats adjusted") { OverrideColor = Color.DarkViolet });
    }
}

public class IGobbleAssUwU : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.UnholyTrident;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MeleeMagicDamage>();
        item.damage = 96;
        item.mana = 16;
        item.shootSpeed = 21.5f;
        item.useTime = 14;
        item.useAnimation = 14;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals magic AND melee damage, stats adjusted") { OverrideColor = Color.DarkViolet });
    }
}

public class Terraprimsma : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.EmpressBlade; //Terraprisma
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MeleeSummonDamage>();
        item.noMelee = false;
        item.scale = 1.33f;
        item.mana = 3;
        item.useTime = 14;
        item.useAnimation = 14;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals summon AND melee damage") { OverrideColor = Color.DarkViolet });
    }
}

public class Frosty : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.StaffoftheFrostHydra;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MagicSummonDamage>();

        item.damage = 198;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals summon AND magic damage, massively increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class IAmSOFuckingGay : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.RainbowGun;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MagicSummonDamage>();

        item.damage = 69;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals magic AND summon damage, increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class IAmSOFuckingLesbian : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.RainbowRod;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MagicSummonDamage>();

        item.damage = 59;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals magic AND summon damage, increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class IAmSOFuckingGenderfluid : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.MagicMissile;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MagicSummonDamage>();

        item.damage = 41;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals magic AND summon damage, increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class IAmSOFuckingGenderfluider : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.Flamelash;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MagicSummonDamage>();

        item.damage = 36;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals magic AND summon damage, increased damage") { OverrideColor = Color.DarkViolet });
    }
}

public class DesertTiger : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.StormTigerStaff;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MeleeSummonDamage>();
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals summon AND melee damage") { OverrideColor = Color.DarkViolet });
    }
}

public class WaffleTime : GlobalItem
{
    public override bool InstancePerEntity => true;
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.WaffleIron;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals Melee AND Stupid damage, uses 2 Lobotometer, and has buffed stats") { OverrideColor = Color.DarkViolet });
    }
    public float LobotometerCost = 2f;
    public override bool? UseItem(Item item, Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(item, player);
    }
    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
        item.rare = ItemRarityID.LightPurple;
        item.damage = 95;
        item.useTime = 17;
        item.useAnimation = 17;
        item.scale = 1.35f;
        item.shootSpeed = 25.75f;
    }
}

public class Peenitzes : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            if (!ModLoader.TryGetMod("FargowiltasSouls", out Mod FargoMerica))
            {
                return item.type == ItemID.Starfury;
            }
        }
        return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now does melee AND magic damage, stats adjusted") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MeleeMagicDamage>();
        item.mana = 5;
        item.useTime = 23;
        item.useAnimation = 23;
    }
}

public class UwUorOwOorSomethingidk : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.StarWrath;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now does melee AND magic damage, stats adjusted") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MeleeMagicDamage>();
        item.mana = 8;
        item.damage = 185;
        item.useTime = 14;
        item.useAnimation = 14;
        item.shootSpeed = 13.75f;
    }
}

public class BloodyFuckingHellMate : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.BloodRainBow;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now does ranged AND magic damage, stats adjusted") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<RangedMagicDamage>();
        item.mana = 8;
        item.damage = 23;
        item.useTime = 18;
        item.useAnimation = 36;
        item.shootSpeed = 18f;
        item.consumeAmmoOnLastShotOnly = true;
    }
}

public class NimbusBuff69 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.NimbusRod;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals magic AND summon damage") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MagicSummonDamage>();
    }
}

public class ClingerBuff69 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.ClingerStaff;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals magic AND summon damage") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MagicSummonDamage>();
    }
}

public class ZenithIsntStrongEnoughLmao : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.Zenith;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals Omni Damage, a combination of ALL damage classes") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<OmniDamage>();
    }
}

public class PulseMage : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.PulseBow;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals Ranged AND Magic damage") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<RangedMagicDamage>();
        item.mana = 5;
        item.damage = 90;
    }
}

public class CrimsonBuff69 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (ModContent.GetInstance<HendecamConfig>().VanillaWeaponClassChanges == true)
        {
            return item.type == ItemID.CrimsonRod;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now deals magic AND summon damage") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.DamageType = ModContent.GetInstance<MagicSummonDamage>();
    }
}