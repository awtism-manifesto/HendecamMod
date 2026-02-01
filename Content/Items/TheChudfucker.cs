using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Rarities;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class TheChudfucker : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.6f;
        Item.rare = ModContent.RarityType<Seizure2>();
        Item.value = 67000000;
        // Use Properties
        // Use Properties
        Item.useTime = 1; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 32; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

        // The sound that this item plays when used.

        // Weapon Properties
        Item.DamageType = ModContent.GetInstance<OmniDamage>();
        Item.damage = 670; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 6.9f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.

        Item.shootSpeed = 30f; // The speed of the projectile (measured in pixels per frame.)

        Item.shoot = ProjectileID.Seed;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ProjectileID.SeedlerNut;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item38, player.position);
        return true; // Return false because we don't want tModLoader to shoot projectile
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Unobtainable. If someone is using this, they are cheating")
        {
            OverrideColor = new Color(255, 15, 85)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "-Developer Item-")
        {
            OverrideColor = new Color(255, 15, 85)
        };
        tooltips.Add(line);

        
    }

    // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-8f, -4f);
    }
}