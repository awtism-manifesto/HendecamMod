using System.Collections.Generic;

namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Legs)]
public class FlinxFurSlippers : ModItem
{
    public static readonly int AdditiveSummonDamageBonus = 4;

    public override void SetDefaults()
    {
        Item.defense = 1;
        Item.rare = ItemRarityID.Blue;
        Item.value = 165000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "4% increased summon damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void UpdateEquip(Player player)
    {
       
        player.GetDamage(DamageClass.Summon) += AdditiveSummonDamageBonus / 100f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FlinxFur, 3);
        recipe.AddTile(TileID.Loom);
        recipe.Register();
    }
}