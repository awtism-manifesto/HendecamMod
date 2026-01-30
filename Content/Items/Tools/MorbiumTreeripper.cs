using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Rarities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Tools;

public class MorbiumTreeripper : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 144;
        Item.DamageType = DamageClass.Melee;
        Item.width = 50;
        Item.height = 50;
        Item.useTime = 4;
        Item.useAnimation = 12;
        Item.scale = 1.5f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 11.5f;
        Item.useTurn = true;

        Item.value = Item.buyPrice(gold: 67);
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item23;
        Item.autoReuse = true;
        Item.tileBoost = 3;


        Item.axe = 48;
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "");
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



        recipe.AddIngredient<MorbiumBar>(12);


        recipe.AddTile(TileID.MythrilAnvil);

        recipe.Register();

    }
}