
using HendecamMod.Content.Global;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories;


public class IQTest : ModItem
{
   

    public override void SetDefaults()
    {
        // We don't need to add anything particularly unique for the stats of this item; so let's just clone the Radar.
        Item.CloneDefaults(ItemID.Radar);
    }
    public override void ModifyTooltips( List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Displays exact Lobotometer number") { OverrideColor = Color.White });
    }
    // This is the main hook that allows for our info display to actually work with this accessory. 
    public override void UpdateInfoAccessory(Player player)
    {
        player.GetModPlayer<IQTestPlayer>().showLobotometer = true;
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
   
}