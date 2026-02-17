using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class AA12 : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 44; 
        Item.height = 18; 
        Item.scale = 1.15f;
        Item.rare = ItemRarityID.Red;
        Item.value = 1350000;
        Item.useTime = 9; 
        Item.useAnimation = 9; 
        Item.useStyle = ItemUseStyleID.Shoot; 
        Item.autoReuse = true; 
        Item.UseSound = SoundID.Item62;
        Item.DamageType = DamageClass.Ranged; 
        Item.damage = 27; 
        Item.knockBack = 6f;
        Item.noMelee = true;
        Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 9.25f; 
        Item.useAmmo = AmmoID.Bullet;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 6; 

        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));

            newVelocity *= 1f - Main.rand.NextFloat(0.2f);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; 
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.TacticalShotgun);
        recipe.AddIngredient(ItemID.FragmentVortex, 8);
        recipe.AddIngredient<Polymer>(35);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-13f, -1f);
    }
}