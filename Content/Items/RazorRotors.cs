using System.Collections.Generic;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items;

public class RazorRotors : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
    }

    public override void SetDefaults()
    {
        // Common Properties
        Item.rare = ItemRarityID.Cyan;
        Item.value = 350000; // The number and type of coins item can be sold for to an NPC

        // Use Properties
        Item.useStyle = ItemUseStyleID.Swing; // How you use the item (swinging, holding out, etc.)
        Item.useAnimation = 30; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useTime = 30; // The length of the item's use time in ticks (60 ticks == 1 second.)
        Item.UseSound = SoundID.Item71; // The sound that this item plays when used.
        Item.autoReuse = true; // Allows the player to hold click to automatically use the item again. Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

        // Weapon Properties
        Item.damage = 130;
        Item.knockBack = 7.5f;
        Item.noUseGraphic = true; // When true, the item's sprite will not be visible while the item is in use. This is true because the spear projectile is what's shown so we do not want to show the spear sprite as well.
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.noMelee = true; // Allows the item's animation to do damage. This is important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
        Item.ArmorPenetration = 25;
        // Projectile Properties
        Item.shootSpeed = 0f; // The speed of the projectile measured in pixels per frame.
        Item.shoot = ModContent.ProjectileType<RazorRotorsProj>(); // The projectile that is fired from this weapon
    }

    public override bool AltFunctionUse(Player player)
    {
        return true;
    }

    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<RazorRotorThrown>();
            Item.shootSpeed = 28.5f;
        }
        else
        {
            Item.useStyle = ItemUseStyleID.Swing;

            Item.shoot = ModContent.ProjectileType<RazorRotorsProj>();
            Item.shootSpeed = 0f;
        }

        return base.CanUseItem(player);
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Rapidly spins around, damaging everything around you");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Right click to throw the smaller, stabilizer rotor")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Ignores 25 enemy defense")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }
}