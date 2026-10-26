using System;
using System.Collections.Generic;
using System.Linq;
using HendecamMod.Networking.Packets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace HendecamMod.Common.Systems.CardGame
{
    [Autoload(Side = ModSide.Client)]
    public class CardGameUISystem : ModSystem
    {
        internal UserInterface CardTableInterface;
        internal CardGameUIState CardTableUI;

        private float _uiAnimProgress;
        private bool _isClosing;

        public override void PostSetupContent()
        {
            if (!Main.dedServ)
                EnsureInitialized();
        }

        public override void Unload()
        {
            CardTableInterface = null;
            CardTableUI = null;
        }

        private void EnsureInitialized()
        {
            CardTableInterface ??= new UserInterface();
            if (CardTableUI == null)
            {
                CardTableUI = new CardGameUIState();
                CardTableUI.Activate();
            }
        }

        public void OpenTableUI()
        {
            EnsureInitialized();
            CardGameManager.Instance.HasPendingInvite = false;
            _isClosing = false;
            CardTableUI.PrepareOpen();
            if (CardTableInterface.CurrentState == null)
            {
                _uiAnimProgress = 0.01f;
                CardTableInterface.SetState(CardTableUI);
                SoundEngine.PlaySound(SoundID.MenuOpen);
            }
        }

        public void OpenInviteUI()
        {
            EnsureInitialized();
            _isClosing = false;
            CardTableUI.PrepareOpen();
            if (CardTableInterface.CurrentState == null)
            {
                _uiAnimProgress = 0.01f;
                CardTableInterface.SetState(CardTableUI);
            }
        }

        public void CloseUI()
        {
            if (CardTableInterface?.CurrentState != null && !_isClosing)
            {
                CardTableUI.StopDragging();
                _isClosing = true;
                SoundEngine.PlaySound(SoundID.MenuClose);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (CardTableInterface?.CurrentState == null)
                return;

            if (_isClosing)
            {
                _uiAnimProgress = MathHelper.Lerp(_uiAnimProgress, 0f, 0.25f);
                if (_uiAnimProgress <= 0.03f)
                {
                    _uiAnimProgress = 0f;
                    _isClosing = false;
                    CardTableInterface.SetState(null);
                    return;
                }
            }
            else
            {
                _uiAnimProgress = MathHelper.Lerp(_uiAnimProgress, 1f, 0.2f);
                if (_uiAnimProgress > 0.995f)
                    _uiAnimProgress = 1f;
            }

            CardTableUI.SetAnimationProgress(_uiAnimProgress);
            CardTableInterface.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "HendecamMod: Card Game",
                    delegate
                    {
                        if (CardTableInterface?.CurrentState != null)
                            CardTableInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }

    public readonly struct ModeTheme
    {
        public readonly Color MainBg;
        public readonly Color HeaderBg;
        public readonly Color SubHeaderBg;
        public readonly Color CenterBg;
        public readonly Color HandBg;
        public readonly Color SeatBg;
        public readonly Color SeatHoverBg;
        public readonly Color AccentBorder;

        public ModeTheme(Color mainBg, Color headerBg, Color subHeaderBg, Color centerBg, Color handBg, Color seatBg, Color seatHoverBg, Color accentBorder)
        {
            MainBg = mainBg;
            HeaderBg = headerBg;
            SubHeaderBg = subHeaderBg;
            CenterBg = centerBg;
            HandBg = handBg;
            SeatBg = seatBg;
            SeatHoverBg = seatHoverBg;
            AccentBorder = accentBorder;
        }

        public static ModeTheme ForMode(CardGameMode mode) => mode switch
        {
            CardGameMode.TexasHoldEm => new ModeTheme(
                new Color(18, 28, 48, 245),
                new Color(10, 16, 30, 255),
                new Color(14, 22, 38, 245),
                new Color(12, 20, 36, 225),
                new Color(15, 24, 42, 225),
                new Color(24, 38, 64, 225),
                new Color(36, 56, 92, 245),
                new Color(80, 150, 235)
            ),
            CardGameMode.Uno => new ModeTheme(
                new Color(52, 20, 24, 245),
                new Color(32, 10, 14, 255),
                new Color(42, 15, 18, 245),
                new Color(36, 12, 16, 225),
                new Color(44, 16, 20, 225),
                new Color(68, 26, 30, 225),
                new Color(92, 36, 42, 245),
                new Color(240, 85, 75)
            ),
            _ => new ModeTheme(
                new Color(20, 46, 34, 245),
                new Color(12, 28, 20, 255),
                new Color(16, 36, 26, 245),
                new Color(14, 32, 23, 225),
                new Color(16, 38, 28, 225),
                new Color(32, 64, 48, 225),
                new Color(48, 92, 70, 245),
                new Color(65, 165, 110)
            )
        };
    }

    public class CardGameUIState : UIState
    {
        private const float PanelWidth = 1080f;
        private const float PanelHeight = 660f;

        private UIPanel _mainPanel;
        private UIPanel _topHeader;
        private UIPanel _subHeaderPanel;
        private ResponsiveButton _tabPresidentBtn;
        private ResponsiveButton _tabHoldEmBtn;
        private ResponsiveButton _tabUnoBtn;
        private UIText _titleText;
        private UIText _potInfoText;
        private UIStatusBox _statusBox;
        private UIElement _tableBodyContainer;

        private Vector2 _basePanelPos = new Vector2(140f, 80f);
        private float _animProgress = 1f;
        private bool _isDragging;
        private Vector2 _dragOffset;
        private bool _hasCenteredOnce;

        private ResponsiveButton _exitBtn;
        private ResponsiveButton _testBotBtn;
        private ResponsiveButton _houseModeBtn;
        private ResponsiveButton _setCardsBtn;
        private ResponsiveButton _hardModeBtn;
        private ResponsiveButton _betBtn;
        private ResponsiveButton _dealCardsBtn;

        private UIPanel _centerTable;
        private UIPanel _handPanel;
        private ResponsiveButton _addBuyInBtn;
        private ResponsiveButton _sellItemBtn;
        private ResponsiveButton _retreatBtn;

        private ResponsiveButton _playBtn;
        private ResponsiveButton _passBtn;
        private ResponsiveButton _selfUnoBtn;
        private ResponsiveButton _forfeitBtn;

        private ResponsiveButton _holdEmCheckCallBtn;
        private ResponsiveButton _holdEmRaiseBtn;
        private ResponsiveButton _holdEmAllInBtn;

        private UIPanel _wildColorModal;
        private PlayingCard? _pendingWildCard;

        private UIPanel _inviteBanner;
        private UIText _inviteText;
        private UIText _inviteBuyInText;
        private ResponsiveButton _acceptBtn;
        private ResponsiveButton _declineBtn;

        private UIPanel _riggedDrawerPanel;
        private UIPanel _disposedDrawerPanel;
        private UIDisposedDeckElement _disposedDeckWidget;
        private UIElement _handContainer;
        private UIText _handHeader;
        private UIElement _pileContainer;
        private UIText _pileComboText;

        private readonly UISeatElement[] _seatPanels = new UISeatElement[CardGameManager.MaxSeats];
        private readonly ResponsiveButton[] _catchUnoButtons = new ResponsiveButton[CardGameManager.MaxSeats];

        private readonly List<FlyingCardBackElement> _flyingDiscards = new List<FlyingCardBackElement>();
        private readonly Dictionary<byte, UICardElement> _activeHandElements = new Dictionary<byte, UICardElement>();

        private bool _showRiggedPicker;
        private bool _showDisposedViewer;
        private int _lastDisposedCount;
        private CardGameMode _lastRenderedMode = (CardGameMode)255;
        private readonly HashSet<byte> _selectedHandCardIds = new HashSet<byte>();
        private List<byte> _lastRenderedHand = new List<byte>();
        private List<byte> _lastRenderedPile = new List<byte>();

        public void StopDragging() => _isDragging = false;

        public void SetAnimationProgress(float progress)
        {
            _animProgress = Math.Clamp(progress, 0f, 1f);
            float eased = 1f - MathF.Pow(1f - _animProgress, 3f);
            float slideOffsetY = (1f - eased) * 48f;

            ModeTheme theme = ModeTheme.ForMode(CardGameManager.Instance.CurrentMode);
            _mainPanel.Left.Set(_basePanelPos.X, 0f);
            _mainPanel.Top.Set(_basePanelPos.Y + slideOffsetY, 0f);
            _mainPanel.BackgroundColor = theme.MainBg * eased;
            _mainPanel.Recalculate();
        }

        public void PrepareOpen()
        {
            _isDragging = false;
            _pendingWildCard = null;
            if (!_hasCenteredOnce && Main.screenWidth > 0 && Main.screenHeight > 0)
            {
                _hasCenteredOnce = true;
                _basePanelPos = new Vector2((Main.screenWidth - PanelWidth) * 0.5f, (Main.screenHeight - PanelHeight) * 0.5f);
            }
            RefreshStaticLocalization();
            SyncActiveViewMode();
            SetAnimationProgress(_animProgress);
        }

        private void RefreshStaticLocalization()
        {
            var mgr = CardGameManager.Instance;
            _tabPresidentBtn?.SetText(CardGameLang.Get("UI.TabPresident"));
            _tabHoldEmBtn?.SetText(CardGameLang.Get("UI.TabHoldEm"));
            _tabUnoBtn?.SetText(CardGameLang.Get("UI.TabUno"));
            _exitBtn?.SetText(CardGameLang.Get("UI.ExitButton"));
            _testBotBtn?.SetText(CardGameLang.Get("UI.BotModeButton"));
            _houseModeBtn?.SetText(CardGameLang.Get("UI.HouseModeButton"));
            _setCardsBtn?.SetText(CardGameLang.Get("UI.SetCardsButton"));
            _hardModeBtn?.SetText(CardGameLang.Get(mgr.HardMode ? "UI.HardModeOnButton" : "UI.HardModeOffButton"));
            _betBtn?.SetText(CardGameLang.Get("UI.BetButton", CardGameLang.FormatCoins(mgr.BetAmountCopper)));
            _addBuyInBtn?.SetText(CardGameLang.Get("UI.AddBuyInButton"));
            _sellItemBtn?.SetText(CardGameLang.Get("UI.SellItemButton"));
            _retreatBtn?.SetText(CardGameLang.Get("UI.RetreatButton"));
            _dealCardsBtn?.SetText(CardGameLang.Get("UI.StartGameButton"));
            _selfUnoBtn?.SetText(CardGameLang.Get("UI.CallUnoSelfButton"));
            _forfeitBtn?.SetText(CardGameLang.Get(mgr.CurrentMode == CardGameMode.TexasHoldEm ? "UI.FoldButton" : "UI.ForfeitButton"));
            _holdEmAllInBtn?.SetText(CardGameLang.Get("UI.AllInButton"));
            _handHeader?.SetText(CardGameLang.Get("UI.YourHandHeader"));
            _acceptBtn?.SetText(CardGameLang.Get("UI.AcceptButton"));
            _declineBtn?.SetText(CardGameLang.Get("UI.DeclineButton"));

            for (int i = 0; i < CardGameManager.MaxSeats; i++)
                _catchUnoButtons[i]?.SetText(CardGameLang.Get("UI.CatchUnoButton"));
        }

        public void SyncActiveViewMode()
        {
            var mgr = CardGameManager.Instance;
            if (mgr.HasPendingInvite)
            {
                _mainPanel.Remove();
                _inviteText.SetText(CardGameLang.Get("UI.InviteHeader", mgr.PendingInviteHostName));
                int buyIn = mgr.PendingInviteBetCopper > 0 ? mgr.PendingInviteBetCopper : mgr.BetAmountCopper;
                _inviteBuyInText.SetText(CardGameLang.Get("UI.InviteBuyInSubheader", CardGameLang.FormatCoins(buyIn)));
                if (_inviteBanner.Parent == null)
                    Append(_inviteBanner);
            }
            else
            {
                _inviteBanner.Remove();
                if (_mainPanel.Parent == null)
                    Append(_mainPanel);
            }
        }

        public override void OnInitialize()
        {
            _mainPanel = new UIPanel();
            _mainPanel.Width.Set(PanelWidth, 0f);
            _mainPanel.Height.Set(PanelHeight, 0f);
            _mainPanel.Left.Set(_basePanelPos.X, 0f);
            _mainPanel.Top.Set(_basePanelPos.Y, 0f);
            _mainPanel.SetPadding(0f);

            _topHeader = new UIPanel();
            _topHeader.Width.Set(PanelWidth, 0f);
            _topHeader.Height.Set(40f, 0f);
            _topHeader.SetPadding(0f);
            _topHeader.OnLeftMouseDown += (evt, _) =>
            {
                if (evt.Target == _topHeader)
                {
                    _isDragging = true;
                    _dragOffset = new Vector2(evt.MousePosition.X - _basePanelPos.X, evt.MousePosition.Y - _basePanelPos.Y);
                }
            };
            _topHeader.OnLeftMouseUp += (_, _) => _isDragging = false;
            _mainPanel.Append(_topHeader);

            _tabPresidentBtn = new ResponsiveButton(CardGameLang.Get("UI.TabPresident"), 0.8f, () =>
            {
                _selectedHandCardIds.Clear();
                _pendingWildCard = null;
                CardGameManager.Instance.RequestSetGameMode(CardGameMode.President);
            });
            _tabPresidentBtn.SetBounds(12f, 5f, 114f, 30f);
            _mainPanel.Append(_tabPresidentBtn);

            _tabHoldEmBtn = new ResponsiveButton(CardGameLang.Get("UI.TabHoldEm"), 0.8f, () =>
            {
                _selectedHandCardIds.Clear();
                _pendingWildCard = null;
                CardGameManager.Instance.RequestSetGameMode(CardGameMode.TexasHoldEm);
            });
            _tabHoldEmBtn.SetBounds(132f, 5f, 132f, 30f);
            _mainPanel.Append(_tabHoldEmBtn);

            _tabUnoBtn = new ResponsiveButton(CardGameLang.Get("UI.TabUno"), 0.8f, () =>
            {
                _selectedHandCardIds.Clear();
                _pendingWildCard = null;
                CardGameManager.Instance.RequestSetGameMode(CardGameMode.Uno);
            });
            _tabUnoBtn.SetBounds(270f, 5f, 96f, 30f);
            _mainPanel.Append(_tabUnoBtn);

            _titleText = new UIText(CardGameLang.Get("UI.TitleDraggable"), 0.85f)
            {
                Left = { Pixels = 384f },
                VAlign = 0.5f,
                IgnoresMouseInteraction = true
            };
            _topHeader.Append(_titleText);

            _exitBtn = new ResponsiveButton(CardGameLang.Get("UI.ExitButton"), 0.85f, () =>
            {
                CardGameManager.Instance.RequestLeaveTable();
                ModContent.GetInstance<CardGameUISystem>().CloseUI();
            })
            {
                BaseColor = new Color(150, 40, 40, 245),
                HoverColor = new Color(200, 60, 60, 255)
            };
            _exitBtn.SetBounds(PanelWidth - 62f, 5f, 50f, 30f);
            _mainPanel.Append(_exitBtn);

            _subHeaderPanel = new UIPanel();
            _subHeaderPanel.Left.Set(0f, 0f);
            _subHeaderPanel.Top.Set(40f, 0f);
            _subHeaderPanel.Width.Set(PanelWidth, 0f);
            _subHeaderPanel.Height.Set(28f, 0f);
            _subHeaderPanel.SetPadding(0f);
            _mainPanel.Append(_subHeaderPanel);

            _potInfoText = new UIText("", 0.8f)
            {
                Left = { Pixels = 16f },
                VAlign = 0.5f,
                TextColor = Color.Gold,
                IgnoresMouseInteraction = true
            };
            _subHeaderPanel.Append(_potInfoText);

            _statusBox = new UIStatusBox();
            _statusBox.Left.Set(16f, 0f);
            _statusBox.Top.Set(72f, 0f);
            _statusBox.Width.Set(PanelWidth - 32f, 0f);
            _statusBox.Height.Set(32f, 0f);
            _mainPanel.Append(_statusBox);

            _tableBodyContainer = new UIElement();
            _tableBodyContainer.Left.Set(0f, 0f);
            _tableBodyContainer.Top.Set(108f, 0f);
            _tableBodyContainer.Width.Set(PanelWidth, 0f);
            _tableBodyContainer.Height.Set(PanelHeight - 108f, 0f);
            _mainPanel.Append(_tableBodyContainer);

            _testBotBtn = CreateBodyButton(CardGameLang.Get("UI.BotModeButton"), 16f, 0f, 92f, 34f, () =>
            {
                CardGameManager.Instance.SetupBotTestMode(Main.myPlayer);
                _selectedHandCardIds.Clear();
            });

            _houseModeBtn = CreateBodyButton(CardGameLang.Get("UI.HouseModeButton"), 112f, 0f, 94f, 34f, () =>
            {
                CardGameManager.Instance.RequestSetupHouseMode();
                _selectedHandCardIds.Clear();
            });
            _houseModeBtn.BaseColor = new Color(95, 45, 115, 240);
            _houseModeBtn.HoverColor = new Color(135, 65, 165, 255);

            _setCardsBtn = CreateBodyButton(CardGameLang.Get("UI.SetCardsButton"), 210f, 0f, 92f, 34f, () =>
            {
                var mgr = CardGameManager.Instance;
                if (!CardGameManager.HideCheats && mgr.IsLocalPlayerHost() && !mgr.GameInProgress)
                {
                    _showDisposedViewer = false;
                    _showRiggedPicker = !_showRiggedPicker;
                    RebuildModals();
                }
            });

            _hardModeBtn = CreateBodyButton(CardGameLang.Get("UI.HardModeOffButton"), 306f, 0f, 112f, 34f, () =>
            {
                CardGameManager.Instance.RequestToggleHardMode();
            });

            _betBtn = new ResponsiveButton(
                CardGameLang.Get("UI.BetButton", CardGameLang.FormatCoins(CardGameManager.MinBetCopper)),
                0.76f,
                onPress: () => CardGameManager.Instance.CycleBetAmount(1),
                onRightPress: () => CardGameManager.Instance.CycleBetAmount(-1))
            {
                BaseColor = new Color(115, 88, 22, 240),
                HoverColor = new Color(155, 120, 32, 255),
                BaseBorderColor = Color.Gold
            };
            _betBtn.SetBounds(422f, 0f, 138f, 34f);
            _tableBodyContainer.Append(_betBtn);

            _dealCardsBtn = CreateBodyButton(CardGameLang.Get("UI.StartGameButton"), 564f, 0f, 102f, 34f, () =>
            {
                _selectedHandCardIds.Clear();
                _pendingWildCard = null;
                CardGameManager.Instance.RequestStartGame();
                SoundEngine.PlaySound(SoundID.Grab);
            });
            _dealCardsBtn.BaseColor = new Color(35, 110, 75, 245);
            _dealCardsBtn.HoverColor = new Color(50, 150, 100, 255);

            _playBtn = CreateBodyButton(CardGameLang.Get("UI.PlayHandButton"), 672f, 0f, 104f, 34f, OnPressPlayButton);
            _playBtn.BaseColor = new Color(35, 120, 65, 240);
            _playBtn.HoverColor = new Color(50, 165, 90, 255);

            _passBtn = CreateBodyButton(CardGameLang.Get("UI.PassTurnButton"), 780f, 0f, 96f, 34f, () =>
            {
                _pendingWildCard = null;
                CardGameManager.Instance.RequestPassTurn();
                _selectedHandCardIds.Clear();
            });
            _passBtn.BaseColor = new Color(125, 75, 35, 240);
            _passBtn.HoverColor = new Color(170, 105, 50, 255);

            _selfUnoBtn = CreateBodyButton(CardGameLang.Get("UI.CallUnoSelfButton"), 880f, 0f, 104f, 34f, () =>
            {
                var mgr = CardGameManager.Instance;
                int mySeat = mgr.GetLocalControlledSeat();
                if (mySeat >= 0)
                {
                    mgr.RequestCallUno(mySeat);
                    SoundEngine.PlaySound(SoundID.Item4);
                }
            });
            _selfUnoBtn.BaseColor = new Color(200, 120, 15, 245);
            _selfUnoBtn.HoverColor = new Color(245, 160, 30, 255);
            _selfUnoBtn.BaseBorderColor = Color.Gold;

            _holdEmCheckCallBtn = CreateBodyButton(CardGameLang.Get("UI.CheckButton"), 672f, 0f, 118f, 34f, () =>
            {
                CardGameManager.Instance.RequestHoldEmAction(HoldEmActionType.CheckOrCall);
            });
            _holdEmCheckCallBtn.BaseColor = new Color(35, 120, 65, 240);
            _holdEmCheckCallBtn.HoverColor = new Color(50, 165, 90, 255);

            _holdEmRaiseBtn = CreateBodyButton(CardGameLang.Get("UI.RaiseButton", "0"), 794f, 0f, 118f, 34f, () =>
            {
                CardGameManager.Instance.RequestHoldEmAction(HoldEmActionType.Raise);
            });
            _holdEmRaiseBtn.BaseColor = new Color(135, 95, 25, 240);
            _holdEmRaiseBtn.HoverColor = new Color(180, 130, 35, 255);

            _holdEmAllInBtn = CreateBodyButton(CardGameLang.Get("UI.AllInButton"), 916f, 0f, 70f, 34f, () =>
            {
                CardGameManager.Instance.RequestHoldEmAction(HoldEmActionType.AllIn);
            });
            _holdEmAllInBtn.BaseColor = new Color(155, 45, 25, 240);
            _holdEmAllInBtn.HoverColor = new Color(205, 65, 35, 255);

            _forfeitBtn = CreateBodyButton(CardGameLang.Get("UI.ForfeitButton"), 990f, 0f, 74f, 34f, () =>
            {
                _pendingWildCard = null;
                CardGameManager.Instance.RequestForfeitRound();
                _selectedHandCardIds.Clear();
            });
            _forfeitBtn.BaseColor = new Color(135, 35, 45, 240);
            _forfeitBtn.HoverColor = new Color(185, 50, 60, 255);

            for (int i = 0; i < CardGameManager.MaxSeats; i++)
            {
                int seatIdx = i;
                var seatPanel = new UISeatElement(seatIdx, () =>
                {
                    if (!CardGameManager.Instance.GameInProgress && CardGameManager.Instance.IsLocalPlayerHost())
                        CardGameManager.Instance.CycleSeatConfiguration(seatIdx);
                });
                seatPanel.Left.Set(16f + (i * 264f), 0f);
                seatPanel.Top.Set(40f, 0f);
                seatPanel.Width.Set(256f, 0f);
                seatPanel.Height.Set(100f, 0f);
                _seatPanels[i] = seatPanel;
                _tableBodyContainer.Append(seatPanel);

                var catchBtn = new ResponsiveButton(CardGameLang.Get("UI.CatchUnoButton"), 0.75f, () =>
                {
                    CardGameManager.Instance.RequestCallUno(seatIdx);
                    SoundEngine.PlaySound(SoundID.Item4);
                })
                {
                    BaseColor = new Color(165, 45, 30, 240),
                    HoverColor = new Color(220, 70, 45, 255),
                    BaseBorderColor = Color.Gold
                };
                catchBtn.SetBounds(16f + (i * 264f) + 38f, 168f, 180f, 24f);
                _catchUnoButtons[i] = catchBtn;
            }

            float centerTableWidth = PanelWidth - 32f - 177f;
            _centerTable = new UIPanel();
            _centerTable.Width.Set(centerTableWidth, 0f);
            _centerTable.Height.Set(184f, 0f);
            _centerTable.Left.Set(16f, 0f);
            _centerTable.Top.Set(196f, 0f);
            _centerTable.SetPadding(0f);
            _tableBodyContainer.Append(_centerTable);

            _pileComboText = new UIText(CardGameLang.Get("UI.CenterPileEmpty"), 0.84f)
            {
                Left = { Pixels = 14f },
                Top = { Pixels = 8f },
                DynamicallyScaleDownToWidth = true,
                IgnoresMouseInteraction = true
            };
            _centerTable.Append(_pileComboText);

            _pileContainer = new UIElement();
            _pileContainer.Width.Set(centerTableWidth - 28f, 0f);
            _pileContainer.Height.Set(140f, 0f);
            _pileContainer.Left.Set(14f, 0f);
            _pileContainer.Top.Set(34f, 0f);
            _centerTable.Append(_pileContainer);

            _disposedDeckWidget = new UIDisposedDeckElement(() =>
            {
                _showRiggedPicker = false;
                _showDisposedViewer = !_showDisposedViewer;
                RebuildModals();
            });
            _disposedDeckWidget.Left.Set(PanelWidth - 16f - 165f, 0f);
            _disposedDeckWidget.Top.Set(196f, 0f);
            _disposedDeckWidget.Height.Set(184f, 0f);
            _tableBodyContainer.Append(_disposedDeckWidget);

            _handPanel = new UIPanel();
            _handPanel.Width.Set(PanelWidth - 32f, 0f);
            _handPanel.Height.Set(158f, 0f);
            _handPanel.Left.Set(16f, 0f);
            _handPanel.Top.Set(386f, 0f);
            _handPanel.SetPadding(0f);
            _tableBodyContainer.Append(_handPanel);

            _handHeader = new UIText(CardGameLang.Get("UI.YourHandHeader"), 0.82f)
            {
                Left = { Pixels = 14f },
                Top = { Pixels = 8f },
                IgnoresMouseInteraction = true
            };
            _handPanel.Append(_handHeader);

            _addBuyInBtn = new ResponsiveButton(CardGameLang.Get("UI.AddBuyInButton"), 0.75f, () =>
            {
                CardGameManager.Instance.RequestAddBuyIn();
            })
            {
                BaseColor = new Color(35, 95, 130, 240),
                HoverColor = new Color(55, 130, 175, 255)
            };
            _addBuyInBtn.SetBounds(PanelWidth - 32f - 355f, 4f, 110f, 26f);

            _sellItemBtn = new ResponsiveButton(CardGameLang.Get("UI.SellItemButton"), 0.75f, () =>
            {
                CardGameManager.Instance.RequestSellHeldItem();
            })
            {
                BaseColor = new Color(95, 75, 30, 240),
                HoverColor = new Color(145, 115, 45, 255)
            };
            _sellItemBtn.SetBounds(PanelWidth - 32f - 238f, 4f, 112f, 26f);

            _retreatBtn = new ResponsiveButton(CardGameLang.Get("UI.RetreatButton"), 0.75f, () =>
            {
                CardGameManager.Instance.RequestRetreatCashOut();
            })
            {
                BaseColor = new Color(135, 55, 35, 240),
                HoverColor = new Color(185, 80, 50, 255)
            };
            _retreatBtn.SetBounds(PanelWidth - 32f - 118f, 4f, 106f, 26f);

            _handContainer = new UIElement();
            _handContainer.Width.Set(PanelWidth - 60f, 0f);
            _handContainer.Height.Set(118f, 0f);
            _handContainer.Left.Set(14f, 0f);
            _handContainer.Top.Set(34f, 0f);
            _handPanel.Append(_handContainer);

            BuildWildColorModal();
            BuildInviteAndDrawerPanels();
        }

        private void BuildWildColorModal()
        {
            _wildColorModal = new UIPanel();
            _wildColorModal.Width.Set(420f, 0f);
            _wildColorModal.Height.Set(96f, 0f);
            _wildColorModal.Left.Set((PanelWidth - 420f) * 0.5f, 0f);
            _wildColorModal.Top.Set(235f, 0f);
            _wildColorModal.BackgroundColor = new Color(24, 12, 16, 250);
            _wildColorModal.BorderColor = Color.Gold;
            _wildColorModal.SetPadding(0f);

            var prompt = new UIText(CardGameLang.Get("UI.ChooseWildColorPrompt"), 0.84f)
            {
                HAlign = 0.5f,
                Top = { Pixels = 10f },
                TextColor = Color.Gold
            };
            _wildColorModal.Append(prompt);

            UnoColor[] colors = { UnoColor.Red, UnoColor.Yellow, UnoColor.Green, UnoColor.Blue };
            for (int i = 0; i < colors.Length; i++)
            {
                UnoColor col = colors[i];
                Color tint = PlayingCard.GetUnoColorTint(col);
                var colBtn = new ResponsiveButton(CardGameLang.Get($"UnoColors.{col}"), 0.78f, () =>
                {
                    if (_pendingWildCard.HasValue)
                    {
                        CardGameManager.Instance.RequestPlayUnoCard(_pendingWildCard.Value, col);
                        _selectedHandCardIds.Clear();
                        _pendingWildCard = null;
                        SoundEngine.PlaySound(SoundID.Item1);
                    }
                })
                {
                    BaseColor = tint * 0.85f,
                    HoverColor = tint,
                    TextColor = col == UnoColor.Yellow ? Color.Black : Color.White
                };
                colBtn.SetBounds(16f + i * 98f, 40f, 90f, 42f);
                _wildColorModal.Append(colBtn);
            }
        }

        private void BuildInviteAndDrawerPanels()
        {
            _inviteBanner = new UIPanel();
            _inviteBanner.Width.Set(560f, 0f);
            _inviteBanner.Height.Set(170f, 0f);
            _inviteBanner.HAlign = 0.5f;
            _inviteBanner.VAlign = 0.35f;
            _inviteBanner.BackgroundColor = new Color(20, 30, 55, 250);
            _inviteBanner.BorderColor = Color.Gold;
            _inviteBanner.SetPadding(0f);

            _inviteText = new UIText("", 0.88f)
            {
                HAlign = 0.5f,
                Top = { Pixels = 18f },
                TextColor = Color.Gold,
                DynamicallyScaleDownToWidth = true,
                IgnoresMouseInteraction = true
            };
            _inviteBanner.Append(_inviteText);

            _inviteBuyInText = new UIText("", 0.82f)
            {
                HAlign = 0.5f,
                Top = { Pixels = 50f },
                TextColor = Color.LightGoldenrodYellow,
                DynamicallyScaleDownToWidth = true,
                IgnoresMouseInteraction = true
            };
            _inviteBanner.Append(_inviteBuyInText);

            _acceptBtn = new ResponsiveButton(CardGameLang.Get("UI.AcceptButton"), 0.85f, () =>
            {
                if (!CardGameManager.Instance.TryRespondToInvite(true, out string err))
                {
                    _inviteBuyInText.SetText(err);
                    _inviteBuyInText.TextColor = Color.OrangeRed;
                    _acceptBtn.TriggerShake();
                }
            })
            {
                BaseColor = new Color(40, 140, 65),
                HoverColor = new Color(60, 185, 90)
            };
            _acceptBtn.SetBounds(90f, 98f, 175f, 44f);
            _inviteBanner.Append(_acceptBtn);

            _declineBtn = new ResponsiveButton(CardGameLang.Get("UI.DeclineButton"), 0.85f, () =>
            {
                CardGameManager.Instance.TryRespondToInvite(false, out _);
            })
            {
                BaseColor = new Color(150, 45, 45),
                HoverColor = new Color(195, 65, 65)
            };
            _declineBtn.SetBounds(295f, 98f, 175f, 44f);
            _inviteBanner.Append(_declineBtn);

            _riggedDrawerPanel = new UIPanel();
            _riggedDrawerPanel.Width.Set(PanelWidth - 32f, 0f);
            _riggedDrawerPanel.Height.Set(495f, 0f);
            _riggedDrawerPanel.Left.Set(16f, 0f);
            _riggedDrawerPanel.Top.Set(146f, 0f);
            _riggedDrawerPanel.BackgroundColor = new Color(18, 22, 36, 255);
            _riggedDrawerPanel.SetPadding(0f);

            _disposedDrawerPanel = new UIPanel();
            _disposedDrawerPanel.Width.Set(PanelWidth - 32f, 0f);
            _disposedDrawerPanel.Height.Set(495f, 0f);
            _disposedDrawerPanel.Left.Set(16f, 0f);
            _disposedDrawerPanel.Top.Set(146f, 0f);
            _disposedDrawerPanel.BackgroundColor = new Color(16, 28, 24, 255);
            _disposedDrawerPanel.SetPadding(0f);
        }

        private ResponsiveButton CreateBodyButton(string text, float left, float top, float width, float height, Action onPress)
        {
            var btn = new ResponsiveButton(text, 0.78f, onPress);
            btn.SetBounds(left, top, width, height);
            _tableBodyContainer.Append(btn);
            return btn;
        }

        private void OnPressPlayButton()
        {
            var mgr = CardGameManager.Instance;
            int mySeat = mgr.GetLocalControlledSeat();
            var cards = mySeat != -1
                ? mgr.Seats[mySeat].Hand.Where(c => _selectedHandCardIds.Contains(c.Id)).ToList()
                : new List<PlayingCard>();

            if (!mgr.ValidateLocalPlayAttempt(cards, out string error))
            {
                mgr.StatusMessage = error;
                _playBtn.TriggerShake();
                return;
            }

            if (mgr.CurrentMode == CardGameMode.Uno)
            {
                PlayingCard unoCard = cards[0];
                if (unoCard.IsUnoWild)
                {
                    _pendingWildCard = unoCard;
                    return;
                }

                mgr.RequestPlayUnoCard(unoCard, unoCard.UnoCol);
                _selectedHandCardIds.Clear();
                SoundEngine.PlaySound(SoundID.Item1);
                return;
            }

            mgr.RequestPlayCards(cards);
            _selectedHandCardIds.Clear();
            SoundEngine.PlaySound(SoundID.Item1);
        }

        private void ApplyModeThemeAndLayout(CardGameMode mode)
        {
            ModeTheme theme = ModeTheme.ForMode(mode);
            float eased = 1f - MathF.Pow(1f - _animProgress, 3f);

            _mainPanel.BackgroundColor = theme.MainBg * eased;
            _mainPanel.BorderColor = theme.AccentBorder;
            _topHeader.BackgroundColor = theme.HeaderBg;
            _subHeaderPanel.BackgroundColor = theme.SubHeaderBg;
            _centerTable.BackgroundColor = theme.CenterBg;
            _handPanel.BackgroundColor = theme.HandBg;
            _disposedDeckWidget.BackgroundColor = theme.CenterBg;

            bool isUno = mode == CardGameMode.Uno;
            float seatHeight = isUno ? 124f : 100f;
            float centerTop = isUno ? 198f : 150f;
            float centerHeight = isUno ? 180f : 228f;

            for (int i = 0; i < CardGameManager.MaxSeats; i++)
                _seatPanels[i].Height.Set(seatHeight, 0f);

            _centerTable.Top.Set(centerTop, 0f);
            _centerTable.Height.Set(centerHeight, 0f);
            _pileContainer.Height.Set(centerHeight - 42f, 0f);

            _disposedDeckWidget.Top.Set(centerTop, 0f);
            _disposedDeckWidget.Height.Set(centerHeight, 0f);
        }

        private void RebuildModals()
        {
            _riggedDrawerPanel.Remove();
            _disposedDrawerPanel.Remove();

            var mgr = CardGameManager.Instance;

            if (!CardGameManager.HideCheats && _showRiggedPicker && mgr.IsLocalPlayerHost() && !mgr.GameInProgress)
            {
                _riggedDrawerPanel.RemoveAllChildren();
                int maxPick = mgr.CurrentMode == CardGameMode.TexasHoldEm ? 2 : (mgr.CurrentMode == CardGameMode.Uno ? 7 : 13);

                var header = new UIText(CardGameLang.Get("UI.RiggedHeader", mgr.RiggedCardIds.Count), 0.85f)
                {
                    Left = { Pixels = 16f },
                    Top = { Pixels = 12f }
                };
                _riggedDrawerPanel.Append(header);

                if (mgr.CurrentMode == CardGameMode.President)
                {
                    var fourTwosBtn = new ResponsiveButton(CardGameLang.Get("UI.ForceFourTwosButton"), 0.78f, () =>
                    {
                        for (byte s = 0; s < 4; s++)
                            mgr.RiggedCardIds.Add(new PlayingCard(CardRank.Two, (CardSuit)s).Id);
                        RebuildModals();
                    });
                    fourTwosBtn.SetBounds(560f, 8f, 150f, 32f);
                    _riggedDrawerPanel.Append(fourTwosBtn);
                }

                var clearBtn = new ResponsiveButton(CardGameLang.Get("UI.ClearButton"), 0.78f, () =>
                {
                    mgr.RiggedCardIds.Clear();
                    RebuildModals();
                });
                clearBtn.SetBounds(720f, 8f, 100f, 32f);
                _riggedDrawerPanel.Append(clearBtn);

                var doneBtn = new ResponsiveButton(CardGameLang.Get("UI.DoneButton"), 0.78f, () =>
                {
                    _showRiggedPicker = false;
                    RebuildModals();
                });
                doneBtn.SetBounds(830f, 8f, 100f, 32f);
                _riggedDrawerPanel.Append(doneBtn);

                if (mgr.CurrentMode == CardGameMode.Uno)
                {
                    for (int v = 0; v < 15; v++)
                    {
                        for (int c = 0; c < 4; c++)
                        {
                            PlayingCard card = PlayingCard.CreateUno((UnoColor)c, (UnoValue)v, c);
                            bool picked = mgr.RiggedCardIds.Contains(card.Id);
                            Color tint = card.IsUnoWild ? new Color(60, 60, 75) : PlayingCard.GetUnoColorTint((UnoColor)c) * 0.75f;

                            var cardBtn = new ResponsiveButton($"{card.GetUnoValueName()}\n{card.GetUnoColorName()}", 0.65f, () =>
                            {
                                if (mgr.RiggedCardIds.Contains(card.Id))
                                    mgr.RiggedCardIds.Remove(card.Id);
                                else if (mgr.RiggedCardIds.Count < maxPick)
                                    mgr.RiggedCardIds.Add(card.Id);
                                RebuildModals();
                            })
                            {
                                BaseColor = picked ? new Color(45, 180, 75) : tint
                            };
                            cardBtn.SetBounds(14f + v * 68f, 54f + c * 102f, 64f, 92f);
                            _riggedDrawerPanel.Append(cardBtn);
                        }
                    }
                }
                else
                {
                    for (int r = 0; r < 13; r++)
                    {
                        for (int s = 0; s < 4; s++)
                        {
                            PlayingCard card = new PlayingCard((CardRank)(r + 3), (CardSuit)s);
                            bool picked = mgr.RiggedCardIds.Contains(card.Id);

                            var cardBtn = new ResponsiveButton($"{card.GetRankName()}\n{card.GetSuitName()}", 0.7f, () =>
                            {
                                if (mgr.RiggedCardIds.Contains(card.Id))
                                    mgr.RiggedCardIds.Remove(card.Id);
                                else if (mgr.RiggedCardIds.Count < maxPick)
                                    mgr.RiggedCardIds.Add(card.Id);
                                RebuildModals();
                            })
                            {
                                BaseColor = picked ? new Color(45, 160, 70) : new Color(40, 50, 80)
                            };
                            cardBtn.SetBounds(18f + r * 78f, 54f + s * 102f, 70f, 92f);
                            _riggedDrawerPanel.Append(cardBtn);
                        }
                    }
                }

                _mainPanel.Append(_riggedDrawerPanel);
                return;
            }

            if (_showDisposedViewer)
            {
                _disposedDrawerPanel.RemoveAllChildren();

                var header = new UIText(CardGameLang.Get("UI.DisposedModalHeader", mgr.DisposedPile.Count), 0.88f)
                {
                    Left = { Pixels = 18f },
                    Top = { Pixels = 12f },
                    TextColor = Color.Gold
                };
                _disposedDrawerPanel.Append(header);

                var closeBtn = new ResponsiveButton(CardGameLang.Get("UI.CloseViewerButton"), 0.8f, () =>
                {
                    _showDisposedViewer = false;
                    RebuildModals();
                });
                closeBtn.SetBounds(PanelWidth - 32f - 170f, 8f, 155f, 32f);
                _disposedDrawerPanel.Append(closeBtn);

                if (mgr.DisposedPile.Count == 0)
                {
                    var emptyLabel = new UIText(CardGameLang.Get("UI.DisposedModalEmpty"), 0.85f)
                    {
                        HAlign = 0.5f,
                        VAlign = 0.5f,
                        TextColor = Color.LightGray
                    };
                    _disposedDrawerPanel.Append(emptyLabel);
                }
                else
                {
                    int maxShow = Math.Min(mgr.DisposedPile.Count, 52);
                    for (int idx = 0; idx < maxShow; idx++)
                    {
                        int col = idx % 13;
                        int row = idx / 13;
                        var cardElem = new UICardElement(mgr.DisposedPile[idx], interactive: false, scale: 1f, spawnOffset: new Vector2(0f, 20f));
                        cardElem.Left.Set(22f + col * 78f, 0f);
                        cardElem.Top.Set(54f + row * 104f, 0f);
                        _disposedDrawerPanel.Append(cardElem);
                    }
                }

                _mainPanel.Append(_disposedDrawerPanel);
            }
        }

        public override void Update(GameTime gameTime)
        {
            SyncActiveViewMode();
            base.Update(gameTime);

            var mgr = CardGameManager.Instance;
            if (mgr.HasPendingInvite)
            {
                if (_inviteBanner.ContainsPoint(Main.MouseScreen))
                    Main.LocalPlayer.mouseInterface = true;
                return;
            }

            if (_mainPanel.ContainsPoint(Main.MouseScreen))
                Main.LocalPlayer.mouseInterface = true;

            if (!Main.mouseLeft || mgr.GameInProgress)
                _isDragging = false;

            if (_isDragging)
            {
                _basePanelPos.X = Math.Clamp(Main.mouseX - _dragOffset.X, 10f, Math.Max(10f, Main.screenWidth - PanelWidth - 10f));
                _basePanelPos.Y = Math.Clamp(Main.mouseY - _dragOffset.Y, 10f, Math.Max(10f, Main.screenHeight - PanelHeight - 10f));
                SetAnimationProgress(_animProgress);
            }

            ApplyModeThemeAndLayout(mgr.CurrentMode);

            bool isPresident = mgr.CurrentMode == CardGameMode.President;
            bool isHoldEm = mgr.CurrentMode == CardGameMode.TexasHoldEm;
            bool isUno = mgr.CurrentMode == CardGameMode.Uno;

            StyleModeTab(_tabPresidentBtn, isPresident, new Color(40, 125, 75, 250));
            StyleModeTab(_tabHoldEmBtn, isHoldEm, new Color(40, 85, 155, 250));
            StyleModeTab(_tabUnoBtn, isUno, new Color(175, 45, 45, 250));

            bool isHost = mgr.IsLocalPlayerHost();
            bool canHostConfigure = isHost && !mgr.GameInProgress;

            int seatedCount = mgr.Seats.Count(s => s.Type != SeatType.Closed);
            int displayPot = mgr.GameInProgress ? mgr.TotalPotCopper : mgr.BetAmountCopper * Math.Max(2, seatedCount);

            if (canHostConfigure)
            {
                if (_testBotBtn.Parent == null) _tableBodyContainer.Append(_testBotBtn);

                if (isHoldEm)
                {
                    if (_houseModeBtn.Parent == null) _tableBodyContainer.Append(_houseModeBtn);
                }
                else
                {
                    _houseModeBtn.Remove();
                }

                if (!CardGameManager.HideCheats)
                {
                    if (_setCardsBtn.Parent == null) _tableBodyContainer.Append(_setCardsBtn);
                }
                else
                {
                    _setCardsBtn.Remove();
                }

                if (isPresident)
                {
                    if (_hardModeBtn.Parent == null) _tableBodyContainer.Append(_hardModeBtn);
                }
                else
                {
                    _hardModeBtn.Remove();
                }

                if (_betBtn.Parent == null) _tableBodyContainer.Append(_betBtn);
                if (_dealCardsBtn.Parent == null) _tableBodyContainer.Append(_dealCardsBtn);

                _hardModeBtn.SetText(CardGameLang.Get(mgr.HardMode ? "UI.HardModeOnButton" : "UI.HardModeOffButton"));
                _hardModeBtn.BaseColor = mgr.HardMode ? new Color(170, 55, 25, 245) : new Color(45, 75, 115, 240);
                _hardModeBtn.HoverColor = mgr.HardMode ? new Color(215, 80, 40, 255) : new Color(75, 115, 170, 255);
                _hardModeBtn.BaseBorderColor = mgr.HardMode ? Color.Gold : new Color(20, 30, 45);

                _betBtn.SetText(CardGameLang.Get("UI.BetButton", CardGameLang.FormatCoins(mgr.BetAmountCopper)));
                if (_betBtn.IsMouseHovering)
                {
                    Main.instance.MouseText(CardGameLang.Get("UI.BetButtonTooltip",
                        CardGameLang.FormatCoins(mgr.BetAmountCopper),
                        CardGameLang.FormatCoins(displayPot)));
                }
            }
            else
            {
                _testBotBtn.Remove();
                _houseModeBtn.Remove();
                _setCardsBtn.Remove();
                _hardModeBtn.Remove();
                _betBtn.Remove();
                _dealCardsBtn.Remove();
                if (_showRiggedPicker)
                {
                    _showRiggedPicker = false;
                    RebuildModals();
                }
            }

            int mySeat = mgr.GetLocalControlledSeat();
            int myStack = mySeat >= 0 ? mgr.Seats[mySeat].StackCopper : 0;

            if (isHoldEm)
            {
                _playBtn.Remove();
                _passBtn.Remove();
                _selfUnoBtn.Remove();
                if (_holdEmCheckCallBtn.Parent == null) _tableBodyContainer.Append(_holdEmCheckCallBtn);
                if (_holdEmRaiseBtn.Parent == null) _tableBodyContainer.Append(_holdEmRaiseBtn);
                if (_holdEmAllInBtn.Parent == null) _tableBodyContainer.Append(_holdEmAllInBtn);

                if (mySeat >= 0)
                {
                    int toCall = Math.Max(0, mgr.CurrentStreetHighBetCopper - mgr.Seats[mySeat].CurrentStreetBetCopper);
                    _holdEmCheckCallBtn.SetText(toCall > 0
                        ? CardGameLang.Get("UI.CallButton", CardGameLang.FormatCoins(Math.Min(myStack, toCall)))
                        : CardGameLang.Get("UI.CheckButton"));
                    _holdEmRaiseBtn.SetText(CardGameLang.Get("UI.RaiseButton", CardGameLang.FormatCoins(mgr.GetRaiseStepCopper())));
                }
            }
            else
            {
                _holdEmCheckCallBtn.Remove();
                _holdEmRaiseBtn.Remove();
                _holdEmAllInBtn.Remove();
                if (_playBtn.Parent == null) _tableBodyContainer.Append(_playBtn);
                if (_passBtn.Parent == null) _tableBodyContainer.Append(_passBtn);

                if (isUno)
                {
                    _playBtn.SetText(CardGameLang.Get("UI.PlayCardButton"));
                    _passBtn.SetText(CardGameLang.Get(mgr.UnoHasDrawnThisTurn && mgr.CurrentTurnSeat == mySeat
                        ? "UI.PassTurnButton"
                        : "UI.DrawCardButton"));

                    if (mgr.GameInProgress && mySeat >= 0 && mgr.Seats[mySeat].IsActiveInRound)
                    {
                        bool shouldGlow = !mgr.Seats[mySeat].CalledUno &&
                            (mgr.Seats[mySeat].Hand.Count == 1 || (mgr.Seats[mySeat].Hand.Count == 2 && mgr.CurrentTurnSeat == mySeat));
                        _selfUnoBtn.GlowWhenAlert = shouldGlow;
                        if (_selfUnoBtn.Parent == null)
                            _tableBodyContainer.Append(_selfUnoBtn);
                    }
                    else
                    {
                        _selfUnoBtn.Remove();
                    }
                }
                else
                {
                    _playBtn.SetText(CardGameLang.Get("UI.PlayHandButton"));
                    _passBtn.SetText(CardGameLang.Get("UI.PassTurnButton"));
                    _selfUnoBtn.Remove();
                }
            }

            for (int i = 0; i < CardGameManager.MaxSeats; i++)
            {
                bool showCatchBtn = isUno && mgr.GameInProgress && i != mySeat && mgr.Seats[i].IsActiveInRound;
                if (showCatchBtn)
                {
                    _catchUnoButtons[i].GlowWhenAlert = mgr.Seats[i].Hand.Count == 1 && !mgr.Seats[i].CalledUno;
                    if (_catchUnoButtons[i].Parent == null)
                        _tableBodyContainer.Append(_catchUnoButtons[i]);
                }
                else
                {
                    _catchUnoButtons[i].Remove();
                }
            }

            if (!isUno || !mgr.GameInProgress || mgr.CurrentTurnSeat != mySeat)
                _pendingWildCard = null;

            if (_pendingWildCard.HasValue)
            {
                if (_wildColorModal.Parent == null)
                    _tableBodyContainer.Append(_wildColorModal);
            }
            else
            {
                _wildColorModal.Remove();
            }

            if (isHoldEm && mySeat >= 0)
            {
                if (_retreatBtn.Parent == null) _handPanel.Append(_retreatBtn);
                if (_retreatBtn.IsMouseHovering)
                    Main.instance.MouseText(CardGameLang.Get("UI.RetreatTooltip", CardGameLang.FormatCoins(myStack)));

                if (!mgr.GameInProgress)
                {
                    if (_addBuyInBtn.Parent == null) _handPanel.Append(_addBuyInBtn);
                    if (_sellItemBtn.Parent == null) _handPanel.Append(_sellItemBtn);

                    if (_addBuyInBtn.IsMouseHovering)
                        Main.instance.MouseText(CardGameLang.Get("UI.AddBuyInTooltip", CardGameLang.FormatCoins(mgr.BetAmountCopper)));

                    _sellItemBtn.GlowWhenAlert = mgr.IsLocalPlayerLowOnFunds();
                    if (_sellItemBtn.IsMouseHovering)
                    {
                        if (CardGameManager.TryGetSellableLocalItem(out Item sellItem, out int sellVal))
                            Main.instance.MouseText(CardGameLang.Get("UI.SellItemTooltip", $"{sellItem.Name} ({sellItem.stack})", CardGameLang.FormatCoins(sellVal)));
                        else
                            Main.instance.MouseText(CardGameLang.Get("UI.SellItemNoneTooltip"));
                    }
                }
                else
                {
                    _addBuyInBtn.Remove();
                    _sellItemBtn.Remove();
                }
            }
            else
            {
                _addBuyInBtn.Remove();
                _sellItemBtn.Remove();
                _retreatBtn.Remove();
            }

            bool canForfeit = mgr.GameInProgress && mySeat >= 0 && mgr.Seats[mySeat].IsActiveInRound;
            if (canForfeit)
            {
                _forfeitBtn.SetText(CardGameLang.Get(isHoldEm ? "UI.FoldButton" : "UI.ForfeitButton"));
                if (_forfeitBtn.Parent == null) _tableBodyContainer.Append(_forfeitBtn);
            }
            else
            {
                _forfeitBtn.Remove();
            }

            bool anyModalOpen = _showRiggedPicker || _showDisposedViewer;
            if (anyModalOpen)
                _tableBodyContainer.Remove();
            else if (_tableBodyContainer.Parent == null)
                _mainPanel.Append(_tableBodyContainer);

            string baseTitle = CardGameLang.Get(mgr.GameInProgress ? "UI.TitleLocked" : "UI.TitleDraggable");
            string hardTag = (isPresident && mgr.HardMode) ? $" {CardGameLang.Get("UI.HardModeTag")}" : "";
            _titleText.SetText(baseTitle + hardTag);
            _titleText.TextColor = (isPresident && mgr.HardMode) ? new Color(255, 180, 90) : Color.White;

            _potInfoText.SetText(!isHoldEm
                ? CardGameLang.Get("UI.PotHeaderSimple",
                    CardGameLang.FormatCoins(mgr.BetAmountCopper),
                    CardGameLang.FormatCoins(displayPot))
                : CardGameLang.Get("UI.PotHeaderInfo",
                    CardGameLang.FormatCoins(mgr.BetAmountCopper),
                    CardGameLang.FormatCoins(displayPot),
                    CardGameLang.FormatCoins(myStack)));

            _statusBox.Text = mgr.StatusMessage;

            if (isHoldEm && mgr.GameInProgress)
            {
                _pileComboText.SetText(CardGameLang.Get("UI.HoldEmCommunityHeader",
                    mgr.HoldEmStreet.ToString(),
                    CardGameLang.FormatCoins(mgr.CurrentStreetHighBetCopper)));
                _pileComboText.TextColor = Color.White;
            }
            else if (isUno)
            {
                if (mgr.CenterPile.Count > 0)
                {
                    PlayingCard top = mgr.CenterPile[mgr.CenterPile.Count - 1];
                    string dirStr = mgr.UnoPlayDirection >= 0 ? CardGameLang.Get("UI.UnoDirClockwise") : CardGameLang.Get("UI.UnoDirCounter");
                    string colStr = CardGameLang.Get($"UnoColors.{mgr.ActiveUnoColor}");
                    _pileComboText.SetText(CardGameLang.Get("UI.UnoCenterHeader", top.GetUnoFullName(), colStr, dirStr));
                    _pileComboText.TextColor = PlayingCard.GetUnoColorTint(mgr.ActiveUnoColor);
                }
                else
                {
                    _pileComboText.SetText(CardGameLang.Get("UI.CenterPileEmpty"));
                    _pileComboText.TextColor = Color.White;
                }
            }
            else
            {
                _pileComboText.TextColor = Color.White;
            }

            if (mgr.DisposedPile.Count > _lastDisposedCount)
            {
                int added = Math.Min(4, mgr.DisposedPile.Count - _lastDisposedCount);
                for (int i = 0; i < added; i++)
                {
                    var fly = new FlyingCardBackElement(
                        new Vector2(400f + i * 25f, 250f),
                        new Vector2(PanelWidth - 98f, 250f));
                    _tableBodyContainer.Append(fly);
                    _flyingDiscards.Add(fly);
                }
                if (_showDisposedViewer)
                    RebuildModals();
            }
            _lastDisposedCount = mgr.DisposedPile.Count;

            for (int i = _flyingDiscards.Count - 1; i >= 0; i--)
            {
                if (_flyingDiscards[i].Finished)
                {
                    _flyingDiscards[i].Remove();
                    _flyingDiscards.RemoveAt(i);
                }
            }

            bool modeChanged = mgr.CurrentMode != _lastRenderedMode;
            _lastRenderedMode = mgr.CurrentMode;

            var currentPileIds = mgr.CenterPile.Select(c => c.Id).ToList();
            if (modeChanged || !currentPileIds.SequenceEqual(_lastRenderedPile))
            {
                _lastRenderedPile = currentPileIds;
                RefreshPileUI(mgr.CenterPile, mgr.LastPlaySeat, mySeat);
            }

            var myHand = mySeat >= 0 ? mgr.Seats[mySeat].Hand : new List<PlayingCard>();
            var currentHandIds = myHand.Select(c => c.Id).ToList();
            if (modeChanged)
                _activeHandElements.Clear();

            if (modeChanged || !currentHandIds.SequenceEqual(_lastRenderedHand))
            {
                _lastRenderedHand = currentHandIds;
                RefreshHandUI(myHand);
            }
        }

        private static void StyleModeTab(ResponsiveButton tab, bool active, Color activeColor)
        {
            tab.BaseColor = active ? activeColor : new Color(26, 34, 42, 215);
            tab.BaseBorderColor = active ? Color.Gold : new Color(20, 30, 45);
        }

        private void RefreshPileUI(List<PlayingCard> pile, int fromSeat, int localSeat)
        {
            _pileContainer.RemoveAllChildren();
            var mgr = CardGameManager.Instance;

            if (pile.Count == 0)
            {
                if (mgr.CurrentMode == CardGameMode.President)
                    _pileComboText.SetText(CardGameLang.Get("UI.CenterPileEmpty"));
                return;
            }

            if (mgr.CurrentMode == CardGameMode.President)
            {
                EvaluatedHand eval = CardHandEvaluator.Evaluate(pile, mgr.HardMode);
                _pileComboText.SetText(CardGameLang.Get("UI.CenterPileHeader", eval.DisplayName));
            }

            Vector2 spawnOffset = (fromSeat == localSeat)
                ? new Vector2(0f, 165f)
                : new Vector2((Math.Max(0, fromSeat) * 254f) - 290f, -100f);

            float usableWidth = _pileContainer.Width.Pixels - 16f;
            float cardWidth = 56f;
            float maxStep = 66f;
            float step = pile.Count <= 1 ? 0f : Math.Min(maxStep, (usableWidth - cardWidth) / (pile.Count - 1));
            float totalOccupiedWidth = cardWidth + (pile.Count - 1) * step;
            float startX = Math.Max(8f, (_pileContainer.Width.Pixels - totalOccupiedWidth) * 0.5f);

            for (int i = 0; i < pile.Count; i++)
            {
                float startRot = mgr.CurrentMode == CardGameMode.TexasHoldEm ? 0f : (i - (pile.Count - 1) * 0.5f) * 0.18f;
                var cardElem = new UICardElement(pile[i], interactive: false, scale: 1f, spawnOffset: spawnOffset, spawnRotation: startRot);
                cardElem.Left.Set(startX + i * step, 0f);
                cardElem.Top.Set(18f, 0f);
                _pileContainer.Append(cardElem);
            }
        }

        private void RefreshHandUI(List<PlayingCard> myHand)
        {
            _handContainer.RemoveAllChildren();
            if (myHand.Count == 0)
            {
                _activeHandElements.Clear();
                return;
            }

            var mgr = CardGameManager.Instance;
            bool interactiveHand = mgr.CurrentMode != CardGameMode.TexasHoldEm;
            float usableWidth = _handContainer.Width.Pixels - 16f;
            float cardWidth = 56f;
            float maxStep = 64f;
            float step = myHand.Count <= 1 ? 0f : Math.Min(maxStep, (usableWidth - cardWidth) / (myHand.Count - 1));
            float totalOccupiedWidth = cardWidth + (myHand.Count - 1) * step;
            float startX = Math.Max(8f, (_handContainer.Width.Pixels - totalOccupiedWidth) * 0.5f);

            var nextDict = new Dictionary<byte, UICardElement>();

            for (int i = 0; i < myHand.Count; i++)
            {
                PlayingCard card = myHand[i];
                float targetX = startX + i * step;
                float targetY = 18f;

                if (_activeHandElements.TryGetValue(card.Id, out var existingElem))
                {
                    existingElem.Card = card;
                    existingElem.Interactive = interactiveHand;
                    existingElem.SetSlideTarget(targetX, targetY);
                    existingElem.IsSelected = interactiveHand && _selectedHandCardIds.Contains(card.Id);
                    _handContainer.Append(existingElem);
                    nextDict[card.Id] = existingElem;
                }
                else
                {
                    var newElem = new UICardElement(card, interactive: interactiveHand, scale: 1f, onCardPressed: elem =>
                    {
                        if (!elem.Interactive)
                            return;

                        if (CardGameManager.Instance.CurrentMode == CardGameMode.Uno)
                        {
                            bool wasSelected = _selectedHandCardIds.Contains(elem.Card.Id);
                            _selectedHandCardIds.Clear();
                            if (!wasSelected)
                                _selectedHandCardIds.Add(elem.Card.Id);

                            foreach (var kvp in _activeHandElements)
                                kvp.Value.IsSelected = _selectedHandCardIds.Contains(kvp.Key);
                        }
                        else
                        {
                            if (_selectedHandCardIds.Contains(elem.Card.Id))
                                _selectedHandCardIds.Remove(elem.Card.Id);
                            else
                                _selectedHandCardIds.Add(elem.Card.Id);

                            elem.IsSelected = _selectedHandCardIds.Contains(elem.Card.Id);
                        }

                        SoundEngine.PlaySound(SoundID.MenuTick);
                    }, spawnOffset: new Vector2(0f, -45f));

                    newElem.Left.Set(targetX, 0f);
                    newElem.Top.Set(targetY, 0f);
                    newElem.IsSelected = interactiveHand && _selectedHandCardIds.Contains(card.Id);
                    _handContainer.Append(newElem);
                    nextDict[card.Id] = newElem;
                }
            }

            _activeHandElements.Clear();
            foreach (var kvp in nextDict)
                _activeHandElements[kvp.Key] = kvp.Value;
        }
    }
}