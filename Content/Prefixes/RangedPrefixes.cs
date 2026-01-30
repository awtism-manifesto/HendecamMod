using HendecamMod.Content.DamageClasses;

namespace HendecamMod.Content.Prefixes;

public class Disciplined : ModPrefix
{
    // We declare a custom *virtual* property here, so that another type, ExampleDerivedPrefix, could override it and change the effective power for itself.
    public virtual float Power => 1f;

    // Change your category this way, defaults to PrefixCategory.Custom. Affects which items can get this prefix.
    public override PrefixCategory Category => PrefixCategory.AnyWeapon;

    // See documentation for vanilla weights and more information.
    // In case of multiple prefixes with similar functions this can be used with a switch/case to provide different chances for different prefixes
    // Note: a weight of 0f might still be rolled. See CanRoll to exclude prefixes.
    // Note: if you use PrefixCategory.Custom, actually use ModItem.ChoosePrefix instead.
    public override float RollChance(Item item)
    {
        return 2.75f;
    }

    // Determines if it can roll at all.
    // Use this to control if a prefix can be rolled or not.
    public override bool CanRoll(Item item)
    {
        // apply to all ranged classes
        if (item.DamageType == DamageClass.Ranged || item.DamageType == ModContent.GetInstance<MeleeRangedDamage>()
                                                  || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<RangedMagicDamage>() || item.DamageType == ModContent.GetInstance<RangedSummonDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        {
            return true;
        }

        return false;
    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 1f + 0.175f * Power;
        critBonus = 20;
        useTimeMult *= 1f + 0.125f * Power;
        shootSpeedMult *= 1f + 0.15f * Power;
    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 0f + 0.925f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {
    }
}

public class Switched : ModPrefix
{
    // We declare a custom *virtual* property here, so that another type, ExampleDerivedPrefix, could override it and change the effective power for itself.
    public virtual float Power => 1f;

    // Change your category this way, defaults to PrefixCategory.Custom. Affects which items can get this prefix.
    public override PrefixCategory Category => PrefixCategory.AnyWeapon;

    // See documentation for vanilla weights and more information.
    // In case of multiple prefixes with similar functions this can be used with a switch/case to provide different chances for different prefixes
    // Note: a weight of 0f might still be rolled. See CanRoll to exclude prefixes.
    // Note: if you use PrefixCategory.Custom, actually use ModItem.ChoosePrefix instead.
    public override float RollChance(Item item)
    {
        return 2.66f;
    }

    // Determines if it can roll at all.
    // Use this to control if a prefix can be rolled or not.
    public override bool CanRoll(Item item)
    {
        // apply to all ranged classes
        if (item.DamageType == DamageClass.Ranged || item.DamageType == ModContent.GetInstance<MeleeRangedDamage>()
                                                  || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<RangedMagicDamage>() || item.DamageType == ModContent.GetInstance<RangedSummonDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        {
            return true;
        }

        return false;
    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 1f - 0.125f * Power;
        critBonus = -5;
        useTimeMult *= 1f - 0.3f * Power;

        shootSpeedMult *= 1f - 0.15f * Power;
    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1f + 0.11f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {
    }
}