using System.Collections.Generic;
using HendecamMod.Content.Projectiles;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class PykreteSword : ModItem
{
    private int shotCounter;

    public override void SetDefaults()
    {
        Item.damage = 14;
        Item.DamageType = DamageClass.Melee;
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.scale = 1.15f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 3.5f;
        Item.value = Item.buyPrice(copper: 300);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<FrostySpark>();
        Item.shootSpeed = 4.25f;
        Item.useTurn = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Has a chance to inflict frostburn");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Flings frostburn sparks all around the player")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        damage = (int)(damage * Main.rand.NextFloat(0.3f, 0.4f));
        if (shotCounter <= 0)
        {
            for (int i = 0; i < 4; i++)
            {
                float rotation = MathHelper.ToRadians(i * 90f);
                Vector2 newVelocity = velocity.RotatedBy(rotation);
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                shotCounter = 2;
            }
        }
        else if (shotCounter == 2)
        {
            for (int i = 0; i < 4; i++)
            {
                float rotation = MathHelper.ToRadians(45f + i * 90f);
                Vector2 newVelocity = velocity.RotatedBy(rotation);
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                shotCounter = 0;
            }
        }

        return false; // Return false because we don't want tModLoader to shoot projectile
    }

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Main.rand.NextBool(4))
        {
            target.AddBuff(BuffID.Frostburn, 150);
        }
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<Pykrete>(15);

        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}