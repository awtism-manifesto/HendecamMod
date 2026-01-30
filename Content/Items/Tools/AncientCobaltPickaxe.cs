using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Tools;

public class AncientCobaltPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 36;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 12;
        Item.useAnimation = 21;
        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 11;
        Item.knockBack = 6;
        Item.ChangePlayerDirectionOnShoot = false;
        Item.pick = 60;
        Item.value = Item.buyPrice(gold: 5);
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item1;
        Item.useTurn = true;
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
        recipe.AddIngredient<AncientCobaltBar>(20);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}