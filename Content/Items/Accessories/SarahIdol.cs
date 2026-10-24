using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

public class SarahIdol : ModItem
{
   

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.LightPurple;
        Item.value = 900000;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();


        recipe.AddIngredient(ItemID.Silk, 10);
        recipe.AddIngredient(ItemID.Amethyst, 5);
     
        recipe.AddIngredient(ItemID.SoulofLight, 5);
        recipe.AddIngredient(ItemID.SliceOfCake);

        recipe.AddTile(TileID.Loom);
        recipe.Register();
    }
    
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<Australian>().UpsideDown = true;

    }
}
public class Australian : ModPlayer
{



    public bool UpsideDown;


    public override void ResetEffects()
    {
        UpsideDown = false;
    }

    public override void PostUpdate()
    {
        if (UpsideDown)

        { 
            Player.gravDir = -1;
            
        }

      

    }


}