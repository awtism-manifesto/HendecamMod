using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Projectiles.Enemies;
using Terraria.Audio;

namespace HendecamMod.Content.Projectiles.Items;

public class UnstablePotion : ModProjectile
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
        Projectile.width = 36;
        Projectile.height = 36;
        Projectile.friendly = true;
        Projectile.penetrate = -1; // Infinite penetration so that the blast can hit all enemies within its radius.
        Projectile.DamageType = GetInstance<OmniDamage>();
        Projectile.light = 0.2f; // How much light emit around the projectile
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        // Rockets use explosive AI, ProjAIStyleID.Explosive (16). You could use that instead here with the correct AIType.
        // But, using our own AI allows us to customize things like the dusts that the rocket creates.
        // Projectile.aiStyle = ProjAIStyleID.Explosive;
        // AIType = ProjectileID.RocketI;
        Projectile.tileCollide = false;
    }

    public override void AI()
    {
        // If timeLeft is <= 3, then explode the rocket.
        if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
        {
            Projectile.PrepareBombToBlow();
        }

        if (Projectile.owner == Main.myPlayer && Main.rand.NextBool(255))
        {
            Projectile.timeLeft = 5;
        }

        Projectile.rotation += 0.275f;
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 11f)
        {
            Projectile.ai[0] = 13f;
            Projectile.velocity.Y += 0.233f;
        }

        if (Projectile.velocity.Y > 21f)
        {
            Projectile.velocity.Y = 26f;
        }
    }

    
    public override void PrepareBombToBlow()
    {
        Projectile.tileCollide = false; // This is important or the explosion will be in the wrong place if the rocket explodes on slopes.
        Projectile.alpha = 255; // Make the rocket invisible.

        // Resize the hitbox of the projectile for the blast "radius".
        // Rocket I: 128, Rocket III: 200, Mini Nuke Rocket: 250
        // Measurements are in pixels, so 128 / 16 = 8 tiles.
        Projectile.Resize(363, 363);
        // Set the knockback of the blast.
        // Rocket I: 8f, Rocket III: 10f, Mini Nuke Rocket: 12f
        Projectile.knockBack = 16f;
    }

    public override void OnKill(int timeLeft)
    {
        // Vanilla code takes care ensuring that in For the Worthy or Get Fixed Boi worlds the blast can damage other players because
        // this projectile is ProjectileID.Sets.Explosive[Type] = true;. It also takes care of hurting the owner. The Projectile.PrepareBombToBlow
        // and Projectile.HurtPlayer methods can be used directly if needed for a projectile not using ProjectileID.Sets.Explosive

        // Play an exploding sound.
        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);
       
        // Resize the projectile again so the explosion dust and gore spawn from the middle.
        // Rocket I: 22, Rocket III: 80, Mini Nuke Rocket: 50
        Projectile.Resize(336, 336);

        // Spawn a bunch of smoke dusts.
        for (int i = 0; i < 10; i++)
        {
            Dust smokeDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptSpray, 0f, 0f, 100, default, 1.5f);
            smokeDust.velocity *= 13.5f;
            smokeDust.noGravity = true;
            Dust smoke3Dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Purple, 0f, 0f, 100, default, 1.5f);
            smoke3Dust.velocity *= 11.25f;
        }

        // Spawn a bunch of fire dusts.
        for (int j = 0; j < 20; j++)
        {
            Dust fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.BubbleBurst_Purple, 0f, 0f, 100, default, 3.5f);
            fireDust.noGravity = true;
            fireDust.velocity *= 9f;
            fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, 0f, 0f, 100, default, 1.5f);
            fireDust.velocity *= 3.5f;
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffType<Purpled>(), 300);
    }

    
}
public class UnstablePotionBoom : ModProjectile
{
    public override void SetStaticDefaults()
    {
        // This set handles some things for us already:
        // Sets the timeLeft to 3 and the projectile direction when colliding with an NPC or player in PVP (so the explosive can detonate).
        // Explosives also bounce off the top of Shimmer, detonate with no blast damage when touching the bottom or sides of Shimmer, and damage other players in For the Worthy worlds.
        ProjectileID.Sets.Explosive[Type] = true;
        ProjectileID.Sets.RocketsSkipDamageForPlayers[Type] = true;

        // This set makes it so the rocket doesn't deal damage to players. Only used for vanilla rockets.
        // Simply remove the Projectile.HurtPlayer() part to stop the projectile from damaging its user.
        // ProjectileID.Sets.RocketsSkipDamageForPlayers[Type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.width = 36;
        Projectile.height = 36;
        Projectile.friendly = true;
        Projectile.penetrate = -1; // Infinite penetration so that the blast can hit all enemies within its radius.
        Projectile.DamageType = GetInstance<OmniDamage>();
        Projectile.light = 0.2f; // How much light emit around the projectile
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.timeLeft = 2;
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

       

       
    }

   

    public override void PrepareBombToBlow()
    {
        Projectile.tileCollide = false; // This is important or the explosion will be in the wrong place if the rocket explodes on slopes.
        Projectile.alpha = 255; // Make the rocket invisible.

        // Resize the hitbox of the projectile for the blast "radius".
        // Rocket I: 128, Rocket III: 200, Mini Nuke Rocket: 250
        // Measurements are in pixels, so 128 / 16 = 8 tiles.
        Projectile.Resize(933, 933);
        // Set the knockback of the blast.
        // Rocket I: 8f, Rocket III: 10f, Mini Nuke Rocket: 12f
        Projectile.knockBack = 16f;
    }

    public override void OnKill(int timeLeft)
    {
        // Vanilla code takes care ensuring that in For the Worthy or Get Fixed Boi worlds the blast can damage other players because
        // this projectile is ProjectileID.Sets.Explosive[Type] = true;. It also takes care of hurting the owner. The Projectile.PrepareBombToBlow
        // and Projectile.HurtPlayer methods can be used directly if needed for a projectile not using ProjectileID.Sets.Explosive

        // Play an exploding sound.
        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);
        SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, Projectile.position);
        // Resize the projectile again so the explosion dust and gore spawn from the middle.
        // Rocket I: 22, Rocket III: 80, Mini Nuke Rocket: 50
        Projectile.Resize(880, 880);

        // Spawn a bunch of smoke dusts.
        for (int i = 0; i < 28; i++)
        {
            Dust smokeDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptSpray, 0f, 0f, 100, default, 1.5f);
            smokeDust.velocity *= 13.5f;
            smokeDust.noGravity = true;
            Dust smoke3Dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Purple, 0f, 0f, 100, default, 1.5f);
            smoke3Dust.velocity *= 11.25f;
        }

        // Spawn a bunch of fire dusts.
        for (int j = 0; j < 36; j++)
        {
            Dust fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.BubbleBurst_Purple, 0f, 0f, 100, default, 3.5f);
            fireDust.noGravity = true;
            fireDust.velocity *= 9f;
            fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, 0f, 0f, 100, default, 1.5f);
            fireDust.velocity *= 3.5f;
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffType<Purpled>(), 300);
    }


}


public class PurpleDeathBlam : GlobalNPC
{
    public override bool InstancePerEntity => true;

    public override void OnKill(NPC npc)
    {
        Player player = Main.player[npc.lastInteraction];

        int baseDamage = 444;
       
        float totalMultiplier = 1f;

       
        totalMultiplier += player.GetDamage(DamageClass.Generic).Additive - 1f;

        // Get all damage classes and check if they should contribute to Omni 
        // Omni Damage gets 67% of specialized class bonuses
        DamageClass[] specializedClasses = new DamageClass[] {
        DamageClass.Magic,
        DamageClass.Melee,
        DamageClass.Ranged,
        DamageClass.Summon,
        DamageClass.Throwing,
        GetInstance<StupidDamage>()
        };
        foreach (var damageClass in specializedClasses)
        {
            
            float classBonus = player.GetDamage(damageClass).Additive - 1f;
            if (classBonus > 0)
            {
                totalMultiplier += classBonus * 0.67f;
            }
        }

       
        int calculatedDamage = (int)(baseDamage * totalMultiplier);

       
        if (calculatedDamage < 1) calculatedDamage = 1;
        int finalDamage = (int)(calculatedDamage + (npc.lifeMax * 0.11));

        var source = npc.GetSource_FromAI();

        if (npc.HasBuff(BuffType<Purpled>()))
        {

            Projectile.NewProjectile(source, npc.Center, Vector2.Zero * 0.01f, ProjectileType<UnstablePotionBoom>(), finalDamage, 6f, Main.myPlayer);
        }
    }
}