using System.Collections.Generic;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Weapons;

public class TransgenderTwinbeam : ModItem
{
    private int shotCounter;

    public override void SetStaticDefaults()
    {
        Item.staff[Type] = true; // This makes the useStyle animate as a staff instead of as a gun.
    }

    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 14;
        Item.useAnimation = 28;
        Item.autoReuse = true;
        Item.mana = 9;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 19;
        Item.knockBack = 2f;
        Item.noMelee = true;
        Item.value = 70000;
        Item.rare = ItemRarityID.Green;
        Item.shoot = ModContent.ProjectileType<Morbeam>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 10f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (shotCounter <= 0)
        {
            Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(1.25f));

            type = ModContent.ProjectileType<TransBeamBlue>();
            SoundEngine.PlaySound(SoundID.Item91, player.position);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, (int)(damage * 1.05f), knockback, player.whoAmI);
            shotCounter = 2;
        }
        else if (shotCounter == 2)
        {
            Vector2 new2Velocity = velocity.RotatedBy(MathHelper.ToRadians(-1.25f));

            type = ModContent.ProjectileType<TransBeamPink>();
            SoundEngine.PlaySound(SoundID.Item91, player.position);

            Projectile.NewProjectileDirect(source, position, new2Velocity, type, (int)(damage * 1.05f), knockback, player.whoAmI);
            shotCounter = 0;
        }

        return false;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Shoots alternating beams of trans energy");
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

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(3f, -7f);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<TransBar>(10);
        recipe.AddIngredient(ItemID.FallenStar, 2);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}