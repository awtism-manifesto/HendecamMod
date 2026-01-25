using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Buttplug)]
public class GrassyPeaceAgreement : ModItem
    {
    public override void SetDefaults()
        {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(copper: 676767);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
        }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
        var line = new TooltipLine(Mod, "Face", "The jungle's denizens should be friendly");
        tooltips.Add(line);
        }
    public override void UpdateEquip(Player player)
        {
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

        }
    public override void AddRecipes()
        {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GolemTrophy);//skibidi toilet sigma balls 67000 lolllllllxd
        recipe.AddIngredient(ItemID.PlanteraTrophy);//skibidi toilet sigma balls 67000 lolllllllxd
        recipe.AddIngredient<FriendCore>(1);
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
        }
    }

