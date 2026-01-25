using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items;

	public class PoorMahogany : ModItem
	{
		public override void SetDefaults()
		{
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.autoReuse = true;
			Item.rare = ItemRarityID.White;
			Item.value = Item.buyPrice(copper: 0);
			Item.consumable = true;
			Item.maxStack = 9999;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.PoorMahoganyTile>(), 0);
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.RichMahogany, 1);
			recipe.AddTile(TileID.CookingPots);
			recipe.Register();
		}
	}