using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Body)]
public class SuperCeramicChestplate : ModItem
{
    public static readonly int AdditiveStupidDamageBonus = 5;
    public static readonly int StupidArmorPenetration = 5;
   
    public override void SetDefaults()
    {
        Item.width = 32; // Width of the item
        Item.height = 28; // Height of the item
        Item.value = Item.sellPrice(gold: 13); // How many coins the item is worth
        Item.rare = ItemRarityID.LightRed; // The rarity of the item
        Item.defense = 15; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "5% increased stupid damage and +5 stupid armor penetration");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+65 max Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<SuperCeramicFedora>() && legs.type == ModContent.ItemType<SuperCeramicLeggings>();
    }

    public override void UpdateEquip(Player player)
    {
       
        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 100f;
        player.GetArmorPenetration<StupidDamage>() += StupidArmorPenetration;

        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
        loboPlayer.MaxBonus += 65f;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<CeramicSheet>(40);
        recipe.AddIngredient<EbonceramicSheet>(20);

        recipe.AddIngredient<PearlceramicSheet>(20);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient<CeramicSheet>(40);

        recipe.AddIngredient<CrimceramicSheet>(20);
        recipe.AddIngredient<PearlceramicSheet>(20);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
    }
}