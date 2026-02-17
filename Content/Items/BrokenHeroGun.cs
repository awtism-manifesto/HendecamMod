using System.Collections.Generic;
using HendecamMod.Content.Projectiles;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class BrokenHeroGun : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 48;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.autoReuse = true;
        Item.scale = 1.33f;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 102;
        Item.knockBack = 3;
        Item.value = Item.buyPrice(gold: 5);
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<ScrapMetal>();
        Item.shootSpeed = 6.7f;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 12; 
        damage = (int)(damage * Main.rand.NextFloat(0.666f, 0.667f));
        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(360));

            newVelocity *= 1f - Main.rand.NextFloat(0.66f);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "It doesn't shoot anymore, but i guess you can hit things with it?");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Pieces of scrap metal flake off every time you swing it...")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BrokenHeroSword);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}