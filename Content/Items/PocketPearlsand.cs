using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;


public class PocketPearlsand : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 13;
        Item.useAnimation = 13;
        Item.autoReuse = true;

        Item.DamageType = ModContent.GetInstance<StupidDamage>();
        Item.damage = 51;
        Item.knockBack = 1;
        Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
        Item.noUseGraphic = true; // Makes the item invisible while using it (the projectile is the visible part).

        Item.value = Item.buyPrice(silver: 100);
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item45;

        Item.shoot = ModContent.ProjectileType<PearlPocket>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 18f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }
    public float LobotometerCost = 5f;
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 1; // The number of projectiles that this gun will shoot.

        for (int i = 0; i < NumProjectiles; i++)
        {
            // Rotate the velocity randomly by 30 degrees at max.
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(2));

            // Decrease velocity randomly for nicer visuals.
            newVelocity *= 1f - Main.rand.NextFloat(0.01f);

            // Create a projectile.
            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; // Return false because we don't want tModLoader to shoot projectile
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Increases in damage and range as it pierces more targets");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Uses 5 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<PlasticScrap>(5);
        recipe.AddIngredient(ItemID.PearlsandBlock, 50);

        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}