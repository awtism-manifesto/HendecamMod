using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories;

// This example attempts to showcase most of the common boot accessory effects.
// Of particular note is a showcase of the correct approaches to various movement speed modifications.
[AutoloadEquip(EquipType.Shoes)]
public class ClimbingBoots : ModItem
{
    public static readonly int MoveSpeedBonus = 8;

    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoveSpeedBonus);

    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 22;

        Item.accessory = true;
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.buyPrice(silver: 650); // Equivalent to Item.buyPrice(0, 1, 0, 0);
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "8% increased movement speed");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Allows the wearer to run super fast")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        // These 2 stat changes are equal to the Lightning Boots
        player.moveSpeed += MoveSpeedBonus / 109f; // Modifies the player movement speed bonus.
        player.accRunSpeed = 6.35f; // Sets the players sprint speed in boots.

        // player.maxRunSpeed and player.runAcceleration are usually not set by boots and should not be changed in UpdateAccessory due to the logic order. See ExampleStatBonusAccessoryPlayer.PostUpdateRunSpeeds for an example of adjusting those speed stats.

        // Determines whether the boots count as rocket boots
        // 0 - These are not rocket boots
        // Anything else - These are rocket boots
        player.rocketBoots = 0;

        player.waterWalk2 = false; // Allows walking on all liquids without falling into it
        player.waterWalk = false; // Allows walking on water, honey, and shimmer without falling into it
        player.iceSkate = true; // Grant the player improved speed on ice and not breaking thin ice when falling onto it
        player.desertBoots = true; // Grants the player increased movement speed while running on sand
        player.fireWalk = true; // Grants the player immunity from Meteorite and Hellstone tile damage
        player.noFallDmg = false; // Grants the player the Lucky Horseshoe effect of nullifying fall damage
        player.lavaRose = false; // Grants the Lava Rose effect

        // player.DoBootsEffect(player.DoBootsEffect_PlaceFlowersOnTile); // Spawns flowers when walking on normal or Hallowed grass

        // These effects are visual only. These are replicated in UpdateVanity below so they apply for vanity equipment.
        if (!hideVisual)
        {
            player.CancelAllBootRunVisualEffects(); // This ensures that boot visual effects don't overlap if multiple are equipped

            // Hellfire Treads sprint dust. For more info on sprint dusts see Player.SpawnFastRunParticles() method in Player.cs
            player.hellfireTreads = false;
            // Other boot run visual effects include: sailDash, coldDash, desertDash, fairyBoots
        }
    }

    public override void UpdateVanity(Player player)
    {
        // This code is a copy of the visual effects code in UpdateAccessory above
        player.CancelAllBootRunVisualEffects();
        player.vanityRocketBoots = 2;
        player.hellfireTreads = false;
    }
    public override void AddRecipes()
    {
        // Hermes Boots
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.HermesBoots, 1);
        recipe.AddIngredient(ItemID.Aglet, 1);
        recipe.AddIngredient(ItemID.AnkletoftheWind, 1);
        recipe.AddIngredient<PyriteBar>(7);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        // Flurry Boots
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FlurryBoots, 1);
        recipe.AddIngredient(ItemID.Aglet, 1);
        recipe.AddIngredient(ItemID.AnkletoftheWind, 1);
        recipe.AddIngredient<PyriteBar>(7);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        // Dunerider Boots
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SandBoots, 1);
        recipe.AddIngredient(ItemID.Aglet, 1);
        recipe.AddIngredient(ItemID.AnkletoftheWind, 1);
        recipe.AddIngredient<PyriteBar>(7);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        // Sailfish Boots
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SailfishBoots, 1);
        recipe.AddIngredient(ItemID.Aglet, 1);
        recipe.AddIngredient(ItemID.AnkletoftheWind, 1);
        recipe.AddIngredient<PyriteBar>(7);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
