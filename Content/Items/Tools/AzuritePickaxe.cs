using HendecamMod.Content.Items.Placeables;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Tools;


public class AzuritePickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 36;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 14;
        Item.useAnimation = 21;
        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 11;
        Item.knockBack = 6;
        Item.ChangePlayerDirectionOnShoot = false;
        Item.pick = 105;


        Item.value = Item.buyPrice(gold: 5);
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item1;

    }



    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
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



        foreach (var l in tooltips)
        {
            if (l.Name.EndsWith(":RemoveMe"))
            {
                l.Hide();
            }
        }

    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AzuriteBar>(20);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

}
