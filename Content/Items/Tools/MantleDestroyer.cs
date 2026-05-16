using System.Collections.Generic;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Items.Placeables;

namespace HendecamMod.Content.Items.Tools;

public class MantleDestroyer : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 39;
        Item.DamageType = DamageClass.Melee;
        Item.width = 60;
        Item.height = 60;
        Item.useTime = 12;
        Item.useAnimation = 24;
        Item.scale = 1.2f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 6.75f;
        Item.useTurn = true;

        Item.value = 165000;
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost = 1;

        Item.hammer = 85;
       
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
        recipe.AddIngredient<MantiusBar>(14);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}