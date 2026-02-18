using HendecamMod.Content.Buffs;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons;

public class CrucibleOfFlameAndSteel : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.GamepadWholeScreenUseRange[Type] = true;
        ItemID.Sets.LockOnIgnoresCollision[Type] = true;
    }

    public override void SetDefaults()
    {
        Item.damage = 236;
        Item.DamageType = DamageClass.Summon;
        Item.sentry = true;
       
        Item.width = 26;
        Item.height = 28;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.knockBack = 6.5f;
        Item.ArmorPenetration = 5;
        Item.value = Item.buyPrice(gold: 115);
        Item.rare = ItemRarityID.Red;
        Item.UseSound = SoundID.Item99;
        Item.shoot = ModContent.ProjectileType<TackParagon>();
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Summons The Crucible of Flame And Steel");
        tooltips.Add(line);


        line = new TooltipLine(Mod, "Face", "Right-click with the item in hand to activate Paragon Maelstrom")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Takes up 10 sentry slots")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<InfernoRing>();
        recipe.AddIngredient<SuperMaelstrom>();
        recipe.AddIngredient<TheTackZone>();

        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }
  
  
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {

        var CDPlayer = player.GetModPlayer<TackCD>();
        if (player.altFunctionUse == 2)
        {
            if (CDPlayer.TackStormCooldown > 0)
                return false;





            player.AddBuff(ModContent.BuffType<ParagonCooldown>(), 1200);
            CDPlayer.TackStormCooldown = 1200;

            SoundEngine.PlaySound(SoundID.Item62, player.position);
            SoundEngine.PlaySound(SoundID.NPCDeath56, player.position);
            CDPlayer.TackStormActive = true;

            return false;
        }
        else
        {

            position = Main.MouseWorld;
            player.LimitPointToPlayerReachableArea(ref position);
            int halfProjectileHeight = (int)Math.Ceiling(ContentSamples.ProjectilesByType[type].height / 2f);
            position.Y -= halfProjectileHeight; // Adjust in-air option to spawn with bottom at cursor.
                                                // Spawn the sentry projectile at the calculated location.
            if (player.maxTurrets >= 10)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, knockback, Main.myPlayer);
            }

            // Kills older sentry projectiles according to player.maxTurrets
            player.UpdateMaxTurrets();

            return false;
        }
    }
}
public class TackCD : ModPlayer
{
    public bool TackStormActive { get; set; } = false;
      public int TackStormCooldown { get; set; } = 0;

    public override void ResetEffects()
    {
        TackStormCooldown--;
        if (TackStormCooldown < 660)
        {
            TackStormActive = false;
        }
        
       
    }
}