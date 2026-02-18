using System.Collections.Generic;

namespace HendecamMod.Content.Items.Materials;

public class AngelShard : ModItem
    {
    public override void SetStaticDefaults()
        {
        Item.ResearchUnlockCount = 25;
        }
    public override void SetDefaults()
        {
        Item.width = 44;
        Item.height = 50;
        Item.rare = ItemRarityID.LightRed;
        Item.value = 500;
        Item.maxStack = Item.CommonMaxStack;
        }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
        var line = new TooltipLine(Mod, "Face", "A fragment of the servants of god");
        tooltips.Add(line);
        }
    }