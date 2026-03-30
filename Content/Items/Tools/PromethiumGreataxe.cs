using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Tiles.Furniture;

namespace HendecamMod.Content.Items.Tools;

public class PromethiumGreataxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 80;
        Item.DamageType = DamageClass.Melee;
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 18;
        Item.useAnimation = 18;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 6;
        Item.value = Item.buyPrice(gold: 10);
        Item.rare = ItemRarityID.Purple;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.axe = 40;

        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PromethiumBar>(12);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }
}