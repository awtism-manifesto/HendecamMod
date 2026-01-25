using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Armor;

	[AutoloadEquip(EquipType.Legs)]
	public class PykreteGreaves : ModItem
	{
		public override void SetDefaults()
		{
			Item.defense = 4;
			Item.rare = ItemRarityID.White;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<Pykrete>(35);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+5% damage reduction";
        player.endurance = 1f - 0.95f * (1f - player.endurance);
    }
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<PykreteChestplate>() && head.type == ModContent.ItemType<PykreteHelmet>();
		}
	}