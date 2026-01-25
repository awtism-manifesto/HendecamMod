
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class GasMask : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Grants double the breath timer, at the cost of Envoirnmental Buffs");
        tooltips.Add(line);
        }
    public override void UpdateEquip(Player player)
        {
        player.GetModPlayer<NastyBreath>().NastyEffect = true;
        player.buffImmune[BuffID.Campfire] = true;
        player.buffImmune[BuffID.DryadsWard] = true;
        player.buffImmune[BuffID.Sunflower] = true;
        player.buffImmune[BuffID.HeartLamp] = true;
        player.buffImmune[BuffID.Honey] = true;
        player.buffImmune[BuffID.PeaceCandle] = true;
        player.buffImmune[BuffID.StarInBottle] = true;
        player.buffImmune[BuffID.CatBast] = true;
        player.buffImmune[BuffID.MonsterBanner] = true;
        }
    public override void AddRecipes()
        {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DivingHelmet, 1);
        recipe.AddIngredient<Rubber>(25);
        recipe.AddTile(TileID.Solidifier);
        recipe.Register();
        }
    }
