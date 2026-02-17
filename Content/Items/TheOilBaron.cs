using System.Collections.Generic;
using HendecamMod.Content.Projectiles;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class TheOilBaron : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 35;
        Item.useTime = 35;
        Item.damage = 79;
        Item.knockBack = 9.25f;
        Item.width = 40;
        Item.height = 40;
        Item.shootSpeed = 7.33f;
        Item.scale = 1.35f;
        Item.ArmorPenetration = 10;
        Item.UseSound = SoundID.Item1;
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.buyPrice(gold: 50); 
        Item.DamageType = DamageClass.Melee;
        Item.shoot = ModContent.ProjectileType<OilSwing>();
        Item.noMelee = true; 
        Item.shootsEveryUse = true; 
        Item.autoReuse = true;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
        Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
        NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.
        type = ModContent.ProjectileType<OilBall>(); // Create a projectile.
        Projectile.NewProjectileDirect(source, position, velocity * 1.667f, type, (int)(damage * 0.67f), knockback, player.whoAmI);

        return true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Flings a small oil ball that inflicts Oiled");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Melee hits on Oiled enemies cause fiery explosions that deal massive damage")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<WaterflameSword>();
        recipe.AddIngredient(ItemID.SoulofNight, 10);
        recipe.AddIngredient<RefinedOil>(50);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}