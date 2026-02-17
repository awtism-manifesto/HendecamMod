using HendecamMod.Content.Items.Placeables;

namespace HendecamMod.Content.Items.Tools;

public class MorbiumEarthquaker : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 208;
        Item.DamageType = DamageClass.Melee;
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 3;
        Item.useAnimation = 21;
        Item.scale = 1.5f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 16;
        Item.value = 325000;
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item23;
        Item.autoReuse = true; // Automatically re-swing/re-use this item after its swinging animation is over.
        Item.useTurn = true;
        Item.tileBoost = 4;
        Item.hammer = 175; // How much axe power the weapon has, note that the axe power displayed in-game is this value multiplied by 5
        Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(10))
        { 
        }
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<MorbiumBar>(18);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}