using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class AR15 : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 62; 
        Item.height = 32;
        Item.scale = 1.2f;
        Item.rare = ItemRarityID.Green;
        Item.value = 236000;
        Item.useTime = 15; 
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.autoReuse = true; 
        Item.UseSound = SoundID.Item41;
        Item.DamageType = DamageClass.Ranged;
        Item.damage = 20;
        Item.knockBack = 2f;
        Item.noMelee = true;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 9.1f;
        Item.useAmmo = AmmoID.Bullet;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 1; // The number of projectiles that this gun will shoot.

        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(1));

            newVelocity *= 1f - Main.rand.NextFloat(0.18f);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; // Return false because we don't want tModLoader to shoot projectile
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DemoniteBar, 15);
        recipe.AddIngredient<Polymer>(15);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.CrimtaneBar, 15);
        recipe.AddIngredient<Polymer>(15);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-13f, 1f);
    }
}