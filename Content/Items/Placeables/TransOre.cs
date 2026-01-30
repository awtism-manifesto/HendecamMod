using System.Collections.Generic;
using HendecamMod.Content.Tiles.Blocks;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Placeables;

public class TransOre : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;

        // Mods can be translated to any of the languages tModLoader supports. See https://github.com/tModLoader/tModLoader/wiki/Localization
        // Translations go in localization files (.hjson files), but these are listed here as an example to help modders become aware of the possibility that users might want to use your mod in other lauguages:
        // English: "Example Block", "This is a modded tile."
        // German: "Beispielblock", "Dies ist ein modded Block"
        // Italian: "Blocco di esempio", "Questo è un blocco moddato"
        // French: "Bloc d'exemple", "C'est un bloc modgé"
        // Spanish: "Bloque de ejemplo", "Este es un bloque modded"
        // Russian: "Блок примера", "Это модифицированный блок"
        // Chinese: "例子块", "这是一个修改块"
        // Portuguese: "Bloco de exemplo", "Este é um bloco modded"
        // Polish: "Przykładowy blok", "Jest to modded blok"
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", ":3");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        // Here we will hide all tooltips whose title end with ':RemoveMe'
        // One like that is added at the start of this method
        foreach (var l in tooltips)
        {
            if (l.Name.EndsWith(":RemoveMe"))
            {
                l.Hide();
            }
        }
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<TransOrePlaced>());
        Item.width = 12;
        Item.height = 12;
        Item.rare = ItemRarityID.Blue;
        Item.value = 1590;
    }
}