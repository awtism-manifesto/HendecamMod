using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Armor;

	[AutoloadEquip(EquipType.Body)]
	public class PykreteChestplate : ModItem
	{
		public override void SetDefaults()
		{
			Item.defense = 5;
			Item.rare = ItemRarityID.White;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<Pykrete>(45);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}