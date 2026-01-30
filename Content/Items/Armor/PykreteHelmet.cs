using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class PykreteHelmet : ModItem
{
    public override void SetDefaults()
    {
        Item.defense = 4;
        Item.rare = ItemRarityID.White;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<Pykrete>(25);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}