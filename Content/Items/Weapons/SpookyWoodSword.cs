using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons;

public class SpookyWoodSword : ModItem
{
    

    public override void SetDefaults()
    {
        Item.damage = 105;
        Item.DamageType = DamageClass.Melee;
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 19;
        Item.useAnimation = 19;
        Item.scale = 1.15f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 7.5f;
        Item.value = 100000;
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<SpookySpawn>();
        Item.shootSpeed = 8.25f;
       
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        
        var line = new TooltipLine(Mod, "Face", "Inflicts Hellfire on hit");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Flings tons of hellfire sparks everywhere")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item13, player.position); // Adding second use sound to item 
        return true;
    }
   

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        
            target.AddBuff(BuffID.OnFire3, 480);
        
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SpookyWood, 150);

        recipe.AddTile<CobaltWorkBenchPlaced>();
        recipe.Register();
    }
}