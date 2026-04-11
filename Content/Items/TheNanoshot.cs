using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class TheNanoshot : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 44; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.1f;
        Item.rare = ItemRarityID.Cyan; // The color that the item's name will be in-game.
        Item.value = 270000;
        Item.useTime = 32; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 32; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        Item.UseSound = SoundID.Item96;
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 72; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 4f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true;
        Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 6.7f; 
        Item.useAmmo = AmmoID.Bullet; 
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ProjectileID.NanoBullet;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 5; // The number of projectiles that this gun will shoot.

        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(2.44f));

            newVelocity *= 1f - Main.rand.NextFloat(0.22f);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Shotgun);
        recipe.AddIngredient<CyberneticGunParts>();
        recipe.AddIngredient(ItemID.Nanites, 15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-4.5f, -0.5f);
    }
}