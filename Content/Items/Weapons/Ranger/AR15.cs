using HendecamMod.Common.Systems.Players;
using HendecamMod.Common.Utilities;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Ranger;

public class AR15 : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
        FrontGunLayer.RegisterData(Item.type);
     
    }
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
        Item.shootSpeed = 10.25f;
        Item.useAmmo = AmmoID.Bullet;
        Item.noUseGraphic = true;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        HendecamPlayer modPlayer = player.GetModPlayer<HendecamPlayer>();

        modPlayer.recoilFront = 0.125f;
        modPlayer.recoilBack = 0.125f;
        
        const int NumProjectiles = 1; // The number of projectiles that this gun will shoot.
        Vector2 muzzleOffset = Vector2.Normalize(velocity) * 10f;
        position += muzzleOffset;
        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(1.25f));

            newVelocity *= 1f - Main.rand.NextFloat(0.18f);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; // Return false because we don't want tModLoader to shoot projectile
    }
    public static void Hold(Player player)
    {
        HendecamPlayer modPlayer = player.GetModPlayer<HendecamPlayer>();
        Vector2 mouse = modPlayer.MousePosition;

        if (mouse.X < player.Center.X)
            player.direction = -1;
        else
            player.direction = 1;

        float rotation = (Vector2.Normalize(mouse - player.MountedCenter) * player.direction).ToRotation();
        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, rotation * player.gravDir - modPlayer.recoilFront * player.direction - MathHelper.PiOver2 * player.direction);
        player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, rotation * player.gravDir - modPlayer.recoilBack * player.direction - MathHelper.PiOver2 * player.direction);
    }

    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        Hold(player);
    }
    public override void HoldStyle(Player player, Rectangle heldItemFrame)
    {
        Hold(player);
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
        return new Vector2(12f, -14f);
    }
}