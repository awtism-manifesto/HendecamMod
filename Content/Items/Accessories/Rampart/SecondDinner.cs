
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class SecondDinner : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 4000);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Face", "Grants immunity to Cursed, Mana Sickness, Suffocation,"));
        tooltips.Add(new TooltipLine(Mod, "Face", "Confused, Distorted, Slowness, Oozed, Peckish, Hungry,"));
        tooltips.Add(new TooltipLine(Mod, "Face", "Starving, Frostburn, Shadowflame, Hellfire, Cursed Inferno,"));
        tooltips.Add(new TooltipLine(Mod, "Face", "Chilled, Frozen, Frostbite, Obstructed, and Electrified"));
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
        recipe.AddIngredient<BackupPlan>(1);
        recipe.AddIngredient<Butterbrot>(1);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.Hellforge);
        recipe.Register();
    }
}