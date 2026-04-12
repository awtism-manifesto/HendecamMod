using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Poop;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class BulletBlade : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 48;
        Item.scale = 1.5f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.autoReuse = true;
        Item.DamageType = GetInstance<MeleeRangedDamage>();
        Item.damage = 51;
        Item.knockBack = 4;
        Item.value = 458000;
        Item.rare = ItemRarityID.Pink;
        Item.UseSound = SoundID.Item1;
        Item.useAmmo = AmmoID.Bullet;
        Item.shoot = ProjectileType<MinieProj>(); 
        Item.shootSpeed = 20f;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (type == ProjectileID.ChlorophyteBullet || type == ProjectileType<ChloroShit>())
        {
            damage = (int)(damage * 0.25f);
        }
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 30; 
        damage = (int)(damage * 0.48f);
        SoundEngine.PlaySound(SoundID.Item38, player.position);
        for (int i = 0; i < NumProjectiles; i++)
        {
            float rotation = MathHelper.ToRadians(i * 12f);
            Vector2 newVelocity = velocity.RotatedBy(rotation);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; 
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Shoots a ring of bullets all around the player")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Massively reduced damage with Chlorophyte Bullets")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'No gun? No problem!'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddRecipeGroup("IronBar", 20);
        recipe.AddIngredient(ItemID.SoulofMight, 10);
       
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind("BulletFilledShotgun", out ModItem BulletFilledShotgun))
        {
            recipe.AddIngredient(BulletFilledShotgun.Type);
        }
        if (!ModLoader.TryGetMod("CalamityMod", out Mod Cal2Merica) )
        {
            recipe.AddIngredient(ItemID.MusketBall, 100);
        }

    }
}