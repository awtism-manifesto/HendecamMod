
using HendecamMod.Content.Global;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Accessories;


public class BrainScanner : ModItem
{
    public override void SetStaticDefaults()
    {
        // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)
        Main.RegisterItemAnimation(Type, new DrawAnimationVertical(9, 17));
        ItemID.Sets.AnimatesAsSoul[Type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation





    }

    public override void SetDefaults()
    {
        // We don't need to add anything particularly unique for the stats of this item; so let's just clone the Radar.
        Item.CloneDefaults(ItemID.Radar);
    }
    public override void ModifyTooltips( List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Displays Lobotometer decay rate") { OverrideColor = Color.White });
    }
    // This is the main hook that allows for our info display to actually work with this accessory. 
    public override void UpdateVanity(Player player)
    {
        player.GetModPlayer<BrainScanPlayer>().showLobotometerDecay = true;
    }
    public override void UpdateInfoAccessory(Player player)
    {
        player.GetModPlayer<BrainScanPlayer>().showLobotometerDecay = true;
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
   
}