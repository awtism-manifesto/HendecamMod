using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;


[AutoloadEquip(EquipType.Body)]
public class YelmutFaeChestplate : ModItem
{
   
    public static readonly int AdditiveDamageBonus = 15;
   
   
    public static readonly int MeleeAttackSpeedBonus = 12;


   

    public override void SetDefaults()
    {
        Item.width = 32; // Width of the item
        Item.height = 28; // Height of the item
        Item.value = Item.sellPrice(gold: 30); // How many coins the item is worth
        Item.rare = ItemRarityID.LightPurple; // The rarity of the item
        Item.defense = 22; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "12% increased melee speed and 15% increased ranged damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+80 max Mana and max Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "+1 max minions and +2 max sentries")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Counts as wings, enhances other wings when combined")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);



        line = new TooltipLine(Mod, "Face", "-Developer Item-")
        {
            OverrideColor = new Color(220, 40, 245)
        };
        tooltips.Add(line);

    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<YelmutsHelmet>() && legs.type == ModContent.ItemType<YelmutLeggings>();
    }

    public override void UpdateEquip(Player player)
    {
       


        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
        loboPlayer.MaxBonus += 80f;

        player.GetAttackSpeed(DamageClass.Melee) += MeleeAttackSpeedBonus / 100f;

        player.GetDamage(DamageClass.Ranged) += AdditiveDamageBonus / 100f;

        player.statManaMax2 += 80;
        player.maxMinions += 1;
        player.maxTurrets += 2;
        if (player.wings <= 0)
        {
            player.wingsLogic = 34;
            player.wingTimeMax += 150;
            player.jumpSpeedBoost += 1.33f;
            player.moveSpeed += 0.1f;
            player.wingRunAccelerationMult += 2f;
            player.wingAccRunSpeed += 2f;
            player.noFallDmg = true;
        }
        else
        {
            player.wingTimeMax += 100;
            player.jumpSpeedBoost += 0.5f;
            player.moveSpeed += 0.1f;
            player.wingRunAccelerationMult += 1.25f;
            player.wingAccRunSpeed += 1.25f;

        }
    }

   
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PoorMahogany>(40);
        recipe.AddIngredient(ItemID.HallowedBar, 10);
        recipe.AddIngredient(ItemID.PalladiumBar, 10);
        recipe.AddIngredient(ItemID.SoulofMight, 5);
        recipe.AddIngredient(ItemID.SoulofFright, 5);
        recipe.AddIngredient(ItemID.SoulofSight, 5);
        recipe.AddIngredient(ItemID.SoulofFlight, 5);
        recipe.AddTile(TileID.MythrilAnvil);

        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Increases damage further the higher your max Mana and Lobotometer";
        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();




        // float damageBonus = lobotometerPercent * 0.10f;
        float damageBonus = (loboPlayer.Max + player.statManaMax2) * 0.0003f;

        player.GetDamage(DamageClass.Generic) += damageBonus;

    }
}