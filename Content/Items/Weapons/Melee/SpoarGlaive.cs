using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using HendecamMod.Content.Projectiles.Items.QuadswordProjectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Melee;

public class SpoarGlaive : ModItem
{
    // Here is an example of blacklisting certain modifiers. Remove this section for standard vanilla behavior.
    // In this example, we are blacklisting the ones that reduce damage of a melee weapon.
    // Make sure that your item can even receive these prefixes (check the vanilla wiki on prefixes).
    private static readonly int[] unwantedPrefixes = new[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Shameful, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 24;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.UseSound = SoundID.Item1;
        Item.damage = 52;
        Item.DamageType = DamageClass.Melee;
        Item.knockBack = 9.5f;
      
        Item.rare = ItemRarityID.Lime;
        Item.value = 460000;

        Item.shoot = ProjectileType<SpoarGlaiveProj>();
        Item.shootSpeed = 14.33f;
    }
   
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Homes in on enemies for a bit before returning");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Prioritizes homing in on new enemies")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'It stands for Spore Perception Optimized Advanced Ricochet'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override bool AllowPrefix(int pre)
    {
        // return false to make the game reroll the prefix.

        // DON'T DO THIS BY ITSELF:
        // return false;
        // This will get the game stuck because it will try to reroll every time. Instead, make it have a chance to return true.

        if (Array.IndexOf(unwantedPrefixes, pre) > -1)
        {
            // IndexOf returns a positive index of the element you search for. If not found, it's less than 0.
            // Here we check if the selected prefix is positive (it was found).
            // If so, we found a prefix that we don't want. Reroll.
            return false;
        }

        // Don't reroll
        return true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<LycopiteGlaive>();
        recipe.AddIngredient(ItemID.ShroomiteBar, 12);
       
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();

    }
}