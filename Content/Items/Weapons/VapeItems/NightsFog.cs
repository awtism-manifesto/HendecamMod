using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items.VapeProjectiles;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using static HendecamMod.Content.Items.Accessories.IronLung;

namespace HendecamMod.Content.Items.Weapons.VapeItems;


public class NightsFog : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;

        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 10;
        Item.useAnimation = 30;
        Item.autoReuse = true;
        Item.reuseDelay = 13;
        Item.DamageType = ModContent.GetInstance<StupidDamage>();
        Item.damage = 32;
        Item.knockBack = 0.67f;
        Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
        Item.ArmorPenetration = 3;

        Item.value = Item.sellPrice(silver: 666);
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/VapeSound")
        {
            Volume = 2.67f,
            PitchVariance = 0.2f,
            MaxInstances = 3,
        };

        Item.shoot = ModContent.ProjectileType<FogSmoke>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 11.99f; // Speed of the projectiles the sword will shoot

       
    }
    public float LobotometerCost = 3f;
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
      
        const int NumProjectiles = 1; // The number of projectiles that this gun will shoot.
        if (player.GetModPlayer<IronLungPlayer>().IronLungs == false)
        {
            for (int i = 0; i < NumProjectiles; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(5.33f));

                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.215f);

                // Create a projectile.
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }

            return false; // Return false because we don't want tModLoader to shoot projectile
        }
        else {


            for (int i = 0; i < NumProjectiles; i++)
            {
               

                // Create a projectile.
                Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            }

            return false;
        
        
        }
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-1.83f, 6f);
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Uses 4 Lobotometer");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Vape smoke spawns extra vape smoke that blindsides enemies")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<ThePenjamin>();
        recipe.AddIngredient<DragonsBreath>();
        recipe.AddIngredient<CacklingCadaver>();
        recipe.AddIngredient<LungsBane>();
       
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind("PurifiedGel", out ModItem PurifiedGel))
        {
            recipe.AddIngredient(PurifiedGel.Type, 10);
        }
        recipe = CreateRecipe();
        recipe.AddIngredient<ThePenjamin>();
        recipe.AddIngredient<DragonsBreath>();
        recipe.AddIngredient<CacklingCadaver>();
        recipe.AddIngredient<PulmonaryHemorrhage>();

        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
        if (ModLoader.TryGetMod("CalamityMod", out Mod Cal2Merica) && Cal2Merica.TryFind("PurifiedGel", out ModItem PurifiedGel2))
        {
            recipe.AddIngredient(PurifiedGel2.Type, 10);
        }
    }
}