using System.Collections.Generic;
using Microsoft.Xna.Framework;
using HendecamMod.Content.NPCs.Bosses;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Consumables
    {
    public class HeadOfCthulhuSummon : ModItem
        {
        public override void SetStaticDefaults()
            {
            Item.ResearchUnlockCount = 1;
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 13;
            }

        public override void SetDefaults()
            {
            Item.width = 32;
            Item.height = 28;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
            {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "Gaze into the night sky, angering a monstrosity that should've stayed on the other side of the moon");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "Summons the Head of Cthulhu, whose true power is released along with the ancient spirits of light and dark.")
                {
                OverrideColor = new Color(155, 0, 0)
                };
            tooltips.Add(line);
            }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
            {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossSpawners;
            }

        public override bool CanUseItem(Player player)
            {
            if (NPC.AnyNPCs(ModContent.NPCType<HeadOfCthulhu>()))
                return false;

            return true;

            return !NPC.AnyNPCs(ModContent.NPCType<HeadOfCthulhu>());
            }

        public override bool? UseItem(Player player)
            {
            if (player.whoAmI == Main.myPlayer)
                {
                SoundEngine.PlaySound(SoundID.Zombie92, player.position);
                int type = ModContent.NPCType<HeadOfCthulhu>();
                NPC.SpawnOnPlayer(player.whoAmI, type);
                return true;
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
                }
            return true;
            }


        public override void AddRecipes()
            {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Binoculars);
            recipe.AddIngredient(ItemID.SkullLantern);
            recipe.AddIngredient(ItemID.ShadowScale, 10);
            recipe.AddIngredient(ItemID.TissueSample, 10);
            recipe.AddIngredient(ItemID.WormTooth, 10);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();

            }
        }
    }