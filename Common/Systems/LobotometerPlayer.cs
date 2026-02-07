using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace HendecamMod.Common.Systems // DO NOT FUCKING VIBE CODE EVER THIS WAS THE WORST NIGHT OF MY LIFE ATTEMPTING TO VIBE CODE THE LOBOTOMETER MADE ME WANT TO KILL MYSELF
{
    public class LobotometerPlayer : ModPlayer
    {
        public float Current;
        public float Max = 250f;

        // --- Tuning knobs ---
        public float GainPerUse = 5f;

        // Base decay per second (before modifiers)
        public float BaseDecayRate = 20f;

        // Multipliers for armor / buffs / accessories
        public float DecayRateMultiplier = 1f;

        // Time since last stupid weapon use
        private int ticksSinceLastUse;

        public override void ResetEffects()
        {
            // Accessories / buffs can modify this
            DecayRateMultiplier = 1f;
        }

        public override void PostUpdate()
        {
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

        public void AddLobotometer()
        {
            Current += GainPerUse;
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