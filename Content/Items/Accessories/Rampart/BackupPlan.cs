
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class BackupPlan : ModItem
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
        tooltips.Add(new TooltipLine(Mod, "Face", "Grants immunity to Cursed, Mana Sickness, Suffocation, "));
        tooltips.Add(new TooltipLine(Mod, "Face", "Confused, Distorted, Slowness, and Oozed"));
    }
    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.Cursed] = true;
        player.buffImmune[BuffID.ManaSickness] = true;
        player.buffImmune[BuffID.Silenced] = true;
        player.buffImmune[BuffID.Suffocation] = true;
        player.buffImmune[BuffID.Confused] = true;
        player.buffImmune[BuffID.VortexDebuff] = true;
        player.buffImmune[BuffID.Slow] = true;
        player.buffImmune[BuffID.OgreSpit] = true;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.CountercurseMantra, 1);
        recipe.AddIngredient(ItemID.ThePlan, 1);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}