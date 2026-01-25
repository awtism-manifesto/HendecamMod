using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items;

public class FerrousThornSpiky : ModItem
{
    

    public override void SetDefaults()
    {
        // This method quickly sets the whip's properties.
        // Mouse over to see its parameters.
        Item.DefaultToWhip(ModContent.ProjectileType<FerroWhipSpiky>(), 33, 9, 5f, 33);
        Item.rare = ItemRarityID.LightRed;
        Item.damage = 72;
        Item.useTime = 33;
        Item.useAnimation = 33;
        Item.knockBack = 7f;
        Item.ArmorPenetration = 15;
        Item.width = 18;
        Item.height = 18;
        Item.value = 90000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "9 summon tag damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Right click in the inventory to swap variants")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Spiky variant: Higher direct damage and +15 armor penetration, but lower speed and tag damage")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        
    }
    public override bool CanRightClick()
    {
        return true;
    }
    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<FerrousThornSmooth>(), 1));
    }
    public override void AddRecipes()
    {
        Recipe


        recipe = CreateRecipe();
        recipe.AddRecipeGroup("IronBar", 15);
        recipe.AddIngredient<Items.CrudeOil>(35);
        recipe.AddIngredient<Items.RefinedOil>(35);
        recipe.AddTile(TileID.MythrilAnvil);

        recipe.Register();
       


    }

    // Makes the whip receive melee prefixes
    public override bool MeleePrefix()
    {
        return true;
    }
}