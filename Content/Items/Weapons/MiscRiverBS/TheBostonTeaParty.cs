using HendecamMod.Content.Global;
using HendecamMod.Content.Rarities;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.MiscRiverBS;

public class TheBostonTeaParty : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 130;
        Item.height = 46;
        Item.scale = 0.75f;
        Item.rare = ModContent.RarityType<HotPink>();
        Item.value = 330000000;
        Item.useTime = 60;
        Item.useAnimation = 60;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.autoReuse = true;
        Item.UseSound = new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/50Cal")
        {
            Volume = 4f,
            PitchVariance = 0.2f,
            MaxInstances = 5,
        };

        Item.DamageType = DamageClass.Ranged;
        Item.damage = 100000;
        Item.knockBack = 5.5f;
        Item.noMelee = true;
        Item.crit = 4;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 18.3f;
        Item.useAmmo = AmmoID.Bullet;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        
            int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            Main.projectile[proj].GetGlobalProjectile<Hitscan>().fromHitscan = true;
            return false; // Prevent vanilla projectile spawn

    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Instead of projectile vomit, this weapon shoots very high-damage bullets.");
        tooltips.Add(line);
        var line1 = new TooltipLine(Mod, "Face", "Right click to zoom");
        tooltips.Add(line1);
        var line2 = new TooltipLine(Mod, "Face", "Made for those with less than great computers.");
        tooltips.Add(line2);
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-27f, 2f);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<TheSecondAmendment>();
        recipe.AddIngredient<PondsSniper>();
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }
    public override void HoldItem(Player player)
    {
        player.GetModPlayer<BostonScope>().Scoped = true;
    }

}
public class BostonScope : ModPlayer
{
    public override void ModifyZoom(ref float zoom)
    {
        Player player = Main.LocalPlayer;
        if (Main.mouseRight == true && Scoped)
        {
            zoom = player.scope ? 0.75f : 0.5f;
        }
        base.ModifyZoom(ref zoom);
    }


    public bool Scoped;


    public override void ResetEffects()
    {
        Scoped = false;
    }

}