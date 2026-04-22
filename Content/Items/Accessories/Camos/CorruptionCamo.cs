using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Accessories.Camos;

public class CorruptionCamo : ModItem
{
    
    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = 22000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Increases crit chance and attack speed by 10% and 5% and decreased aggro while in the Corruption");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

      

    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
      
        recipe.AddIngredient(ItemID.Ebonwood, 20);
        recipe.AddIngredient(ItemID.EbonstoneBlock, 10);
        recipe.AddIngredient(ItemID.ShadowScale, 5);
        recipe.AddIngredient(ItemID.Silk, 5);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.ZoneCorrupt)

        {
            player.GetModPlayer<CamoBoost>().isCamo = true;
        }
       
    }
}

public class CamoBoost : ModPlayer
{
  
    public static readonly int CamoBonus = 5;

    public bool isCamo;

    public override void ResetEffects()
    {
        isCamo = false;
    }


    public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
    {
        if (!isCamo)
        {
            return;
        }
        a = 128;
    }
    public override void PostUpdateEquips()
    {
        if (!isCamo)
        {
            return;
        }

       
        Player.GetAttackSpeed(DamageClass.Generic) += CamoBonus / 100f;
        Player.GetCritChance(DamageClass.Generic) += 10;
        Player.aggro -= 500;

    }
}