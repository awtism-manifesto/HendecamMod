using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

/// <summary>
///     Star Wrath/Starfury style weapon. Spawn projectiles from sky that aim towards mouse.
///     See Source code for Star Wrath projectile to see how it passes through tiles.
///     For a detailed sword guide see <see cref="ExampleSword" />
/// </summary>
public class BulletBlade : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 48;
        Item.scale = 1.5f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.autoReuse = true;

        Item.DamageType = ModContent.GetInstance<MeleeRangedDamage>();
        Item.damage = 51;
        Item.knockBack = 4;
        Item.value = Item.buyPrice(gold: 12);
        Item.rare = ItemRarityID.Pink;
        Item.UseSound = SoundID.Item1;

        Item.useAmmo = AmmoID.Bullet;
        Item.shoot = ModContent.ProjectileType<MinieProj>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 20f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (type == ProjectileID.ChlorophyteBullet)
        {
            damage = (int)(damage * 0.25f);
        }
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 27; // The number of projectiles that this gun will shoot.
        damage = (int)(damage * Main.rand.NextFloat(0.49f, 0.5f));
        SoundEngine.PlaySound(SoundID.Item38, player.position);
        for (int i = 0; i < NumProjectiles; i++)
        {
            // Rotate the velocity randomly by 30 degrees at max.
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(360));

            // Create a projectile.
            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; // Return false because we don't want tModLoader to shoot projectile
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Shoots a ring of bullets all around the player")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Massively reduced damage with Chlorophyte Bullets")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'No gun? No problem!'")
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
        recipe.AddIngredient(ItemID.AdamantiteBar, 12);
        recipe.AddIngredient(ItemID.SoulofMight, 10);
        recipe.AddIngredient(ItemID.MusketBall, 100);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.TitaniumBar, 12);
        recipe.AddIngredient(ItemID.SoulofMight, 10);
        recipe.AddIngredient(ItemID.MusketBall, 100);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}