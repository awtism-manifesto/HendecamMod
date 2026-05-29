using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.Audio;

namespace HendecamMod.Content.Items.Weapons.Ranger;

public class PneumaticRifle : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.05f;
        Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
        Item.value = 155000;
        // Use Properties
        Item.useTime = 38; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 38; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        Item.ArmorPenetration = 10;

        Item.UseSound = new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/RifleShot")
        {
            Volume = 2.67f,
            PitchVariance = 0.2f,
            MaxInstances = 15,
        };
        // Weapon Properties
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 69; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 7f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.

        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 12f; // The speed of the projectile (measured in pixels per frame.)
        Item.useAmmo = AmmoID.Bullet;
        Item.shoot = ProjectileType<MantleStoneChunk>();
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ProjectileType<MantleStoneChunk>();
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Converts all bullets into Mantle Rays that can pierce through almost anything, including blocks");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Deals more damage to high defense targets")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Fires at super high velocity")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<Polymer>(25);
        recipe.AddIngredient<MantiusBar>(10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        if (ModLoader.TryGetMod("SpiritReforged", out Mod Spirit2Merica) && Spirit2Merica.TryFind("HuntingRifle", out ModItem HuntingRifle))
        {
            recipe.AddIngredient(HuntingRifle.Type);
        }
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-16.5f, -1.66f);
    }
}