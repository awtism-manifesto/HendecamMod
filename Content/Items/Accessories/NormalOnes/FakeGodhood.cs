using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Armor;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.DataStructures;
using static HendecamMod.Content.Items.Armor.ArchangelGreaves;

namespace HendecamMod.Content.Items.Accessories.NormalOnes;

[AutoloadEquip(EquipType.Wings)]
public class FakeGodhood : ModItem
{

    public override void SetStaticDefaults()
    {
       
        ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(333, 14.44f, 3.33f);
      
    }
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(gold: 67);
        Item.rare = ItemRarityID.Master;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Gives effects of Archangel Wings");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "-Testing Item-")
        {
            OverrideColor = new Color(255, 25, 99)
        };
        tooltips.Add(line);
      
    }

    public override void UpdateEquip(Player player)
    {
        player.wingTimeMax += 225;
        player.noFallDmg = true;
        player.jumpSpeedBoost += 1.25f;
        player.moveSpeed += 0.35f;
        player.wingRunAccelerationMult += 1.77f;
        player.wingAccRunSpeed += 1.77f;
        player.GetModPlayer<ArchangelWings>().ArchWings = true;
        player.GetModPlayer<AngelSpeed>().Angel = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<Bullshit5>();
        recipe.AddIngredient<SoulOfImmunityAccessory>();
        recipe.AddTile<CultistCyclotronPlaced>();
       
        recipe.AddTile(TileID.VoidMonolith);
        recipe.Register();
    }
}