using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.DataStructures;
using static HendecamMod.Content.Items.Accessories.ImprovisedLaserSight;

namespace HendecamMod.Content.Items.Accessories;

public class TacticalLaserSight : ModItem
{
    // By declaring these here, changing the values will alter the effect, and the tooltip

    public static readonly int AdditiveRangedDamageBonus = 8;

    public static readonly int RangedCritBonus = 10;

    // Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.LightPurple;
        Item.value = 636000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "8% increased ranged damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "10% increased ranged crit chance")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Lens, 2);
        recipe.AddIngredient<PlutoniumBar>(9);
        recipe.AddIngredient<ImprovisedLaserSight>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        
        player.GetDamage(DamageClass.Ranged) += AdditiveRangedDamageBonus / 100f;

        player.GetCritChance(DamageClass.Ranged) += RangedCritBonus;
        player.GetModPlayer<LaserDrawPurple>().Laser = true;
    }
    public class LaserDrawPurple : ModPlayer
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
    public class LaserSightPurpleDrawLayer : PlayerDrawLayer
    {
        // Define where in the draw order this layer appears
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.BackAcc);

        // Control when this layer is visible
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.GetModPlayer<LaserDrawPurple>().Laser && !drawInfo.drawPlayer.dead;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            var modPlayer = player.GetModPlayer<LaserDrawPurple>();

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
            if (distance > 924f) return;

            // Get screen position
            Vector2 screenStart = startPos - Main.screenPosition;
            Vector2 screenEnd = endPos - Main.screenPosition;

            // Draw the laser beam
            DrawLaserBeam(drawInfo, screenStart, screenEnd, distance);
        }

        private void DrawLaserBeam(PlayerDrawSet drawInfo, Vector2 start, Vector2 end, float distance)
        {
            // You'll need these textures - create them in your Assets folder
            Texture2D laserTexture = ModContent.Request<Texture2D>("HendecamMod/Content/Effects/LaserBeamPurple").Value;
            Texture2D circleTexture = ModContent.Request<Texture2D>("HendecamMod/Content/Effects/LaserEndPurple").Value;

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

            Color laserColor = new Color(210, 20, 255, 100) * 0.6f; // Green with transparency

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
                new Vector2(0, laserTexture.Height / 2 ),
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