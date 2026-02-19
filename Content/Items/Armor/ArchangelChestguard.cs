using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader.IO;


namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip( EquipType.Wings, EquipType.Body)]
public class ArchangelChestguard : ModItem
{
    public static readonly int AdditiveDamageBonus = 35;
   


    public override void SetStaticDefaults()
    {
       
      
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(225, 9.5f, 1.85f);
        ArmorIDs.Body.Sets.HidesTopSkin[Item.bodySlot] = true;
    }
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 28;
        Item.value = Item.sellPrice(gold: 777);
        Item.rare = ItemRarityID.Purple;
        Item.defense = 40;

       
    }
    
    public override void UpdateEquip(Player player)
    {
        

      
        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;

        player.statLifeMax2 = (int)(player.statLifeMax2 * 1.05) + 35;





    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
      
        tooltips.Add(new TooltipLine(Mod, "Face", "35% increased damage"));

        tooltips.Add(new TooltipLine(Mod, "Face", "Significantly boosts max life")
        {
            OverrideColor = new Color(255, 255, 255)
        });

       
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<ArchangelHelmet>() &&
               legs.type == ModContent.ItemType<ArchangelGreaves>();
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AngelShard>(14);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();

    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Gives the player Archangel Wings, which stack with other flight items and increase ALL of the player's stats while airborne";
        player.GetModPlayer<ArchangelWings>().ArchWings = true;
        player.wingsLogic = Item.wingSlot;
        player.wingTimeMax += 500;
        player.noFallDmg = true;
        player.jumpSpeedBoost += 1.25f;
        player.moveSpeed += 0.35f;
        player.wingRunAccelerationMult += 2.25f;
        player.wingAccRunSpeed += 2.25f;

    }
}
public class ArchangelWings : ModPlayer
{
    public bool ArchWings;

    public override void ResetEffects()
    {
        ArchWings = false;
    }

    public override void PostUpdateEquips()
    {
        if (ArchWings && Player.velocity.Y != 0)
        {
            Player.statManaMax2 += 150;
            Player.statLifeMax2 = (int)(Player.statLifeMax2 * 1.05f);
            Player.maxMinions += 1;
            Player.maxTurrets += 1;
            Player.GetDamage(DamageClass.Generic) += 0.1f;
            Player.GetAttackSpeed(DamageClass.Generic) += 0.1f;
            Player.GetCritChance(DamageClass.Generic) += 5;
            Player.GetArmorPenetration(DamageClass.Generic) += 5;
            Player.lifeRegen += 6;
            Player.statDefense += 7;
            Player.endurance = 1f - 0.95f * (1f - Player.endurance);
            Player.fishingSkill += 77;
            Player.runAcceleration *= 1.67f;
            Player.maxRunSpeed *= 1.15f;
            Player.accRunSpeed *= 1.15f;
            Player.runSlowdown *= 1.67f;
            Player.breathEffectiveness = Player.breathEffectiveness * 1.67f;
            Player.chloroAmmoCost80 = true;
            Player.pickSpeed += 0.01f;
            Player.luck += 0.77f;
            Player.maxFallSpeed = Player.maxFallSpeed * 1.11f;
            Player.manaRegen += (int)(Player.manaRegen *1.67f);
            var loboPlayer = Player.GetModPlayer<LobotometerPlayer>();
            loboPlayer.MaxBonus += 110f;
            loboPlayer.DecayRateMultiplier += 1.25f;
            
        }
    }
    public override void FrameEffects()
    {

        if (ArchWings)
        {
            Lighting.AddLight(Player.Center, 1.5f, 1.5f, 1.5f);
            Player.wings = EquipLoader.GetEquipSlot(Mod, "ArchangelChestguard", EquipType.Wings);
            if (Player.velocity.Y != 0 )
            {
                if (Main.rand.NextBool(10))
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.GolfPaticle, 0, 0, 100, default);
                }
               


            }
        }
    }
   


}