using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons;


public class WoodenBlocks : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.damage = 7;
        Item.knockBack = 1.25f;
        Item.width = 32;
        Item.height = 32;
        Item.shootSpeed = 9.99f;
        Item.scale = 1f;
        Item.noUseGraphic = true;
        Item.UseSound = SoundID.Item1;
        Item.rare = ItemRarityID.White;
        Item.value = 500;
        Item.DamageType = ModContent.GetInstance<StupidDamage>();
        Item.shoot = ModContent.ProjectileType<ToyBlockRed>();
        Item.noMelee = true; // This is set the sword itself doesn't deal damage (only the projectile does).
        Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
        Item.autoReuse = true;
        
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(1.33f));
        newVelocity *= 1f - Main.rand.NextFloat(0.1f);

        // Pick a number from 0 to 4 to choose one of the five projectiles
        int choice = Main.rand.Next(3);

        switch (choice)
        {
            case 0:
                type = ModContent.ProjectileType<ToyBlockRed>();
                Projectile.NewProjectileDirect(source, position, newVelocity * 1f, type, (int)(damage * 1f), knockback, player.whoAmI);
                break;
            case 1:
                type = ModContent.ProjectileType<ToyBlockGreen>();
                Projectile.NewProjectileDirect(source, position, newVelocity * 1f, type, (int)(damage * 1f), knockback, player.whoAmI);
                break;
            case 2:
                type = ModContent.ProjectileType<ToyBlockYellow>();
                Projectile.NewProjectileDirect(source, position, newVelocity * 1f, type, (int)(damage * 1f), knockback, player.whoAmI);
                break;
          
        }

        return false; // return true only if you want the default projectile to be fired as well
    }
    public float LobotometerCost = 1f;
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
       
        var line = new TooltipLine(Mod, "Face", "Throws wooden toy blocks at your opponents");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Uses 1 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "'Don't know your ABC's? Now you can throw them!'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

       
        recipe.AddIngredient(ItemID.Wood, 12);

        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}