using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class RiotShield : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.defense = 8;
        Item.value = Item.sellPrice(silver: 4000);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Face", "Grants immunity to Weakness, Withered Weapon,"));
        tooltips.Add(new TooltipLine(Mod, "Face", "Broken Armor, Withered Armor, Bleeding,"));
        tooltips.Add(new TooltipLine(Mod, "Face", "Feral Bite, Poison, Acid Venom. Burning,"));
        tooltips.Add(new TooltipLine(Mod, "Face", "Blackout, Darkness, Stoned, Horrified,"));
        tooltips.Add(new TooltipLine(Mod, "Face", "OnFire, Dazed, and Knockback"));
    }

    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.Blackout] = true;
        player.buffImmune[BuffID.Darkness] = true;
        player.buffImmune[BuffID.Stoned] = true;
        player.buffImmune[BuffID.Horrified] = true;
        player.buffImmune[BuffID.Weak] = true;
        player.buffImmune[BuffID.WitheredWeapon] = true;
        player.buffImmune[BuffID.BrokenArmor] = true;
        player.buffImmune[BuffID.WitheredArmor] = true;
        player.buffImmune[BuffID.Bleeding] = true;
        player.buffImmune[BuffID.Rabies] = true;
        player.buffImmune[BuffID.Poisoned] = true;
        player.buffImmune[BuffID.Venom] = true;
        player.buffImmune[BuffID.Burning] = true;
        player.buffImmune[BuffID.OnFire] = true;
        player.buffImmune[BuffID.Dazed] = true;
        player.fireWalk = true;
        player.noKnockback = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<SpotlightShield>();
        recipe.AddIngredient<ArmoredSling>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.AddTile(TileID.Hellforge);
        recipe.Register();
    }
}