using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class Butterbrot : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 2000);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Face", "Grants immunity to Peckish, Hungry, Starving, Frostburn,"));
        tooltips.Add(new TooltipLine(Mod, "Face", "Shadowflame, Hellfire, Cursed Inferno, Chilled, Frozen,"));
        tooltips.Add(new TooltipLine(Mod, "Face", "Frostbite, Obstructed, and Electrified"));
    }

    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.NeutralHunger] = true;
        player.buffImmune[BuffID.Hunger] = true;
        player.buffImmune[BuffID.Starving] = true;
        player.buffImmune[BuffID.Frostburn] = true;
        player.buffImmune[BuffID.ShadowFlame] = true;
        player.buffImmune[BuffID.OnFire3] = true;
        player.buffImmune[BuffID.CursedInferno] = true;
        player.buffImmune[BuffID.Chilled] = true;
        player.buffImmune[BuffID.Frozen] = true;
        player.buffImmune[BuffID.Frostburn2] = true;
        player.buffImmune[BuffID.Obstructed] = true;
        player.buffImmune[BuffID.Electrified] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<SpiceKingStarterKit>();
        recipe.AddIngredient<Ushanka>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}