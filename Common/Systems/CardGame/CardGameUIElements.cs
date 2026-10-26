using System;
using System.Collections.Generic;
using HendecamMod.Networking.Packets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace HendecamMod.Common.Systems.CardGame
{
    public static class UIFittedText
    {
        public static void Draw(SpriteBatch sb, string text, Vector2 pos, float maxWidth, float maxScale, Color color, bool center = false)
        {
            if (string.IsNullOrEmpty(text))
                return;

            DynamicSpriteFont font = FontAssets.MouseText.Value;
            Vector2 size = ChatManager.GetStringSize(font, text, Vector2.One);
            if (size.X <= 0f)
                return;

            float scale = maxScale;
            if (size.X * scale > maxWidth && maxWidth > 10f)
                scale = maxWidth / size.X;

            Vector2 drawPos = center ? new Vector2(pos.X - (size.X * scale * 0.5f), pos.Y) : pos;
            ChatManager.DrawColorCodedStringWithShadow(sb, font, text, drawPos, color, 0f, Vector2.Zero, new Vector2(scale));
        }
    }

    public class ResponsiveButton : UIPanel
    {
        private string _text;
        private readonly float _maxScale;
        private readonly Action _onPress;
        private readonly Action _onRightPress;
        private float _anchorLeft;
        private int _flashTimer;
        private int _shakeTimer;

        public bool GlowWhenAlert;
        public Color BaseColor = new Color(45, 75, 115, 240);
        public Color BaseBorderColor = new Color(20, 30, 45);
        public Color HoverColor = new Color(75, 115, 170, 255);
        public Color PressColor = new Color(110, 165, 220, 255);
        public Color ErrorColor = new Color(200, 45, 45, 255);
        public Color TextColor = Color.White;

        public ResponsiveButton(string text, float textScale, Action onPress, Action onRightPress = null)
        {
            _text = text;
            _maxScale = textScale;
            _onPress = onPress;
            _onRightPress = onRightPress;
            SetPadding(0f);
            BackgroundColor = BaseColor;
            BorderColor = BaseBorderColor;
        }

        public void SetBounds(float left, float top, float width, float height)
        {
            _anchorLeft = left;
            Left.Set(left, 0f);
            Top.Set(top, 0f);
            Width.Set(width, 0f);
            Height.Set(height, 0f);
        }

        public void SetText(string text) => _text = text ?? "";

        public void TriggerShake()
        {
            _shakeTimer = 22;
            SoundEngine.PlaySound(SoundID.MenuClose);
        }

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);
            _flashTimer = 8;
            SoundEngine.PlaySound(SoundID.MenuTick);
            _onPress?.Invoke();
        }

        public override void RightMouseDown(UIMouseEvent evt)
        {
            base.RightMouseDown(evt);
            if (_onRightPress != null)
            {
                _flashTimer = 8;
                SoundEngine.PlaySound(SoundID.MenuTick);
                _onRightPress.Invoke();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (ContainsPoint(Main.MouseScreen))
                Main.LocalPlayer.mouseInterface = true;

            if (_shakeTimer > 0)
            {
                _shakeTimer--;
                float offset = _shakeTimer > 0 ? MathF.Sin(_shakeTimer * 2.1f) * 7f : 0f;
                Left.Set(_anchorLeft + offset, 0f);
                BackgroundColor = ErrorColor;
                BorderColor = Color.Red;
                Recalculate();
                return;
            }

            if (Left.Pixels != _anchorLeft)
            {
                Left.Set(_anchorLeft, 0f);
                Recalculate();
            }

            if (_flashTimer > 0)
            {
                _flashTimer--;
                BackgroundColor = PressColor;
                BorderColor = Color.Gold;
            }
            else if (GlowWhenAlert)
            {
                float pulse = 0.5f + 0.5f * MathF.Sin((float)Main.timeForVisualEffects * 0.14f);
                BackgroundColor = Color.Lerp(new Color(145, 90, 15, 245), new Color(225, 160, 30, 255), pulse);
                BorderColor = Color.Lerp(Color.Gold, Color.White, pulse);
            }
            else if (IsMouseHovering)
            {
                BackgroundColor = HoverColor;
                BorderColor = Color.Yellow;
            }
            else
            {
                BackgroundColor = BaseColor;
                BorderColor = BaseBorderColor;
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            if (string.IsNullOrEmpty(_text))
                return;

            CalculatedStyle dim = GetDimensions();
            string[] lines = _text.Split('\n');
            float lineStep = 18f;
            float totalH = lines.Length * lineStep;
            float startY = dim.Y + (dim.Height - totalH) * 0.5f + 2f;

            for (int i = 0; i < lines.Length; i++)
            {
                UIFittedText.Draw(
                    spriteBatch,
                    lines[i],
                    new Vector2(dim.X + dim.Width * 0.5f, startY + i * lineStep),
                    dim.Width - 12f,
                    _maxScale,
                    TextColor,
                    center: true);
            }
        }
    }

    public class UISeatElement : UIPanel
    {
        private struct SeatParticle
        {
            public Vector2 Pos;
            public Vector2 Vel;
            public Color Color;
            public float Scale;
            public float Rotation;
            public int Life;
            public int MaxLife;
            public bool IsBadSmoke;
        }

        private readonly int _seatIndex;
        private readonly Action _onClick;
        private readonly List<SeatParticle> _particles = new List<SeatParticle>();
        private int _lastFinishRank;
        private float _animTick;

        public UISeatElement(int seatIndex, Action onClick)
        {
            _seatIndex = seatIndex;
            _onClick = onClick;
            SetPadding(0f);
        }

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);
            _onClick?.Invoke();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (ContainsPoint(Main.MouseScreen))
                Main.LocalPlayer.mouseInterface = true;

            _animTick += 0.05f;
            var mgr = CardGameManager.Instance;
            ModeTheme theme = ModeTheme.ForMode(mgr.CurrentMode);
            SeatInfo seat = mgr.Seats[_seatIndex];
            int rank = seat.HasForfeited ? 4 : seat.FinishRank;

            if (rank != _lastFinishRank)
            {
                if (rank == 1 || rank == 2)
                {
                    int burstCount = rank == 1 ? 26 : 16;
                    Color burstColor = rank == 1 ? new Color(255, 225, 75) : new Color(135, 235, 255);
                    for (int i = 0; i < burstCount; i++)
                    {
                        float angle = Main.rand.NextFloat(MathHelper.TwoPi);
                        float speed = Main.rand.NextFloat(1.2f, 4.2f);
                        _particles.Add(new SeatParticle
                        {
                            Pos = new Vector2(Width.Pixels * 0.5f + Main.rand.NextFloat(-40f, 40f), 46f + Main.rand.NextFloat(-16f, 16f)),
                            Vel = angle.ToRotationVector2() * speed,
                            Color = Color.Lerp(burstColor, Color.White, Main.rand.NextFloat(0f, 0.45f)),
                            Scale = Main.rand.NextFloat(0.35f, 0.75f),
                            Rotation = Main.rand.NextFloat(MathHelper.TwoPi),
                            Life = 0,
                            MaxLife = Main.rand.Next(28, 52),
                            IsBadSmoke = false
                        });
                    }
                    SoundEngine.PlaySound(rank == 1 ? SoundID.ResearchComplete : SoundID.Item4);
                }
                else if (rank == 3 || rank == 4)
                {
                    int burstCount = rank == 4 ? 22 : 14;
                    Color badColor = rank == 4 ? new Color(110, 25, 25) : new Color(115, 75, 35);
                    for (int i = 0; i < burstCount; i++)
                    {
                        _particles.Add(new SeatParticle
                        {
                            Pos = new Vector2(Main.rand.NextFloat(16f, Width.Pixels - 16f), Main.rand.NextFloat(10f, 40f)),
                            Vel = new Vector2(Main.rand.NextFloat(-0.6f, 0.6f), Main.rand.NextFloat(0.6f, 2.2f)),
                            Color = badColor,
                            Scale = Main.rand.NextFloat(0.35f, 0.65f),
                            Rotation = Main.rand.NextFloat(MathHelper.TwoPi),
                            Life = 0,
                            MaxLife = Main.rand.Next(30, 55),
                            IsBadSmoke = true
                        });
                    }
                    SoundEngine.PlaySound(rank == 4 ? SoundID.NPCDeath1 : SoundID.Item16);
                }
                _lastFinishRank = rank;
            }

            if ((rank == 1 || rank == 2) && Main.rand.NextBool(rank == 1 ? 5 : 9))
            {
                Color c = rank == 1 ? new Color(255, 225, 75) : new Color(135, 235, 255);
                _particles.Add(new SeatParticle
                {
                    Pos = new Vector2(Main.rand.NextFloat(12f, Width.Pixels - 12f), Main.rand.NextFloat(10f, Height.Pixels - 12f)),
                    Vel = new Vector2(Main.rand.NextFloat(-0.3f, 0.3f), Main.rand.NextFloat(-0.9f, -0.3f)),
                    Color = c,
                    Scale = Main.rand.NextFloat(0.28f, rank == 1 ? 0.58f : 0.42f),
                    Rotation = Main.rand.NextFloat(MathHelper.TwoPi),
                    Life = 0,
                    MaxLife = Main.rand.Next(24, 42),
                    IsBadSmoke = false
                });
            }
            else if ((rank == 3 || rank == 4) && Main.rand.NextBool(rank == 4 ? 6 : 10))
            {
                Color badColor = rank == 4 ? new Color(120, 30, 30) : new Color(115, 80, 40);
                _particles.Add(new SeatParticle
                {
                    Pos = new Vector2(Main.rand.NextFloat(14f, Width.Pixels - 14f), Main.rand.NextFloat(8f, 28f)),
                    Vel = new Vector2(Main.rand.NextFloat(-0.25f, 0.25f), Main.rand.NextFloat(0.5f, 1.3f)),
                    Color = badColor,
                    Scale = Main.rand.NextFloat(0.28f, 0.5f),
                    Rotation = Main.rand.NextFloat(MathHelper.TwoPi),
                    Life = 0,
                    MaxLife = Main.rand.Next(26, 46),
                    IsBadSmoke = true
                });
            }

            for (int i = _particles.Count - 1; i >= 0; i--)
            {
                var p = _particles[i];
                p.Pos += p.Vel;
                p.Vel *= 0.96f;
                p.Rotation += p.IsBadSmoke ? -0.05f : 0.08f;
                p.Life++;
                if (p.Life >= p.MaxLife)
                    _particles.RemoveAt(i);
                else
                    _particles[i] = p;
            }

            float pulse = 0.5f + 0.5f * MathF.Sin((float)Main.timeForVisualEffects * 0.08f);
            if (rank == 1)
            {
                BackgroundColor = Color.Lerp(new Color(125, 95, 18, 245), new Color(165, 128, 28, 250), pulse);
                BorderColor = IsMouseHovering ? Color.Yellow : Color.Lerp(Color.Gold, Color.LightYellow, pulse);
            }
            else if (rank == 2)
            {
                BackgroundColor = Color.Lerp(new Color(38, 85, 130, 245), new Color(55, 110, 165, 250), pulse);
                BorderColor = IsMouseHovering ? Color.Yellow : new Color(140, 235, 255);
            }
            else if (rank == 3)
            {
                BackgroundColor = new Color(75, 50, 28, 240);
                BorderColor = IsMouseHovering ? Color.Yellow : new Color(150, 95, 45);
            }
            else if (rank == 4)
            {
                BackgroundColor = new Color(68, 22, 26, 240);
                BorderColor = IsMouseHovering ? Color.Yellow : new Color(185, 45, 50);
            }
            else if (mgr.GameInProgress && mgr.CurrentTurnSeat == _seatIndex)
            {
                BackgroundColor = new Color(45, 115, 65, 245);
                BorderColor = Color.Gold;
            }
            else
            {
                BackgroundColor = IsMouseHovering ? theme.SeatHoverBg : theme.SeatBg;
                BorderColor = IsMouseHovering ? Color.Yellow : new Color(20, 30, 45);
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            CalculatedStyle dim = GetDimensions();
            var mgr = CardGameManager.Instance;
            SeatInfo s = mgr.Seats[_seatIndex];
            int rank = s.HasForfeited ? 4 : s.FinishRank;
            bool canHostConfigure = mgr.IsLocalPlayerHost() && !mgr.GameInProgress;

            Texture2D tearTex = TextureAssets.Extra[ExtrasID.SharpTears].Value;
            Vector2 tearOrigin = tearTex.Size() * 0.5f;

            for (int i = 0; i < _particles.Count; i++)
            {
                var p = _particles[i];
                float progress = (float)p.Life / p.MaxLife;
                float alpha = MathF.Sin(progress * MathHelper.Pi);
                Vector2 drawPos = new Vector2(dim.X, dim.Y) + p.Pos;

                if (p.IsBadSmoke)
                {
                    Color darkCol = p.Color * (alpha * 0.85f);
                    spriteBatch.Draw(tearTex, drawPos, null, darkCol, p.Rotation, tearOrigin, new Vector2(p.Scale * 0.7f, p.Scale * 0.7f), SpriteEffects.None, 0f);
                }
                else
                {
                    Color glowCol = p.Color with { A = 0 } * alpha;
                    spriteBatch.Draw(tearTex, drawPos, null, glowCol, p.Rotation, tearOrigin, new Vector2(p.Scale * 0.45f, p.Scale), SpriteEffects.None, 0f);
                    spriteBatch.Draw(tearTex, drawPos, null, glowCol, p.Rotation + MathHelper.PiOver2, tearOrigin, new Vector2(p.Scale * 0.45f, p.Scale), SpriteEffects.None, 0f);
                }
            }

            if (rank >= 1 && rank <= 4)
            {
                Vector2 badgePos = new Vector2(dim.X + dim.Width - 22f, dim.Y + 20f + MathF.Sin(_animTick * 2f) * 2.5f);
                int iconItemId = rank switch
                {
                    1 => ItemID.GoldCrown,
                    2 => ItemID.PlatinumCrown,
                    3 => ItemID.PoopBlock,
                    _ => ItemID.Skull
                };
                Main.instance.LoadItem(iconItemId);
                Texture2D iconTex = TextureAssets.Item[iconItemId].Value;
                spriteBatch.Draw(iconTex, badgePos, null, Color.White, MathF.Sin(_animTick) * 0.1f, iconTex.Size() * 0.5f, 0.88f, SpriteEffects.None, 0f);
            }

            string turnMarker = (mgr.GameInProgress && mgr.CurrentTurnSeat == _seatIndex) ? ">> " : "";
            string nameLine = $"{turnMarker}{s.GetDisplayName(_seatIndex)}";

            string midLine = "";
            if (s.Type != SeatType.Closed || s.HasForfeited)
            {
                if (mgr.CurrentMode == CardGameMode.TexasHoldEm)
                {
                    string extra = s.CurrentStreetBetCopper > 0
                        ? CardGameLang.Get("Seats.BetSuffix", CardGameLang.FormatCoins(s.CurrentStreetBetCopper))
                        : "";
                    string cardsPart = CardGameLang.Get("Seats.CardsCount", s.Hand.Count, extra);
                    string stackText = s.Type == SeatType.House ? "INF" : CardGameLang.FormatCoins(s.StackCopper);
                    midLine = CardGameLang.Get("Seats.StackLine", stackText, cardsPart);
                }
                else if (mgr.CurrentMode == CardGameMode.Uno)
                {
                    string unoTag = s.CalledUno ? CardGameLang.Get("Seats.UnoCalledSuffix") : "";
                    midLine = CardGameLang.Get("Seats.CardsCount", s.Hand.Count, unoTag);
                }
                else
                {
                    string passText = s.HasPassed && !s.HasForfeited ? CardGameLang.Get("Seats.PassedSuffix") : "";
                    midLine = CardGameLang.Get("Seats.CardsCount", s.Hand.Count, passText);
                }
            }
            else if (canHostConfigure && _seatIndex > 0)
            {
                midLine = CardGameLang.Get("Seats.ClickToChange");
            }

            string rawBadge = s.GetTitleBadge(mgr.CurrentMode);
            Color badgeColor = rank switch
            {
                1 => new Color(255, 232, 85),
                2 => new Color(140, 240, 255),
                3 => new Color(215, 145, 85),
                4 => new Color(255, 90, 90),
                _ => Color.Gold
            };

            float centerX = dim.X + dim.Width * 0.5f;
            float maxTextW = dim.Width - 18f;

            UIFittedText.Draw(spriteBatch, nameLine, new Vector2(centerX, dim.Y + 8f), maxTextW, 0.82f, Color.White, center: true);
            if (!string.IsNullOrEmpty(midLine))
                UIFittedText.Draw(spriteBatch, midLine, new Vector2(centerX, dim.Y + 32f), maxTextW, 0.75f, s.CalledUno ? Color.Gold : Color.LightGray, center: true);

            if (mgr.CurrentMode == CardGameMode.Uno && s.Type != SeatType.Closed && s.Hand.Count > 0)
            {
                DrawMiniUnoHandFan(spriteBatch, dim, s.Hand.Count);
                if (!string.IsNullOrEmpty(rawBadge))
                    UIFittedText.Draw(spriteBatch, rawBadge, new Vector2(centerX, dim.Y + 98f), maxTextW, 0.78f, badgeColor, center: true);
            }
            else if (!string.IsNullOrEmpty(rawBadge))
            {
                UIFittedText.Draw(spriteBatch, rawBadge, new Vector2(centerX, dim.Y + 60f), maxTextW, 1f, badgeColor, center: true);
            }
        }

        private static void DrawMiniUnoHandFan(SpriteBatch spriteBatch, CalculatedStyle dim, int cardCount)
        {
            int visibleIcons = Math.Min(cardCount, 12);
            float iconW = 30f;
            float iconH = 38f;
            float step = visibleIcons <= 1 ? 0f : Math.Min(16f, (dim.Width - 40f - iconW) / (visibleIcons - 1));
            float totalW = iconW + (visibleIcons - 1) * step;
            float startX = dim.X + (dim.Width - totalW) * 0.5f;
            float topY = dim.Y + 58f;

            bool hasUnoBack = ModContent.HasAsset(UICardElement.UnoBackPath);
            Texture2D tex = hasUnoBack
                ? ModContent.Request<Texture2D>(UICardElement.UnoBackPath).Value
                : TextureAssets.MagicPixel.Value;

            for (int i = 0; i < visibleIcons; i++)
            {
                Rectangle r = new Rectangle((int)(startX + i * step), (int)topY, (int)iconW, (int)iconH);
                if (hasUnoBack)
                {
                    spriteBatch.Draw(tex, r, Color.White);
                }
                else
                {
                    spriteBatch.Draw(tex, new Rectangle(r.X - 1, r.Y - 1, r.Width + 2, r.Height + 2), Color.Gold);
                    spriteBatch.Draw(tex, r, new Color(30, 30, 35));
                }
            }
        }
    }

    public class UIStatusBox : UIPanel
    {
        public string Text = "";

        public UIStatusBox()
        {
            BackgroundColor = new Color(14, 32, 23, 230);
            SetPadding(0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            CalculatedStyle dim = GetDimensions();
            UIFittedText.Draw(spriteBatch, Text, new Vector2(dim.X + 10f, dim.Y + 6f), dim.Width - 20f, 0.82f, Color.Gold, center: false);
        }
    }

    public class UICardElement : UIElement
    {
        public PlayingCard Card;
        public bool IsSelected;
        public bool Interactive;
        private readonly Action<UICardElement> _onCardPressed;

        private Vector2 _currentDrawOffset;
        private float _currentLift;
        private float _currentRotation;
        private float _currentScale = 1f;
        private readonly float _baseScale;

        public const string SheetPath = "HendecamMod/Assets/Textures/Misc/CardDeck";
        public const string BackPath = "HendecamMod/Assets/Textures/Misc/CardBack";
        public const string UnoSheetPath = "HendecamMod/Assets/Textures/Misc/UnoDeck";
        public const string UnoBackPath = "HendecamMod/Assets/Textures/Misc/UnoBack";

        public UICardElement(PlayingCard card, bool interactive, float scale = 1f, Action<UICardElement> onCardPressed = null, Vector2? spawnOffset = null, float spawnRotation = 0f)
        {
            Card = card;
            Interactive = interactive;
            _baseScale = scale;
            _currentScale = spawnOffset.HasValue ? scale * 0.75f : scale;
            _onCardPressed = onCardPressed;
            _currentDrawOffset = spawnOffset ?? Vector2.Zero;
            _currentRotation = spawnRotation;

            Width.Set(56f * scale, 0f);
            Height.Set(80f * scale, 0f);
        }

        public void SetSlideTarget(float targetLeft, float targetTop)
        {
            Vector2 oldPos = new Vector2(Left.Pixels, Top.Pixels) + _currentDrawOffset;
            Left.Set(targetLeft, 0f);
            Top.Set(targetTop, 0f);
            _currentDrawOffset = oldPos - new Vector2(targetLeft, targetTop);
        }

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);
            if (Interactive)
                _onCardPressed?.Invoke(this);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _currentDrawOffset = Vector2.Lerp(_currentDrawOffset, Vector2.Zero, 0.22f);
            if (_currentDrawOffset.LengthSquared() < 0.1f)
                _currentDrawOffset = Vector2.Zero;

            _currentRotation = MathHelper.Lerp(_currentRotation, 0f, 0.22f);
            _currentScale = MathHelper.Lerp(_currentScale, _baseScale, 0.22f);

            float targetLift = IsSelected ? -16f : (IsMouseHovering && Interactive ? -7f : 0f);
            _currentLift = MathHelper.Lerp(_currentLift, targetLift, 0.28f);

            if (IsMouseHovering)
            {
                bool isUno = CardGameManager.Instance.CurrentMode == CardGameMode.Uno;
                Main.instance.MouseText(isUno ? Card.GetUnoFullName() : Card.GetFullName());
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dim = GetDimensions();
            Vector2 center = new Vector2(dim.X + dim.Width * 0.5f, dim.Y + dim.Height * 0.5f) + _currentDrawOffset + new Vector2(0f, _currentLift);
            Vector2 size = new Vector2(56f * _currentScale, 80f * _currentScale);
            Rectangle rect = new Rectangle((int)(center.X - size.X * 0.5f), (int)(center.Y - size.Y * 0.5f), (int)size.X, (int)size.Y);

            Texture2D pixel = TextureAssets.MagicPixel.Value;
            Color borderColor = IsSelected ? Color.Gold : (IsMouseHovering && Interactive ? Color.Yellow : new Color(20, 20, 30));
            spriteBatch.Draw(pixel, new Rectangle(rect.X - 2, rect.Y - 2, rect.Width + 4, rect.Height + 4), borderColor);

            bool isUno = CardGameManager.Instance.CurrentMode == CardGameMode.Uno;
            if (isUno)
            {
                if (ModContent.HasAsset(UnoSheetPath))
                {
                    Texture2D unoSheet = ModContent.Request<Texture2D>(UnoSheetPath).Value;
                    int frameW = unoSheet.Width;
                    int frameH = unoSheet.Height / 16;
                    Rectangle src = Card.GetUnoSheetSourceRect(frameW, frameH);
                    Vector2 origin = new Vector2(frameW * 0.5f, frameH * 0.5f);
                    Vector2 drawScale = new Vector2((56f / frameW) * _currentScale, (80f / frameH) * _currentScale);

                    Color tint = Card.IsUnoWild ? Color.White : Card.GetUnoTintColor();
                    spriteBatch.Draw(unoSheet, center, src, tint, _currentRotation, origin, drawScale, SpriteEffects.None, 0f);
                    return;
                }

                Color fallbackBg = Card.IsUnoWild ? new Color(25, 25, 30) : Card.GetUnoTintColor();
                spriteBatch.Draw(pixel, rect, fallbackBg);
                Terraria.Utils.DrawBorderString(spriteBatch, Card.GetUnoValueName(), new Vector2(rect.X + 5, rect.Y + 26), Color.White, 0.75f * _currentScale);
                return;
            }

            if (ModContent.HasAsset(SheetPath))
            {
                Texture2D sheet = ModContent.Request<Texture2D>(SheetPath).Value;
                int frameW = sheet.Width / 13;
                int frameH = sheet.Height / 4;
                Rectangle src = Card.GetSheetSourceRect(frameW, frameH);
                Vector2 origin = new Vector2(frameW * 0.5f, frameH * 0.5f);
                Vector2 drawScale = new Vector2((56f / frameW) * _currentScale, (80f / frameH) * _currentScale);
                spriteBatch.Draw(sheet, center, src, Color.White, _currentRotation, origin, drawScale, SpriteEffects.None, 0f);
                return;
            }

            spriteBatch.Draw(pixel, rect, IsSelected ? new Color(255, 252, 220) : new Color(245, 242, 235));
            Color suitColor = Card.GetSuitColor();
            Terraria.Utils.DrawBorderString(spriteBatch, Card.GetRankName(), new Vector2(rect.X + 5, rect.Y + 6), suitColor, 0.78f * _currentScale);
            Terraria.Utils.DrawBorderString(spriteBatch, Card.GetSuitName(), new Vector2(rect.X + 5, rect.Y + 28), suitColor, 0.65f * _currentScale);
        }
    }

    public class FlyingCardBackElement : UIElement
    {
        private Vector2 _pos;
        private readonly Vector2 _target;
        private float _rotation;
        public bool Finished => Vector2.DistanceSquared(_pos, _target) < 9f;

        public FlyingCardBackElement(Vector2 startPos, Vector2 targetPos)
        {
            _pos = startPos;
            _target = targetPos;
            IgnoresMouseInteraction = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _pos = Vector2.Lerp(_pos, _target, 0.22f);
            _rotation += 0.12f;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle parentDim = Parent.GetDimensions();
            Vector2 drawCenter = new Vector2(parentDim.X, parentDim.Y) + _pos;

            string activeBackPath = CardGameManager.Instance.CurrentMode == CardGameMode.Uno
                ? UICardElement.UnoBackPath
                : UICardElement.BackPath;

            if (ModContent.HasAsset(activeBackPath))
            {
                Texture2D backTex = ModContent.Request<Texture2D>(activeBackPath).Value;
                Vector2 scale = new Vector2(48f / backTex.Width, 68f / backTex.Height);
                spriteBatch.Draw(backTex, drawCenter, null, Color.White * 0.9f, _rotation, backTex.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
            else
            {
                Texture2D pixel = TextureAssets.MagicPixel.Value;
                Rectangle r = new Rectangle((int)drawCenter.X - 24, (int)drawCenter.Y - 34, 48, 68);
                spriteBatch.Draw(pixel, r, new Color(140, 35, 45));
            }
        }
    }

    public class UIDisposedDeckElement : UIPanel
    {
        private readonly Action _onClick;
        private float _hoverLift;

        public UIDisposedDeckElement(Action onClick)
        {
            _onClick = onClick;
            Width.Set(165f, 0f);
            Height.Set(175f, 0f);
            BackgroundColor = new Color(14, 32, 23, 225);
            BorderColor = new Color(25, 50, 38);
            SetPadding(0f);
        }

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);
            SoundEngine.PlaySound(SoundID.MenuOpen);
            _onClick?.Invoke();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (ContainsPoint(Main.MouseScreen))
                Main.LocalPlayer.mouseInterface = true;

            _hoverLift = MathHelper.Lerp(_hoverLift, IsMouseHovering ? -5f : 0f, 0.25f);
            BorderColor = IsMouseHovering ? Color.Gold : new Color(25, 50, 38);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            CalculatedStyle dim = GetDimensions();
            var mgr = CardGameManager.Instance;
            var disposed = mgr.DisposedPile;

            string title = CardGameLang.Get("UI.DisposedDeckTitle");
            UIFittedText.Draw(spriteBatch, title, new Vector2(dim.X + dim.Width * 0.5f, dim.Y + 8f), dim.Width - 16f, 0.82f, Color.White, center: true);

            int stackLayers = disposed.Count == 0 ? 1 : Math.Min(4, 1 + disposed.Count / 6);
            Vector2 baseCardPos = new Vector2(dim.X + (dim.Width - 56f) * 0.5f, dim.Y + 34f + _hoverLift);
            Texture2D pixel = TextureAssets.MagicPixel.Value;

            string activeBackPath = mgr.CurrentMode == CardGameMode.Uno
                ? UICardElement.UnoBackPath
                : UICardElement.BackPath;
            bool hasBackAsset = ModContent.HasAsset(activeBackPath);

            for (int i = 0; i < stackLayers; i++)
            {
                Rectangle cardRect = new Rectangle((int)baseCardPos.X + i * 3, (int)baseCardPos.Y - i * 2, 56, 80);

                if (disposed.Count == 0)
                {
                    spriteBatch.Draw(pixel, cardRect, new Color(30, 55, 42, 140));
                }
                else if (hasBackAsset)
                {
                    Texture2D backTex = ModContent.Request<Texture2D>(activeBackPath).Value;
                    spriteBatch.Draw(backTex, cardRect, Color.White);
                }
                else
                {
                    spriteBatch.Draw(pixel, new Rectangle(cardRect.X - 2, cardRect.Y - 2, cardRect.Width + 4, cardRect.Height + 4), Color.Gold);
                    spriteBatch.Draw(pixel, cardRect, new Color(155, 35, 45));
                }
            }

            string countStr = CardGameLang.Get("UI.DisposedDeckCount", disposed.Count);
            string hintStr = CardGameLang.Get("UI.DisposedDeckClickHint");
            UIFittedText.Draw(spriteBatch, countStr, new Vector2(dim.X + dim.Width * 0.5f, dim.Y + 124f), dim.Width - 16f, 0.8f, Color.Gold, center: true);
            UIFittedText.Draw(spriteBatch, hintStr, new Vector2(dim.X + dim.Width * 0.5f, dim.Y + 146f), dim.Width - 16f, 0.72f, IsMouseHovering ? Color.Yellow : Color.LightGray, center: true);
        }
    }
}