using System.Collections.Generic;
using HendecamMod.Content.Projectiles;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class BugBuzz : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 0.995f;
        Item.rare = ItemRarityID.Lime; // The color that the item's name will be in-game.
        Item.value = 715000;
        Item.useTime = 1; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 6; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        Item.reuseDelay = 16;
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 54; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 2.25f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; 
        Item.shootSpeed = 8.5f; // The speed of the projectile (measured in pixels per frame.)
        Item.useAmmo = ItemID.WoodenArrow;
        Item.shoot = ModContent.ProjectileType<BugWave>();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.NPCDeath57, player.position);
        return true;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ModContent.ProjectileType<BugWave>();
        if (type == ModContent.ProjectileType<BugWave>())
        {
            damage = (int)(damage * 0.8f);
        }
    }

    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        return Main.rand.NextFloat() >= 0.5f;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Converts arrows into a tightly-packed burst of multiple soundwaves");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "50% chance to save arrows")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
        recipe.AddIngredient(ItemID.BeetleHusk, 8);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();

        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind("LivingShard", out ModItem LivingShard))
        {
            recipe.AddIngredient(LivingShard.Type, 4);
        }
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-16f, -1f);
    }
}