using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Body)]
public class PlasticChestplate : ModItem
{
    public static readonly int AdditiveStupidDamageBonus = 2;
    
    public static readonly int StupidCritBonus = 2;
    public static LocalizedText SetBonusText { get; private set; }

  

    public override void SetDefaults()
    {
        Item.width = 32; // Width of the item
        Item.height = 28; // Height of the item
        Item.value = 21250;
        Item.rare = ItemRarityID.Blue; // The rarity of the item
        Item.defense = 3; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "2% increased stupid damage and crit chance");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+20% Lobotometer decay rate")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<PlasticHeadgear>() && legs.type == ModContent.ItemType<PlasticPants>();
    }

    public override void UpdateEquip(Player player)
    {
       
        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 100f;

        player.GetCritChance<StupidDamage>() += StupidCritBonus;

        var loboDecay = player.GetModPlayer<LobotometerPlayer>();
        loboDecay.DecayRateMultiplier *= 1.2f;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PlasticScrap>(35);
        recipe.AddIngredient<Polymer>(7);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {



    }
}