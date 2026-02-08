using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class BadGrades : ModItem
{
    public static readonly int AdditiveStupidDamageBonus = 6;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs();

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.White;
        Item.value = 67;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "6% increased stupid damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+30 max Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'The F students are inventors'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<Paper>();
        recipe.AddIngredient<BrainrotPotion>();
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 100f;

        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
        loboPlayer.MaxBonus += 30f;
    }
}