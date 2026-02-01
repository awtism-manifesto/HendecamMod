using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Beard)]
public class PhatBlunt : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 22; // Width of the item
        Item.height = 18; // Height of the item
        Item.value = Item.sellPrice(gold: 7); // How many coins the item is worth
        Item.rare = ItemRarityID.LightRed; // The rarity of the item
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
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
    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect

    public override void UpdateEquip(Player player)
    {
       
        player.statLifeMax2 = (int)(player.statLifeMax2 * 1.1f);
        player.lifeRegen += 3;
        player.breathMax = 167;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<LuckyCigarette>();
        recipe.AddIngredient<WeedLeaves>(28);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}