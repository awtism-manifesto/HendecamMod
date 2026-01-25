using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items;

/// <summary>
///     Star Wrath/Starfury style weapon. Spawn projectiles from sky that aim towards mouse.
///     See Source code for Star Wrath projectile to see how it passes through tiles.
///     For a detailed sword guide see <see cref="ExampleSword" />
/// </summary>
public class PyriteProjectorBlade : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 9;
        Item.useAnimation = 27;
        Item.autoReuse = true;
        

        Item.DamageType = ModContent.GetInstance<MeleeMagicDamage>();
        Item.damage = 22;
        Item.knockBack = 3f;
        Item.scale = 1.33f;
        Item.mana = 6;
        Item.value = 59500;
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item1;

        Item.shoot = ModContent.ProjectileType<PyriteMagicSword>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 9.5f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }
    private int shotCounter = 0;
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
       
       
      

        if (shotCounter <= 0)
        {
            Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(6.25f));


            SoundEngine.PlaySound(SoundID.Item90, player.position);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, (int)(damage * 0.7f), knockback, player.whoAmI);
            shotCounter = 2;
        }
        else if (shotCounter == 2)
        {
            Vector2 new2Velocity = velocity.RotatedBy(MathHelper.ToRadians(0f));


            SoundEngine.PlaySound(SoundID.Item90, player.position);

            Projectile.NewProjectileDirect(source, position, new2Velocity, type, (int)(damage * 0.7f), knockback, player.whoAmI);
            shotCounter = 3;
        }
        else if (shotCounter == 3)
        {
            Vector2 new3Velocity = velocity.RotatedBy(MathHelper.ToRadians(-6.25f));


            SoundEngine.PlaySound(SoundID.Item90, player.position);

            Projectile.NewProjectileDirect(source, position, new3Velocity, type, (int)(damage * 0.7f), knockback, player.whoAmI);
            shotCounter = 0;
        }


        return false;
    }
   

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<PyriteBar>(12);
        recipe.AddIngredient(ItemID.FallenStar, 3);

        recipe.AddTile(TileID.Anvils);
        recipe.Register();







    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Projects three magical swords in an arc pattern");
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
    

}
