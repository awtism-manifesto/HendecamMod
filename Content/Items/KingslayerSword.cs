using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace HendecamMod.Content.Items;

public class KingslayerSword : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.1f;
        Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
        Item.value = 125000;


        // Use Properties
        // Use Properties
        Item.useTime = 42; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 42; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Swing; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

        Item.useTurn = true;

        // The sound that this item plays when used.
        Item.UseSound = SoundID.Item1;


        // Weapon Properties
        Item.DamageType = DamageClass.Melee;
        Item.damage = 32; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 7f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.


        if (ModLoader.TryGetMod("ContinentOfJourney", out Mod HomeMerica))



        {
            Item.damage = 40;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.rare = ItemRarityID.LightRed;

        }

        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shoot = ModContent.ProjectileType<SpinningRuby>();

        Item.shootSpeed = 0.05f; // The speed of the projectile (measured in pixels per frame.)

    }


    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {

        // Rotate the velocity randomly by 30 degrees at max.
        Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(0.01f));
        Vector2 new1Velocity = velocity.RotatedByRandom(MathHelper.ToRadians(0.01f));

        // Decrease velocity randomly for nicer visuals.


        // Create a projectile.
        type = ModContent.ProjectileType<SpinningDiamond>();
        Projectile.NewProjectileDirect(source, position, new1Velocity, type, (int)(damage * 0.66f), knockback, player.whoAmI);
        type = ModContent.ProjectileType<SpinningRuby>();
        Projectile.NewProjectileDirect(source, position, newVelocity, type, (int)(damage * 0.6f), knockback, player.whoAmI);



        return false; // Return false because we don't want tModLoader to shoot projectile
    }




    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Throws large, accelerating gems at your target");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'What's a king to a mob?'")
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
        Recipe recipe = CreateRecipe();

        if (ModLoader.TryGetMod("ContinentOfJourney", out Mod HomeMerica) && HomeMerica.TryFind<ModItem>("Overhead", out ModItem Overhead))



        {
            recipe = CreateRecipe();
            recipe.AddIngredient(Overhead.Type);
            recipe.AddIngredient<Items.KingslayerBar>(14);
            recipe.AddIngredient(ItemID.Diamond, 2);
            recipe.AddIngredient(ItemID.Ruby, 2);
            recipe.AddTile(TileID.Solidifier);
            recipe.Register();

        }
        else
        {

            recipe = CreateRecipe();

            recipe.AddIngredient<Items.KingslayerBar>(14);
            recipe.AddIngredient(ItemID.GoldShortsword);
            recipe.AddIngredient(ItemID.GoldBroadsword);

            recipe.AddIngredient(ItemID.PlatinumShortsword);
            recipe.AddIngredient(ItemID.PlatinumBroadsword);
            recipe.AddIngredient(ItemID.Diamond, 2);
            recipe.AddIngredient(ItemID.Ruby, 2);
            recipe.AddTile(TileID.Solidifier);
            recipe.Register();


        }


    }
    // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.

}