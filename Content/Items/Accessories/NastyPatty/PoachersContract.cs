using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class PoachersContract : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Grants light, at the cost of Light Pets");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyLight>().NastyEffect = true;
        player.ClearBuff(BuffID.CrimsonHeart);
        player.ClearBuff(BuffID.FairyBlue);
        player.ClearBuff(BuffID.FairyGreen);
        player.ClearBuff(BuffID.FairyRed);
        player.ClearBuff(BuffID.FairyQueenPet);
        player.ClearBuff(BuffID.PetDD2Ghost);
        player.ClearBuff(BuffID.PumpkingPet);
        player.ClearBuff(BuffID.MagicLantern);
        player.ClearBuff(BuffID.ShadowOrb);
        player.ClearBuff(BuffID.SuspiciousTentacle);
        player.ClearBuff(BuffID.GolemPet);
        player.ClearBuff(BuffID.Wisp);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SoulofLight, 50);
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.Bookcases);
        recipe.Register();
    }
}