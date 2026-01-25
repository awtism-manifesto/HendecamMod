using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Placeables.Uno;

public class BlueDrawFour : ModItem
    {
    public override void SetStaticDefaults()
        {
        Item.ResearchUnlockCount = 25;
        ItemID.Sets.SortingPriorityMaterials[Type] = 2;
        Item.rare = ItemRarityID.Expert;
        }
    public override void SetDefaults()
        {
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Uno.BlueDrawFour>());
        Item.width = 20;
        Item.height = 20;
        Item.value = 1;
        }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
        var line = new TooltipLine(Mod, "Face", "Right Click to make it Red")
            {
            OverrideColor = new Color(255, 0, 0)
            };
        tooltips.Add(line);
        }
    public override bool CanRightClick()
        {
        return true;
        }
    public override void ModifyItemLoot(ItemLoot itemLoot)
        {
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<RedDrawFour>(), 1));
        }
    }
