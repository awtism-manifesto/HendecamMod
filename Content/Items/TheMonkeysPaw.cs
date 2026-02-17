using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class TheMonkeysPaw : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32; 
        Item.height = 32; 
        Item.scale = 1.4f;
        Item.rare = ItemRarityID.Blue; 
        Item.value = 10000;
        Item.useTime = 15; 
        Item.useAnimation = 15; 
        Item.useStyle = ItemUseStyleID.Swing; 
        Item.autoReuse = true; 
        Item.noUseGraphic = true;
        Item.UseSound = SoundID.Item1;
        Item.DamageType = DamageClass.Ranged; 
        Item.damage = 11; 
        Item.knockBack = 2f; 
        Item.noMelee = true;
        Item.shootSpeed = 16.5f; 
        Item.useAmmo = AmmoID.Dart;
        Item.shoot = ProjectileID.PoisonDart;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int NumProjectiles = Main.rand.Next(0, 3); 
        damage = (int)(damage * Main.rand.NextFloat(0.775f, 1.595f));
        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3.5f));

            newVelocity *= 1f - Main.rand.NextFloat(0.47f);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; 
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Throws between 0-3 darts that vary wildly in damage and velocity");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'some wishes are better left ungranted...'")
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

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-6f, -1f);
    }
}