using System.Collections.Generic;

namespace HendecamMod.Content.Items;

public class CrimtaneAmr : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 0.5f;
        Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
        Item.value = 265000;
        AmmoID.Sets.SpecificLauncherAmmoProjectileFallback[Type] = ItemID.RocketLauncher;

        // Use Properties
        // Use Properties
        Item.useTime = 60; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 60; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        // The sound that this item plays when used.
        Item.UseSound = SoundID.Item88;
        // Weapon Properties
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 34; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.
        Item.ArmorPenetration = 10;

        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shoot = ProjectileID.PurificationPowder;

        Item.shootSpeed = 15f; // The speed of the projectile (measured in pixels per frame.)
        Item.useAmmo = ItemID.RocketI;

        if (ModLoader.TryGetMod("Snipers_More", out Mod JfkMerica))
        {
            Item.damage = 36;
            Item.useTime = 58;
            Item.useAnimation = 58;
        }
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ModContent.ProjectileType<Projectiles.AMRRound>();
        if (type == ModContent.ProjectileType<Projectiles.AMRRound>())
        {
            damage = (int)(damage * 0.85f);
        }
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Shoots rapid, piercing rockets that deal massive damage on impact");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Explodes every time it pierces")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient(ItemID.CrimtaneBar, 30);
        recipe.AddIngredient(ItemID.TissueSample, 15);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        if (ModLoader.TryGetMod("Snipers_More", out Mod JfkMerica) && JfkMerica.TryFind("Crimtane_Auto_Rifle", out ModItem CrimtaneAutoRifle))
        {
            recipe.AddIngredient(CrimtaneAutoRifle.Type);
        }
    }

    // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-50f, -4f);
    }
}