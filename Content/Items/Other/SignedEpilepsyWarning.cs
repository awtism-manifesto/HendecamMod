using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Consumables;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Other;

public class SignedEpilepsyWarning : ModItem
{

    public override void SetStaticDefaults()
    {
        ItemID.Sets.ShimmerTransformToItem[Type] = ItemType<MonsterStemCells>();
    }

    public static readonly int AdditiveStupidDamageBonus = 6;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs();

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
       
        Item.rare = ItemRarityID.White;
        Item.value = 250000;
    }

   

    
}