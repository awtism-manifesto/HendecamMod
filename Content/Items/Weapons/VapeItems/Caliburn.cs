using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles.Items.VapeProjectiles;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using static HendecamMod.Content.Items.Accessories.IronLung;

namespace HendecamMod.Content.Items.Weapons.VapeItems;


public class Caliburn : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;

        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 9;
        Item.useAnimation = 27;
        Item.autoReuse = true;
        Item.reuseDelay = 15;
        Item.DamageType = GetInstance<StupidDamage>();
        Item.damage = 50;
        Item.knockBack = 0.5f;
        Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
       

        Item.value = Item.sellPrice(silver: 932);
        Item.rare = ItemRarityID.Pink;
        Item.UseSound = new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/VapeSound")
        {
            Volume = 2.67f,
            PitchVariance = 0.2f,
            MaxInstances = 3,
        };

        Item.shoot = ProjectileType<CaliburnSmoke>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 16.67f; // Speed of the projectiles the sword will shoot

       
    }
    public float LobotometerCost = 4f;
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
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(5.22f));

                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.167f);

                // Create a projectile.
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }
            return false; // Return false because we don't want tModLoader to shoot projectile
        }
        else return true;
       
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Hitting enemies causes extra vape smoke to float upwards at them");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Uses 4 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-1.83f, 6f);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

       
        recipe.AddIngredient(ItemID.HallowedBar, 12);
        recipe.AddIngredient<VapeKit>();
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}