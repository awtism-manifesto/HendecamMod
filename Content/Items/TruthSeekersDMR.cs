using HendecamMod.Content.Projectiles;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class TruthSeekersDMR : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 0.725f;
        Item.rare = ItemRarityID.Yellow; // The color that the item's name will be in-game.
        Item.value = 630000;
        // Use Properties
        Item.useTime = 4; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 12; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.reuseDelay = 24;
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        // The sound that this item plays when used.
       
        // Weapon Properties
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 65; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 6f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.
        Item.crit = 10;
        Item.ArmorPenetration = 10;
        Item.shoot = ProjectileType<TrueBullet>();
       

        // Gun Properties
        Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 26f; // The speed of the projectile (measured in pixels per frame.)
        Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (type == ProjectileID.Bullet)
        {
            type = ProjectileType<TrueBullet>();
        }

        if (type == ProjectileType<TrueBullet>())
        {
            damage = (int)(damage * 1.09f);
        }
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {


        SoundEngine.PlaySound(new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/SVDShoot")
        {
            Volume = 2.67f,
            Pitch = 0.5f,
            MaxInstances = 100,
        });


        return true;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Converts musket balls into hitscan True Bullets");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.ChlorophyteBar, 14);
        recipe.AddIngredient<SpecOpsRifle>();
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-33f, -1f);
    }
}