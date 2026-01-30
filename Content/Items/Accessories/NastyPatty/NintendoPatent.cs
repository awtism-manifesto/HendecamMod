using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class NintendoPatent : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 500);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Grants 25% Crit Chance, at the cost of Summons");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyCrit>().NastyEffect = true;
        player.maxMinions = 0;
        player.ClearBuff(BuffID.BabyBird);
        player.ClearBuff(BuffID.BabySlime);
        player.ClearBuff(BuffID.DeadlySphere);
        player.ClearBuff(BuffID.StormTiger);
        player.ClearBuff(BuffID.Smolstar);
        player.ClearBuff(BuffID.FlinxMinion);
        player.ClearBuff(BuffID.HornetMinion);
        player.ClearBuff(BuffID.ImpMinion);
        player.ClearBuff(BuffID.PirateMinion);
        player.ClearBuff(BuffID.Pygmies);
        player.ClearBuff(BuffID.Ravens);
        player.ClearBuff(BuffID.BatOfLight);
        player.ClearBuff(BuffID.SharknadoMinion);
        player.ClearBuff(BuffID.SpiderMinion);
        player.ClearBuff(BuffID.StardustMinion);
        player.ClearBuff(BuffID.StardustDragonMinion);
        player.ClearBuff(BuffID.EmpressBlade);
        player.ClearBuff(BuffID.TwinEyesMinion);
        player.ClearBuff(BuffID.UFOMinion);
        player.ClearBuff(BuffID.VampireFrog);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GemBunnyDiamond, 5);
        recipe.AddIngredient(ItemID.PlumbersHat);
        recipe.AddIngredient(ItemID.PlumbersShirt);
        recipe.AddIngredient(ItemID.PlumbersPants);
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.Hellforge);
        recipe.Register();
    }
}