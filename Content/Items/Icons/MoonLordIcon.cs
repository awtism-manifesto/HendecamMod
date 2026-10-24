using System.Collections.Generic;
using HendecamMod.Content.Rarities;

namespace HendecamMod.Content.Items.Icons;

public class MoonLordIcon : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 3;
    }

    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.rare = RarityType<DarkGreen>();
        Item.value = 750000;
        Item.maxStack = 9999;
    }
}