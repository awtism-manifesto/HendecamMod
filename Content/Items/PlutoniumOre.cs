using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items;

public class PlutoniumOre : ModItem
{
    public override void SetStaticDefaults()
    {
        // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)
        ItemID.Sets.ItemIconPulse[Item.type] = true; // The item pulses while in the player's inventory

        Item.ResearchUnlockCount = 100; // Configure the amount of this item that's needed to research it in Journey mode.
    }

    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1f;
        Item.rare = ItemRarityID.LightPurple; // The color that the item's name will be in-game.
        Item.value = 6000;
        Item.maxStack = 9999;
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.PlutoniumOrePlaced>());
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }
}