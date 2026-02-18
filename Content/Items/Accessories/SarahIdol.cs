using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class SarahIdol : ModItem
{
   

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.LightPurple;
        Item.value = 900000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Permanently makes the player upside down");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'Dedicated to my girlfriend, who i love very much despite the fact that she's upside down'")
        {
            OverrideColor = new Color(255,255,255)
        };
        tooltips.Add(line);

       
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<Australian>().UpsideDown = true;

    }
}
public class Australian : ModPlayer
{



    public bool UpsideDown;


    public override void ResetEffects()
    {
        UpsideDown = false;
    }

    public override void PostUpdate()
    {
        if (UpsideDown)

        { 
            Player.gravDir = -1;
            
        }

      

    }


}