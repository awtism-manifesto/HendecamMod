
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty
    {
    //[AutoloadEquip(EquipType.Beard)]
    public class CeaseAndDesist : ModItem
        {
        public override void SetDefaults()
            {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(silver: 1000);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
            {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Grants 25% Crit Chance and double movement speed"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "No longer gain effects from Summons or Mounts"));
            }
        public override void UpdateEquip(Player player)
            {
            player.GetModPlayer<NastyCrit>().NastyEffect = true;
            player.GetModPlayer<NastyMovement>().NastyEffect = true;
            player.ClearBuff(BuffID.Flamingo);
            player.ClearBuff(BuffID.Rudolph);
            player.ClearBuff(BuffID.WitchBroom);
            player.maxMinions = 0;
            player.ClearBuff(BuffID.BabyBird);
            player.ClearBuff(BuffID.BabySlime);
            player.ClearBuff(BuffID.DeadlySphere);
            player.ClearBuff(BuffID.StormTiger);
            player.ClearBuff(BuffID.Smolstar);
            player.ClearBuff(BuffID.FlinxMinion);
            player.ClearBuff(BuffID.HornetMinion);
            player.ClearBuff(BuffID.ImpMinion);
            player.ClearBuff(BuffID.PirateMinion);
            player.ClearBuff(BuffID.Pygmies);
            player.ClearBuff(BuffID.Ravens);
            player.ClearBuff(BuffID.BatOfLight);
            player.ClearBuff(BuffID.SharknadoMinion);
            player.ClearBuff(BuffID.SpiderMinion);
            player.ClearBuff(BuffID.StardustMinion);
            player.ClearBuff(BuffID.StardustDragonMinion);
            player.ClearBuff(BuffID.EmpressBlade);
            player.ClearBuff(BuffID.TwinEyesMinion);
            player.ClearBuff(BuffID.UFOMinion);
            player.ClearBuff(BuffID.VampireFrog);
            player.ClearBuff(BuffID.BasiliskMount);
            player.ClearBuff(BuffID.BeeMount);
            player.ClearBuff(BuffID.BunnyMount);
            player.ClearBuff(BuffID.CuteFishronMount);
            player.ClearBuff(BuffID.DarkHorseMount);
            player.ClearBuff(BuffID.DarkMageBookMount);
            player.ClearBuff(BuffID.DrillMount);
            player.ClearBuff(BuffID.GolfCartMount);
            player.ClearBuff(BuffID.LavaSharkMount);
            player.ClearBuff(BuffID.MajesticHorseMount);
            player.ClearBuff(BuffID.PaintedHorseMount);
            player.ClearBuff(BuffID.PigronMount);
            player.ClearBuff(BuffID.PirateShipMount);
            player.ClearBuff(BuffID.PogoStickMount);
            player.ClearBuff(BuffID.QueenSlimeMount);
            player.ClearBuff(BuffID.SantankMount);
            player.ClearBuff(BuffID.ScutlixMount);
            player.ClearBuff(BuffID.SlimeMount);
            player.ClearBuff(BuffID.SpookyWoodMount);
            player.ClearBuff(BuffID.TurtleMount);
            player.ClearBuff(BuffID.UFOMount);
            player.ClearBuff(BuffID.UnicornMount);
            player.ClearBuff(BuffID.WallOfFleshGoatMount);
            player.ClearBuff(BuffID.WolfMount);
            }
        public override void AddRecipes()
            {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient<NintendoPatent>(1);
            recipe.AddIngredient<TornSaddle>(1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            }
        }
    }