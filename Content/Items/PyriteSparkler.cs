using System.Collections.Generic;
using HendecamMod.Content.Projectiles;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class PyriteSparkler : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.staff[Type] = true; // This makes the useStyle animate as a staff instead of as a gun.
    }

    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.1f;
        Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
        Item.value = 51000;
        // Use Properties
        // Use Properties
        Item.useTime = 15; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 30; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        // The sound that this item plays when used.

        Item.UseSound = SoundID.Item13 with
            {
                Volume = 10f,
                Pitch = 0f,
                PitchVariance = 0.00f,
                MaxInstances = 5,
                SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
            }
            ;

        // Weapon Properties
        Item.DamageType = DamageClass.Magic; // Sets the damage type to ranged.
        Item.damage = 17; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 0.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.
        Item.shoot = ModContent.ProjectileType<IchSpark>();

        Item.mana = 9;

        Item.shootSpeed = 8.75f; // The speed of the projectile (measured in pixels per frame.)
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ModContent.ProjectileType<IchSpark>();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 2; // The number of projectiles that this gun will shoot.

        for (int i = 0; i < NumProjectiles; i++)
        {
            // Rotate the velocity randomly by 30 degrees at max.
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(13.5f));

            // Decrease velocity randomly for nicer visuals.
            newVelocity *= 1f - Main.rand.NextFloat(0.36f);

            // Create a projectile.
            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; // Return false because we don't want tModLoader to shoot projectile
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Quickly, inaccurately shoots sparks of ichor");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        // Here we will hide all tooltips whose title end with ':RemoveMe'
        // One like that is added at the start of this method
        foreach (var l in tooltips)
        {
            if (l.Name.EndsWith(":RemoveMe"))
            {
                l.Hide();
            }
        }

        // Another method of hiding can be done if you want to hide just one line.
        // tooltips.FirstOrDefault(x => x.Mod == "ExampleMod" && x.Name == "Verbose:RemoveMe")?.Hide();
    }

    public override void AddRecipes()
    {
        Recipe
            recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WandofSparking);
        recipe.AddIngredient<PyriteBar>(10);

        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WandofFrosting);
        recipe.AddIngredient<PyriteBar>(10);

        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient<ShadewoodWand>();
        recipe.AddIngredient<PyriteBar>(10);

        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient<ShadewoodWand>();
        recipe.AddIngredient<PyriteBar>(10);

        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
    // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
}