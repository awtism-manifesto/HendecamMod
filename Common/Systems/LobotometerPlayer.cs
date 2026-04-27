using HendecamMod.Content.Items.Accessories;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader.IO;

namespace HendecamMod.Common.Systems
{
    public class LobotometerPlayer : ModPlayer
    {
        public float Current;
        public float BaseMax = 50f;
        public float Max;

        // This is the PERMANENT bonus from consumed items (character-specific)
        public float PermanentBonus = 0f;

        // This is the TEMPORARY bonus from equipment/buffs
        public float TemporaryBonus = 0f;

        // Total MaxBonus = PermanentBonus + TemporaryBonus
        public float MaxBonus => PermanentBonus + TemporaryBonus;

        public float BaseDecayRate = 40f;
        public float DecayRateMultiplier = 1f;
        public int ticksSinceLastUse;
        public float decayPerTick = 0.66f;
        public int decayPerSecond = 40;
        public float MaxScalingFactor = 0.1f;

        public override void ResetEffects()
        {
            // Reset temporary bonuses each frame
            TemporaryBonus = 0f;
            DecayRateMultiplier = 1f;

            // DON'T calculate Max or clamp Current here anymore!
            // This happens BEFORE accessories add their bonuses
        }

        public override void PostUpdate()
        {
            // Recalculate Max AFTER all temporary bonuses have been applied
            // This runs after all equipment updates
            Max = BaseMax + MaxBonus;

            // Clamp current to the new max (in case max decreased)
            if (Current > Max)
                Current = Max;

            // Calculate decay rate based on current Max
            if (Max == 200 && DecayRateMultiplier == 1f)
            {
                decayPerTick = 0.66f;
            }
            else
            {
                float basePerTick = (BaseDecayRate * DecayRateMultiplier) / 60f;
                float maxScaling = (Max * MaxScalingFactor) / 60f;
                decayPerTick = basePerTick + maxScaling;
                decayPerSecond = (int)(decayPerTick * 60);
            }
        }

        public override void PreUpdate()
        {
            // Recalculate Max again to be safe (temporary bonuses are already set by now)
            Max = BaseMax + MaxBonus;

            if (Current > Max)
                Current = Max;

            ticksSinceLastUse++;

            if (ticksSinceLastUse > 120 && Current > 0f && !Player.GetModPlayer<BaseSpike>().Spiked)
            {
                float basePerTick = (BaseDecayRate * DecayRateMultiplier) / 60f;
                float maxScaling = (Max * MaxScalingFactor) / 60f;
                decayPerTick = basePerTick + maxScaling;

                Current -= decayPerTick;
                if (Current < 0f)
                    Current = 0f;
            }
        }

        // CHARACTER-SPECIFIC SAVE DATA
        public override void SaveData(TagCompound tag)
        {
            // Only save the permanent bonus (temporary bonuses don't need saving)
            if (PermanentBonus > 0f)
                tag["LoboPermanentBonus"] = PermanentBonus;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("LoboPermanentBonus"))
                PermanentBonus = tag.GetFloat("LoboPermanentBonus");
            else
                PermanentBonus = 0f;
        }

        // Helper method to check if character has reached max permanent bonus
        public bool CanIncreasePermanent(int amount)
        {
            float maxTotalBonus = 200f; // Maximum permanent bonus allowed
            return PermanentBonus + amount <= maxTotalBonus;
        }

        // Helper method to permanently increase the stat
        public void IncreasePermanent(int amount)
        {
            if (CanIncreasePermanent(amount))
            {
                PermanentBonus += amount;

                // Heal current by the same amount (like Life Crystals)
                Current += amount;
                if (Current > Max)
                    Current = Max;
            }
        }

        // Helper method for accessories to add temporary bonuses
        public void AddTemporaryBonus(float bonus)
        {
            TemporaryBonus += bonus;
        }

        public override void PostUpdateMiscEffects()
        {
            /*if (Main.myPlayer == 0)
            {
                Main.NewText("0: " + Current / Max);
            }
            else if (Main.myPlayer == 1)
            {
                Main.NewText("1: " + Current / Max);
            }*/

           

            if (!Main.dedServ)
            {
                // Redigit is the greatest Israeli soldier
                // The world has ever known
                if (Current > 0 && !Player.dead)  // does player.dead even do anything? this code does not fucking work
                {
                    if (!Filters.Scene["HendecamMod:LobotomyScreen"].IsActive())
                    {
                        Filters.Scene.Activate("HendecamMod:LobotomyScreen");
                        if (Player.dead)
                        {
                            Filters.Scene.Deactivate("HendecamMod:LobotomyScreen");
                        }
                        
                    }
                    float effectIntensityMultiplier = 0.5f;
                    if (Main.zenithWorld)
                    {
                        effectIntensityMultiplier = 0.9f;
                    }

                    Filters.Scene["HendecamMod:LobotomyScreen"].GetShader().UseIntensity((Current / Max) * effectIntensityMultiplier);
                }
            }
        }

        public void AddLobotometer(float amount = 5f)
        {
            Current += amount;
            if (Current > Max)
                Current = Max;

            ticksSinceLastUse = 0;
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
                frame = Request<Texture2D>("HendecamMod/Content/Effects/Lobotometer", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

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

           
            float lastDisplayedValue = -1f;
            if (Math.Abs(lobo.Current - lastDisplayedValue) > 1f)
            {

                lastDisplayedValue = lobo.Current;
            }

            if (lobo.Current <= 0f) return;

            try
            {
                // Position above player's head
                Vector2 worldPos = player.Center + new Vector2(-86f, 25f);
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
                int currentWidth = (int)((int)(fillWidth * fillPercent) * 0.9f);

                if (currentWidth > 0)
                {
                    Rectangle fillRect = new Rectangle(
                        (int)(screenPos.X - origin.X + border + 16),
                        (int)(screenPos.Y - origin.Y + border + 0.1f),
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
