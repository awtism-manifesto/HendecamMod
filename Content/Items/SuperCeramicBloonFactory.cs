using System.Collections.Generic;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class SuperCeramicBloonFactory : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

        ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; // The default value is 1, but other values are supported. See the docs for more guidance. 
    }

    public override void SetDefaults()
    {
        Item.damage = 34;
        Item.knockBack = 0.8f;
       
        Item.width = 32;
        Item.height = 32;
        Item.scale = 0.9f;
        Item.useTime = 22;
        Item.useAnimation = 22;
        Item.useStyle = ItemUseStyleID.Shoot; // how the player's arm moves when using the item
        Item.value = Item.sellPrice(gold: 12);
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item44; // What sound should play when using the item

        // These below are needed for a minion weapon
        Item.noMelee = true; // this item doesn't do any melee damage
        Item.DamageType = DamageClass.Summon; // Makes the damage register as summon. If your item does not have any damage type, it becomes true damage (which means that damage scalars will not affect it). Be sure to have a damage type
        Item.buffType = ModContent.BuffType<SuperCeramBuff>();
        // No buffTime because otherwise the item tooltip would say something like "1 minute duration"
        Item.shoot = ModContent.ProjectileType<SuperCeramicBloon>(); // This item creates the minion projectile
        Item.shootSpeed = 11f;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
        player.AddBuff(Item.buffType, 2);

        // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
        var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
        projectile.originalDamage = Item.damage;

        // Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
        return false;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Summons Super Ceramic Bloons to fight for you");
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

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<CrimceramicSheet>(25);
        recipe.AddIngredient<PearlceramicSheet>(25);
        recipe.AddIngredient(ItemID.HallowedBar, 7);
        recipe.AddIngredient(ItemID.SpectreBar, 7);
        recipe.AddIngredient<Rubber>(105);
        recipe.AddIngredient<CeramicBloonFactory>();
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient<EbonceramicSheet>(25);

        recipe.AddIngredient<PearlceramicSheet>(25);
        recipe.AddIngredient(ItemID.HallowedBar, 7);
        recipe.AddIngredient(ItemID.SpectreBar, 7);
        recipe.AddIngredient<Rubber>(105);
        recipe.AddIngredient<CeramicBloonFactory>();
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-12f, 0f);
    }
}