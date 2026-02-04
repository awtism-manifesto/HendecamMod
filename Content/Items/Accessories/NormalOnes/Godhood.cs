using HendecamMod.Content.Buffs;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.NormalOnes;

//[AutoloadEquip(EquipType.Beard)]
public class Godhood : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(gold: 77777777);
        Item.rare = ItemRarityID.Master;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Nothing can touch you, everything you touch crumbles to dust"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Works best with Hallowed Armor"));
    }

    public override void UpdateEquip(Player player)
    {
        player.AddBuff(BuffID.ShadowDodge, 300);
        player.AddBuff(BuffID.ParryDamageBuff, 300);
        player.AddBuff(ModContent.BuffType<DarkPower>(), 300);
        player.AddBuff(ModContent.BuffType<MorbinTime>(), 300);
        player.AddBuff(ModContent.BuffType<CasaBuff>(), 300);
        player.AddBuff(ModContent.BuffType<MambaBuff>(), 300);
        player.AddBuff(ModContent.BuffType<HeartBuff>(), 300);
        player.AddBuff(ModContent.BuffType<JungleHealing>(), 300);
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