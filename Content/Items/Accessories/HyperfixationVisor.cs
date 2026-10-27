using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;
using static HendecamMod.Content.Items.Armor.YelmutLeggings;

namespace HendecamMod.Content.Items.Accessories;
[AutoloadEquip(EquipType.Beard)]
public class HyperfixationVisor : ModItem
{


    
    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Green;
        Item.value = 90000;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<AutismOrb>(6);
        recipe.AddIngredient(ItemID.Sunglasses);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

    }
    
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<HyperTagger>().HyperTag = true;


    }

}
public class HyperTagger : ModPlayer
{
    public bool HyperTag;

    public override void ResetEffects()
    {
        HyperTag = false;
    }

    public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (HyperTag)
        {
            target.AddBuff(BuffType<AutismTag>(), 480);
        }
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (HyperTag)
        {
            target.AddBuff(BuffType<AutismTag>(), 480);
        }
    }


}
