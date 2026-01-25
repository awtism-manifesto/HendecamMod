using HendecamMod.Content.Items.Weapons;
using HendecamMod.Content.Poop;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using HendecamMod.Content.Rarities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace HendecamMod.Content.Items
{
    public class TheSecondAmendment : ModItem
    {
        
        public override void SetDefaults()
        {

          


            Item.width = 130;
            Item.height = 60; 
            Item.scale = 1f;
            Item.rare = ModContent.RarityType<HotPink>(); 
            Item.value = 250000000;
            // Use Properties
            Item.useTime = 1; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 12; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.



            // Weapon Properties
            Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            Item.damage = 1776; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 20f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            
            
            Item.ArmorPenetration = 176;

            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 19.95f; // The speed of the projectile (measured in pixels per frame.)
            Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
        }



        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) || (ModLoader.TryGetMod("FargowiltasSouls", out Mod FargoMerica)|| (ModLoader.TryGetMod("ContinentOfJourney", out Mod HomeMerica))))
            { 
                damage = (int)(damage * 0.67f);
            }
            else 
            {
                damage = (int)(damage * 0.45f);
            }



               
            SoundEngine.PlaySound(SoundID.Item68, player.position);
            SoundEngine.PlaySound(SoundID.Item61, player.position);
            for (int i = 0; i < 3; i++) // Fire 3 random projectiles
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(2.25f));
                newVelocity *= 1f - Main.rand.NextFloat(0.2f);

                int choice = Main.rand.Next(70); // Choose from 0 to 69 (adjust as needed)

                int projType = type;
                int projDamage = damage;
                

                switch (choice)
                {
                    case 0:
                        projType = ModContent.ProjectileType<DracoRound>();
                        break;
                    case 1:
                        projType = ModContent.ProjectileType<PhotonSpawn>();
                        break;
                    case 2:
                        projType = ModContent.ProjectileType<RadShot>();
                        break;
                    case 3:
                        projType = ModContent.ProjectileType<ChargeLaser>();
                        break;
                    case 4:
                        projType = ModContent.ProjectileType<PlutoShot>();
                        break;
                    case 5:
                        projType = ModContent.ProjectileType<MagnumShot>();
                        break;
                    case 6:
                        projType = ModContent.ProjectileType<AstatineBullet>();
                        break;
                    case 7:
                        projType = ModContent.ProjectileType<MerFlare>();
                        break;
                    case 8:
                        projType = ModContent.ProjectileType<BeetleRoundProjectile>();
                        break;
                    case 9:
                        projType = ModContent.ProjectileType<fiveproj>();
                        break;
                    case 10:
                        projType = ModContent.ProjectileType<DragonSpawn>();
                        break;
                    case 11:
                        projType = ModContent.ProjectileType<fiveproj>();
                        break;
                    case 12:
                        projType = ModContent.ProjectileType<ZazaSmoke>();
                        break;
                    case 13:
                        projType = ModContent.ProjectileType<JfkBullet>();
                        break;
                    case 14:
                        projType = ModContent.ProjectileType<DaeRound>();
                        break;
                    case 15:
                        projType = ModContent.ProjectileType<FakeRocket2>();
                        break;
                    case 16:
                        projType = ModContent.ProjectileType<SpermRange>();
                        break;
                    case 17:
                        projType = ModContent.ProjectileType<CiaSpawn>();
                        break;
                    case 18:
                        projType = ModContent.ProjectileType<PulseShot>();
                        break;
                    case 19:
                        projType = ModContent.ProjectileType<TerraRound>();
                        break;
                    case 20:
                        projType = ProjectileID.IchorBullet;
                        break;
                    case 21:
                        projType = ProjectileID.CursedBullet;
                        break;
                    case 22:
                        projType = ProjectileID.BlackBolt;
                        break;
                    case 23:
                        projType = ProjectileID.VenomBullet;
                        break;
                    case 24:
                        projType = ModContent.ProjectileType<CryoBullet>();
                        break;
                    case 25:
                        projType = ProjectileID.NanoBullet;
                        break;
                    case 26:
                        projType = ProjectileID.SilverBullet;
                        break;
                    case 27:
                        projType = ProjectileID.ZapinatorLaser;
                        break;
                    case 28:
                        projType = ModContent.ProjectileType<LycoSpawn>();
                        break;
                    case 29:
                        projType = ProjectileID.SnowBallFriendly;
                        break;
                    case 30:
                        projType = ProjectileID.CandyCorn;
                        break;
                    case 31:
                        projType = ProjectileID.CrystalBullet;
                       
                        break;
                    case 32:
                        projType = ModContent.ProjectileType<OilBallRanged>();
                        projDamage = (int)(damage * 1.5f);
                        break;
                    case 33:
                        projType = ProjectileID.PlatinumCoin;
                        break;
                    case 34:
                        projType = ProjectileID.GoldCoin;
                        break;
                    case 35:
                        projType = ProjectileID.SilverCoin;
                        break;
                    case 36:
                        projType = ProjectileID.CopperCoin;
                        break;
                    case 37:
                        projType = ProjectileID.VortexBeaterRocket;
                        break;
                    case 38:
                        projType = ProjectileID.MoonlordBullet;
                        break;
                    case 39:
                        projType = ModContent.ProjectileType<CopperShort>();
                        break;
                    case 40:
                        projType = ProjectileID.FairyQueenRangedItemShot;
                        break;
                    case 41:
                        projType = ModContent.ProjectileType<ApexPlasmaBullet>();
                        break;
                    case 42:
                        projType = ModContent.ProjectileType<TerraRound>();
                        break;
                    case 43:
                        projType = ModContent.ProjectileType<TerraRound>();
                        break;
                    case 44:
                        projType = ProjectileID.ChlorophyteBullet;
                        break;
                    case 45:
                        projType = ModContent.ProjectileType<ApexPlasmaBullet>();
                        break;
                    case 46:
                        projType = ProjectileID.GoldenBullet;
                        break;
                    case 47:
                        projType = ProjectileID.MeteorShot;
                        break;
                    case 48:
                        projType = ProjectileID.BulletHighVelocity;
                        break;
                    case 49:
                        projType = ProjectileID.ChlorophyteBullet;
                        break;
                    case 50:
                        projType = ProjectileID.BloodArrow;
                        break;
                    case 51:
                        projType = ProjectileID.Bullet;
                        break;
                    case 52:
                        projType = ProjectileID.Bullet;
                        break;
                    case 53:
                        projType = ModContent.ProjectileType<fiveproj>();
                        break;
                    case 54:
                        projType = ModContent.ProjectileType<BouncingBulletProj>();
                        break;
                    case 55:
                        projType = ProjectileID.MoonlordBullet;
                        break;
                    case 56:
                        projType = ProjectileID.MoonlordBullet;
                        break;
                    case 57:
                        projType = ModContent.ProjectileType<MerFlare>();
                        break;
                    case 58:
                        projType = ModContent.ProjectileType<MerFlare>();
                        break;
                    case 59:
                        projType = ModContent.ProjectileType<MerFlare>();
                        break;
                    case 60:
                        projType = ModContent.ProjectileType<PhotonSpawn>();
                        break;
                    case 61:
                        projType = ProjectileID.ZapinatorLaser;
                        break;
                    case 62:
                        projType = ModContent.ProjectileType<BeetleRoundProjectile>();
                        break;
                    case 63:
                        projType = ModContent.ProjectileType<DragonSpawnShadow>();
                        break;
                    case 64:
                        projType = ProjectileID.SilverBullet;
                        break;
                    case 65:
                        projType = ModContent.ProjectileType<RadShot>();
                        break;
                    case 66:
                        projType = ModContent.ProjectileType<DracoRound>();
                        break;
                    case 67:
                        projType = ModContent.ProjectileType<LycoSpawn>();
                        break;
                    case 68:
                        projType = ModContent.ProjectileType<PlutoShot>();
                        break;
                    case 69:
                        projType = ModContent.ProjectileType<fiveproj>();
                        break;
                }

                Projectile.NewProjectileDirect(source, position, newVelocity, projType, projDamage, knockback, player.whoAmI);
            }

           
            if (Main.rand.NextBool(69))
            {
                int bonusType = ModContent.ProjectileType<RiverHead>();
                Vector2 bonusVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(8));
                Projectile.NewProjectileDirect(source, position, bonusVelocity, bonusType, damage, knockback, player.whoAmI);
            }
            if (Main.rand.NextBool(15))
            {
                return true;
            }
            else return false;
        }



        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "Spews out a truly insane amount of munitions. ");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "We once fought for the false promise of freedom. Now we must fight for true freedom. 161.")
            {
                OverrideColor = new Color(255, 45, 95)
            };
            tooltips.Add(line);



            
        }
        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<TrueJfkExperience>();
            recipe.AddIngredient(ItemID.SDMG);
            recipe.AddIngredient<GlockAndBalls>();
            recipe.AddIngredient<AA12>();
            recipe.AddIngredient(ItemID.VortexBeater);
            recipe.AddIngredient<DaedalusStormgun>();
            recipe.AddIngredient<PhotonShotgun>();
            recipe.AddIngredient(ItemID.Xenopopper);
            recipe.AddIngredient<TheMagnum>();
            recipe.AddIngredient<MidnightAfterburner>();
            recipe.AddIngredient<ApexPlasmaCannon>();
            recipe.AddIngredient<PlutoniumAutoPistol>();
            recipe.AddIngredient<ATFsNightmare>();
            recipe.AddIngredient<GunThatKillsPeople>();
            recipe.AddIngredient<TheNanoshot>();
            recipe.AddIngredient<PoopyAutoPistol>();
            recipe.AddIngredient(ItemID.CandyCornRifle);
            recipe.AddIngredient(ItemID.VenusMagnum);
           
            recipe.AddIngredient<ThePrimeTime>();
            recipe.AddIngredient(ItemID.Megashark);
            recipe.AddIngredient<CryonicCarbine>();
            recipe.AddIngredient<Bundlebuss>();
            recipe.AddIngredient<MintalMachinePistol>();
            recipe.AddIngredient<VP70>();
            recipe.AddIngredient<GenderDefender>();
            recipe.AddIngredient(ItemID.SnowballCannon);
           
            recipe.AddIngredient<CopperShortmachinegun>();
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
           

            if (ModLoader.TryGetMod("Terbritish", out Mod TerBritish) && TerBritish.TryFind<ModItem>("BrenGun", out ModItem BrenGun))
            {
                recipe.AddIngredient(BrenGun.Type);
            }
           
            if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind<ModItem>("SomaPrime", out ModItem SomaPrime)
                && CalMerica.TryFind<ModItem>("HalibutCannon", out ModItem HalibutCannon))


            {

                recipe.AddIngredient(SomaPrime.Type);

                recipe.AddIngredient(HalibutCannon.Type);
            }
           
            if (ModLoader.TryGetMod("Paracosm", out Mod ParaMerica) && ParaMerica.TryFind<ModItem>("Hyperion", out ModItem Hyperion))


            {
                recipe.AddIngredient(Hyperion.Type);


            }
            if (ModLoader.TryGetMod("Consolaria", out Mod ConsMerica) && ConsMerica.TryFind("VolcanicRepeater", out ModItem VolcanicRepeater))


            {
                recipe.AddIngredient(VolcanicRepeater.Type);



            }
            if (ModLoader.TryGetMod("Macrocosm", out Mod MacroMerica) && MacroMerica.TryFind<ModItem>("ArtemiteMagnum", out ModItem ArtemiteMagnum)
                 && MacroMerica.TryFind<ModItem>("TychoDesertEagle", out ModItem TychoDesertEagle) && MacroMerica.TryFind<ModItem>("Cruithne", out ModItem Cruithne))
            {
                recipe.AddIngredient(Cruithne.Type);
                recipe.AddIngredient(ArtemiteMagnum.Type);
                recipe.AddIngredient(TychoDesertEagle.Type);
            }
            if (ModLoader.TryGetMod("ZenithGunReturn", out Mod ZenithMerica) && ZenithMerica.TryFind<ModItem>("ZenithGun", out ModItem ZenithGun))


            {
                recipe.AddIngredient(ZenithGun.Type);


            }
            if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica) && ThorMerica.TryFind<ModItem>("TerrariumPulseRifle", out ModItem TerrariumPulseRifle)
               && ThorMerica.TryFind<ModItem>("OmniCannon", out ModItem OmniCannon)
                && ThorMerica.TryFind<ModItem>("TrenchSpitter", out ModItem TrenchSpitter))

            {
                recipe.AddIngredient(TrenchSpitter.Type);
                recipe.AddIngredient(TerrariumPulseRifle.Type);

                recipe.AddIngredient(OmniCannon.Type);

            }
           
            if (ModLoader.TryGetMod("Redemption", out Mod RedMerica) && RedMerica.TryFind<ModItem>("DepletedCrossbow", out ModItem DepletedCrossbow)
              && RedMerica.TryFind<ModItem>("XeniumElectrolaser", out ModItem XeniumElectrolaser))

            {
                recipe.AddIngredient(DepletedCrossbow.Type);

                recipe.AddIngredient(XeniumElectrolaser.Type);
            }
            if (ModLoader.TryGetMod("StarsAbove", out Mod StarMerica) && StarMerica.TryFind<ModItem>("CosmicDestroyer", out ModItem CosmicDestroyer)
              && StarMerica.TryFind<ModItem>("InheritedCaseM4A1", out ModItem InheritedCaseM4A1))

            {
                recipe.AddIngredient(CosmicDestroyer.Type);
                recipe.AddIngredient(InheritedCaseM4A1.Type);
            }
           
            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod FargoMerica) && FargoMerica.TryFind<ModItem>("TheBiggestSting", out ModItem TheBiggestSting)
          && FargoMerica.TryFind<ModItem>("NavalRustrifle", out ModItem NavalRustrifle)
          && FargoMerica.TryFind<ModItem>("Lightslinger", out ModItem Lightslinger))

            {

                recipe.AddIngredient(NavalRustrifle.Type);
                recipe.AddIngredient(Lightslinger.Type);
                recipe.AddIngredient(TheBiggestSting.Type);
            }
            if (ModLoader.TryGetMod("ContinentOfJourney", out Mod HomeMerica) && HomeMerica.TryFind<ModItem>("Blackout", out ModItem Blackout)
              && HomeMerica.TryFind<ModItem>("FortSniper", out ModItem FortSniper)
               
               && HomeMerica.TryFind<ModItem>("ClockworkMinigun", out ModItem ClockworkMinigun)
                && HomeMerica.TryFind<ModItem>("EssenceofBright", out ModItem EssenceofBright))


            {
                recipe.AddIngredient(ClockworkMinigun.Type);
                recipe.AddIngredient(Blackout.Type);

                recipe.AddIngredient(FortSniper.Type);
               
                recipe.AddIngredient(EssenceofBright.Type, 5);
            }

        }
        
        

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-45f, -4f);
        }
    }
}