using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class Lunchly : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 1000);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Grants 50% generic damage and much higher jump speed"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "No longer gain effects from Minecarts or Accessory Buffs"));
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyDamage>().NastyEffect = true;
        player.GetModPlayer<NastyJump>().NastyEffect = true;
        player.buffImmune[BuffID.MinecartLeft] = true;
        player.buffImmune[BuffID.MinecartRight] = true;
        player.buffImmune[BuffID.MinecartLeftMech] = true;
        player.buffImmune[BuffID.MinecartRightMech] = true;
        player.buffImmune[BuffID.MinecartLeftWood] = true;
        player.buffImmune[BuffID.MinecartRightWood] = true;
        player.buffImmune[BuffID.AmberMinecartLeft] = true;
        player.buffImmune[BuffID.AmberMinecartRight] = true;
        player.buffImmune[BuffID.AmethystMinecartLeft] = true;
        player.buffImmune[BuffID.AmethystMinecartRight] = true;
        player.buffImmune[BuffID.BeeMinecartLeft] = true;
        player.buffImmune[BuffID.BeeMinecartRight] = true;
        player.buffImmune[BuffID.BeetleMinecartLeft] = true;
        player.buffImmune[BuffID.BeetleMinecartRight] = true;
        player.buffImmune[BuffID.CoffinMinecartLeft] = true;
        player.buffImmune[BuffID.CoffinMinecartRight] = true;
        player.buffImmune[BuffID.DesertMinecartLeft] = true;
        player.buffImmune[BuffID.DesertMinecartRight] = true;
        player.buffImmune[BuffID.DiamondMinecartLeft] = true;
        player.buffImmune[BuffID.DiamondMinecartRight] = true;
        player.buffImmune[BuffID.DiggingMoleMinecartLeft] = true;
        player.buffImmune[BuffID.DiggingMoleMinecartRight] = true;
        player.buffImmune[BuffID.EmeraldMinecartLeft] = true;
        player.buffImmune[BuffID.EmeraldMinecartRight] = true;
        player.buffImmune[BuffID.FartMinecartLeft] = true;
        player.buffImmune[BuffID.FartMinecartRight] = true;
        player.buffImmune[BuffID.FishMinecartLeft] = true;
        player.buffImmune[BuffID.FishMinecartRight] = true;
        player.buffImmune[BuffID.HellMinecartLeft] = true;
        player.buffImmune[BuffID.HellMinecartRight] = true;
        player.buffImmune[BuffID.LadybugMinecartLeft] = true;
        player.buffImmune[BuffID.LadybugMinecartRight] = true;
        player.buffImmune[BuffID.MeowmereMinecartLeft] = true;
        player.buffImmune[BuffID.MeowmereMinecartRight] = true;
        player.buffImmune[BuffID.PartyMinecartLeft] = true;
        player.buffImmune[BuffID.PartyMinecartRight] = true;
        player.buffImmune[BuffID.PigronMinecartLeft] = true;
        player.buffImmune[BuffID.PigronMinecartRight] = true;
        player.buffImmune[BuffID.PirateMinecartLeft] = true;
        player.buffImmune[BuffID.PirateMinecartRight] = true;
        player.buffImmune[BuffID.RubyMinecartLeft] = true;
        player.buffImmune[BuffID.RubyMinecartRight] = true;
        player.buffImmune[BuffID.SapphireMinecartLeft] = true;
        player.buffImmune[BuffID.SapphireMinecartRight] = true;
        player.buffImmune[BuffID.ShroomMinecartLeft] = true;
        player.buffImmune[BuffID.ShroomMinecartRight] = true;
        player.buffImmune[BuffID.SteampunkMinecartLeft] = true;
        player.buffImmune[BuffID.SteampunkMinecartRight] = true;
        player.buffImmune[BuffID.SunflowerMinecartLeft] = true;
        player.buffImmune[BuffID.SunflowerMinecartRight] = true;
        player.buffImmune[BuffID.TerraFartMinecartLeft] = true;
        player.buffImmune[BuffID.TerraFartMinecartRight] = true;
        player.buffImmune[BuffID.TopazMinecartLeft] = true;
        player.buffImmune[BuffID.TopazMinecartRight] = true;
        player.buffImmune[BuffID.Werewolf] = true;
        player.buffImmune[BuffID.Merfolk] = true;
        player.buffImmune[BuffID.IceBarrier] = true;
        player.buffImmune[BuffID.PaladinsShield] = true;
        player.buffImmune[BuffID.Panic] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<NostalgicDiamond>();
        recipe.AddIngredient<AlphaMaleChecklist>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}