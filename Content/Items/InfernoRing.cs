using System;
using System.Collections.Generic;
using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items;

// This is the item that summons ExampleSentry.
public class InfernoRing : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.GamepadWholeScreenUseRange[Type] = true;
        ItemID.Sets.LockOnIgnoresCollision[Type] = true;
    }

    public override void SetDefaults()
    {
        Item.damage = 95;
        Item.DamageType = DamageClass.Summon;
        Item.sentry = true;
        Item.mana = 10;
        Item.width = 26;
        Item.height = 28;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.knockBack = 6.5f;
        Item.ArmorPenetration = 150;
        Item.value = Item.buyPrice(gold: 720);
        Item.rare = ItemRarityID.Red;
        Item.UseSound = SoundID.Item99;
        Item.shoot = ModContent.ProjectileType<InfernoRingProj>();
        if (ModLoader.TryGetMod("CalamityMod", out Mod Cal2Merica))
        {
            Item.damage = 126;
        }
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Summons the Inferno Ring, which incinerates absolutely everything and shoots a powerful homing meteor");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Takes up 5 sentry slots")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<RingOfFire>();
        recipe.AddIngredient<FissionDrive>(3);
        recipe.AddIngredient(ItemID.LivingFireBlock, 72);

        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();

        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind("NightmareFuel", out ModItem NightmareFuel) && CalMerica.TryFind("CosmiliteBar", out ModItem CosmiliteBar))
        {
            recipe.AddIngredient(CosmiliteBar.Type, 6);
            recipe.AddIngredient(NightmareFuel.Type, 15);
        }

        if (!ModLoader.TryGetMod("CalamityMod", out Mod Cal2Merica))
        {
            recipe.AddIngredient(ItemID.LunarBar, 9);
        }
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        position = Main.MouseWorld;
        player.LimitPointToPlayerReachableArea(ref position);
        int halfProjectileHeight = (int)Math.Ceiling(ContentSamples.ProjectilesByType[type].height / 2f);
        position.Y -= halfProjectileHeight; // Adjust in-air option to spawn with bottom at cursor.

        if (player.maxTurrets >= 5)
        {
            // Spawn the sentry projectile at the calculated location.
            Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, knockback, Main.myPlayer);
        }

        // Kills older sentry projectiles according to player.maxTurrets
        player.UpdateMaxTurrets();
        return false;
    }
}