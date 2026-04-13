using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
using static HendecamMod.Content.Items.Accessories.OffenseShield;

namespace HendecamMod.Content.Items.Accessories;

public class FreeHealthcare : ModItem
{

    

     public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs();

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Green;
        Item.value = 0;
        Item.lifeRegen = (int)2.5f;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Increases life regen and makes enemies much more likely to drop hearts on death");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'They'd never let this exist in TerMerica'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       

    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<Healthcare>().HasFreeHC = true;
    }
}
public class Healthcare : ModPlayer
{



    public bool HasFreeHC;

    public override void ResetEffects()
    {
        HasFreeHC = false;
    }



    
}
public class HeartBooster : GlobalNPC
{
    public override bool InstancePerEntity => true;

    public override void OnKill(NPC npc)
    {
       
        Player player = Main.player[npc.lastInteraction];

        if (player != null && player.active && player.GetModPlayer<Healthcare>().HasFreeHC)
        {
           
            if (Main.rand.NextBool(4))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Heart);
            }
        }
    }
}
