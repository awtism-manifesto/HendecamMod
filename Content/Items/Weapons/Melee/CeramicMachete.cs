using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Melee;

public class CeramicMachete : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 14;
        Item.useAnimation = 14;
        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 31;
        Item.knockBack = 1.5f;
        Item.scale = 1.15f;
        Item.value = Item.buyPrice(gold: 5);
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item1;
        Item.shootSpeed = 5.67f;
        Item.shoot = ProjectileType<CeramicScrap>(); 
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
       int NumProjectiles = Main.rand.Next(2, 4); 
        damage = (int)(damage * 0.35f);
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

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<CeramicSheet>(25);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}