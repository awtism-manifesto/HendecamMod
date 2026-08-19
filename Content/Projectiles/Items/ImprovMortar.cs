using HendecamMod.Common.Systems.Particles;
using Terraria.Audio;

namespace HendecamMod.Content.Projectiles.Items;

public class ImprovMortar : ModProjectile
{
    public override void SetStaticDefaults()
    {
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
        Projectile.width = 34;
        Projectile.height = 34;
        Projectile.friendly = true;
        Projectile.penetrate = -1; // Infinite penetration so that the blast can hit all enemies within its radius.
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.light = 0.1f; // How much light emit around the projectile
        Projectile.usesLocalNPCImmunity = true;
      
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

       

        Projectile.rotation += 0.275f;
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 16f)
        {
            Projectile.ai[0] = 16f;
            Projectile.velocity.Y += 0.244f;
        }

        if (Projectile.velocity.Y > 19f)
        {
            Projectile.velocity.Y = 21f;
        }
    }

    // When the rocket hits a tile, NPC, or player, get ready to explode.
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.velocity *= 0f; // Stop moving so the explosion is where the rocket was.
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
        Projectile.Resize(184, 184);
        // Set the knockback of the blast.
        // Rocket I: 8f, Rocket III: 10f, Mini Nuke Rocket: 12f
        Projectile.knockBack = 6.5f;
    }

    public override void OnKill(int timeLeft)
    {
        // Vanilla code takes care ensuring that in For the Worthy or Get Fixed Boi worlds the blast can damage other players because
        // this projectile is ProjectileID.Sets.Explosive[Type] = true;. It also takes care of hurting the owner. The Projectile.PrepareBombToBlow
        // and Projectile.HurtPlayer methods can be used directly if needed for a projectile not using ProjectileID.Sets.Explosive

        // Play an exploding sound.
        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

        // Resize the projectile again so the explosion dust and gore spawn from the middle.
        // Rocket I: 22, Rocket III: 80, Mini Nuke Rocket: 50
        Projectile.Resize(175, 175);

        for (int i = 0; i < 15; i++)
        {
            Color randColor = Main.rand.NextFromList(Color.Orange, Color.OrangeRed, Color.SlateGray, Color.Gray, Color.Black);
            ParticleSystem.SpawnParticle(
                ParticleID.Streak,
                Projectile.Center,
                Vector2.UnitY.RotatedByRandom(3f) * Main.rand.NextFloat(5, 9),
                randColor,
                1f,
                Main.rand.NextFloat(1f, 1.5f));

            ParticleSystem.SpawnParticle(
                ParticleID.Fire,
                Projectile.Center,
                Vector2.UnitY.RotatedByRandom(1.3f) * Main.rand.NextFloat(1, 3),
                randColor,
                1f,
                Main.rand.NextFloat(0.7f, 1f));
        }
    }

   

  
}