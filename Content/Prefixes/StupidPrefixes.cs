using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using HendecamMod.Content.DamageClasses;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HendecamMod.Content.Prefixes;


public class Dumb : ModPrefix
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
        return 4.5f;
    }

    // Determines if it can roll at all.
    // Use this to control if a prefix can be rolled or not.
    public override bool CanRoll(Item item)
    {
        // 
        if (item.DamageType == ModContent.GetInstance<StupidDamage>() || item.DamageType == ModContent.GetInstance<MeleeStupidDamage>()
            || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<AutismDamage>() || item.DamageType == ModContent.GetInstance<SummonStupidDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        { return true; }
        else
        { return false; }

    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 1f + 0.075f * Power;

    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 0f + 0.945f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {

    }
}
public class Lobotomized : ModPrefix
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
        return 1.5f;
    }

    // Determines if it can roll at all.
    // Use this to control if a prefix can be rolled or not.
    public override bool CanRoll(Item item)
    {
        // 
        if (item.DamageType == ModContent.GetInstance<StupidDamage>() || item.DamageType == ModContent.GetInstance<MeleeStupidDamage>()
            || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<AutismDamage>() || item.DamageType == ModContent.GetInstance<SummonStupidDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        { return true; }
        else
        { return false; }

    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 1f + 0.185f * Power;
        critBonus = 5;
        useTimeMult *= 1f - 0.135f * Power;
        knockbackMult *= 1f + 0.25f * Power;

    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1f + 1.825f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {

    }
}
public class Intelligent : ModPrefix
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
        return 1.99f;
    }

    // Determines if it can roll at all.
    // Use this to control if a prefix can be rolled or not.
    public override bool CanRoll(Item item)
    {
        // 
        if (item.DamageType == ModContent.GetInstance<StupidDamage>() || item.DamageType == ModContent.GetInstance<MeleeStupidDamage>()
            || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<AutismDamage>() || item.DamageType == ModContent.GetInstance<SummonStupidDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        { return true; }
        else
        { return false; }

    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 1f - 0.18f * Power;
        critBonus = -6;

        knockbackMult *= 1f - 0.36f * Power;

    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 0f + 0.2f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {

    }
}
public class Brainy : ModPrefix
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
        return 3.15f;
    }

    // Determines if it can roll at all.
    // Use this to control if a prefix can be rolled or not.
    public override bool CanRoll(Item item)
    {
        // 
        if (item.DamageType == ModContent.GetInstance<StupidDamage>() || item.DamageType == ModContent.GetInstance<MeleeStupidDamage>()
            || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<AutismDamage>() || item.DamageType == ModContent.GetInstance<SummonStupidDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        { return true; }
        else
        { return false; }

    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 1f - 0.1f * Power;

        useTimeMult *= 1f + 0.075f * Power;
        knockbackMult *= 1f - 0.15f * Power;

    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 0f + 0.5f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {

    }
}
public class Serious : ModPrefix
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
        return 2.95f;
    }

    // Determines if it can roll at all.
    // Use this to control if a prefix can be rolled or not.
    public override bool CanRoll(Item item)
    {
        // 
        if (item.DamageType == ModContent.GetInstance<StupidDamage>() || item.DamageType == ModContent.GetInstance<MeleeStupidDamage>()
            || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<AutismDamage>() || item.DamageType == ModContent.GetInstance<SummonStupidDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        { return true; }
        else
        { return false; }

    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 1f + 0.075f * Power;

        useTimeMult *= 1f + 0.1f * Power;
        knockbackMult *= 1f - 0.15f * Power;

    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 0f + 0.65f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {

    }
}
public class Academic : ModPrefix
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
        return 3.25f;
    }

    // Determines if it can roll at all.
    // Use this to control if a prefix can be rolled or not.
    public override bool CanRoll(Item item)
    {
        // 
        if (item.DamageType == ModContent.GetInstance<StupidDamage>() || item.DamageType == ModContent.GetInstance<MeleeStupidDamage>()
            || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<AutismDamage>() || item.DamageType == ModContent.GetInstance<SummonStupidDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        { return true; }
        else
        { return false; }

    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 1f - 0.05f * Power;

        useTimeMult *= 1f + 0.15f * Power;
        knockbackMult *= 1f + 0.10f * Power;

    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 0f + 0.45f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {

    }
}
public class Smart : ModPrefix
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
        return 3.85f;
    }

    // Determines if it can roll at all.
    // Use this to control if a prefix can be rolled or not.
    public override bool CanRoll(Item item)
    {
        // 
        if (item.DamageType == ModContent.GetInstance<StupidDamage>() || item.DamageType == ModContent.GetInstance<MeleeStupidDamage>()
            || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<AutismDamage>() || item.DamageType == ModContent.GetInstance<SummonStupidDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        { return true; }
        else
        { return false; }

    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 1f - 0.07f * Power;
        critBonus = -1;
        useTimeMult *= 1f + 0.05f * Power;


    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 0f + 0.69f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {

    }
}
public class Skibidi : ModPrefix
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
        return 3.33f;
    }

    // Determines if it can roll at all.
    // Use this to control if a prefix can be rolled or not.
    public override bool CanRoll(Item item)
    {
        // 
        if (item.DamageType == ModContent.GetInstance<StupidDamage>() || item.DamageType == ModContent.GetInstance<MeleeStupidDamage>()
            || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<AutismDamage>() || item.DamageType == ModContent.GetInstance<SummonStupidDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        { return true; }
        else
        { return false; }

    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 1f + 0.05f * Power;
        critBonus = 3;
        useTimeMult *= 1f - 0.09f * Power;


    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 0f + 0.815f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {

    }
}
public class Brainrotted : ModPrefix
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
        return 1.95f;
    }

    // Determines if it can roll at all.
    // Use this to control if a prefix can be rolled or not.
    public override bool CanRoll(Item item)
    {
        // 
        if (item.DamageType == ModContent.GetInstance<StupidDamage>() || item.DamageType == ModContent.GetInstance<MeleeStupidDamage>()
            || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<AutismDamage>() || item.DamageType == ModContent.GetInstance<SummonStupidDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        { return true; }
        else
        { return false; }

    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 1f + 0.125f * Power;
        critBonus = 3;
        useTimeMult *= 1f - 0.085f * Power;
        knockbackMult *= 1f + 0.175f * Power;

    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1f + 1.1f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {

    }
}
public class Idiotic : ModPrefix
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
        return 2.95f;
    }

    // Determines if it can roll at all.
    // Use this to control if a prefix can be rolled or not.
    public override bool CanRoll(Item item)
    {
        // 
        if (item.DamageType == ModContent.GetInstance<StupidDamage>() || item.DamageType == ModContent.GetInstance<MeleeStupidDamage>()
            || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<AutismDamage>() || item.DamageType == ModContent.GetInstance<SummonStupidDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        { return true; }
        else
        { return false; }

    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 1f + 0.09f * Power;
        critBonus = 10;
        useTimeMult *= 1f - 0.05f * Power;
        knockbackMult *= 1f + 0.05f * Power;

    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1f + 1.01f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {

    }
}
public class Clunky : ModPrefix
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
        return 2.5f;
    }

    // Determines if it can roll at all.
    // Use this to control if a prefix can be rolled or not.
    public override bool CanRoll(Item item)
    {
        // 
        if (item.DamageType == ModContent.GetInstance<StupidDamage>() || item.DamageType == ModContent.GetInstance<MeleeStupidDamage>()
            || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<AutismDamage>() || item.DamageType == ModContent.GetInstance<SummonStupidDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        { return true; }
        else
        { return false; }

    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 1f + 0.22f * Power;
        critBonus = 2;
        useTimeMult *= 1f + 0.15f * Power;
        knockbackMult *= 1f + 0.05f * Power;

    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1f + 0.05f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {

    }
}
public class Thoughtless : ModPrefix
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
        // 
        if (item.DamageType == ModContent.GetInstance<StupidDamage>() || item.DamageType == ModContent.GetInstance<MeleeStupidDamage>()
            || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<AutismDamage>() || item.DamageType == ModContent.GetInstance<SummonStupidDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        { return true; }
        else
        { return false; }

    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 1f - 0.10f * Power;
        critBonus = 15;
        useTimeMult *= 1f - 0.075f * Power;
        knockbackMult *= 1f - 0.25f * Power;

    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1f + 0.05f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {

    }
}
public class Tiktokified : ModPrefix
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
        return 3.05f;
    }

    // Determines if it can roll at all.
    // Use this to control if a prefix can be rolled or not.
    public override bool CanRoll(Item item)
    {
        // 
        if (item.DamageType == ModContent.GetInstance<StupidDamage>() || item.DamageType == ModContent.GetInstance<MeleeStupidDamage>()
            || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<AutismDamage>() || item.DamageType == ModContent.GetInstance<SummonStupidDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        { return true; }
        else
        { return false; }

    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 1f - 0.125f * Power;

        useTimeMult *= 1f - 0.225f * Power;


    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 0f + 0.88f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {

    }
}
public class Sigma : ModPrefix
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
        return 2.25f;
    }

    // Determines if it can roll at all.
    // Use this to control if a prefix can be rolled or not.
    public override bool CanRoll(Item item)
    {
        // 
        if (item.DamageType == ModContent.GetInstance<StupidDamage>() || item.DamageType == ModContent.GetInstance<MeleeStupidDamage>()
            || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<AutismDamage>() || item.DamageType == ModContent.GetInstance<SummonStupidDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        { return true; }
        else
        { return false; }

    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 1f + 0.20f * Power;
        critBonus = -25;
        useTimeMult *= 1f - 0.20f * Power;


    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1f + 0.55f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {

    }
}
public class Silly : ModPrefix
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
        return 4.33f;
    }

    // Determines if it can roll at all.
    // Use this to control if a prefix can be rolled or not.
    public override bool CanRoll(Item item)
    {
        // 
        if (item.DamageType == ModContent.GetInstance<StupidDamage>() || item.DamageType == ModContent.GetInstance<MeleeStupidDamage>()
            || item.DamageType == ModContent.GetInstance<RangedStupidDamage>() || item.DamageType == ModContent.GetInstance<AutismDamage>() || item.DamageType == ModContent.GetInstance<SummonStupidDamage>() || item.DamageType == ModContent.GetInstance<OmniDamage>())
        { return true; }
        else
        { return false; }

    }

    // Use this function to modify these stats for items which have this prefix:
    // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {

        useTimeMult *= 1f - 0.067f * Power;
        knockbackMult *= 1f + 0.33f * Power;

    }

    // Modify the cost of items with this modifier with this function.
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 0f + 0.82f * Power;
    }

    // This is used to modify most other stats of items which have this modifier.
    public override void Apply(Item item)
    {

    }
}