using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Ranger;

public class Boomshark : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 44; 
        Item.height = 18; 
        Item.scale = 1f;
        Item.rare = ItemRarityID.Orange;
        Item.value = 335000;
        Item.useTime = 10; 
        Item.useAnimation = 10; 
        Item.useStyle = ItemUseStyleID.Shoot; 
        Item.autoReuse = true;
        Item.UseSound = new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/HeavyShotgun")
        {
            Volume = 0.67f,
            Pitch = 0.5f,
            PitchVariance = 0.2f,
            MaxInstances = 10,
        };
        Item.ArmorPenetration = 2;
        Item.DamageType = DamageClass.Ranged; 
        Item.damage = 4; 
        Item.knockBack = 0.25f;
        Item.noMelee = true;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 7.15f; 
        Item.useAmmo = AmmoID.Bullet;
    }
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        return Main.rand.NextFloat() >= 0.33f;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int NumProjectiles = Main.rand.Next(3,5);
        damage = (int)(damage * 0.8f);
        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(14.25f));

            newVelocity *= 1f - Main.rand.NextFloat(0.35f);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; 
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "33% chance to not consume ammo");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'trust me, this is a WONDERFUL idea'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Boomstick);
        recipe.AddIngredient(ItemID.Minishark);
       
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-4f, -1f);
    }
}