using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class VeryBigStick : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.bitsnbobs.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 25;
			Item.DamageType = DamageClass.Melee;
			Item.scale = 1.5f;
			Item.width = 34;
			Item.height = 34;
			Item.useTime = 17;
			Item.useAnimation = 17;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(silver: 20);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			// With Ebonwood
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.WoodenSword, 1);
			recipe.AddIngredient(ItemID.EbonwoodSword, 1);
			recipe.AddIngredient(ItemID.BorealWoodSword, 1);
			recipe.AddIngredient(ItemID.PalmWoodSword, 1);
			recipe.AddIngredient(ItemID.RichMahoganySword, 1);
            recipe.AddIngredient<Items.PoorMahoganySword>();
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
			// With Shadewood
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.WoodenSword, 1);
			recipe.AddIngredient(ItemID.ShadewoodSword, 1);
			recipe.AddIngredient(ItemID.BorealWoodSword, 1);
			recipe.AddIngredient(ItemID.PalmWoodSword, 1);
			recipe.AddIngredient(ItemID.RichMahoganySword, 1);
            recipe.AddIngredient<Items.PoorMahoganySword>();
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (Main.rand.NextBool(4))

			{
				target.AddBuff(BuffID.Confused, 120);
			}

		}
	}
}
