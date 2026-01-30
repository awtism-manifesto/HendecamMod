using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class Disenchanter : ModItem
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
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Grants 50 Defense and double generic armor penetration"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "No longer gain effects from Weapon or Armor Buffs"));
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyPenetration>().NastyEffect = true;
        player.GetModPlayer<NastyDefense>().NastyEffect = true;
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
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<LeadPlating>();
        recipe.AddIngredient<Grindstone>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}