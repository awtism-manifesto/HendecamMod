using System.Collections.Generic;
using HendecamMod.Content.Items.Weapons;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class HeavyDutyBoots : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 500);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Grants immunity to Mighty Wind");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.WindPushed] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<MiniThrowableBoulder>();
        recipe.AddIngredient(ItemID.SpiderFang, 2);
        recipe.AddIngredient(ItemID.Spike, 3);
        recipe.AddIngredient(ItemID.RocketBoots);
        recipe.AddIngredient(ItemID.HeavyWorkBench);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}