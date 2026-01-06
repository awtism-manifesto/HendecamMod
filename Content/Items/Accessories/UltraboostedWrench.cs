using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Accessories
{
    public class UltraboostedWrench : ModItem
    {
      


        public override void SetDefaults()
        {
            Item.width = 45;
            Item.height = 30;
            Item.accessory = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 9599000;
            
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<OverclockedWrench>();
          

            recipe.AddIngredient<FissionDrive>();

            recipe.AddIngredient(ItemID.FragmentStardust, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "20% increased whip speed and movement speed");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "+4 max sentries")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "Does not stack with Overclocked Wrench")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);



           
        }
       
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
           

            player.GetModPlayer<Ultraboostified>().Ultraboosted = true;// Put all boosts in the ModPlayer below to toggle dynamically
        }
    }
    public class Ultraboostified : ModPlayer
    {
        public static readonly int MeleeAttackSpeedBonus = 20;
        public static readonly int MoveSpeedBonus = 20;
        public bool Ultraboosted = false;

        public override void ResetEffects()
        {
            Ultraboosted = false;
        }
        public override void PostUpdateRunSpeeds()
        {
            if (Player.GetModPlayer<Ultraboostified>().Ultraboosted == false) // Strongest boost takes priority, weaker boosts shouldn't prevent this
            {
                return;
            }
            if (Main.rand.NextBool(4)) // 1-in-3 chance every tick
            {
                int dust = Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric,
                    Player.velocity.X * Main.rand.NextFloat(-1.75f, 2.67f), Player.velocity.Y * Main.rand.NextFloat(-1.75f, 2.67f), 70, default, 0.82f);
                Main.dust[dust].noGravity = true;
            }
        }
        public override void PostUpdateEquips()
        {
            
            if (Player.GetModPlayer<Ultraboostified>().Ultraboosted == false) // Strongest boost takes priority, weaker boosts shouldn't prevent this
            {
                return;
            }
            else 
            {
                Player.maxTurrets += 4;
                Player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += MeleeAttackSpeedBonus / 121f;
                Player.runAcceleration *= 1.21f;
                Player.moveSpeed += MoveSpeedBonus / 121f;
            }

                
        }
    }
}