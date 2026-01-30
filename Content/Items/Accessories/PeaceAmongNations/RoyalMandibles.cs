using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Beard)]
public class RoyalMandibles : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 1000);
        Item.rare = ItemRarityID.LightPurple;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "The dwellers of the desert should be friendly");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.npcTypeNoAggro[NPCID.Vulture] = true;
        player.npcTypeNoAggro[NPCID.Antlion] = true;
        player.npcTypeNoAggro[NPCID.Mummy] = true;
        player.npcTypeNoAggro[NPCID.LightMummy] = true;
        player.npcTypeNoAggro[NPCID.DarkMummy] = true;
        player.npcTypeNoAggro[NPCID.BloodMummy] = true;
        player.npcTypeNoAggro[NPCID.Golfer] = true;
        player.npcTypeNoAggro[NPCID.GolferRescue] = true;
        player.npcTypeNoAggro[NPCID.FlyingAntlion] = true;
        player.npcTypeNoAggro[NPCID.GiantFlyingAntlion] = true;
        player.npcTypeNoAggro[NPCID.GiantWalkingAntlion] = true;
        player.npcTypeNoAggro[NPCID.LarvaeAntlion] = true;
        player.npcTypeNoAggro[NPCID.WalkingAntlion] = true;
        player.npcTypeNoAggro[NPCID.SandSlime] = true;
        player.npcTypeNoAggro[NPCID.DesertBeast] = true;
        player.npcTypeNoAggro[NPCID.DesertDjinn] = true;
        player.npcTypeNoAggro[NPCID.DesertGhoul] = true;
        player.npcTypeNoAggro[NPCID.DesertGhoulCorruption] = true;
        player.npcTypeNoAggro[NPCID.DesertGhoulCrimson] = true;
        player.npcTypeNoAggro[NPCID.DesertGhoulHallow] = true;
        player.npcTypeNoAggro[NPCID.DesertLamiaDark] = true;
        player.npcTypeNoAggro[NPCID.DesertLamiaLight] = true;
        player.npcTypeNoAggro[NPCID.DesertScorpionWalk] = true;
        player.npcTypeNoAggro[NPCID.DesertScorpionWall] = true;
        player.npcTypeNoAggro[NPCID.SandElemental] = true;
        player.npcTypeNoAggro[NPCID.SandShark] = true;
        player.npcTypeNoAggro[NPCID.SandsharkCorrupt] = true;
        player.npcTypeNoAggro[NPCID.SandsharkCrimson] = true;
        player.npcTypeNoAggro[NPCID.SandsharkHallow] = true;
        player.npcTypeNoAggro[NPCID.TombCrawlerBody] = true;
        player.npcTypeNoAggro[NPCID.TombCrawlerHead] = true;
        player.npcTypeNoAggro[NPCID.TombCrawlerTail] = true;
        player.npcTypeNoAggro[NPCID.DuneSplicerBody] = true;
        player.npcTypeNoAggro[NPCID.DuneSplicerHead] = true;
        player.npcTypeNoAggro[NPCID.DuneSplicerTail] = true;
        player.npcTypeNoAggro[NPCID.Tumbleweed] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.AntlionClaw);
        recipe.AddIngredient<FriendCore>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}