using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Accessories.PeaceAmongNations;

//[AutoloadEquip(EquipType.Buttplug)]
public class FlamingPeaceAgreement : ModItem
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
        var line = new TooltipLine(Mod, "Face", "The damned should be friendly");
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
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WallofFleshTrophy);
        recipe.AddIngredient<FriendCore>();
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}