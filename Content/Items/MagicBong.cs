using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items;

public class MagicBong : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
        ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
    }

    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1f;
        Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
        Item.value = 67000;
        // Use Properties
        // Use Properties
        Item.useTime = 10; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 40; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        // The sound that this item plays when used.
        Item.UseSound = SoundID.Item45;
        Item.DamageType = ModContent.GetInstance<AutismDamage>();
        Item.damage = 13; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 0.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.
        Item.mana = 11;
        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shoot = ModContent.ProjectileType<ZazaMagic>();

        Item.shootSpeed = 4.5f; // The speed of the projectile (measured in pixels per frame.)
    }
    public float LobotometerCost = 11f;
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ModContent.ProjectileType<ZazaSpawn>();
        position = Main.MouseWorld;
    }

    //  public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    // {
    // int NumProjectiles = 1;

    // for (int i = 0; i < NumProjectiles; i++)
    //{
    // Rotate the velocity randomly by 30 degrees at max.
    //Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(0));

    // Create a projectile.
    //Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
    // }

    // return false; // Return false because we don't want tModLoader to shoot projectile
    // }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Creates magic, tile-ignoring smoke that rises up at enemies");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Uses 11 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        {
            recipe.AddIngredient<WeedLeaves>(28);
            recipe.AddIngredient(ItemID.BottledWater, 3);
            recipe.AddIngredient(ItemID.NaturesGift);
            recipe.AddTile(TileID.GlassKiln);
            recipe.Register();
        }
    }

    // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(2f, 4f);
    }
}