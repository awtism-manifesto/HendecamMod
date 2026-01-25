
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class MoldyCheese : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 4000);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Grants 50 Defense, Doubled Armor Penetraton, 50% more Generic Damage, and much higher jump speed"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "No longer gain effects from Minecarts, Weapon, Armor, or Accessory Buffs"));
    }
    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyPenetration>().NastyEffect = true;
        player.GetModPlayer<NastyDefense>().NastyEffect = true;
        player.GetModPlayer<NastyDamage>().NastyEffect = true;
        player.GetModPlayer<NastyJump>().NastyEffect = true;
        player.buffImmune[BuffID.TitaniumStorm] = true;
        player.buffImmune[BuffID.StardustGuardianMinion] = true;
        player.buffImmune[BuffID.ShadowDodge] = true;
        player.buffImmune[BuffID.BeetleEndurance1] = true;
        player.buffImmune[BuffID.BeetleEndurance2] = true;
        player.buffImmune[BuffID.BeetleEndurance3] = true;
        player.buffImmune[BuffID.BeetleMight1] = true;
        player.buffImmune[BuffID.BeetleMight2] = true;
        player.buffImmune[BuffID.BeetleMight3] = true;
        player.buffImmune[BuffID.SolarShield1] = true;
        player.buffImmune[BuffID.SolarShield2] = true;
        player.buffImmune[BuffID.SolarShield3] = true;
        player.buffImmune[BuffID.LeafCrystal] = true;
        player.buffImmune[BuffID.NebulaUpDmg1] = true;
        player.buffImmune[BuffID.NebulaUpDmg2] = true;
        player.buffImmune[BuffID.NebulaUpDmg3] = true;
        player.buffImmune[BuffID.NebulaUpLife1] = true;
        player.buffImmune[BuffID.NebulaUpLife2] = true;
        player.buffImmune[BuffID.NebulaUpLife3] = true;
        player.buffImmune[BuffID.NebulaUpMana1] = true;
        player.buffImmune[BuffID.NebulaUpMana2] = true;
        player.buffImmune[BuffID.NebulaUpMana3] = true;
        player.buffImmune[BuffID.BallistaPanic] = true;
        player.buffImmune[BuffID.RapidHealing] = true;
        player.buffImmune[BuffID.ParryDamageBuff] = true;
        player.buffImmune[BuffID.SoulDrain] = true;
        player.buffImmune[BuffID.HeartyMeal] = true;
        player.buffImmune[BuffID.CoolWhipPlayerBuff] = true;
        player.buffImmune[BuffID.ScytheWhipPlayerBuff] = true;
        player.buffImmune[BuffID.SwordWhipPlayerBuff] = true;
        player.buffImmune[BuffID.ThornWhipPlayerBuff] = true;
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
        recipe.AddIngredient<Disenchanter>(1);
        recipe.AddIngredient<Lunchly>(1);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}