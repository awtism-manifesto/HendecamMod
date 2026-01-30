
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard))
public class TornSaddle : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Grants double your movement speed, at the cost of Mounts");
        tooltips.Add(line);
    }
    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyMovement>().NastyEffect = true;
        player.ClearBuff(BuffID.BasiliskMount);
        player.ClearBuff(BuffID.BeeMount);
        player.ClearBuff(BuffID.BunnyMount);
        player.ClearBuff(BuffID.CuteFishronMount);
        player.ClearBuff(BuffID.DarkHorseMount);
        player.ClearBuff(BuffID.DarkMageBookMount);
        player.ClearBuff(BuffID.DrillMount);
        player.ClearBuff(BuffID.GolfCartMount);
        player.ClearBuff(BuffID.LavaSharkMount);
        player.ClearBuff(BuffID.MajesticHorseMount);
        player.ClearBuff(BuffID.PaintedHorseMount);
        player.ClearBuff(BuffID.PigronMount);
        player.ClearBuff(BuffID.PirateShipMount);
        player.ClearBuff(BuffID.PogoStickMount);
        player.ClearBuff(BuffID.QueenSlimeMount);
        player.ClearBuff(BuffID.SantankMount);
        player.ClearBuff(BuffID.ScutlixMount);
        player.ClearBuff(BuffID.SlimeMount);
        player.ClearBuff(BuffID.SpookyWoodMount);
        player.ClearBuff(BuffID.TurtleMount);
        player.ClearBuff(BuffID.UFOMount);
        player.ClearBuff(BuffID.UnicornMount);
        player.ClearBuff(BuffID.WallOfFleshGoatMount);
        player.ClearBuff(BuffID.WolfMount);
        player.ClearBuff(BuffID.Flamingo);
        player.ClearBuff(BuffID.Rudolph);
        player.ClearBuff(BuffID.WitchBroom);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DarkHorseSaddle, 1);
        recipe.AddIngredient(ItemID.MajesticHorseSaddle, 1);
        recipe.AddIngredient(ItemID.PaintedHorseSaddle, 1);
        recipe.AddIngredient(ItemID.SlimySaddle, 1);
        recipe.AddIngredient(ItemID.HardySaddle, 1);
        recipe.AddTile(TileID.Sawmill);
        recipe.Register();
    }
}
