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

[AutoloadEquip(EquipType.Wings, EquipType.Body)]
public class ArchangelChestguard : ModItem
{
    public static readonly int AdditiveDamageBonus = 35;

    public override void SetStaticDefaults()
    {
        // Register wing stats - needed for flight time and basic functionality
        ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(333, 14.44f, 3.33f);
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
        player.setBonus = "Gives the player Archangel Wings, which increase ALL of the player's stats while airborne";
        player.GetModPlayer<ArchangelWings>().ArchWings = true;


        if (player.wings <= 0 && player.GetModPlayer<CelestialStarboardPlayer>().hasCelestialStarboard == false)
        {
            player.wingsLogic = Item.wingSlot;


        }
        player.wingTimeMax += 225;
        player.noFallDmg = true;
        player.jumpSpeedBoost += 1.25f;
        player.moveSpeed += 0.35f;
        player.wingRunAccelerationMult += 1.77f;
        player.wingAccRunSpeed += 1.77f;
    }

   
}

public class ArchangelWings : ModPlayer
{
    public bool ArchWings;
    private bool wasFlying = false;
    
    public override void ResetEffects()
    {
        ArchWings = false;
    }

    public override void PreUpdateMovement()
    {
        if (ArchWings)
        {
            Player.wingsLogic = EquipLoader.GetEquipSlot(Mod, "ArchangelChestguard", EquipType.Wings);
        }

        var starboardPlayer = Player.GetModPlayer<CelestialStarboardPlayer>();

        // Only apply custom flight if ALL conditions are met:
        // 1. ArchWings is true
        // 2. No Celestial Starboard
        // 3. Has flight time remaining (wingTime > 0)
        if (ArchWings && !starboardPlayer.hasCelestialStarboard)
        {
            // Check if player is actively flying (holding jump/up AND has flight time)
            if ((Player.controlJump) && Player.wingTime > 0)
            {
                wasFlying = true;

                // Override vertical speed when holding UP or JUMP
                if ( Player.controlJump)
                {
                    float boostStrength = 7.77f;
                    Player.velocity.Y -= boostStrength;

                    // Set our own high speed cap
                    float maxUpwardSpeed = 100f;
                    if (Player.velocity.Y < -maxUpwardSpeed)
                    {
                        Player.velocity.Y = -maxUpwardSpeed;
                    }
                }

                // Initial takeoff boost when just starting to fly
                // Only apply if we have flight time
                if (Player.velocity.Y == 0 && (Player.controlJump || Player.controlUp) && Player.wingTime > 0)
                {
                    Player.velocity.Y = -4f;
                }
            }
            // Handle slow fall/hover - also check wingTime > 0
            else if (wasFlying && Player.controlDown && Player.wingTime > 0)
            {
                // Slow fall/hover when holding DOWN
                if (Player.velocity.Y > 1f)
                {
                    Player.velocity.Y *= 0.95f;
                }
                else if (Player.velocity.Y < -1f)
                {
                    Player.velocity.Y += 0.5f;
                }
                else
                {
                    Player.velocity.Y = 0f;
                }
            }
            else
            {
                wasFlying = false;
            }

            // Cap maximum fall speed
            if (Player.velocity.Y > 18f)
            {
                Player.velocity.Y = 18f;
            }
        }
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
            Player.manaRegen += (int)(Player.manaRegen * 1.67f);

            var loboPlayer = Player.GetModPlayer<LobotometerPlayer>();
            loboPlayer.MaxBonus += 150f;
            loboPlayer.DecayRateMultiplier += 1.5f;
        }
    }

   

   

    public override void FrameEffects()
    {
        if (ArchWings)
        {
            Lighting.AddLight(Player.Center, 1.5f, 1.5f, 1.5f);
            Player.wings = EquipLoader.GetEquipSlot(Mod, "ArchangelChestguard", EquipType.Wings);

            // Spawn dust particles while flying
            if (Player.velocity.Y != 0 && Player.wingTime > 0)
            {
                if (Main.rand.NextBool(10))
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.GolfPaticle, 0, 0, 100, default);
                }
            }
        }
    }
}


public class CelestialStarboardPlayer : ModPlayer
{
    public bool hasCelestialStarboard = false;

    public override void ResetEffects()
    {
        hasCelestialStarboard = false;
    }

    public override void UpdateEquips()
    {

       
            for (int i = 1; i <= 9; i++)
            {
                if (Player.armor[i].type == ItemID.LongRainbowTrailWings)
                {
                    hasCelestialStarboard = true;
                    break;
                }
            }
        
    }
}