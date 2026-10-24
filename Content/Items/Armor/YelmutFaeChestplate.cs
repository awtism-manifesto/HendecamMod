using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria.DataStructures;

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
        loboPlayer.TemporaryBonus += 80f;

        player.GetAttackSpeed(DamageClass.Melee) += MeleeAttackSpeedBonus / 100f;
        player.GetDamage(DamageClass.Ranged) += AdditiveDamageBonus / 100f;

        player.statManaMax2 += 80;
        player.maxMinions += 1;
        player.maxTurrets += 2;

        player.GetModPlayer<YelmutStupidPixieWings>().YelWings = true;
        player.wingsLogic = Item.wingSlot;
            player.wingTimeMax += 150;
            player.noFallDmg = true;
        player.jumpSpeedBoost += 0.67f;
        player.moveSpeed += 0.1f;
        player.wingRunAccelerationMult += 1.5f;
        player.wingAccRunSpeed += 1.5f;
    

    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ItemType<YelmutsHelmet>() &&
               legs.type == ItemType<YelmutLeggings>();
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
            Player.AddBuff(BuffType<OtherworldPoisoning>(), 67);
            Player.wings = EquipLoader.GetEquipSlot(Mod, "YelmutFaeChestplate", EquipType.Wings);
            if (Player.velocity.Y != 0)
            {
                if (Main.rand.NextBool(4))
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustType<LycopiteDust>(), 0, 0, 100, default);
                }
                if (Main.rand.NextBool(4))
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustType<PlutoniumDust>(), 0, 0, 100, Color.LightPink);
                }


            }
        }
    }
   


}