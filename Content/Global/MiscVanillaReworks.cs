using HendecamMod.Common.Systems;
using HendecamMod.Content.Items;
using HendecamMod.Content.Poop;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Global;

// if you have to read through these unhinged ahh public classes and youre not Autism Manifesto, i apologize.
public class ShroomBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.ShroomiteDiggingClaw;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 65;
        item.useTime = 3;
        item.pick = 205;
        item.useAnimation = 6;
        item.knockBack = 1.75f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased damage, swing speed, and pick speed") { OverrideColor = Color.DarkViolet });
    }
}

public class PeeminScythe : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.DemonScythe;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now fires in a four round burst") { OverrideColor = Color.DarkViolet });


    }

    public override void SetDefaults(Item item)
    {
        item.damage = 40;
        item.shootSpeed = 2.5f;
        item.useTime = 13;
        item.mana = 19;
        item.useAnimation = 52;
        item.reuseDelay = 39;
    }
}

public class CockworkAssGun : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.ClockworkAssaultRifle;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Damage, Knockback, Velocity, and Sound Effects significantly buffed") { OverrideColor = Color.DarkViolet });

        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Time between shots decreased, time between bursts increased") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 54;
        item.shootSpeed = 15.95f;
        item.useTime = 3;
        item.knockBack = 3.33f;
        item.useAnimation = 9;
        item.reuseDelay = 36;
        item.UseSound = null;
    }
    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/HeavyRifle")
        {
            Volume = 1f,
            Pitch = 0.1f,
            MaxInstances = 100,
        });
        return true;
    }
}
public class PenisMagnum : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.VenusMagnum;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Fires slower, but deals significantly more damage") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 125;
        item.useTime = 13;

        item.useAnimation = 13;
    }
}

public class ShittyShitgun : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.TacticalShotgun;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Reduced damage, Doubled fire rate, incurs a damage penalty when using Chlorophyte Bullets") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 18;
        item.damage = 27;
        item.useAnimation = 18;
    }

    public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (type == ProjectileID.ChlorophyteBullet)
        {
            damage = (int)(damage * 0.75f);
        }
    }
}

public class RipRalphiesEye : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.RedRyder;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 17;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Slightly decreased damage, but shoots out an additional bloodshot with your bullet") { OverrideColor = Color.DarkViolet });
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectileDirect(source, player.Center, velocity * 21.25f, ProjectileID.BloodArrow, damage, knockback, player.whoAmI);

        return true;
    }
}

public class BakerBlade : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.BreakerBlade;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Massively increased damage and size, but slightly slower use speed") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.scale = 1.5f;
        item.useTime = 40;
        item.damage = 115;
        item.useAnimation = 40;
    }
}
public class BloodyHell : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.TendonBow;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 23;
        item.useTime = 24;
        item.useAnimation = 24;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Converts all arrows into high velocity blood shots") { OverrideColor = Color.DarkViolet });
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectileDirect(source, player.Center, velocity * 29.95f, ProjectileID.BloodArrow, damage, knockback, player.whoAmI);

        return false;
    }
}

public class DemonTime : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.DemonBow;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 16;
        item.useTime = 23;
        item.useAnimation = 23;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Converts all arrows into Unholy Arrows") { OverrideColor = Color.DarkViolet });
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectileDirect(source, player.Center, velocity * 3.1f, ProjectileID.UnholyArrow, damage, knockback, player.whoAmI);

        return false;
    }
}

public class CactusDildo : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.CactusSword;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 10;

        item.shoot = ProjectileType<CactusSpawn>();
        item.shootSpeed = 3.33f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Flings cactus spines with every swing") { OverrideColor = Color.DarkViolet });
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectileDirect(source, player.Center, velocity * 4f, ProjectileType<CactusSpawn>(), damage, knockback, player.whoAmI);

        return false;
    }
}
public class FuckAntlions : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
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
public class BEEEEEEZ : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.AntlionClaw;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased size and damage, reduced swing speed") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 43;
        item.scale = 1.75f;
        item.useTime = 25;
        item.useAnimation = 25;
    }
}
public class Coballs : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.CobaltSword;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod);
        item.DamageType = DamageClass.Melee;
        item.useTime = 25;
        item.shoot = ProjectileType<CobaltBolt>();
        item.shootSpeed = 5f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Shoots a bolt of Cobalt energy") { OverrideColor = Color.DarkViolet });
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica))
        {
            Projectile.NewProjectileDirect(source, player.Center, velocity * 5f, ProjectileType<CobaltBolt>(), (int)(damage * 0.6f), knockback, player.whoAmI);
        }
        else
        {
            Projectile.NewProjectileDirect(source, player.Center, velocity * 5f, ProjectileType<CobaltBolt>(), (int)(damage * 0.999f), knockback, player.whoAmI);
        }

        return false;
    }
}

public class Mlady : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.PalladiumSword;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod);
        item.DamageType = DamageClass.Melee;
        item.useTime = 28;
        item.shoot = ProjectileType<PalladiumBolt>();
        item.shootSpeed = 7.25f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Shoots a bolt of Palladium energy") { OverrideColor = Color.DarkViolet });
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica))
        {
            Projectile.NewProjectileDirect(source, player.Center, velocity * 5f, ProjectileType<PalladiumBolt>(), (int)(damage * 0.5f), knockback, player.whoAmI);
        }
        else
        {
            Projectile.NewProjectileDirect(source, player.Center, velocity * 5f, ProjectileType<PalladiumBolt>(), (int)(damage * 0.999f), knockback, player.whoAmI);
        }

        return false;
    }
}

public class Milady : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.MythrilSword;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod);
        item.DamageType = DamageClass.Melee;
        item.useTime = 26;
        item.shoot = ProjectileType<MythrilBolt>();
        item.shootSpeed = 9.5f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Shoots a bolt of Mythril energy") { OverrideColor = Color.DarkViolet });
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica))
        {
            Projectile.NewProjectileDirect(source, player.Center, velocity * 5f, ProjectileType<MythrilBolt>(), (int)(damage * 0.5f), knockback, player.whoAmI);
        }
        else
        {
            Projectile.NewProjectileDirect(source, player.Center, velocity * 5f, ProjectileType<MythrilBolt>(), (int)(damage * 0.999f), knockback, player.whoAmI);
        }

        return false;
    }
}

public class PinkPussy : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.OrichalcumSword;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod);
        item.DamageType = DamageClass.Melee;
        item.useTime = 28;
        item.shoot = ProjectileType<OrichalcumBolt>();
        item.shootSpeed = 12.25f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Shoots a bolt of Orichalcum energy") { OverrideColor = Color.DarkViolet });
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica))
        {
            Projectile.NewProjectileDirect(source, player.Center, velocity * 5f, ProjectileType<OrichalcumBolt>(), (int)(damage * 0.66f), knockback, player.whoAmI);
        }
        else
        {
            Projectile.NewProjectileDirect(source, player.Center, velocity * 5f, ProjectileType<OrichalcumBolt>(), (int)(damage * 1.001f), knockback, player.whoAmI);
        }

        return false;
    }
}

public class Adamantitties : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.AdamantiteSword;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod);
        item.DamageType = DamageClass.Melee;
        item.useTime = 27;
        item.shoot = ProjectileType<AdamantiteBolt>();
        item.shootSpeed = 4f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Shoots a bolt of Adamantite energy") { OverrideColor = Color.DarkViolet });
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica))
        {
            Projectile.NewProjectileDirect(source, player.Center, velocity * 5f, ProjectileType<AdamantiteBolt>(), (int)(damage * 0.9f), knockback, player.whoAmI);
        }
        else
        {
            Projectile.NewProjectileDirect(source, player.Center, velocity * 5f, ProjectileType<AdamantiteBolt>(), (int)(damage * 1.02f), knockback, player.whoAmI);
        }

        return false;
    }
}

public class Tittyanium : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.TitaniumSword;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod);
        item.DamageType = DamageClass.Melee;
        item.useTime = 26;
        item.shoot = ProjectileType<TitaniumBolt>();
        item.shootSpeed = 4.5f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Shoots a bolt of Titanium energy") { OverrideColor = Color.DarkViolet });
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica))
        {
            Projectile.NewProjectileDirect(source, player.Center, velocity * 5f, ProjectileType<TitaniumBolt>(), (int)(damage * 0.92f), knockback, player.whoAmI);
        }
        else
        {
            Projectile.NewProjectileDirect(source, player.Center, velocity * 5f, ProjectileType<TitaniumBolt>(), (int)(damage * 1.025f), knockback, player.whoAmI);
        }

        return false;
    }
}
public class CIAAwardInJournalism : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (!ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.SniperRifle;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {


        item.shoot = ProjectileType<CiaSpawn>();
        item.damage = 255;


    }
    public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {

        type = ProjectileType<CiaSpawn>();


    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {

        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Shoots an enhanced triplex round") { OverrideColor = Color.DarkViolet });


    }


}
public class MagnetSphereGoodNow : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {

        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.MagnetSphere;
        }
        else return false;

    }

    public override void SetDefaults(Item item)
    {


        item.shoot = ProjectileType<MagnetSpawn>();
        item.damage = 40;
        item.shootSpeed = 6.67f;

    }
    public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {

        type = ProjectileType<MagnetSpawn>();


    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {

        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now shoots eight Magnet Spheres in all directions") { OverrideColor = Color.DarkViolet });
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Auxillary magnets take a moment to power up") { OverrideColor = Color.DarkViolet });

    }


}
public class BoneSwordBuffChudfucker6969 : GlobalItem // review in 1.4.5
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.BoneSword;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod);
        item.useTime = 17;
        item.useAnimation = 17;

        item.shoot = ProjectileID.BoneGloveProj;
        item.shootSpeed = 4f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Flings bones with every swing") { OverrideColor = Color.DarkViolet });
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectileDirect(source, player.Center, velocity * 4.5f, ProjectileID.BoneGloveProj, (int)(damage * 0.666f), knockback, player.whoAmI);

        return false;
    }
}
public class TxkrpBff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.Toxikarp;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {

        item.useTime = 9;
        item.useAnimation = 9;


        item.shootSpeed = 12f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Faster attack speed and velocity") { OverrideColor = Color.DarkViolet });
    }


}
public class ToxiFast : GlobalProjectile
{


    public override bool InstancePerEntity => true;


    public override void AI(Projectile projectile)
    {
        if (projectile.type == ProjectileID.ToxicBubble && Math.Abs(projectile.velocity.X) <= 10.25f && Math.Abs(projectile.velocity.Y) <= 10.25f && GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            projectile.velocity *= 1.09f;
        }
    }


}
public class DrugsNir : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.Gungnir;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 77;
        item.useTime = 21;
        item.useAnimation = 21;

        item.shoot = ProjectileType<PiercingLight>();
        item.shootSpeed = 11.25f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Shoots a piercing light beam, has higher stats") { OverrideColor = Color.DarkViolet });
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectileDirect(source, player.Center, velocity * 4.5f, ProjectileType<PiercingLight>(), (int)(damage * 0.777f), knockback, player.whoAmI);
        Projectile.NewProjectileDirect(source, player.Center, velocity * 1f, ProjectileID.Gungnir, (int)(damage * 1.01f), knockback, player.whoAmI);
        return false;
    }
}
public class ChloroshitFartisan : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.ChlorophytePartisan;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 58;



    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased damage, shoots a lot more spore clouds") { OverrideColor = Color.DarkViolet });
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {

        Projectile.NewProjectileDirect(source, player.Center, velocity * 6f, ProjectileID.SporeCloud, (int)(damage * 0.45f), knockback, player.whoAmI);
        Projectile.NewProjectileDirect(source, player.Center, velocity * 5f, ProjectileID.SporeCloud, (int)(damage * 0.5f), knockback, player.whoAmI);
        Projectile.NewProjectileDirect(source, player.Center, velocity * 4f, ProjectileID.SporeCloud, (int)(damage * 0.55f), knockback, player.whoAmI);
        Projectile.NewProjectileDirect(source, player.Center, velocity * 3f, ProjectileID.SporeCloud, (int)(damage * 0.6f), knockback, player.whoAmI);
        Projectile.NewProjectileDirect(source, player.Center, velocity * 2f, ProjectileID.SporeCloud, (int)(damage * 0.67f), knockback, player.whoAmI);
        return true;
    }
}
public class ChloroshitSaber : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.ChlorophyteSaber;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 64;

        item.useTime = 17;
        item.useAnimation = 17;
        item.scale = 1.1f;

    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased damage, shoots a lot more spore clouds") { OverrideColor = Color.DarkViolet });
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(6f));
        Vector2 new2Velocity = velocity.RotatedBy(MathHelper.ToRadians(-6f));
        Vector2 new3Velocity = velocity.RotatedBy(MathHelper.ToRadians(12f));
        Vector2 new4Velocity = velocity.RotatedBy(MathHelper.ToRadians(-12f));

        Projectile.NewProjectileDirect(source, player.Center, newVelocity * 5f, ProjectileID.SporeCloud, (int)(damage * 0.667f), knockback, player.whoAmI);
        Projectile.NewProjectileDirect(source, player.Center, new2Velocity * 5f, ProjectileID.SporeCloud, (int)(damage * 0.667f), knockback, player.whoAmI);
        Projectile.NewProjectileDirect(source, player.Center, velocity * 5f, ProjectileID.SporeCloud, (int)(damage * 0.667f), knockback, player.whoAmI);
        Projectile.NewProjectileDirect(source, player.Center, new3Velocity * 5f, ProjectileID.SporeCloud, (int)(damage * 0.667f), knockback, player.whoAmI);
        Projectile.NewProjectileDirect(source, player.Center, new4Velocity * 5f, ProjectileID.SporeCloud, (int)(damage * 0.667f), knockback, player.whoAmI);

        return false;
    }
}
public class ChloroshitGaymore : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.ChlorophyteClaymore;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 106;

        item.useTime = 20;

        item.scale = 1.33f;

    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased damage, size and orb speed") { OverrideColor = Color.DarkViolet });
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {



        Projectile.NewProjectileDirect(source, player.Center, velocity * 1.9f, ProjectileID.ChlorophyteOrb, (int)(damage * 1f), knockback, player.whoAmI);


        return false;
    }
}

public class ChloroLessShitIframes : GlobalProjectile
{


    public override bool InstancePerEntity => true;
    public override void SetDefaults(Projectile projectile)
    {
        if (projectile.type == ProjectileID.SporeCloud && GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.usesOwnerMeleeHitCD = false;
            projectile.usesIDStaticNPCImmunity = false;

        }
    }





}
public class ThunderZappies : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.ThunderStaff;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 8;

        item.useTime = 5;
        item.useAnimation = 20;

        item.ArmorPenetration = 5;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Massively increased attack and projectile speed at the cost of damage") { OverrideColor = Color.DarkViolet });
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Also ignores 5 enemy defense") { OverrideColor = Color.DarkViolet });
    }


}
public class HeatRayBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.HeatRay;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 6;
        item.useAnimation = 6;
        item.mana = 7;
    }

    public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (Main.dayTime)
        {
            damage = (int)(damage * 1.15f);
        }
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Increased fire rate, decreased mana cost, also deals increased damage during daytime") { OverrideColor = Color.DarkViolet });
    }
}

public class HelFireCompNerf : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.HelFire;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 31;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Lower base damage, now explodes upon hitting enemies") { OverrideColor = Color.DarkViolet });
    }
}
public class HelFireBoom : GlobalProjectile
{


    public override bool InstancePerEntity => true;
    public override void SetDefaults(Projectile entity)
    {
        if (entity.type == ProjectileID.HelFire && GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            entity.usesLocalNPCImmunity = true;
            entity.localNPCHitCooldown = 10;
            entity.usesOwnerMeleeHitCD = false;
            entity.usesIDStaticNPCImmunity = false;
        }
    }
    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (projectile.type == ProjectileID.HelFire && GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            // target.immune[projectile.owner] = 5;
            Vector2 velocity2 = Vector2.Zero;
            Vector2 Peanits2 = projectile.Center;
            Projectile.NewProjectile(projectile.GetSource_FromThis(), Peanits2, velocity2,
            ProjectileType<BoomSmallish>(), (int)(projectile.damage * 1f), projectile.knockBack, projectile.owner);
        }
    }




}
public class Blowie : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.Blowgun;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Shoots out a flurry of darts or seeds") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {


        item.useTime = 8;
        item.useAnimation = 32;
        item.reuseDelay = 40;
    }
}
public class EEEEEEEE : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.FairyQueenRangedItem;// why isnt it just called the fucking eventide relogic
        }
        else return false;
    }

    public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        damage = (int)(damage * 0.65f);
        if (type == ProjectileID.FairyQueenRangedItemShot)
        {
            damage = (int)(damage * 1.83f);
        }
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Converts wooden arrows into a CONSTANT barrage of twilight lances") { OverrideColor = Color.DarkViolet });

        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Deals less damage with other arrow types") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.rare = ItemRarityID.Cyan;
        item.useTime = 3;
        item.useAnimation = 15;
        item.shootSpeed = 15.25f;

        item.damage = 33;
    }
}
public class Laserz420 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.LaserRifle;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Hits much harder, but fires slower") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 69;
        item.useTime = 17;
        item.useAnimation = 17;
    }
}
public class KandyKorn : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.CandyCornRifle;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 81;
        item.useTime = 12;
        item.useAnimation = 12;
        item.shootSpeed = 23.95f;
        item.useAmmo = AmmoID.Bullet;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Can use candy corn OR bullets as ammo, deals boosted damage with candy corn") { OverrideColor = Color.DarkViolet });
    }

    public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (type == ProjectileID.CandyCorn)
        {
            damage = (int)(damage * 1.667f);
        }
    }
}
public class KandyKorn2 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.CandyCorn;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod); // Notify the game that we've made a functional change to this item.

        item.damage = 24;
        item.ammo = AmmoID.Bullet;
        item.rare = ItemRarityID.Yellow;
        item.shootSpeed = 15.95f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Can be used as bullets, Deals extra damage when fired out of the Candy Corn Rifle ") { OverrideColor = Color.DarkViolet });
    }
}
public class Steak2 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.Stake;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod); // Notify the game that we've made a functional change to this item.

        item.ammo = AmmoID.Arrow;
        item.rare = ItemRarityID.Yellow;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Can be used as ammo in all bows as well as the Stake Launcher") { OverrideColor = Color.DarkViolet });
    }
}

public class Nailz : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.Nail;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 6;
        item.ammo = AmmoID.Dart;
        item.rare = ItemRarityID.Orange;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Sticks into enemies and explodes after a short time") { OverrideColor = Color.White });

        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Can be used as ammo in all dartguns as well as the Nail Gun") { OverrideColor = Color.DarkViolet });
    }
}

public class Pinkeeeee : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.PinkGel;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 2;
        item.DamageType = DamageClass.Ranged;
        item.ammo = AmmoID.Gel;
        item.rare = ItemRarityID.Blue;
        item.shoot = ProjectileType<GelShot>();
        item.shootSpeed = 1.33f;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Can be used as ammo in flamethrowers") { OverrideColor = Color.DarkViolet });
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Shoots bouncy gel shots instead of fire") { OverrideColor = Color.White });
    }
}
public class Steak : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.StakeLauncher;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod); // Notify the game that we've made a functional change to this item.
        item.damage = 120;
        item.useTime = 16;
        item.useAnimation = 16;
        item.shootSpeed = 15.95f;
        item.useAmmo = AmmoID.Arrow;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Can use stakes or arrows as ammo") { OverrideColor = Color.DarkViolet });
    }
}

public class Nailzzz : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.NailGun;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod); // Notify the game that we've made a functional change to this item.
        item.damage = 123;

        item.shootSpeed = 15.99f;
        item.useAmmo = AmmoID.Dart;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Can use nails or darts as ammo") { OverrideColor = Color.DarkViolet });
    }
}
public class Harpussy : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.Harpoon;
        }
        else return false;
    }

    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod); // Notify the game that we've made a functional change to this item.
        item.damage = 32;
        item.useTime = 40;
        item.useAnimation = 40;
        item.shootSpeed = 11.25f;
        item.useAmmo = AmmoID.Dart;
        item.UseSound = SoundID.Item99;
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectileDirect(source, player.Center, velocity * 3f, ProjectileType<HarpoonProj>(), (int)(damage * 1.05f), knockback, player.whoAmI);

        return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Converts darts into powerful harpoons that pierce multiple enemies") { OverrideColor = Color.DarkViolet });
    }
}
public class ChainCunt : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.ChainGun;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Decrased damage, but massively increased fire rate") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 22;
        item.useTime = 2;
        item.useAnimation = 4;
        item.reuseDelay = 1;
    }
}

public class Stiingeer : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.Stynger;
        }
        else return false;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Decrased damage, but massively increased fire rate") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.damage = 42;
        item.useTime = 14;
        item.useAnimation = 14;
    }
}
