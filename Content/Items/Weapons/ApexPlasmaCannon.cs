using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons;

public class ApexPlasmaCannon : ModItem
{
    private int shotCounter;

    public override void SetStaticDefaults()
    {
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
    }

    public override void SetDefaults()
    {
        // shoutouts to manifesto for this weapon
        // thanks river shoutout to you for the boss it drops from
        Item.width = 104;
        Item.height = 70;
        Item.rare = ItemRarityID.Cyan;
        Item.value = 635000;
        Item.useTime = 5;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.autoReuse = true;
        Item.DamageType = ModContent.GetInstance<RangedMagicDamage>();
        Item.damage = 69;
        Item.knockBack = 2.5f;
        Item.noMelee = true;
        Item.ArmorPenetration = 15;
        Item.mana = 8;
        Item.shoot = ModContent.ProjectileType<ApexPlasmaBullet>();
        Item.shootSpeed = 15.95f;
    }

    public override bool AltFunctionUse(Player player)
    {
        return true;
    }

    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            Item.damage = 120;
            Item.useTime = 4;
            Item.useAnimation = 28;
            Item.reuseDelay = 44;
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.Rocket;
            Item.shoot = ModContent.ProjectileType<ElfMagicMissile>();
            Item.mana = 20;
        }
        else
        {
            Item.damage = 69;
            Item.useTime = 5;
            Item.useAnimation = 15;
            Item.reuseDelay = 0;
            Item.shoot = ModContent.ProjectileType<ApexPlasmaBullet>();
            Item.useAmmo = AmmoID.Bullet;
            Item.mana = 8;
        }

        return base.CanUseItem(player);
    }

    public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
    {
        scale = 0.725f;
        Texture2D texture = Terraria.GameContent.TextureAssets.Item[Item.type].Value;
        Vector2 position = Item.position - Main.screenPosition + new Vector2(Item.width / 2, Item.height - texture.Height * 0.5f);
        spriteBatch.Draw(texture, position, null, lightColor, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
        return false;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (player.altFunctionUse == 2)
        {
            type = ModContent.ProjectileType<ElfMagicMissile>();
        }
        else
        {
            type = ModContent.ProjectileType<ApexPlasmaBullet>();
        }
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        // golden sigma
        if (shotCounter <= 0)
        {
            Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(0f));

            SoundEngine.PlaySound(SoundID.Item42, player.position);
            SoundEngine.PlaySound(SoundID.Item99, player.position);
            SoundEngine.PlaySound(SoundID.Item114, player.position);
            Projectile.NewProjectileDirect(source, position, newVelocity, type, (int)(damage * 1f), knockback, player.whoAmI);
            shotCounter = 2;
        }
        else if (shotCounter == 2)
        {
            Vector2 new2Velocity = velocity.RotatedByRandom(MathHelper.ToRadians(1.33f));
            SoundEngine.PlaySound(SoundID.Item42, player.position);
            SoundEngine.PlaySound(SoundID.Item99, player.position);
            SoundEngine.PlaySound(SoundID.Item114, player.position);
            Projectile.NewProjectileDirect(source, position, new2Velocity, type, (int)(damage * 1f), knockback, player.whoAmI);
            shotCounter = 3;
        }

        else if (shotCounter == 3)
        {
            Vector2 new2Velocity = velocity.RotatedByRandom(MathHelper.ToRadians(3.25f));
            SoundEngine.PlaySound(SoundID.Item42, player.position);
            SoundEngine.PlaySound(SoundID.Item99, player.position);
            SoundEngine.PlaySound(SoundID.Item114, player.position);

            Projectile.NewProjectileDirect(source, position, new2Velocity, type, (int)(damage * 1f), knockback, player.whoAmI);
            shotCounter = 0;
        }

        return false;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Left click to rapidly cast Apex Plasma Bullets that pierce many enemies");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Right click to shoot a swarm of homing Elf Magic Missiles")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Requires either bullets or rockets based on firing mode")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-50f, -1.5f);
    }
}