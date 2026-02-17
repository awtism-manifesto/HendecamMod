using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using static HendecamMod.Content.Items.Armor.YelmutLeggings;

namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip( EquipType.Wings, EquipType.Body)]
public class YelmutFaeChestplate : ModItem
{
    public static readonly int AdditiveDamageBonus = 15;
    public static readonly int MeleeAttackSpeedBonus = 12;


    public override void SetStaticDefaults()
    {
       
      
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(135, 7f, 1.33f);
        
    }
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 28;
        Item.value = Item.sellPrice(gold: 30);
        Item.rare = ItemRarityID.LightPurple;
        Item.defense = 22;

       
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

      
            player.wingsLogic = Item.wingSlot;
            player.wingTimeMax += 150;
            player.noFallDmg = true;
        player.jumpSpeedBoost += 0.67f;
        player.moveSpeed += 0.1f;
        player.wingRunAccelerationMult += 1.5f;
        player.wingAccRunSpeed += 1.5f;
    

    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
      
        tooltips.Add(new TooltipLine(Mod, "Face", "12% increased melee speed and 15% increased ranged damage"));

        tooltips.Add(new TooltipLine(Mod, "Face", "+80 max Mana and max Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        });

        tooltips.Add(new TooltipLine(Mod, "Face", "+1 max minions and +2 max sentries")
        {
            OverrideColor = new Color(255, 255, 255)
        });

        tooltips.Add(new TooltipLine(Mod, "Face", "Counts as wings, enhances other wings when combined")
        {
            OverrideColor = new Color(255, 255, 255)
        });

        tooltips.Add(new TooltipLine(Mod, "Face", "-Developer Item-")
        {
            OverrideColor = new Color(220, 40, 245)
        });
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<YelmutsHelmet>() &&
               legs.type == ModContent.ItemType<YelmutLeggings>();
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        if (ModLoader.TryGetMod("TheConfectionRebirth", out Mod SweetMerica) && SweetMerica.TryFind("NeapoliniteBar", out ModItem NeapoliniteBar))
        {
            recipe.AddIngredient<PoorMahogany>(40);
            recipe.AddIngredient(NeapoliniteBar.Type, 10);
            recipe.AddIngredient(ItemID.PalladiumBar, 10);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.SoulofFlight, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        else
        {
            recipe.AddIngredient<PoorMahogany>(40);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddIngredient(ItemID.PalladiumBar, 10);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.SoulofFlight, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Increases damage further the higher your max Mana and Lobotometer";
        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();


        player.GetModPlayer<YelmutStupidPixieWings>().YelWings = true;

        // float damageBonus = lobotometerPercent * 0.10f;
        float damageBonus = (loboPlayer.Max + player.statManaMax2) * 0.0003f;

        player.GetDamage(DamageClass.Generic) += damageBonus;

    }
}
public class YelmutStupidPixieWings: ModPlayer
{
    public bool YelWings;

    public override void ResetEffects()
    {
        YelWings = false;
    }
    public override void FrameEffects()
    {

        if (YelWings)
        {
            Player.wings = EquipLoader.GetEquipSlot(Mod, "YelmutFaeChestplate", EquipType.Wings);
            if (Player.velocity.Y != 0)
            {
                if (Main.rand.NextBool(4))
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<LycopiteDust>(), 0, 0, 100, default);
                }
                if (Main.rand.NextBool(4))
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<PlutoniumDust>(), 0, 0, 100, Color.LightPink);
                }


            }
        }
    }
   


}