using System.Collections.Generic;

namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class FlinxFurEarmuffs : ModItem
{
    public static readonly int AdditiveSummonDamageBonus = 8;

    public override void SetDefaults()
    {
        Item.defense = 1;
        Item.rare = ItemRarityID.Blue;
        Item.value = 155000;
    }

    public override void UpdateEquip(Player player)
    {
        player.maxTurrets += 1;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "+1 sentry slot");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void SetStaticDefaults()
    {
        ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FlinxFur, 4);
        recipe.AddTile(TileID.Loom);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "8% increased summon damage";
        player.GetDamage(DamageClass.Summon) += AdditiveSummonDamageBonus / 100f;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ItemID.FlinxFurCoat && legs.type == ModContent.ItemType<FlinxFurSlippers>();
    }
}