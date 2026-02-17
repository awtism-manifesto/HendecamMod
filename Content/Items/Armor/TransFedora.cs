using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class TransFedora : ModItem
{
   
    public static readonly int StupidCritBonus = 9;

    public static LocalizedText SetBonusText { get; private set; }

    public override void SetStaticDefaults()
    {
        ArmorIDs.Head.Sets.IsTallHat[Item.headSlot] = true;
        ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
       
    }

    public override void SetDefaults()
    {
        Item.width = 22; // Width of the item
        Item.height = 18; // Height of the item
        Item.value = Item.sellPrice(gold: 3); // How many coins the item is worth
        Item.rare = ItemRarityID.Green; // The rarity of the item
        Item.defense = 4; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "15% faster Lobotometer decay rate");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "9% increased stupid critical strike")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);


    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<TransBodyplate>() && legs.type == ModContent.ItemType<TransGreaves>();
    }

    public override void UpdateEquip(Player player)
    {
        player.GetCritChance<StupidDamage>() += StupidCritBonus;

        var loboDecay = player.GetModPlayer<LobotometerPlayer>();
        loboDecay.DecayRateMultiplier *= 1.15f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<TransBar>(10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "+50 Max Lobotometer";
        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
        loboPlayer.MaxBonus += 50f; 
    }
}