using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;

namespace HendecamMod.Content.Items;


public class GiantBone : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 50;
        Item.height = 40;
        Item.scale = 2.5f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 32;
        Item.useAnimation = 32;
        Item.autoReuse = true;

        Item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
        Item.damage = 60;
        Item.knockBack = 7;
        Item.value = Item.buyPrice(gold: 1);
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item1;

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }
    public float LobotometerCost = 9f;
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Uses 9 Lobotometer");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Get your mind out of the gutter")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

      
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Bone, 200);

        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-64f, -64f);
    }
}