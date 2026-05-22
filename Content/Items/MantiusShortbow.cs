using System.Collections.Generic;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class MantiusShortbow : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.1f;
        Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
        Item.value = 175000;
        // Use Properties
        Item.useTime = 17; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 17; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        

        // The sound that this item plays when used.

        // Weapon Properties
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 37; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 3.25f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.
        Item.ArmorPenetration = 10;
        Item.UseSound = new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/BowShot")
        {
            Volume = 3f,
            PitchVariance = 0.2f,
            MaxInstances = 10,
        };
        Item.shootSpeed = 13f; // The speed of the projectile (measured in pixels per frame.)
        Item.useAmmo = ItemID.WoodenArrow;
        Item.shoot = ProjectileType<MantleEnergyArrow>();
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
      
            type = ProjectileType<MantleEnergyArrow>();
       
    }

   

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Shoots large, fast-flying energy arrows that deal increased damage to high defense targets");
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

        recipe.AddIngredient<MantiusBar>(14);

        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(5f, -1f);
    }
}