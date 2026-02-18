using HendecamMod.Content.Buffs;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Accessories;

public class ImprovisedLaserSight : ModItem
{

    public static readonly int AdditiveRangedDamageBonus = 4;

    public static readonly int RangedCritBonus = 6;

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Green;
        Item.value = 85000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "4% increased ranged damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "6% increased ranged crit chance")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);



    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe = CreateRecipe();

        recipe.AddIngredient<UraniumBar>(6);
        recipe.AddIngredient<Polymer>(10);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Ranged) += AdditiveRangedDamageBonus / 100f;
        player.GetCritChance(DamageClass.Ranged) += RangedCritBonus;
        player.GetModPlayer<LaserDrawGreen>().Laser = true;

    }

    public class LaserDrawGreen : ModPlayer
    {



        public bool Laser;
        public Vector2 laserEndPosition;

        public override void ResetEffects()
        {
            Laser = false;
        }
        public override void PreUpdate()
        {
            if (Laser)
            {
                // Get mouse position in world coordinates
                laserEndPosition = Main.MouseWorld;
            }
        }

    }
    public class LaserSightPlayerDrawLayer : PlayerDrawLayer
    {
        // Define where in the draw order this layer appears
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.BackAcc);

        // Control when this layer is visible
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.GetModPlayer<LaserDrawGreen>().Laser && !drawInfo.drawPlayer.dead;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            var modPlayer = player.GetModPlayer<LaserDrawGreen>();

            if (modPlayer.laserEndPosition == Vector2.Zero)
                return;

            // Calculate laser start position (from player center)
            Vector2 startPos = player.Center;
            Vector2 endPos = modPlayer.laserEndPosition;

            // Calculate direction and distance
            Vector2 direction = endPos - startPos;
            float distance = direction.Length();
            direction.Normalize();

            // Don't draw if too far
            if (distance > 556f) return;

            // Get screen position
            Vector2 screenStart = startPos - Main.screenPosition;
            Vector2 screenEnd = endPos - Main.screenPosition;

            // Draw the laser beam
            DrawLaserBeam(drawInfo, screenStart, screenEnd, distance);
        }

        private void DrawLaserBeam(PlayerDrawSet drawInfo, Vector2 start, Vector2 end, float distance)
        {
            // You'll need these textures - create them in your Assets folder
            Texture2D laserTexture = ModContent.Request<Texture2D>("HendecamMod/Content/Effects/LaserBeam").Value;
            Texture2D circleTexture = ModContent.Request<Texture2D>("HendecamMod/Content/Effects/LaserEnd").Value;

            // Fallback if textures don't exist - creates simple textures
            if (laserTexture == null || laserTexture.IsDisposed)
            {
                laserTexture = new Texture2D(Main.graphics.GraphicsDevice, 1, 1);
                laserTexture.SetData(new[] { Color.White });
            }

            if (circleTexture == null || circleTexture.IsDisposed)
            {
                circleTexture = new Texture2D(Main.graphics.GraphicsDevice, 5, 5);
                Color[] data = new Color[25];
                for (int i = 0; i < data.Length; i++)
                    data[i] = Color.White;
                circleTexture.SetData(data);
            }

            Color laserColor = new Color(0, 255, 0, 100) * 0.6f; // Green with transparency

            Vector2 direction = end - start;
            float rotation = direction.ToRotation();

            // Draw main beam
            drawInfo.DrawDataCache.Add(new DrawData(
                laserTexture,
                new Vector2(start.X, start.Y),
                null,
                laserColor,
                rotation,
                new Vector2(0, laserTexture.Height / 2),
                new Vector2(distance, 2f),
                SpriteEffects.None,
                0
            ));

            // Draw glow effect (wider, more transparent)
            drawInfo.DrawDataCache.Add(new DrawData(
                laserTexture,
                new Vector2(start.X, start.Y),
                null,
                laserColor * 0.3f,
                rotation,
                new Vector2(0, laserTexture.Height / 2),
                new Vector2(distance, 6f),
                SpriteEffects.None,
                0
            ));

            // Draw end cap (circle at the end)
            drawInfo.DrawDataCache.Add(new DrawData(
                circleTexture,
                end,
                null,
                laserColor,
                0f,
                new Vector2(circleTexture.Width / 2, circleTexture.Height / 2),
                1f,
                SpriteEffects.None,
                0
            ));
        }
    }

}
