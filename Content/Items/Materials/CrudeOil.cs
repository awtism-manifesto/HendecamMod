using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Materials;

public class CrudeOil : ModItem
{
    public override void SetStaticDefaults()
    {
        // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)

        Item.ResearchUnlockCount = 99; // Configure the amount of this item that's needed to research it in Journey mode.
    }

    public override void SetDefaults()
    {
        Item.damage = 3; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = DamageClass.Ranged;

        Item.width = 16;
        Item.height = 16;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 0.5f;
        Item.value = 500;
        Item.rare = ItemRarityID.Blue;
        Item.shoot = ProjectileID.Flames;
        Item.shootSpeed = 0f; // The speed of the projectile.
        Item.ammo = AmmoID.Gel; // The ammo class this ammo belongs to.
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(2);

        recipe.AddIngredient<OilBlock>();
        recipe.AddTile(TileID.Furnaces);
        recipe.Register();

        recipe = CreateRecipe();

        recipe.AddIngredient<CoalLump>(10);
        recipe.AddIngredient(ItemID.BottledWater, 2);
        recipe.AddTile(TileID.CookingPots);
        recipe.Register();

        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica) && ThorMerica.TryFind("SmoothCoal", out ModItem SmoothCoal))
        {
            recipe = CreateRecipe(5);
            recipe.AddIngredient(SmoothCoal.Type);

            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}