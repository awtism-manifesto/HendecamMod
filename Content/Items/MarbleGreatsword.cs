using System.Collections.Generic;
using HendecamMod.Content.Projectiles;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

/// <summary>
///     Star Wrath/Starfury style weapon. Spawn projectiles from sky that aim towards mouse.
///     See Source code for Star Wrath projectile to see how it passes through tiles.
///     For a detailed sword guide see <see cref="ExampleSword" />
/// </summary>
public class MarbleGreatsword : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 36;
        Item.useTime = 36;
        Item.damage = 32;
        Item.knockBack = 11.25f;
        Item.width = 40;
        Item.height = 40;
        Item.scale = 1.25f;

        Item.UseSound = SoundID.Item1;
        Item.rare = ItemRarityID.White;
        Item.value = 82500;
        Item.DamageType = DamageClass.Melee;
        Item.shoot = ModContent.ProjectileType<MarbleSwing>();
        Item.noMelee = true; // This is set the sword itself doesn't deal damage (only the projectile does).
        Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
        Item.autoReuse = true;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
        Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
        NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.

        return base.Shoot(player, source, position, velocity, type, damage, knockback);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Marble, 66);
        recipe.AddIngredient(ItemID.SilverBar, 18);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Marble, 66);
        recipe.AddIngredient(ItemID.TungstenBar, 18);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        if (ModLoader.TryGetMod("VitalityMod", out Mod VitalMerica) && VitalMerica.TryFind("ArcaneGoldShard", out ModItem ArcaneGoldShard))
        {
            recipe.AddIngredient(ArcaneGoldShard.Type, 20);
        }
    }


    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Swings in a large, powerful arc");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        // Here we will hide all tooltips whose title end with ':RemoveMe'
        // One like that is added at the start of this method
        foreach (var l in tooltips)
        {
            if (l.Name.EndsWith(":RemoveMe"))
            {
                l.Hide();
            }
        }

        // Another method of hiding can be done if you want to hide just one line.
        // tooltips.FirstOrDefault(x => x.Mod == "ExampleMod" && x.Name == "Verbose:RemoveMe")?.Hide();
    }
}