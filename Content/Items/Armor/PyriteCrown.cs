using System.Collections.Generic;

namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class PyriteCrown : ModItem
{
    public static readonly int MeleeAttackSpeedBonus = 9;
    public static readonly int AdditiveSummonDamageBonus = 9;

    public override void SetDefaults()
    {
        Item.defense = 2;
        Item.rare = ItemRarityID.Blue;
        Item.value = 54000;
    }

    public override void SetStaticDefaults()
    {
        ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PyriteBar>(10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void UpdateEquip(Player player)
    {
       

        player.GetDamage(DamageClass.Summon) += AdditiveSummonDamageBonus / 100f;
        player.GetAttackSpeed(DamageClass.Melee) += MeleeAttackSpeedBonus / 100f;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "+9% summon damage and whip speed");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "1 extra minion & sentry slot";
        player.maxMinions += 1;
        player.maxTurrets += 1;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<PyriteChestguard>() && legs.type == ModContent.ItemType<PyriteLegPlating>();
    }
}