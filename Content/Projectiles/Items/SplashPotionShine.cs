using HendecamMod.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles.Items;

public class SplashPotionShine : ModProjectile
{
    public override void SetStaticDefaults()
    {

        ProjectileID.Sets.PlayerHurtDamageIgnoresDifficultyScaling[Type] = true; // Damage dealt to players does not scale with difficulty in vanilla.

        // This set handles some things for us already:
        // Sets the timeLeft to 3 and the projectile direction when colliding with an NPC or player in PVP (so the explosive can detonate).
        // Explosives also bounce off the top of Shimmer, detonate with no blast damage when touching the bottom or sides of Shimmer, and damage other players in For the Worthy worlds.
        ProjectileID.Sets.Explosive[Type] = true;
        ProjectileID.Sets.RocketsSkipDamageForPlayers[Type] = true;
        ProjectileID.Sets.IsARocketThatDealsDoubleDamageToPrimaryEnemy[Type] = true;
        // This set makes it so the rocket doesn't deal damage to players. Only used for vanilla rockets.
        // Simply remove the Projectile.HurtPlayer() part to stop the projectile from damaging its user.
        // ProjectileID.Sets.RocketsSkipDamageForPlayers[Type] = true;
    }
    public override void SetDefaults()
    {
        Projectile.width = 30;
        Projectile.height = 30;
        Projectile.friendly = true;
        Projectile.penetrate = -1; // Infinite penetration so that the blast can hit all enemies within its radius.
        Projectile.DamageType = DamageClass.Summon;
        Projectile.light = 1f; // How much light emit around the projectile
        Projectile.usesLocalNPCImmunity = true;
        Projectile.extraUpdates = 1;
        // Rockets use explosive AI, ProjAIStyleID.Explosive (16). You could use that instead here with the correct AIType.
        // But, using our own AI allows us to customize things like the dusts that the rocket creates.
        // Projectile.aiStyle = ProjAIStyleID.Explosive;
        // AIType = ProjectileID.RocketI;
    }
    public override void AI()
    {
        // If timeLeft is <= 3, then explode the rocket.
        if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
        {
            Projectile.PrepareBombToBlow();
        }



        Projectile.rotation += 0.18f;
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 24f)
        {
            Projectile.ai[0] = 24f;
            Projectile.velocity.Y += 0.25f;
        }
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 20f;
        }


    }

    // When the rocket hits a tile, NPC, or player, get ready to explode.
    public override bool OnTileCollide(Vector2 oldVelocity)
    {

        Projectile.timeLeft = 3; // Set the timeLeft to 3 so it can get ready to explode.
        return false; // Returning false is important here. Otherwise the projectile will die without being resized (no blast radius).
    }

    public override void PrepareBombToBlow()
    {
        Projectile.tileCollide = false; // This is important or the explosion will be in the wrong place if the rocket explodes on slopes.
        Projectile.alpha = 255; // Make the rocket invisible.

        // Resize the hitbox of the projectile for the blast "radius".
        // Rocket I: 128, Rocket III: 200, Mini Nuke Rocket: 250
        // Measurements are in pixels, so 128 / 16 = 8 tiles.
        Projectile.Resize(134, 134);
        // Set the knockback of the blast.
        // Rocket I: 8f, Rocket III: 10f, Mini Nuke Rocket: 12f
        Projectile.knockBack = 4f;
    }

    public override void OnKill(int timeLeft)
    {
        // Vanilla code takes care ensuring that in For the Worthy or Get Fixed Boi worlds the blast can damage other players because
        // this projectile is ProjectileID.Sets.Explosive[Type] = true;. It also takes care of hurting the owner. The Projectile.PrepareBombToBlow
        // and Projectile.HurtPlayer methods can be used directly if needed for a projectile not using ProjectileID.Sets.Explosive

        // Play an exploding sound.
        SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);

        // Resize the projectile again so the explosion dust and gore spawn from the middle.
        // Rocket I: 22, Rocket III: 80, Mini Nuke Rocket: 50
        Projectile.Resize(134, 134);





        // Spawn a bunch of fire dusts.
        for (int j = 0; j < 25; j++)
        {
            Dust fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.YellowStarDust, 0f, 0f, 100, default, 0.85f);
            fireDust.noGravity = true;
            fireDust.velocity *= 9f;
            fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.YellowStarDust, 0f, 0f, 100, default, 1.15f);
            fireDust.velocity *= 3.5f;
        }


    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(ModContent.BuffType<ShinyTag>(), 300);

        Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;

    }


}
