using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items;

public class TarriffStamper : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 35;
        Item.height = 35;
        Item.scale = 1f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 22;
        Item.useAnimation = 22;
        Item.autoReuse = true;
        Item.shoot = ProjectileType<NuhUh>();
        Item.DamageType = GetInstance<MeleeStupidDamage>();
        Item.damage = 23;
        Item.knockBack = 9f;
        Item.ChangePlayerDirectionOnShoot = true;

        Item.value = Item.buyPrice(gold: 90);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
        Item.useTurn = true;
        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }
    public float LobotometerCost = 8f;

    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        // Inflict the OnFire debuff for 1 second onto any NPC/Monster that this hits.
        // 60 frames = 1 second
        target.AddBuff(BuffType<Stamped>(), 450);
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Stamps your enemies with red ink that reduces their defense");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Uses 8 Braincells")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'May also have adverse effects on the U.S economy...'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        
    }
}