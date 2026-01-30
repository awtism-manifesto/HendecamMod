using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons;

public class GunThatKillsPeople : ModItem
{
    public override void SetDefaults()
    {

        Item.width = 62;
        Item.scale = 0.75f;
        Item.rare = ItemRarityID.Yellow;
        Item.value = 140000;
        Item.useTime = 6;
        Item.useAnimation = 6;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item11;
        Item.DamageType = DamageClass.Ranged;
        Item.damage = 59;
        Item.knockBack = 1.5f;
        Item.noMelee = true;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 11.1f;
        Item.useAmmo = AmmoID.Bullet;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 1;

        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3.33f));

            newVelocity *= 1f - Main.rand.NextFloat(0.3f);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false;
    }


    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
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
        recipe.AddIngredient<Items.Placeables.MorbiumBar>(10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();





    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ModContent.ProjectileType<Projectiles.MorbeamRanged>();
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-20f, -1f);
    }
}
