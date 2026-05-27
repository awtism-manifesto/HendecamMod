using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Items.Weapons.Magic;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons;

public class CoreEnforcer: ModItem
{
   

    public override void SetStaticDefaults()
    {
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        Item.staff[Type] = true;
    }

    public override void SetDefaults()
    {
       
        Item.width = 104;
        Item.height = 70;
        Item.rare = ItemRarityID.LightRed;
        Item.value = 650000;
        Item.useTime = 8;
        Item.useAnimation = 40;
        Item.reuseDelay = 32;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 24;
        Item.knockBack = 2.5f;
        Item.noMelee = true;
        Item.ArmorPenetration = 10;
        Item.mana = 21;
        Item.shoot = ProjectileType<ZygardeArrow>();
        Item.shootSpeed = 12.25f;
    }

    public override bool AltFunctionUse(Player player)
    {
        return true;
    }

    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
           
            Item.shoot = ProjectileType<ZygardeWave>();
           
        }
        else
        {
          
            Item.shoot = ProjectileType<ZygardeArrow>();
           
        }

        return base.CanUseItem(player);
    }





    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (Main.hardMode)
        {
            damage = (int)(damage * 1.33f);
        }
        if (player.altFunctionUse == 2)
        {
           
            type = ProjectileType<ZygardeWave>();
            Projectile.NewProjectileDirect(source, position, (velocity * 0.75f), type, (int)(damage * 2f), knockback, player.whoAmI);
            SoundEngine.PlaySound(new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/CoreEnforcerUse")
            {
                Volume = 4f,
                Pitch = 0.22f,
                MaxInstances = 100,
            });


            return false;
        }
        else 
        {
            int NumProjectiles = 3; // The number of projectiles that this gun will shoot.
           
            for (int i = 0; i < NumProjectiles; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(6.67f));

                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.35f);

                Projectile.NewProjectile(source, position, newVelocity * 1.33f, type, (int)(damage * 0.8f), knockback, player.whoAmI);
            }
            SoundEngine.PlaySound(new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/CoreEnforcerUse")
            {
                Volume = 4f,
                Pitch = 0.8f,
                MaxInstances = 100,
            });
        }
          


        return false; // Return false because we don't want tModLoader to shoot projectile}
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BeeGun);
        recipe.AddIngredient<PlanetoidPunisher>();
        recipe.AddIngredient(ItemID.Flamelash);
        recipe.AddIngredient<ScaldingScepter>();
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Left click to fire Thousand Arrows");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Right click to fire Thousand Waves")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Deals more damage when the ancient spirits of light and dark are present in your world")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

   
}