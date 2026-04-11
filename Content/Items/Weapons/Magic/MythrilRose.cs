using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Magic;

public class MythrilRose : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.staff[Type] = true; // This makes the useStyle animate as a staff instead of as a gun.
    }

    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;

        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 19;
        Item.useAnimation = 19;
        Item.autoReuse = true;
      
        Item.mana = 9;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 40;
        Item.knockBack = 0.01f;
        Item.noMelee = true;
        Item.ArmorPenetration = 3;
        Item.value = 175000;
        Item.rare = ItemRarityID.LightRed;
        Item.shoot = ProjectileType<MythrilRosePetal>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 13.25f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }
    public override void HoldItem(Player player)
    {
        if (player.direction == 1)
        {
            Lighting.AddLight(player.Right, 0.45f, 1.53f, 1.61f);
        }
        else
        {
            Lighting.AddLight(player.Left, 0.45f, 1.53f, 1.61f);
        }
        
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ProjectileType<MythrilRosePetal>();
    }
    private int shotCounter;
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(0.5f));
        Vector2 new3Velocity = velocity.RotatedBy(MathHelper.ToRadians(-0.5f));
        Vector2 new2Velocity = velocity.RotatedBy(MathHelper.ToRadians(-2.25f));
        Vector2 new4Velocity = velocity.RotatedBy(MathHelper.ToRadians(2.25f));
        if (shotCounter == 4)
        {
            SoundEngine.PlaySound(SoundID.Item42, player.position);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            Projectile.NewProjectileDirect(source, position, new3Velocity, type, damage, knockback, player.whoAmI);
            Projectile.NewProjectileDirect(source, position, new2Velocity, type, damage, knockback, player.whoAmI);
            Projectile.NewProjectileDirect(source, position, new4Velocity, type, damage, knockback, player.whoAmI);

            shotCounter = 0;
        }
        else if (shotCounter <= 1)
        {
            SoundEngine.PlaySound(SoundID.Item42, player.position);

            Projectile.NewProjectileDirect(source, position, new2Velocity, type, damage, knockback, player.whoAmI);
            Projectile.NewProjectileDirect(source, position, new4Velocity, type, damage, knockback, player.whoAmI);

            shotCounter++;
        }
        else
        {
            SoundEngine.PlaySound(SoundID.Item42, player.position);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            Projectile.NewProjectileDirect(source, position, new3Velocity, type, damage, knockback, player.whoAmI);

            shotCounter++;
        }

        return false;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Casts multiple venomous Mythril petals");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Emits light while held")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        
    }

    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MythrilBar, 10);
        recipe.AddIngredient(ItemID.JungleRose);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MythrilBar, 8);
        recipe.AddIngredient(ItemID.NaturesGift);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}