using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
namespace HendecamMod.Content.Items;

public class SubstrateSpreader : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 0.875f;
        Item.rare = ItemRarityID.Orange; // The color that the item's name will be in-game.
        Item.value = 97500;
        // Use Properties
        // Use Properties
        Item.useTime = 6; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 12; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

        Item.consumeAmmoOnFirstShotOnly = true;

        Item.ArmorPenetration = 5;
        // Weapon Properties
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 21; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 1f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.

        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shoot = ModContent.ProjectileType<LycoShot>();
        Item.useAmmo = AmmoID.Gel;
        Item.shootSpeed = 11.75f; // The speed of the projectile (measured in pixels per frame.)

    }
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        return Main.rand.NextFloat() >= 0.5f;
    }
    private int shotCounter = 0;

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {

        if (shotCounter <= 0)
        {
            Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(0.25f));

            type = ModContent.ProjectileType<LycoShot>();
            SoundEngine.PlaySound(SoundID.Item42, player.position);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, (int)(damage * 1.05f), knockback, player.whoAmI);
            shotCounter = 2;
        }
        else if (shotCounter == 2)
        {
            Vector2 new2Velocity = velocity.RotatedBy(MathHelper.ToRadians(-0.25f));

            type = ModContent.ProjectileType<LycoSporeRanged>();
            SoundEngine.PlaySound(SoundID.Item42, player.position);

            Projectile.NewProjectileDirect(source, position, new2Velocity, type, (int)(damage * 0.67f), knockback, player.whoAmI);
            shotCounter = 0;
        }

        return false;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Uses gel as ammo");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Shoots two alternating streams of Lycopite spores: One that fires straight and a weaker homing one")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Ignores 5 enemy armor")
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
        if (ModLoader.TryGetMod("RangerFlame", out Mod FireMerica) && FireMerica.TryFind("ThrowerParts", out ModItem ThrowerParts))
        {

            recipe = CreateRecipe();
            recipe.AddIngredient<Items.AshSpewer>();
            recipe.AddIngredient(ThrowerParts.Type);
            recipe.AddIngredient<Items.LycopiteBar>(13);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        else
        {
            recipe = CreateRecipe();
            recipe.AddIngredient<Items.AshSpewer>();
            recipe.AddIngredient<Items.LycopiteBar>(13);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
    // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-22f, -1f);
    }
}