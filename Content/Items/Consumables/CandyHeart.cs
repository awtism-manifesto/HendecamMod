using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Consumables
    {
    public class CandyHeart : ModItem
        {
        public override void SetStaticDefaults()
            {
            Item.ResearchUnlockCount = 20;

            // Dust that will appear in these colors when the item with ItemUseStyleID.DrinkLiquid is used
            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(92, 42, 9),
                new Color(51, 24, 6),
                new Color(122, 51, 4)
            };
            }

        public override void SetDefaults()
            {
            Item.width = 20;
            Item.height = 32;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.buyPrice(silver: 3);
            Item.buffType = BuffID.WellFed3;
            Item.buffTime = 300; // The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
            }
        public override Color? GetAlpha(Color lightColor)
            {
            return Color.White;
            }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
            {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "A small candy given by santa to his most loyal and skilled elf soldiers");
            tooltips.Add(line);
            var line2 = new TooltipLine(Mod, "Face", "Also just so happens to be DELICIOUS");
            tooltips.Add(line2);
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
        }
    }

