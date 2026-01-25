using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using HendecamMod.Content.Projectiles.Items;

namespace HendecamMod.Content.Items.Weapons;

public class SplashPotionOfShine : ModItem // MAG THE COMMENTS ON THIS FILE ARE NOT EXAMPLEMOD COMMENTS, DO NOT DELETE THEM
{
    public override void SetStaticDefaults() 
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 9; 
        Item.DamageType = DamageClass.Summon;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.width = 13;
        Item.height = 13;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; 
        Item.knockBack = 3.5f;
        Item.value = 125;
        Item.rare = ItemRarityID.White;
        Item.shoot = ModContent.ProjectileType<SplashPotionShine>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 8.25f; // The speed of the projectile.

       // if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))
       // {
         //   Item.DamageType = DamageClass.Throwing; // make throwing summon hybrid when thorium thrower multiclasses are implemented 
       // }
    }
   
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Throws a splash potion that lights up hit enemies and causes your summons to focus them");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "5 summon tag damage")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        

    
       // if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))
       // {

         //   tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "TerMerica Cross-Mod (Thorium): Now deals Throwing damage") { OverrideColor = Color.LightSeaGreen });
      //  }


    

       
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(25);
        recipe.AddIngredient(ItemID.ShinePotion);
       
        recipe.Register();
       

    }
}
