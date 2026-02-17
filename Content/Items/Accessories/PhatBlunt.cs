using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Beard)]
public class PhatBlunt : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 18;
        Item.value = Item.sellPrice(gold: 7);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "+1.5 hp/s life regen and +10% max life");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Slightly reduces underwater breath time")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Also looks really fucking dope")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.statLifeMax2 = (int)(player.statLifeMax2 * 1.1f);
        player.lifeRegen += 3;
        player.breathMax = 167;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<LuckyCigarette>();
        recipe.AddIngredient<WeedLeaves>(28);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}