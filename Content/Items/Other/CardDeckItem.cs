using System;
using HendecamMod.Common.Systems.CardGame;
using HendecamMod.Networking.Packets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Other
{
    public class CardDeckItem : ModItem
    {
        public override string Texture => "HendecamMod/Content/Items/Other/CardDeckItem";

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<CardDeckProjectile>();
            Item.shootSpeed = 5.5f;
            Item.value = Item.buyPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return player.ownedProjectileCounts[Item.shoot] <= 0;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }

    public class CardDeckProjectile : ModProjectile
    {
        public override string Texture => "HendecamMod/Content/Items/Other/CardDeckItem";

        private bool _hostedTable;
        private Vector2 _anchorCenter;

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 3600;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            if (!_hostedTable)
            {
                Projectile.velocity *= 0.9f;
                Projectile.rotation += Projectile.velocity.X * 0.05f;

                if (Projectile.velocity.LengthSquared() < 0.2f)
                {
                    Projectile.velocity = Vector2.Zero;
                    Projectile.rotation = 0f;
                    _anchorCenter = Projectile.Center;
                    TriggerHostSetup();
                }
            }
            else
            {
                Projectile.velocity = Vector2.Zero;
                Projectile.rotation = MathF.Sin(Main.GameUpdateCount * 0.04f) * 0.08f;
                Projectile.Center = _anchorCenter + new Vector2(0f, MathF.Sin(Main.GameUpdateCount * 0.06f) * 4f);

                if (CardGameManager.Instance.TableIsOpen && CardGameManager.Instance.HostPlayerWhoAmI == Projectile.owner)
                    Projectile.timeLeft = 60;
            }
        }

        private void TriggerHostSetup()
        {
            _hostedTable = true;
            if (Projectile.owner == Main.myPlayer)
            {
                CardGameManager.Instance.RequestHostGameFromThrow(Projectile.Center);
                ModContent.GetInstance<CardGameUISystem>().OpenTableUI();
            }
        }
    }
}