using System.Collections.Generic;

namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class PyriteHood : ModItem
{
    public static readonly int AdditiveMagicDamageBonus = 15;
    public static readonly int MaxManaIncrease = 70;
    public static readonly int MagicCritBonus = 7;

    public override void SetDefaults()
    {
        Item.defense = 3;
        Item.rare = ItemRarityID.Blue;
        Item.value = 54000;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PyriteBar>(10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void UpdateEquip(Player player)
    {
      
        player.GetCritChance(DamageClass.Magic) += MagicCritBonus;
        player.statManaMax2 += MaxManaIncrease;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "+70 max mana and +7% magic crit chance");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "15% increased magic damage";
        player.GetDamage(DamageClass.Magic) += AdditiveMagicDamageBonus / 100f;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<PyriteChestguard>() && legs.type == ModContent.ItemType<PyriteLegPlating>();
    }
}