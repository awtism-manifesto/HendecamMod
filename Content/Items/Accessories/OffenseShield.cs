using HendecamMod.Content.Buffs;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Shield)] 
public class OffenseShield : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32; 
        Item.height = 32; 
        Item.rare = ItemRarityID.LightPurple;
        Item.value = 750000;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.defense = 5;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
       
        player.GetModPlayer<OffenseProc>().Offended = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.AvengerEmblem);
        recipe.AddIngredient<PanicShield>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind("CrawCarapace", out ModItem CrawCarapace))
        {
            recipe.AddIngredient(CrawCarapace.Type);
        }

    }
    public class OffenseProc : ModPlayer
    {
      
       

        public bool Offended;
       
        public override void ResetEffects()
        {
            Offended = false;
        }

      

        public override void OnHurt(Player.HurtInfo info)
        {
            if (!Offended)
                return;
            Player.AddBuff(BuffID.Panic, 480);

          
            Player.AddBuff(BuffType<ShieldOffense>(), 480);
           
           
        }
    }
}