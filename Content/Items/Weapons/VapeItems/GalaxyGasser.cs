using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles.Items.VapeProjectiles;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using static HendecamMod.Content.Items.Accessories.IronLung;

namespace HendecamMod.Content.Items.Weapons.VapeItems;


public class GalaxyGasser : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;

        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 8;
        Item.useAnimation = 24;
        Item.autoReuse = true;
        Item.reuseDelay = 1;
        Item.DamageType = GetInstance<StupidDamage>();
        Item.damage = 79;
        Item.knockBack = 0.4f;
        Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
       

        Item.value = Item.sellPrice(silver: 1200);
        Item.rare = ItemRarityID.Red;
        Item.UseSound = new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/VapeSound")
        {
            Volume = 2.67f,
            PitchVariance = 0.2f,
            MaxInstances = 3,
        };

        Item.shoot = ProjectileType<GalaxyVapeSmoke>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 19f; // Speed of the projectiles the sword will shoot

       
    }
    public float LobotometerCost = 5f;
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
  
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-1.83f, 6f);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();


        recipe.AddIngredient<FragmentFlatEarth>(12);
        recipe.AddIngredient<HeavyDutyVapeKit>();
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}