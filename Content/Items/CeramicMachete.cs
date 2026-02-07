using System.Collections.Generic;
using HendecamMod.Content.Projectiles;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class CeramicMachete : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 12;
        Item.useAnimation = 12;
        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 31;
        Item.knockBack = 1.5f;

        Item.value = Item.buyPrice(gold: 5);
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item1;

        Item.shoot = ModContent.ProjectileType<CeramicScrap>(); 
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 3; 
        damage = (int)(damage * Main.rand.NextFloat(0.48f, 0.51f));
        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(25));

            newVelocity *= 1f - Main.rand.NextFloat(0.4f);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; 
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Pieces of ceramic chip off every time you swing it")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        foreach (var l in tooltips)
        {
            if (l.Name.EndsWith(":RemoveMe"))
            {
                l.Hide();
            }
        }
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<CeramicSheet>(25);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}