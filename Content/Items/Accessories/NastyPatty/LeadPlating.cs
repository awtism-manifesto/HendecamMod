using System.Collections.Generic;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class LeadPlating : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Grants 50 defense, at the cost of Armor Buffs");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
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
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.LeadBar, 150);
        recipe.AddTile(TileID.Hellforge);
        recipe.Register();
    }
}