
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations
    {
    //[AutoloadEquip(EquipType.Beard)]
    public class PeaceAmongBiomes : ModItem
        {
        public override void SetDefaults()
            {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(silver: 4000);
            Item.rare = ItemRarityID.LightPurple;
            Item.accessory = true;
            }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
            {
            var line = new TooltipLine(Mod, "Face", "Most biome-based entities should be friendly");
            tooltips.Add(line);
            }
        public override void UpdateEquip(Player player)
            {
            player.npcTypeNoAggro[NPCID.WallofFlesh] = true;
            player.npcTypeNoAggro[NPCID.WallofFleshEye] = true;
            player.npcTypeNoAggro[NPCID.TheHungry] = true;
            player.npcTypeNoAggro[NPCID.TheHungryII] = true;
            player.npcTypeNoAggro[NPCID.Demon] = true;
            player.npcTypeNoAggro[NPCID.VoodooDemon] = true;
            player.npcTypeNoAggro[NPCID.Hellbat] = true; //redundant in PAM
            player.npcTypeNoAggro[NPCID.Lavabat] = true; //redundant in PAM 
            player.npcTypeNoAggro[NPCID.LavaSlime] = true; //redundant in PAM
            player.npcTypeNoAggro[NPCID.RedDevil] = true;
            player.npcTypeNoAggro[NPCID.FireImp] = true;
            player.npcTypeNoAggro[NPCID.BoneSerpentBody] = true; //redundant in PAM
            player.npcTypeNoAggro[NPCID.BoneSerpentHead] = true; //redundant in PAM
            player.npcTypeNoAggro[NPCID.BoneSerpentTail] = true; //redundant in PAM
            player.npcTypeNoAggro[NPCID.Plantera] = true;
            player.npcTypeNoAggro[NPCID.PlanterasHook] = true;
            player.npcTypeNoAggro[NPCID.PlanterasTentacle] = true;
            player.npcTypeNoAggro[NPCID.JungleBat] = true; //redundant in PAM
            player.npcTypeNoAggro[NPCID.JungleCreeper] = true;
            player.npcTypeNoAggro[NPCID.JungleCreeperWall] = true;
            player.npcTypeNoAggro[NPCID.SpikedJungleSlime] = true;//redundant in PAM
            player.npcTypeNoAggro[NPCID.Piranha] = true;
            player.npcTypeNoAggro[NPCID.Snatcher] = true;
            player.npcTypeNoAggro[NPCID.ManEater] = true;
            player.npcTypeNoAggro[NPCID.DoctorBones] = true; //redundant in PAM
            player.npcTypeNoAggro[NPCID.AngryTrapper] = true;
            player.npcTypeNoAggro[NPCID.GiantTortoise] = true;
            player.npcTypeNoAggro[NPCID.GiantFlyingFox] = true; //redundant in PAM
            player.npcTypeNoAggro[NPCID.Hornet] = true;
            player.npcTypeNoAggro[NPCID.Arapaima] = true;
            player.npcTypeNoAggro[NPCID.AnglerFish] = true;
            player.npcTypeNoAggro[NPCID.LacBeetle] = true; //redundant in PAM
            player.npcTypeNoAggro[NPCID.Moth] = true;
            player.npcTypeNoAggro[NPCID.MossHornet] = true;
            player.npcTypeNoAggro[NPCID.Hornet] = true;
            player.npcTypeNoAggro[NPCID.HornetFatty] = true;
            player.npcTypeNoAggro[NPCID.HornetHoney] = true;
            player.npcTypeNoAggro[NPCID.HornetStingy] = true;
            player.npcTypeNoAggro[NPCID.HornetLeafy] = true;
            player.npcTypeNoAggro[NPCID.HornetSpikey] = true;
            player.npcTypeNoAggro[NPCID.Crab] = true;
            player.npcTypeNoAggro[NPCID.Shark] = true;
            player.npcTypeNoAggro[NPCID.Sharkron] = true;
            player.npcTypeNoAggro[NPCID.Sharkron2] = true;
            player.npcTypeNoAggro[NPCID.Squid] = true;
            player.npcTypeNoAggro[NPCID.SeaSnail] = true;
            player.npcTypeNoAggro[NPCID.Pixie] = true;
            player.npcTypeNoAggro[NPCID.Unicorn] = true;
            player.npcTypeNoAggro[NPCID.RainbowSlime] = true;
            player.npcTypeNoAggro[NPCID.Gastropod] = true;
            player.npcTypeNoAggro[NPCID.LightMummy] = true;
            player.npcTypeNoAggro[NPCID.IlluminantSlime] = true;
            player.npcTypeNoAggro[NPCID.IlluminantBat] = true;
            player.npcTypeNoAggro[NPCID.ChaosElemental] = true;
            player.npcTypeNoAggro[NPCID.EnchantedSword] = true;
            player.npcTypeNoAggro[NPCID.BigMimicHallow] = true;
            player.npcTypeNoAggro[NPCID.DesertGhoulHallow] = true;
            player.npcTypeNoAggro[NPCID.PigronHallow] = true;
            player.npcTypeNoAggro[NPCID.SandsharkHallow] = true;
            player.npcTypeNoAggro[NPCID.EaterofSouls] = true;
            player.npcTypeNoAggro[NPCID.EaterofWorldsBody] = true;
            player.npcTypeNoAggro[NPCID.EaterofWorldsHead] = true;
            player.npcTypeNoAggro[NPCID.EaterofWorldsTail] = true;
            player.npcTypeNoAggro[NPCID.VileSpitEaterOfWorlds] = true;
            player.npcTypeNoAggro[NPCID.VileSpit] = true;
            player.npcTypeNoAggro[NPCID.CorruptBunny] = true;
            player.npcTypeNoAggro[NPCID.CorruptGoldfish] = true;
            player.npcTypeNoAggro[NPCID.CorruptPenguin] = true;
            player.npcTypeNoAggro[NPCID.DevourerBody] = true;
            player.npcTypeNoAggro[NPCID.DevourerHead] = true;
            player.npcTypeNoAggro[NPCID.DevourerTail] = true;
            player.npcTypeNoAggro[NPCID.Corruptor] = true;
            player.npcTypeNoAggro[NPCID.CorruptSlime] = true;
            player.npcTypeNoAggro[NPCID.Slimer] = true;
            player.npcTypeNoAggro[NPCID.SeekerBody] = true;
            player.npcTypeNoAggro[NPCID.SeekerHead] = true;
            player.npcTypeNoAggro[NPCID.SeekerTail] = true;
            player.npcTypeNoAggro[NPCID.DarkMummy] = true;
            player.npcTypeNoAggro[NPCID.CursedHammer] = true;
            player.npcTypeNoAggro[NPCID.Clinger] = true;
            player.npcTypeNoAggro[NPCID.BigMimicCorruption] = true;
            player.npcTypeNoAggro[NPCID.DesertGhoulCorruption] = true;
            player.npcTypeNoAggro[NPCID.PigronCorruption] = true;
            player.npcTypeNoAggro[NPCID.BloodCrawler] = true;
            player.npcTypeNoAggro[NPCID.BloodCrawlerWall] = true;
            player.npcTypeNoAggro[NPCID.FaceMonster] = true;
            player.npcTypeNoAggro[NPCID.CrimsonBunny] = true;
            player.npcTypeNoAggro[NPCID.CrimsonGoldfish] = true;
            player.npcTypeNoAggro[NPCID.CrimsonPenguin] = true;
            player.npcTypeNoAggro[NPCID.BigMimicCrimson] = true;
            player.npcTypeNoAggro[NPCID.SandsharkCrimson] = true;
            player.npcTypeNoAggro[NPCID.SandsharkCorrupt] = true;
            player.npcTypeNoAggro[NPCID.DesertGhoulCrimson] = true;
            player.npcTypeNoAggro[NPCID.PigronCrimson] = true;
            player.npcTypeNoAggro[NPCID.Crimera] = true;
            player.npcTypeNoAggro[NPCID.BrainofCthulhu] = true;
            player.npcTypeNoAggro[NPCID.Creeper] = true;
            player.npcTypeNoAggro[NPCID.Herpling] = true;
            player.npcTypeNoAggro[NPCID.Crimslime] = true;
            player.npcTypeNoAggro[NPCID.BloodJelly] = true;
            player.npcTypeNoAggro[NPCID.BloodFeeder] = true;
            player.npcTypeNoAggro[NPCID.BloodMummy] = true;
            player.npcTypeNoAggro[NPCID.CrimsonAxe] = true;
            player.npcTypeNoAggro[NPCID.IchorSticker] = true;
            player.npcTypeNoAggro[NPCID.FloatyGross] = true;
            player.npcTypeNoAggro[NPCID.DesertGhoulCrimson] = true;
            player.npcTypeNoAggro[NPCID.BigMimicCrimson] = true;
            player.npcTypeNoAggro[NPCID.PigronCrimson] = true;

            }
        public override void AddRecipes()
            {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient<FlamingPeaceAgreement>(1);
            recipe.AddIngredient<GrassyPeaceAgreement>(1);
            recipe.AddIngredient<OceanicPeaceAgreement>(1);
            recipe.AddIngredient<SpreadBiomePeaceAgreement>(1);
            recipe.AddIngredient<FriendCore>(1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();
            }
        }
    }