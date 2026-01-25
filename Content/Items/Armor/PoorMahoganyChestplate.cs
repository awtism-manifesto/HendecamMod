using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Armor;

	[AutoloadEquip(EquipType.Body)]
	public class PoorMahoganyChestplate : ModItem
	{
		public override void SetDefaults()
		{
			Item.defense = 2;
			Item.rare = ItemRarityID.White;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<PoorMahogany>(40);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}