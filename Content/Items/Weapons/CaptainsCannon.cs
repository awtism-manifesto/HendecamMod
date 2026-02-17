using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons;

public class CaptainsCannon : ModItem
{
   

    public override void SetStaticDefaults()
    {
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
    }

    public override void SetDefaults()
    {
       
        Item.width = 104;
        Item.height = 70;
        Item.rare = ItemRarityID.Pink;
        Item.value = 825000;
        Item.useTime = 8;
        Item.useAnimation = 8;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Ranged;
        Item.damage = 35;
        Item.knockBack = 2.5f;
        Item.noMelee = true;
     
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 17f;
    }

    public override bool AltFunctionUse(Player player)
    {
        return true;
    }

    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
          
            Item.useTime = 39;
            Item.useAnimation = 39;
            Item.UseSound = SoundID.Item62;
            Item.shootSpeed = 17f;
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.None;
            Item.shoot = ModContent.ProjectileType<CaptainBall>();
          
        }
        else
        {
            Item.damage = 35;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.shootSpeed = 17f;
            Item.UseSound = SoundID.Item11;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
          
           
        }

        return base.CanUseItem(player);
    }
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        return Main.rand.NextFloat() >= 0.33f;
    }
   
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (player.altFunctionUse == 2)
        {
            type = ModContent.ProjectileType<CaptainBall>();
        }
        if (type == ModContent.ProjectileType<CaptainBall>())
        {
            damage = damage * 5;
        }

    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
       
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3.65f));
        newVelocity *= 1f - Main.rand.NextFloat(0.18f);


        Projectile.NewProjectileDirect(source, position, newVelocity, type, (int)(damage * 1f), knockback, player.whoAmI);
          
       
        return false;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Left click to rapidly shoot bullets");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Right click to shoot a powerful, piercing cannonball")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "33% chance to not consume ammo")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-5f, 2f);
    }
}