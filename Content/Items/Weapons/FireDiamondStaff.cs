using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Weapons;

public class FireDiamondStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.staff[Type] = true; // This makes the useStyle animate as a staff instead of as a gun.
    }

    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1f;
        Item.rare = ItemRarityID.Orange; // The color that the item's name will be in-game.
        Item.value = 110000;
        // Use Properties
        Item.useTime = 30; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 30; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        Item.mana = 15;
        // The sound that this item plays when used.
        Item.UseSound = SoundID.Item43;
        // Weapon Properties
        Item.DamageType = DamageClass.Magic; // Sets the damage type to ranged.
        Item.damage = 30; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 8.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.
        // Gun Properties
        Item.shoot = ModContent.ProjectileType<Projectiles.FireDiamondStaffProjectile>();
        Item.shootSpeed = 12.5f; // The speed of the projectile (measured in pixels per frame.)
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Fires a piercing, explosive fireball");
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

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient(ItemID.Obsidian, 25);
        recipe.AddIngredient(ItemID.HellstoneBar, 10);
        recipe.AddIngredient<FireDiamond>(8);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        if (ModLoader.TryGetMod("SOTS", out Mod SOTS) && SOTS.TryFind("DissolvingEarth", out ModItem DissolvingEarth))
        {
            recipe.AddIngredient(DissolvingEarth.Type);
        }
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ModContent.ProjectileType<Projectiles.FireDiamondStaffProjectile>();
    }
}