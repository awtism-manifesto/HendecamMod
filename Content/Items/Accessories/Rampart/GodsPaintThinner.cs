using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class GodsPaintThinner : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 8000);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Face", "Grants immunity to Penetrated, Blood Butchered, Midas,"));
        tooltips.Add(new TooltipLine(Mod, "Face", "Dryad's Bane, Betsy's Curse, Oiled, Daybroken, and Celled"));
        tooltips.Add(new TooltipLine(Mod, "Face", "Slimed, Sparkle Slime, Wet, Lovestruck, Stinky, Ichor, Webbed,"));
        tooltips.Add(new TooltipLine(Mod, "Face", "Webbed, Cerebral Mindtrick, Tipsy, the Water Candle,"));
        tooltips.Add(new TooltipLine(Mod, "Face", "Chaos State, Potion Sickness, Creative Shock, Might Wind,"));
        tooltips.Add(new TooltipLine(Mod, "Face", "Moon Bite, The Tongue, Shimmer, and the Shadow Candle"));
    }

    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.BoneJavelin] = true;
        player.buffImmune[BuffID.BloodButcherer] = true;
        player.buffImmune[BuffID.Midas] = true;
        player.buffImmune[BuffID.DryadsWardDebuff] = true;
        player.buffImmune[BuffID.BetsysCurse] = true;
        player.buffImmune[BuffID.Oiled] = true;
        player.buffImmune[BuffID.Daybreak] = true;
        player.buffImmune[BuffID.StardustMinionBleed] = true;
        player.buffImmune[BuffID.Slimed] = true;
        player.buffImmune[BuffID.GelBalloonBuff] = true;
        player.buffImmune[BuffID.Wet] = true;
        player.buffImmune[BuffID.Lovestruck] = true;
        player.buffImmune[BuffID.Stinky] = true;
        player.buffImmune[BuffID.Ichor] = true;
        player.buffImmune[BuffID.Webbed] = true;
        player.buffImmune[BuffID.BrainOfConfusionBuff] = true;
        player.buffImmune[BuffID.Tipsy] = true;
        player.buffImmune[BuffID.WaterCandle] = true;
        player.buffImmune[BuffID.ChaosState] = true;
        player.ClearBuff(BuffID.PotionSickness);
        player.buffImmune[BuffID.NoBuilding] = true;
        player.buffImmune[BuffID.WindPushed] = true;
        player.buffImmune[BuffID.MoonLeech] = true;
        player.buffImmune[BuffID.TheTongue] = true;
        player.buffImmune[BuffID.Shimmer] = true;
        player.buffImmune[BuffID.ShadowCandle] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<MagicSoap>();
        recipe.AddIngredient<ResearchChemicals>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AdamantiteForge);
        recipe.Register();
    }
}