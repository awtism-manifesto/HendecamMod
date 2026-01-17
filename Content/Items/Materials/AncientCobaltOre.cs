using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using HendecamMod.Content.Tiles;

namespace HendecamMod.Content.Items.Materials
{
    public class AncientCobaltOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)


           
          
            Item.ResearchUnlockCount = 100; // Configure the amount of this item that's needed to research it in Journey mode.
        }
        public override void SetDefaults()
        {
            Item.width = 32; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.scale = 1f;
            Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
            Item.value = 1250;
            Item.maxStack = 9999;
            Item.DefaultToPlaceableTile(ModContent.TileType<AncientCobaltOrePlaced>());
            
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "'It's true power lies behind a fleshy wall'");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);



           
        }


        
    }
}