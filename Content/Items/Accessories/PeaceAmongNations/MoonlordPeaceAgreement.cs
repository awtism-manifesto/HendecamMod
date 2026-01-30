using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Beard)]
public class MoonlordPeaceAgreement : ModItem
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
        var line = new TooltipLine(Mod, "Face", "The Moonlord and his cultists should be friendly");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.npcTypeNoAggro[NPCID.MoonLordCore] = true;
        player.npcTypeNoAggro[NPCID.MoonLordFreeEye] = true;
        player.npcTypeNoAggro[NPCID.MoonLordHand] = true;
        player.npcTypeNoAggro[NPCID.MoonLordHead] = true;
        player.npcTypeNoAggro[NPCID.MoonLordLeechBlob] = true;
        player.npcTypeNoAggro[NPCID.CultistArcherBlue] = true;
        player.npcTypeNoAggro[NPCID.CultistArcherWhite] = true;
        player.npcTypeNoAggro[NPCID.CultistBoss] = true;
        player.npcTypeNoAggro[NPCID.CultistBossClone] = true;
        player.npcTypeNoAggro[NPCID.CultistDevote] = true;
        player.npcTypeNoAggro[NPCID.CultistTablet] = true;
        player.npcTypeNoAggro[NPCID.AncientCultistSquidhead] = true;
        player.npcTypeNoAggro[NPCID.AncientDoom] = true;
        player.npcTypeNoAggro[NPCID.CultistDragonHead] = true;
        player.npcTypeNoAggro[NPCID.CultistDragonBody1] = true;
        player.npcTypeNoAggro[NPCID.CultistDragonBody2] = true;
        player.npcTypeNoAggro[NPCID.CultistDragonBody3] = true;
        player.npcTypeNoAggro[NPCID.CultistDragonBody4] = true;
        player.npcTypeNoAggro[NPCID.CultistDragonTail] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MoonLordTrophy);
        recipe.AddIngredient(ItemID.AncientCultistTrophy);
        recipe.AddIngredient<FriendCore>();
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}