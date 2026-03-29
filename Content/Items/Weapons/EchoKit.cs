using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Items.Armor;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace HendecamMod.Content.Items.Weapons;

// TODO: Properly reset flight time on grapple use

[AutoloadEquip(EquipType.Wings, EquipType.HandsOn, EquipType.HandsOff)]
public class EchoKit : ModItem
{
    private bool leftMouseHeld = false;
    private bool rightMouseHeld = false;


    public override void SetStaticDefaults()
    {
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        int wingSlot = EquipLoader.GetEquipSlot(Mod, "EchoKit", EquipType.Wings);
        ArmorIDs.Wing.Sets.Stats[wingSlot] = new WingStats(180, 10.5f, 2.5f);
       
    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 28;
        Item.value = 1320000;
        Item.rare = ItemRarityID.LightRed;

        Item.useTime = 21;
        Item.useAnimation = 21;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.autoReuse = true;
        Item.DamageType = ModContent.GetInstance<OmniDamage>();
        Item.damage = 33;
        Item.knockBack = 2.5f;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.shoot = ModContent.ProjectileType<TriShot>();
        Item.shootSpeed = 15.5f;
        Item.channel = true;
    }

    public override bool AltFunctionUse(Player player)
    {
        return true;
    }

    public override bool CanUseItem(Player player)
    {
        if (leftMouseHeld && rightMouseHeld)
            return false;

        EchoWings modPlayer = player.GetModPlayer<EchoWings>();

        // Safeguard: if counter is stuck but reuse delay is 0, reset the counter
        if (modPlayer.StickyBombCounter > 0 && modPlayer.StickyBombReuseDelay <= 0 && !modPlayer.StickyBurstInProgress)
        {
            modPlayer.StickyBombCounter = 0;
        }

        if (player.altFunctionUse == 2)
        {
            // Check if enough time has passed since last burst start
            int timeSinceLastBurst = (int)(Main.GameUpdateCount - modPlayer.LastBurstStartTime);

            // Block if:
            // - Burst is in progress
            // - On cooldown
            // - Counter is active
            // - Not enough time has passed since last burst start
            if (modPlayer.StickyBombReuseDelay > 0 ||
                modPlayer.StickyBombCounter > 0 ||
                modPlayer.StickyBurstInProgress ||
                timeSinceLastBurst < EchoWings.BURST_COOLDOWN)
                return false;

            Item.useTime = 5;
            Item.useAnimation = 30;
            Item.reuseDelay = 0;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<EchoStickyBomb>();
            Item.useAmmo = AmmoID.None;
        }
        else
        {
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.reuseDelay = 0;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<TriShot>();
            Item.useAmmo = AmmoID.None;
        }

        return base.CanUseItem(player);
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {

        tooltips.Add(new TooltipLine(Mod, "Face", "Left click to fire high velocity tri-shots"));

        tooltips.Add(new TooltipLine(Mod, "Face", "Right click to fire a volley of sticky bombs")
        {
            OverrideColor = new Color(255, 255, 255)
        });

        tooltips.Add(new TooltipLine(Mod, "Face", "Hold both left and right click to fire a focus beam that deals more damage to weakened enemies")
        {
            OverrideColor = new Color(255, 255, 255)
        });

        tooltips.Add(new TooltipLine(Mod, "Face", "Enables flight and slow fall while held")
        {
            OverrideColor = new Color(255, 255, 255)
        });

        tooltips.Add(new TooltipLine(Mod, "Face", "'I am always ready to learn'")
        {
            OverrideColor = new Color(151, 212, 255)
        });
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (leftMouseHeld && rightMouseHeld)
            return false;

        EchoWings modPlayer = player.GetModPlayer<EchoWings>();

        if (player.altFunctionUse == 2)
        {
            if (modPlayer.StickyBombReuseDelay > 0)
                return false;

            // Set burst start time
            modPlayer.LastBurstStartTime = (int)Main.GameUpdateCount;

            // Set burst in progress when first bomb is thrown
            modPlayer.StickyBurstInProgress = true;

            SoundEngine.PlaySound(SoundID.Item42, player.position);
            SoundEngine.PlaySound(SoundID.Item99, player.position);
            SoundEngine.PlaySound(SoundID.Item114, player.position);

            Projectile.NewProjectileDirect(
                source,
                position,
                velocity * 1.5f,
                type,
                (int)(damage * 0.5f),
                knockback,
                player.whoAmI
            );

            modPlayer.StickyBombCounter++;
            if (modPlayer.StickyBombCounter >= 6)
            {
                modPlayer.StickyBombReuseDelay = 67;
                modPlayer.StickyBombCounter = 0;
                modPlayer.StickyBurstInProgress = false; // Burst completed normally
            }
        }
        else
        {
            // Tri-shot code...
            SoundEngine.PlaySound(SoundID.Item42, player.position);
            SoundEngine.PlaySound(SoundID.Item99, player.position);
            SoundEngine.PlaySound(SoundID.Item114, player.position);

            Vector2 new2Velocity = velocity.RotatedBy(MathHelper.ToRadians(1.25f));
            Vector2 new3Velocity = velocity.RotatedBy(MathHelper.ToRadians(-1.25f));

            Projectile.NewProjectileDirect(source, position, velocity * 1.67f, type, (int)(damage * 1.15f), knockback, player.whoAmI);
            Projectile.NewProjectileDirect(source, position, new2Velocity * 1.33f, type, (int)(damage * 1.15f), knockback, player.whoAmI);
            Projectile.NewProjectileDirect(source, position, new3Velocity * 1.33f, type, (int)(damage * 1.15f), knockback, player.whoAmI);
        }

        return false;
    }

    public override void HoldItem(Player player)
    {
        leftMouseHeld = Main.mouseLeft;
        rightMouseHeld = Main.mouseRight;

        EchoWings modPlayer = player.GetModPlayer<EchoWings>();
        modPlayer.EchoKitted = true;

        // Speed bonuses applied every frame while holding
        player.noFallDmg = true;
        player.jumpSpeedBoost += 2f;
        player.moveSpeed += 0.1f;
        player.wingRunAccelerationMult += 1.25f;
        player.wingAccRunSpeed += 1.25f;
        player.lifeRegen += 1;

        // In HoldItem method, when Focus Beam is activated:
        if (leftMouseHeld && rightMouseHeld && player.whoAmI == Main.myPlayer)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<FocusBeam>()] < 1)
            {
                Vector2 direction = Vector2.Normalize(Main.MouseWorld - player.Center);
                SoundEngine.PlaySound(SoundID.Item15, player.position);

                Projectile.NewProjectile(
                    player.GetSource_ItemUse(Item),
                    player.Center,
                    direction,
                    ModContent.ProjectileType<FocusBeam>(),
                    Item.damage,
                    Item.knockBack,
                    player.whoAmI
                );

                // Reset ALL sticky bomb states when switching to focus beam
                modPlayer.StickyBombReuseDelay = 0;
                modPlayer.StickyBombCounter = 0;
                modPlayer.StickyBurstInProgress = false;
                modPlayer.LastBurstStartTime = 0; // Reset burst start time
            }

            player.itemAnimation = Math.Max(player.itemAnimation, 2);
            player.itemTime = Math.Max(player.itemTime, 2);
        }
    }

    public override void UpdateInventory(Player player)
    {
        if (player.HeldItem == Item)
            player.GetModPlayer<EchoWings>().EchoKitted = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<MintalBar>(15);
        recipe.AddIngredient<UraniumBar>(15);
       
        recipe.AddIngredient<Kevlar>(30);
        recipe.AddIngredient(ItemID.SoulofFlight, 30);
        recipe.AddIngredient<Polymer>(45);
        recipe.AddIngredient(ItemID.Wire, 90);
       
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}

public class EchoWings : ModPlayer
{
    public bool EchoKitted;
    public int StickyBombCounter;
    public int StickyBombReuseDelay;
    public bool StickyBurstInProgress;
    public const int BURST_COOLDOWN = 30; // Half second at 60fps
    public int LastBurstStartTime;

    private int _wingSlot = -1;
    private int _handsOnSlot = -1;
    private int _handsOffSlot = -1;
   

    private void EnsureSlots()
    {
        if (_wingSlot < 0)
            _wingSlot = EquipLoader.GetEquipSlot(Mod, "EchoKit", EquipType.Wings);
        if (_handsOnSlot < 0)
            _handsOnSlot = EquipLoader.GetEquipSlot(Mod, "EchoKit", EquipType.HandsOn);
        if (_handsOffSlot < 0)
            _handsOffSlot = EquipLoader.GetEquipSlot(Mod, "EchoKit", EquipType.HandsOff);
    }

    public override void ResetEffects()
    {
        EchoKitted = false;
    }

    public override void PreUpdate()
    {
        if (StickyBombReuseDelay > 0)
            StickyBombReuseDelay--;

        // Only auto-cancel burst if:
        // 1. A burst is actually in progress
        // 2. The player has released right-click
        // 3. We're not in the middle of the burst animation (use proper timing)
        if (StickyBurstInProgress && !Main.mouseRight)
        {
            // Don't cancel immediately - give a small buffer
            // This prevents cancelling between clicks when spamming
            if (StickyBombCounter > 0 && StickyBombCounter < 6)
            {
                // If they released early, apply a short penalty
                StickyBombReuseDelay = 33; // Short penalty for early release
            }

            StickyBurstInProgress = false;
            StickyBombCounter = 0;
        }
    }

    public override void UpdateEquips()
    {
        if (EchoKitted)
        {
            EnsureSlots();

            Player.wingsLogic = _wingSlot;
            Player.wings = _wingSlot;
            Player.handon = _handsOnSlot;
            Player.handoff = _handsOffSlot;
            Player.accRunSpeed = 8f;
            Player.wingRunAccelerationMult = 1.66f;

            // Reset flight time when on ground
            if (Player.velocity.Y == 0)
                Player.wingTime = 180;

            if (Player.velocity.Y != 0 && Main.rand.NextBool(4))
                Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric, 0, 0, 100, default, 0.8f);
        }
    }

    public override void PostUpdate()
    {
        
        if (EchoKitted && Player.grappling[0] > -1)
            Player.wingTime = 180;

      
    }

    public override void FrameEffects()
    {
        if (EchoKitted)
        {
            EnsureSlots();
            Player.wings = _wingSlot;
            Player.handon = _handsOnSlot;
            Player.handoff = _handsOffSlot;
        }
    }

   
}
