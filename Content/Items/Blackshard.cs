using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Global;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Rarities;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class Blackshard : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.staff[Type] = true; // This makes the useStyle animate as a staff instead of as a gun.
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
    }

    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;

        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 5;
        Item.useAnimation = 5;
        Item.autoReuse = true;

        Item.mana = 6;
        Item.DamageType = ModContent.GetInstance<OmniDamage>();
        Item.damage = 160;
        Item.knockBack = 6.66f;
        Item.noMelee = true;
        Item.ArmorPenetration = 50;
        Item.value = 6666666;
        Item.rare = ModContent.RarityType<Seizure>();
        Item.shoot = ModContent.ProjectileType<KnightSpawn>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 11.25f; // Speed of the projectiles the sword will shoot
        if (ModLoader.TryGetMod("FargowiltasSouls", out Mod FargoMerica) && FargoMerica.TryFind("Eridanium", out ModItem Eridanium) && (!ModLoader.TryGetMod("ThoriumMod", out Mod Thor2Merica) && (!ModLoader.TryGetMod("CalamityMod", out Mod Cal2Merica))))

        {
            Item.damage = 196;
        }
        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica) || (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica)))

        {
            Item.damage = 216;
           
        }

       
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ModContent.ProjectileType<KnightSpawn>();
    }

    public override bool AltFunctionUse(Player player)
    {
        return true;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        // Rotate the velocity randomly by 30 degrees at max.
        Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(24.5f));
        Vector2 new2Velocity = velocity.RotatedByRandom(MathHelper.ToRadians(23.5f));

        // Decrease velocity randomly for nicer visuals.
        newVelocity *= 1f - Main.rand.NextFloat(0.375f);
        if (player.altFunctionUse == 2)
        {
            int proj = Projectile.NewProjectile(source, position, newVelocity * 2.1f, ModContent.ProjectileType<KnightStar>(), (int)(damage * 1.15f), (int)(knockback * 2f), player.whoAmI);
            player.AddBuff(ModContent.BuffType<DarkPower>(), 363);
            Main.projectile[proj].GetGlobalProjectile<KnightComboSetup>().fromtheBlackshard = true;
            SoundEngine.PlaySound(SoundID.NPCDeath51, player.position);
            SoundEngine.PlaySound(SoundID.NPCDeath44, player.position);
            SoundEngine.PlaySound(SoundID.NPCDeath55, player.position);
            SoundEngine.PlaySound(SoundID.Item32, player.position);
            SoundEngine.PlaySound(SoundID.Item103, player.position);
            if (Main.rand.NextBool(3))
            {
                int proj2 = Projectile.NewProjectile(source, position, new2Velocity * 2.1f, ModContent.ProjectileType<KnightStar>(), (int)(damage * 1.15f), (int)(knockback * 2f), player.whoAmI);

                Main.projectile[proj2].GetGlobalProjectile<KnightComboSetup>().fromtheBlackshard = true;
            }

            return false;
        }

        else
        {
            int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            player.AddBuff(ModContent.BuffType<DarkPower>(), 363);
            Main.projectile[proj].GetGlobalProjectile<KnightComboSetup>().fromtheBlackshard = false;

            return false; // Prevent vanilla projectile spawn
        }
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Left click to shoot a barrage of swords");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Right click to shoot a wave of splitting stars")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Tag enemies with the stars to enhance the swords")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        //
        line = new TooltipLine(Mod, "Face", "Increases the player's max mana while in use")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
       
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-2f, 1f);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<TheWither>();
        recipe.AddIngredient(ItemID.BrokenHeroSword);
        recipe.AddIngredient(ItemID.StarWrath);
        recipe.AddIngredient<DeliriantDagger>();
        recipe.AddIngredient<BallisticKnife>();
        recipe.AddIngredient(ItemID.UnholyTrident);

        recipe.AddIngredient(ItemID.EmpressBlade);
        recipe.AddIngredient(ItemID.CandyCaneSword);

        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
        if (ModLoader.TryGetMod("Terbritish", out Mod TerBritish) && TerBritish.TryFind("TerraShank", out ModItem TerraShank))
        {
            recipe.AddIngredient(TerraShank.Type);
        }

        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica) && ThorMerica.TryFind("TerrariansLastKnife", out ModItem TerrariansLastKnife) && ThorMerica.TryFind("DarkMatter", out ModItem DarkMatter))
        {
            recipe.AddIngredient(TerrariansLastKnife.Type);
            recipe.AddIngredient(ItemID.DarkShard, 3);
            recipe.AddIngredient(DarkMatter.Type, 10);
        }

        if (!ModLoader.TryGetMod("ThoriumMod", out Mod SkillIssue))
        {
            recipe.AddIngredient(ItemID.DarkShard, 3);
        }

        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind("NightmareFuel", out ModItem NightmareFuel))
        {
            recipe.AddIngredient(NightmareFuel.Type, 15);
        }

        if (ModLoader.TryGetMod("FargowiltasSouls", out Mod FargoMerica) && FargoMerica.TryFind("Eridanium", out ModItem Eridanium))
        {
            recipe.AddIngredient(Eridanium.Type, 5);
        }
    }
}