using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items;

public class PoorMahoganySword : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 25;
        Item.height = 25;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 14;
        Item.useAnimation = 14;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 8;
        Item.knockBack = 4.5f;
        Item.ChangePlayerDirectionOnShoot = false;

        Item.value = Item.buyPrice(gold: 0);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<Items.PoorMahogany>(7);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }

}
