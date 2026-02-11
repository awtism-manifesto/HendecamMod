using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class ContentEmblem : ModItem
{
   

   

    public override void SetStaticDefaults()
    {
       
    }

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Expert;

        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica))
        {
            Item.value += 425000;

        }
        if (ModLoader.TryGetMod("InfernumMode", out Mod Infernum))
        {

            Item.value += 250000;

        }
        if (ModLoader.TryGetMod("CatalystMod", out Mod CataMerica))
        {
            Item.value += 750000;
        }
        if (ModLoader.TryGetMod("FargowiltasSouls", out Mod FargoMerica))
        {
            Item.value += 130000;
        }
        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))
        {
            Item.value += 99000;
        }
        if (ModLoader.TryGetMod("ThoriumRework", out Mod ThorRework))
        {
            Item.value += 275000;
        }
        if (ModLoader.TryGetMod("Avalon", out Mod Avalon))
        {
            Item.value += 110000;
        }
        if (ModLoader.TryGetMod("SpiritMod", out Mod SpiritMerica))
        {
            Item.value += 150000;
        }
        if (ModLoader.TryGetMod("Consolaria", out Mod ConsMerica))
        {
            Item.value += 225000;
        }
        if (ModLoader.TryGetMod("Macrocosm", out Mod MacroMerica))
        {
            Item.value += 250000;
        }
        if (ModLoader.TryGetMod("AwfulGarbageMod", out Mod AwfulMerica))
        {
            Item.value += 5000;
        }
        if (ModLoader.TryGetMod("FishGunsPlus", out Mod Fish))
        {
            Item.value += 350000;
        }
        if (ModLoader.TryGetMod("RangerFlame", out Mod FireMerica))
        {
            Item.value += 125000;
        }
        if (ModLoader.TryGetMod("SOTS", out Mod SOTSMerica))
        { 
            Item.value += 350000; 
        }
        if (ModLoader.TryGetMod("Arsenal_Mod", out Mod Arse))
        {
            Item.value += 150000;
        }
        if (ModLoader.TryGetMod("Spooky", out Mod SpookMerica2))
        {
            Item.value += 105000;
        }
        if (ModLoader.TryGetMod("StarsAbove", out Mod Stars))
        {
            Item.value += 9500000;
        }
        if (ModLoader.TryGetMod("TerrorMod", out Mod Terror))
        {
           
                Item.value += 105000;
            
        }

        if (ModLoader.TryGetMod("Paracosm", out Mod ParaMerica))
        {
            Item.value += 950000;
        }
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Gives more bonuses the more content mods you have enabled");
        tooltips.Add(line);

        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica))
        {

            line = new TooltipLine(Mod, "Face", "Calamity Mod- +15% damage")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);

        }
        if (ModLoader.TryGetMod("InfernumMode", out Mod Infernum))
        {

            line = new TooltipLine(Mod, "Face", "Calamity Infernum- +10% attack speed during any bossfight or invasion")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);

        }
        if (ModLoader.TryGetMod("CatalystMod", out Mod CataMerica))
        {
            line = new TooltipLine(Mod, "Face", "Catalyst- +15% crit chance")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        if (ModLoader.TryGetMod("FargowiltasSouls", out Mod FargoMerica))
        {
            line = new TooltipLine(Mod, "Face", "Fargo's Souls- Causes friendly sand to rain down upon enemies near you at low health")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))
        {
            line = new TooltipLine(Mod, "Face", "Thorium Mod- +5 Armor Penetration")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        
        if (ModLoader.TryGetMod("ThoriumRework", out Mod ThorRework))
        {
            line = new TooltipLine(Mod, "Face", "Thorium Hellheim- +10% max life during any bossfight or invasion")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        if (ModLoader.TryGetMod("Avalon", out Mod Avalon))
        {
            line = new TooltipLine(Mod, "Face", "Exxo Avalon Origins- 20% increased damage reduction at full HP")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        if (ModLoader.TryGetMod("StarsAbove", out Mod Stars))
        {
            line = new TooltipLine(Mod, "Face", "The Stars Above- Increases max life by 2/3 of max mana")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        if (ModLoader.TryGetMod("SOTS", out Mod SOTSMerica))
        {
            line = new TooltipLine(Mod, "Face", "Secrets of the Shadows- Increased offensive stats while Invisibility buff is active")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        if (ModLoader.TryGetMod("SpiritMod", out Mod SpiritMerica))
        {
            line = new TooltipLine(Mod, "Face", "Spirit Classic- +1.5 hp/s life regen")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        if (ModLoader.TryGetMod("Consolaria", out Mod ConsMerica))
        {
            line = new TooltipLine(Mod, "Face", "Consolaria- Grants 'human joystick' movement")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        if (ModLoader.TryGetMod("Spooky", out Mod SpookMerica2))
        {
            line = new TooltipLine(Mod, "Face", "Spooky Mod- The player emits light above 80% life, grants 5 defense below 80% life")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        if (ModLoader.TryGetMod("Macrocosm", out Mod MacroMerica))
        {

            line = new TooltipLine(Mod, "Face", "Macrocosm- Increased flight time and aerial movement")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        if (ModLoader.TryGetMod("VitalityMod", out Mod Vital))
        {
            line = new TooltipLine(Mod, "Face", "Vitality Mod- Increases max health by 15%")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        if (ModLoader.TryGetMod("Arsenal_Mod", out Mod Arse))
        {
            line = new TooltipLine(Mod, "Face", "Arsenal- 5% increased attack speed")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        if (ModLoader.TryGetMod("TerrorMod", out Mod Terror))
        {
            line = new TooltipLine(Mod, "Face", "Terror Mode- +33% damage when under 33% health")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        if (ModLoader.TryGetMod("Paracosm", out Mod ParaMerica))
        {
            line = new TooltipLine(Mod, "Face", "Paracosm- Increases life regen by the number of debuffs you have")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        if (ModLoader.TryGetMod("FishGunsPlus", out Mod Fish))
        {
            line = new TooltipLine(Mod, "Face", "Fish Guns Plus- +25 fishing power")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        if (ModLoader.TryGetMod("RangerFlame", out Mod FireMerica))
        {
            line = new TooltipLine(Mod, "Face", "Flamethrowers Plus- 5% increased ranged crit chance")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
        if (ModLoader.TryGetMod("AwfulGarbageMod", out Mod AwfulMerica))
        {
            line = new TooltipLine(Mod, "Face", "Awful Garbage Mod- +50 max Lobotometer")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);

        }
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();


        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        if (ModLoader.TryGetMod("StarsAbove", out Mod StarMerica) && StarMerica.TryFind("TwinStars", out ModItem TwinStars))
        {
            recipe.AddIngredient(TwinStars.Type);

        }
        if (ModLoader.TryGetMod("ThoriumRework", out Mod ThorRework) && ThorRework.TryFind("BeholderBlade", out ModItem BeholderBlade))
        {
            recipe.AddIngredient(BeholderBlade.Type);
        }
        if (ModLoader.TryGetMod("InfernumMode", out Mod Infernum) && Infernum.TryFind("CallUponTheEggs", out ModItem CallUponTheEggs))
        {
            recipe.AddIngredient(CallUponTheEggs.Type);
        }
        if (ModLoader.TryGetMod("FargowiltasSouls", out Mod FargoMerica) && FargoMerica.TryFind("CoffinSummon", out ModItem CoffinSummon))
        {
            recipe.AddIngredient(CoffinSummon.Type);
        }
        if (ModLoader.TryGetMod("Arsenal_Mod", out Mod Arse) && Arse.TryFind("SlingerShooter", out ModItem SlingerShooter))
        {
            recipe.AddIngredient(SlingerShooter.Type);
        }
        if (ModLoader.TryGetMod("FishGunsPlus", out Mod Fish) && Fish.TryFind("BrokenFish", out ModItem BrokenFish))
        {
            recipe.AddIngredient(BrokenFish.Type);
        }
        if (ModLoader.TryGetMod("RangerFlame", out Mod FireMerica) && FireMerica.TryFind("ThrowerParts", out ModItem ThrowerParts))
        {
            recipe.AddIngredient(ThrowerParts.Type);
        }
        if (ModLoader.TryGetMod("Spooky", out Mod SpookMerica2) && SpookMerica2.TryFind("GhostPetItem", out ModItem GhostPetItem))
        {
            recipe.AddIngredient(GhostPetItem.Type);
        }
        if (ModLoader.TryGetMod("TerrorMod", out Mod Terror) && Terror.TryFind("UndeadAmulet", out ModItem UndeadAmulet))
        {
            recipe.AddIngredient(UndeadAmulet.Type);
        }
        if (ModLoader.TryGetMod("Paracosm", out Mod ParaMerica) && ParaMerica.TryFind("CorruptedDragonHeart", out ModItem CorruptedDragonHeart))
        {
            recipe.AddIngredient(CorruptedDragonHeart.Type);
        }
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind("AshesofCalamity", out ModItem AshesofCalamity))
        {
            recipe.AddIngredient(AshesofCalamity.Type, 10);
        }
       
        if (ModLoader.TryGetMod("CatalystMod", out Mod CataMerica) && CataMerica.TryFind("AstraJelly", out ModItem AstraJelly))
        {
            recipe.AddIngredient(AstraJelly.Type, 10);
        }
        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica) && ThorMerica.TryFind("ThoriumBar", out ModItem ThoriumBar))
        {
            recipe.AddIngredient(ThoriumBar.Type, 10);
        }
        if (ModLoader.TryGetMod("SpiritMod", out Mod SpiritMerica) && SpiritMerica.TryFind("SpiritBar", out ModItem SpiritBar))
        {
            recipe.AddIngredient(SpiritBar.Type, 10);
        }
       
        if (ModLoader.TryGetMod("Avalon", out Mod Avalon) && Avalon.TryFind("BacciliteBar", out ModItem BacciliteBar))
        {
            recipe.AddIngredient(BacciliteBar.Type, 10);
        }
        if (ModLoader.TryGetMod("Consolaria", out Mod ConsMerica) && ConsMerica.TryFind("SoulofBlight", out ModItem SoulofBlight))
        {
            recipe.AddIngredient(SoulofBlight.Type, 10);
        }
        if (ModLoader.TryGetMod("VitalityMod", out Mod Vital) && Vital.TryFind("SoulofVitality", out ModItem SoulofVitality))
        {
            recipe.AddIngredient(SoulofVitality.Type, 10);
        }
        if (ModLoader.TryGetMod("Macrocosm", out Mod MacroMerica) && MacroMerica.TryFind("Moonstone", out ModItem Moonstone))
        {
            recipe.AddIngredient(Moonstone.Type, 15);
        }
        if (ModLoader.TryGetMod("SOTS", out Mod SOTSMerica) && SOTSMerica.TryFind("PhaseOre", out ModItem PhaseOre))
        {
            recipe.AddIngredient(PhaseOre.Type, 50);
        }
        if (ModLoader.TryGetMod("AwfulGarbageMod", out Mod AwfulMerica) && AwfulMerica.TryFind("Garbage", out ModItem Garbage))
        {
            recipe.AddIngredient(Garbage.Type, 100);
        }
       
    }
    public static readonly int CalDamageBonus = 15;
    public static readonly int SweatyAttackSpeed = 10;
    public static readonly int ArseAttackSpeed = 5;
    public static readonly int SotsBonus = (int)7.5;
    public static readonly int TerrorDamageBonus = 33;
    public override void UpdateAccessory(Player player, bool hideVisual)
    {

        if (ModLoader.TryGetMod("SOTS", out Mod SOTSMerica))
        {
            if (player.HasBuff(BuffID.Invisibility))
            {
                player.GetDamage(DamageClass.Generic) += SotsBonus / 100f;
                player.GetAttackSpeed(DamageClass.Generic) += SotsBonus / 100f;
                player.GetCritChance(DamageClass.Generic) += 10;
                player.GetArmorPenetration(DamageClass.Generic) += 15;
            }
        }
        if (ModLoader.TryGetMod("FishGunsPlus", out Mod Fish))
        {
            player.fishingSkill += 25;
        }

        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica))
        {
            player.GetDamage(DamageClass.Generic) += CalDamageBonus / 100f;
           
        }
        if (ModLoader.TryGetMod("Arsenal_Mod", out Mod Arse))
        {
            player.GetAttackSpeed(DamageClass.Generic) += ArseAttackSpeed / 100f;
        }
        if (ModLoader.TryGetMod("RangerFlame", out Mod FireMerica))
        {
            player.GetCritChance(DamageClass.Ranged) += 5;
        }
        if (ModLoader.TryGetMod("CatalystMod", out Mod CataMerica))
        {
            player.GetCritChance(DamageClass.Generic) += 15;
        }
        if (ModLoader.TryGetMod("InfernumMode", out Mod Infernum))
        {
            if (NPC.AnyDanger())
            {
                player.GetAttackSpeed(DamageClass.Generic) += SweatyAttackSpeed / 100f;
            }
        }
        if (ModLoader.TryGetMod("VitalityMod", out Mod Vital))
        {
            player.statLifeMax2 += (int)(player.statLifeMax2 * 1.15f);
        }
        if (ModLoader.TryGetMod("Spooky", out Mod SpookMerica2))
        {
            if (player.statLife >= player.statLifeMax2 * 0.8)
            {
                Lighting.AddLight(player.Center, 1.5f, 1.5f, 1.5f);
            }
            else 
            {
                player.statDefense += 5;
            }
        }
        if (ModLoader.TryGetMod("TerrorMod", out Mod Terror))
        {
            if (player.statLife <= player.statLifeMax2 * 0.33)
            {
                player.GetDamage(DamageClass.Generic) += TerrorDamageBonus / 100f;
            }
           
        }
        if (ModLoader.TryGetMod("Paracosm", out Mod ParaMerica))
        {
            int debuffs = 0;

            for (int i = 0; i < Player.MaxBuffs; i++)
            {
                int buffType = player.buffType[i];

                if (buffType > 0 && Main.debuff[buffType] && buffType != BuffID.PotionSickness)
                {
                    debuffs++;
                }
            }

            player.lifeRegen += debuffs * 2;
        }
        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))
        {
            player.GetArmorPenetration(DamageClass.Generic) += 5;
        }
        if (ModLoader.TryGetMod("ThoriumRework", out Mod ThorRework))
        {
            if (NPC.AnyDanger()) 
            {
                player.statLifeMax2 += (int)(player.statLifeMax2 * 1.1f); 
            }
            
        }
        if (ModLoader.TryGetMod("FargowiltasSouls", out Mod FargoMerica))
        {
            player.GetModPlayer<FargoSandDrop>().Sanding = true;
        }
        if (ModLoader.TryGetMod("StarsAbove", out Mod Stars))
        {
            player.GetModPlayer<Starlife>().Starlifed = true;
        }
        if (ModLoader.TryGetMod("SpiritMod", out Mod SpiritMerica))
        {

            player.lifeRegen += 3;
        }
        if (ModLoader.TryGetMod("Avalon", out Mod Avalon))
        {
            if (player.statLife == player.statLifeMax2)
            {
                player.endurance = 1f - 0.8f * (1f - player.endurance);
            }
        }
        if (ModLoader.TryGetMod("Consolaria", out Mod ConsMerica))
        {
            player.GetModPlayer<JoystickMovement>().JoySticky = true;
        }
        if (ModLoader.TryGetMod("Macrocosm", out Mod MacroMerica))
        {
            player.wingTimeMax += 125;
            player.jumpSpeedBoost += 1.25f;
            player.maxFallSpeed = player.maxFallSpeed * 1.2f;
            player.wingRunAccelerationMult += 1.33f;
            player.wingAccRunSpeed += 1.33f;
        }
        if (ModLoader.TryGetMod("AwfulGarbageMod", out Mod AwfulMerica))
        {
            var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
            loboPlayer.MaxBonus += 50f;
        }
    }
}
public class JoystickMovement : ModPlayer
{
    public bool JoySticky = false;

    public override void ResetEffects()
    {
        JoySticky = false;
    }

    public override void PostUpdateRunSpeeds()
    {
        
        if (!JoySticky)
        {
            return;
        }

       
        Player.runAcceleration *= 3.33f; 
        Player.maxRunSpeed *= 1.15f;
        Player.accRunSpeed *= 1.15f;
        Player.runSlowdown *= 3.33f;
    }
}
public class FargoSandDrop : ModPlayer
{

    private const int SandUseTimeMax = 11;

    public bool Sanding;
    private int SandUseTime;

    public override void ResetEffects()
    {
        Sanding = false;
    }

    public override void PostUpdate()
    {
       
        if (SandUseTime > 0)
            SandUseTime--;
       

       
        if (!Sanding)
            return;

       
        if (SandUseTime > 0)
            return;

        int baseDamage = 25;

        if (Player.statLife <= Player.statLifeMax2 /4)
        {
            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
               Player.Center - new Vector2(Main.rand.Next(-90, 90), 910),
                new Vector2(Main.rand.Next(-2, 2), 20f),
                ModContent.ProjectileType<FargoSand>(),
                baseDamage,
                1.5f,
                Player.whoAmI
            );
        }
       
        SandUseTime = SandUseTimeMax;

    }


}
public class Starlife : ModPlayer
{
    public bool Starlifed;

    public override void ResetEffects()
    {
        Starlifed = false;
    }

    public override void ModifyMaxStats(out StatModifier health, out StatModifier mana)
    {
        health = StatModifier.Default;
        mana = StatModifier.Default;

        if (Starlifed)
        {
          

            float healthBonus = Player.statManaMax2 /1.5f;
            health.Flat += (int)healthBonus;
        }
    }
}