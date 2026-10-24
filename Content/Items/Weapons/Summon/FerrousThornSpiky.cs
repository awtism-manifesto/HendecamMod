using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles;
using Terraria.GameContent.ItemDropRules;

namespace HendecamMod.Content.Items.Weapons.Summon;

public class FerrousThornSpiky : ModItem
{
    public override void SetDefaults()
    {
        // This method quickly sets the whip's properties.
        // Mouse over to see its parameters.
        Item.DefaultToWhip(ProjectileType<FerroWhipSpiky>(), 33, 9, 5f, 33);
        Item.rare = ItemRarityID.LightRed;
        Item.damage = 72;
        Item.useTime = 33;
        Item.useAnimation = 33;
        Item.knockBack = 7f;
        Item.ArmorPenetration = 15;
        Item.width = 18;
        Item.height = 18;
        Item.value = 390000;
    }

    public override bool CanRightClick()
    {
        return true;
    }

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<FerrousThornSmooth>()));
    }

    public override void AddRecipes()
    {
        Recipe
            recipe = CreateRecipe();
        recipe.AddRecipeGroup("IronBar", 15);
        recipe.AddIngredient<CrudeOil>(35);
        recipe.AddIngredient<RefinedOil>(35);
        recipe.AddTile(TileID.MythrilAnvil);

        recipe.Register();
    }

    // Makes the whip receive melee prefixes
    public override bool MeleePrefix()
    {
        return true;
    }
}