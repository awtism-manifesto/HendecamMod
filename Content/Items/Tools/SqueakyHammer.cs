using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Tools;


public class SqueakyHammer : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 40;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 11;
        Item.useAnimation = 11;
        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 10;
        Item.knockBack = 0.5f;
        Item.useTurn = true;

        Item.hammer = 40;
        Item.value = Item.buyPrice(silver: 3);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.DSTMaleHurt;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<PlasticScrap>(10);

        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}