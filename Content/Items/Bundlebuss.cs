using System.Collections.Generic;
using HendecamMod.Content.Projectiles;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class Bundlebuss : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 0.66f;
        Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
        Item.value = 685000;
        Item.useTime = 4; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 32; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.reuseDelay = 24;
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        Item.consumeAmmoOnLastShotOnly = true;
        Item.UseSound = SoundID.Item40;
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 32; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 4.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; 
        Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 9.25f; // The speed of the projectile (measured in pixels per frame.)
        Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (type == ProjectileID.Bullet)
        {
            type = ModContent.ProjectileType<MusketBallSpinny>();
        }

        if (type == ModContent.ProjectileType<MusketBallSpinny>())
        {
            damage = (int)(damage * 1.2f);
        }
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Unleashes a large burst of bullets");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Converts musket balls into bigger, faster, more powerful musket balls")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 1; 
        damage = (int)(damage * 0.92f);
        SoundEngine.PlaySound(SoundID.NPCDeath63, player.position);
        SoundEngine.PlaySound(SoundID.Item31, player.position);
        SoundEngine.PlaySound(SoundID.Item11, player.position);
        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(5.45f));

            newVelocity *= 1f - Main.rand.NextFloat(0.33f);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; 
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-42f, -2f);
    }
}