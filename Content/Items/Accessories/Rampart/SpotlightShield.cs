using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class SpotlightShield : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.defense = 4;
        Item.value = Item.sellPrice(silver: 2000);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Grants immunity to Burning, OnFire, Dazed, Blackout, Darkness, Stoned, Horrified, and Knockback");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.Blackout] = true;
        player.buffImmune[BuffID.Darkness] = true;
        player.buffImmune[BuffID.Stoned] = true;
        player.buffImmune[BuffID.Horrified] = true;
        player.buffImmune[BuffID.Burning] = true;
        player.buffImmune[BuffID.OnFire] = true;
        player.buffImmune[BuffID.Dazed] = true;
        player.fireWalk = true;
        player.noKnockback = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.ObsidianShield);
        recipe.AddIngredient(ItemID.ReflectiveShades);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}