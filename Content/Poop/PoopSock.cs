using Microsoft.Xna.Framework;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace HendecamMod.Content.Poop
{
    public class PoopSock : ModItem
    {
      

        public override void SetDefaults()
        {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.DefaultToWhip(ModContent.ProjectileType<SockPoop>(), 20, 2, 7);
            Item.rare = ItemRarityID.White;
            Item.damage = 11;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 2;
            Item.width = 14;
            Item.height = 14;
            Item.value = 1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "3 summon tag damage");
            tooltips.Add(line);
            line = new TooltipLine(Mod, "Face", "Makes the user stinky")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
            line = new TooltipLine(Mod, "Face", "'Represents the average Summoner player'")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);



            // Here we will hide all tooltips whose title end with ':RemoveMe'
            // One like that is added at the start of this method
            foreach (var l in tooltips)
            {
                if (l.Name.EndsWith(":RemoveMe"))
                {
                    l.Hide();
                }
            }

            // Another method of hiding can be done if you want to hide just one line.
            // tooltips.FirstOrDefault(x => x.Mod == "ExampleMod" && x.Name == "Verbose:RemoveMe")?.Hide();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
           
            player.AddBuff(BuffID.Stinky, 61);
           

            return true; 
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PoopBlock, 4);
            recipe.AddIngredient(ItemID.Silk, 4);
            recipe.Register();
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}