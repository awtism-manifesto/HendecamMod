using HendecamMod.Content.Items;
using HendecamMod.Content.Poop;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Global;

// if you have to read through these unhinged ahh public classes and youre not Autism Manifesto, i apologize.




public class MetalAmogus : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.MetalDetector;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now able to be used as a melee weapon") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.scale = 1.025f;
        item.DamageType = DamageClass.Melee;
        item.useTime = 15;
        item.damage = 15;
        item.useAnimation = 15;
        item.knockBack = 1.75f;
        item.useStyle = ItemUseStyleID.Swing;
    }
}



public class LightningFast : GlobalProjectile
{


    public override bool InstancePerEntity => true;
    public override void SetDefaults(Projectile projectile)
    {
        if (projectile.type == ProjectileID.ThunderStaffShot)
        {
            projectile.extraUpdates += 1;

        }
    }





}



public class BigBoner : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemType<TheBoner>();
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Shoots out an additional bone with your rocket") { OverrideColor = Color.White });
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectileDirect(source, player.Center, velocity * 17.5f, ProjectileID.BoneGloveProj, damage, knockback, player.whoAmI);

        return true;
    }
}

public class ImImpulsiveLol : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemType<ImpulseBow>();
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item5, player.position);
        Projectile.NewProjectileDirect(source, player.Center, velocity * 17.5f, ProjectileType<AstatineBullet>(), (int)(damage * 0.75f), knockback, player.whoAmI);

        return true;
    }
}

public class Anal12 : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemType<AA12>();
    }

    public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (type == ProjectileID.ChlorophyteBullet || type == ProjectileType<ChloroShit>())
        {
            damage = (int)(damage * 0.5f);
        }
    }
}



public class Icey : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.IceBlade;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 38;
    }
}


public class Evil : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.HolyArrow;
    }

   

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Deals 777x damage to Red Devils") { OverrideColor = Color.DarkViolet });
    }
}

public class Enchantedy : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.EnchantedSword;
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 40;
    }
}








public class Wyrmyyyy : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.Worm;
    }

    public override void SetDefaults(Item item)
    {
        item.value = 0;
    }
}

public class Demoney : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.DemonHeart;
    }

    public override void SetDefaults(Item item)
    {
        item.value = 6750000;
    }
}




public class NightmareSigma : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.NightmarePickaxe;
    }

    public override void SetDefaults(Item item)
    {
        item.pick = 70;
    }
}

public class DeathbringerSigma : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.DeathbringerPickaxe;
    }

    public override void SetDefaults(Item item)
    {
        item.pick = 70;
    }
}








public class YouAreAwful : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.EmpressButterfly;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now able to be used as bait, if you're a terrible enough person.") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.bait = 115;
    }
}



public class MagicFagic : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.MagicMirror;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Faster use animation") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 15;
        item.useAnimation = 15;
    }
}

public class Cock : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.MagicConch;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Faster use animation") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 15;
        item.useAnimation = 15;
    }
}

public class DemonCock : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.DemonConch;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Faster use animation") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 15;
        item.useAnimation = 15;
    }
}



public class SnowgraveMirror : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.IceMirror;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Faster use animation") { OverrideColor = Color.DarkViolet });
    }

    public override void SetDefaults(Item item)
    {
        item.useTime = 15;
        item.useAnimation = 15;
    }
}








