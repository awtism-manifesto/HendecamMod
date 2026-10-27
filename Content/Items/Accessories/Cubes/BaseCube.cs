using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.Cubes;

public abstract class BaseCube : ModItem
{
    protected abstract int Rarity { get; }
    protected abstract int Value { get; }
    protected virtual int Defense => 0;
    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 26;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.rare = Rarity;
        Item.value = Value;
        Item.defense = Defense;
    }
    protected void CreateCubeRecipe(int ingredientID, int amount = 12, int tileID = TileID.Anvils)
    {
        CreateRecipe()
            .AddIngredient<CubicMold>()
            .AddIngredient(ingredientID, amount)
            .AddTile(tileID)
            .Register();
    }
}