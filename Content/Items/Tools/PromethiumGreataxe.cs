using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;

namespace HendecamMod.Content.Items.Tools;

public class PromethiumGreataxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 364;
        Item.DamageType = DamageClass.Melee;
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 3;
        Item.useAnimation = 15;
        Item.scale = 1.75f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 17;
        Item.value = Item.buyPrice(gold: 248);
        Item.rare = ItemRarityID.Purple;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.axe = 57;

        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
    }
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffType<RadPoisoning4>(), 400);
        for (int i = 0; i < 4; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustType<PromethiumDust>());
            dust.noGravity = true;
            dust.velocity *= 7.33f;
            dust.scale *= 0.96f;
        }
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PromethiumBar>(32);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }
}