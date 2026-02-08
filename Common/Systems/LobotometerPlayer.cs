using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace HendecamMod.Common.Systems 
{
    public class LobotometerPlayer : ModPlayer
    {
        public float Current;
        public float BaseMax = 200f; // Store the base max
        public float Max; // This will be calculated
        public float MaxBonus = 0f; // Track bonus from equipment

        // Base decay per second (before modifiers)
        public float BaseDecayRate = 40f;

        // Multipliers for armor / buffs / accessories
        public float DecayRateMultiplier = 1f;

        // Time since last stupid weapon use
        private int ticksSinceLastUse;

        public override void ResetEffects()
        {
            // Accessories / buffs can modify this
            DecayRateMultiplier = 1f;

            // Reset the bonus each frame first
            MaxBonus = 0f;
        }

        public override void PostUpdate()
        {
            // Calculate the actual Max after all bonuses have been applied
            Max = BaseMax + MaxBonus;

            // Clamp current to the new max (in case max decreased)
            if (Current > Max)
                Current = Max;

            ticksSinceLastUse++;

            // Start decaying after ~2 seconds (120 ticks)
            if (ticksSinceLastUse > 120 && Current > 0f)
            {
                float decayPerTick =
                    (BaseDecayRate * DecayRateMultiplier) / 60f;

                Current -= decayPerTick;
                if (Current < 0f)
                    Current = 0f;
            }
        }

        public override void PostUpdateMiscEffects()
        {
            // Activating shader
            ScreenShaderData shaderData = Filters.Scene.Activate("HendecamMod:LobotomyScreen").GetShader();
            // Moving to [0, 1] range
            float effectIntensityMultiplier = 0.5f;
            shaderData.UseIntensity((Current / Max) * effectIntensityMultiplier);
        }

        public void AddLobotometer(float amount = 5f)
        {
            Current += amount;
            if (Current > Max)
                Current = Max;

            ticksSinceLastUse = 0;
        }

        /// <summary>
        /// 0–1 value with gentle scaling so higher Max doesn’t explode visuals.
        /// </summary>
        public float NormalizedIntensity
        {
            get
            {
                if (Max <= 0f)
                    return 0f;

                float t = MathHelper.Clamp(Current / Max, 0f, 1f);

                // Gentle curve: strong early feedback, softer at high values
                // You can tweak the exponent freely
                return MathF.Pow(t, 0.6f);
            }
        }
    }
    [Autoload(Side = ModSide.Client)]
    public class LobotometerUISystem : ModSystem
    {
        private Texture2D frame;
        private bool textureLoaded = false;

        public override void Load()
        {
            Main.NewText("LobotometerUISystem loading...");

            try
            {
                // Updated path to match where you moved it
                frame = ModContent.Request<Texture2D>("HendecamMod/Content/Effects/Lobotometer", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

                if (frame != null && !frame.IsDisposed)
                {
                    textureLoaded = true;
                    Main.NewText($"UI texture loaded: {frame.Width}x{frame.Height}");
                }
                else
                {
                    Main.NewText("ERROR: Texture loaded but is null or disposed!");
                }
            }
            catch (Exception ex)
            {
                Main.NewText($"ERROR loading UI texture: {ex.Message}");
                Main.NewText($"Make sure Lobotometer.png exists at: HendecamMod/Content/Effects/");
            }
        }

        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            if (!textureLoaded) return;

            Player player = Main.LocalPlayer;
            if (player == null || !player.active) return;

            var lobo = player.GetModPlayer<LobotometerPlayer>();
            if (lobo == null) return;

            // Remove the spammy debug line - only show when significant changes happen
            float lastDisplayedValue = -1f;
            if (Math.Abs(lobo.Current - lastDisplayedValue) > 1f)
            {

                lastDisplayedValue = lobo.Current;
            }

            if (lobo.Current <= 0f) return;

            try
            {
                // Position above player's head
                Vector2 worldPos = player.Center + new Vector2(-87f, 25f);
                Vector2 screenPos = worldPos - Main.screenPosition;

                // Only draw if on screen
                Rectangle screenRect = new Rectangle(0, 0, Main.screenWidth, Main.screenHeight);
                if (!screenRect.Contains((int)screenPos.X, (int)screenPos.Y))
                    return;

                // Center the frame
                Vector2 origin = new Vector2(frame.Width / 2, frame.Height / 2);

                // Draw frame
                spriteBatch.Draw(frame, screenPos, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);

                // Fill bar - adjust these values based on your actual texture
                int border = 4; // Adjust this based on your texture's border
                int fillWidth = frame.Width - (border * 6);
                int fillHeight = frame.Height - (border * 2);

                float fillPercent = MathHelper.Clamp(lobo.Current / lobo.Max, 0f, 1f);
                int currentWidth = (int)(fillWidth * fillPercent);

                if (currentWidth > 0)
                {
                    Rectangle fillRect = new Rectangle(
                        (int)(screenPos.X - origin.X + border),
                        (int)(screenPos.Y - origin.Y + border),
                        currentWidth,
                        fillHeight
                    );

                    spriteBatch.Draw(
                        TextureAssets.MagicPixel.Value,
                        fillRect,
                        new Color(250, 140, 80) * 0.5f // Slightly transparent
                    );
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}