using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HendecamMod.Content.Tiles;
using HendecamMod.Content.Items.Materials;


namespace HendecamMod.Content.Items
{
	public class Pykrete : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.ExtractinatorMode[Item.type] = Item.type;
		}

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<PykreteTile>());
			Item.width = 12;
			Item.height = 12;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe(2)
				.AddIngredient<Sawdust>(10)
				.AddIngredient(ItemID.IceBlock, 2)
				.AddTile(TileID.Sawmill)
				.Register();
		}
	}
}
