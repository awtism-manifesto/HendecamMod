using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Legs)]
public class PurifiedSaltLeggings : ModItem
{
    public static readonly int AdditiveStupidDamageBonus = 11;

    public static readonly int MoveSpeedBonus = 11;

    public static LocalizedText SetBonusText { get; private set; }

    

    public override void SetDefaults()
    {
        Item.width = 22; // Width of the item
        Item.height = 18; // Height of the item
        Item.value = Item.sellPrice(gold: 25); // How many coins the item is worth
        Item.rare = ItemRarityID.Yellow; // The rarity of the item
        Item.defense = 14; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "11% increased movement speed and stupid damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+25% Lobotometer decay rate and +2.5% max life")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<PurifiedSaltChestplate>() && head.type == ModContent.ItemType<PurifiedSaltFedora>();
    }

    public override void UpdateEquip(Player player)
    {
       
        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 100f;
      
        player.statLifeMax2 = (int)(player.statLifeMax2 * 1.025f);

        var loboDecay = player.GetModPlayer<LobotometerPlayer>();
        loboDecay.DecayRateMultiplier *= 1.25f;

        player.moveSpeed += MoveSpeedBonus / 100f;
        player.runAcceleration *= 1.11f;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        if (ModLoader.TryGetMod("TheConfectionRebirth", out Mod SweetMerica) && SweetMerica.TryFind("NeapoliniteBar", out ModItem NeapoliniteBar))
        {
            recipe.AddIngredient<RockSaltLeggings>();
            recipe.AddIngredient<PurifiedSalt>(72);
            recipe.AddIngredient(NeapoliniteBar.Type, 10);
            recipe.AddIngredient(ItemID.SpectreBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        else
        {
            recipe.AddIngredient<RockSaltLeggings>();
            recipe.AddIngredient<PurifiedSalt>(72);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddIngredient(ItemID.SpectreBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

    public override void UpdateArmorSet(Player player)
    {
    }

   
}