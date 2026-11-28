using HendecamMod.Content.Items.Weapons.Multiclass;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Wings)]
public class NeoWings : ModItem
{
    public static int WingSlot { get; private set; }

    public override void SetStaticDefaults()
    {
        WingSlot = Item.wingSlot;
        ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(155, 8.33f, 1.55f);
        ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<TheSpamCannon>();
    }

    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 20;
        Item.value = 950000;
        Item.rare = ItemRarityID.Yellow;
        Item.accessory = true;
        Item.defense = 5;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        var sneo = player.GetModPlayer<NeoWingsPlayer>();
        sneo.hasNeoWings = true;
        if (!hideVisual)
            sneo.showWings = true;
    }

    public override void UpdateVanity(Player player)
    {
        var sneo = player.GetModPlayer<NeoWingsPlayer>();
        sneo.hasNeoWings = true;
        sneo.showWings = true;
    }

    public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
        ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
    {
        ascentWhenFalling = 1.025f;
        ascentWhenRising = 0.215f;
        maxCanAscendMultiplier = 1.125f;
        maxAscentMultiplier = 3.25f;
        constantAscend = 0.145f;
    }
}

public struct SneoVineCut
{
    public float timer;
    public float shakeamount;
    public float shakeamount2;
    public float x1;
    public float y1;
    public float x2;
    public float width;
    public bool bottomlinevisible;
}

public class NeoWingsPlayer : ModPlayer
{
    public const int BgVineMax = 8;
    public const int FgVineMax = 4;
    public const int PartCount = 8;

    public static readonly float[] partx_back =
    [
        -2f, 2f, -6f, 6f, -10f, 10f, -14f, 14f
    ];

    public static readonly float[] party_back =
    [
        -12f, -12f, -7f, -7f, -11f, -11f, -5f, -5f
    ];

    public static readonly int[] fgorder = [1, 2, 0, 3];

    public bool hasNeoWings;
    public bool showWings;
    public bool isAirborne;

    public int partmode = 1;
    public readonly float[] partsiner = new float[PartCount];
    public readonly float[] partrot = new float[PartCount];
    public readonly float[] partx = new float[PartCount];
    public readonly float[] party = new float[PartCount];
    public readonly float[] idealrot = new float[PartCount];

    public readonly float[] partsiner_back = new float[BgVineMax];

    public int hurttimer2;
    public float shocker;
    public float shocker_threshold = 4f;
    public float shakevar;
    public float fgjitter;

    public int lastwirecon;
    public float lastwiretimer;

    public int bgvinecount = BgVineMax;
    public int fgvinecount = FgVineMax;

    public readonly List<SneoVineCut> vinecuts = [];

    private bool vinesReady;
    private int prevBgCount;
    private int prevFgCount;

    public bool ShouldDrawVines()
    {
        if (!Player.active || !hasNeoWings || Player.dead || Player.invis || Player.mount.Active)
            return false;

        if (Player.wings != 0 && Player.wings != NeoWings.WingSlot)
            return false;

        return true;
    }

    public bool ShouldDrawWings()
    {
        return ShouldDrawVines() && (showWings || isAirborne);
    }

    public override void Initialize()
    {
        for (int ii = 0; ii < BgVineMax; ii++)
            partsiner_back[ii] = ii * 9f;

        for (int i = 0; i < PartCount; i++)
        {
            partsiner[i] = i * 14f;
            partrot[i] = 0f;
            partx[i] = 0f;
            party[i] = 0f;
            idealrot[i] = 0f;
        }

        partmode = 1;
        shocker = 0f;
        shocker_threshold = 4f;
        vinesReady = false;
        vinecuts.Clear();
    }

    public override void ResetEffects()
    {
        hasNeoWings = false;
        showWings = false;
    }

    public override void OnHurt(Player.HurtInfo info)
    {
        if (Main.dedServ || !hasNeoWings)
            return;

        hurttimer2 = 18;
        shocker = -1f;
        shocker_threshold = 4f;
    }

    public override void HideDrawLayers(PlayerDrawSet drawInfo)
    {
        if (ShouldDrawVines())
            PlayerDrawLayers.Wings.Hide();
    }

    private void CreateBgCut(int ii)
    {
        int dir = Player.direction;
        float baseX = partx_back[ii] * dir;
        float sway = MathF.Sin(partsiner_back[ii] / 30f) * 2f;

        vinecuts.Add(new SneoVineCut
        {
            timer = 0f,
            shakeamount = 10f,
            x1 = baseX + sway,
            y1 = party_back[ii],
            x2 = baseX,
            width = 1f,
            bottomlinevisible = true
        });
    }

    private void CreateFgCut(int slot)
    {
        int dir = Player.direction;
        float offset = slot - (FgVineMax - 1) * 0.5f;
        float sway = MathF.Sin(partsiner[slot] / 30f) * 2f;

        vinecuts.Add(new SneoVineCut
        {
            timer = 0f,
            shakeamount = 10f,
            x1 = offset * 5f * dir + sway,
            y1 = -10f,
            x2 = offset * 8f * dir,
            width = 2f,
            bottomlinevisible = true
        });
    }

    public override void PostUpdate()
    {
        if (Main.dedServ)
            return;

        const float f = 0.5f;

        if (hurttimer2 > 0)
            hurttimer2--;

        if (!ShouldDrawVines() || Player.statLifeMax2 <= 0)
        {
            isAirborne = false;
            vinesReady = false;
            shakevar = 0f;
            fgjitter = 0f;
            vinecuts.Clear();
            return;
        }

        float hp = MathHelper.Clamp(Player.statLife / (float)Player.statLifeMax2, 0f, 1f);
        bool hidebgvines = hp <= 0.12f;

        if (hidebgvines)
        {
            bgvinecount = 0;
            fgvinecount = 0;

            if (lastwirecon == 0)
                lastwirecon = 1;

            if (lastwirecon == 1)
            {
                lastwiretimer += f;
                if (lastwiretimer >= 30f)
                {
                    lastwiretimer = 30f;
                    lastwirecon = 2;
                }
            }
        }
        else
        {
            float scaledHp = MathHelper.Clamp((hp - 0.12f) / 0.88f, 0f, 1f);
            bgvinecount = Math.Clamp((int)MathF.Ceiling(scaledHp * BgVineMax), 0, BgVineMax);
            fgvinecount = Math.Clamp((int)MathF.Ceiling(scaledHp * FgVineMax), 1, FgVineMax);

            lastwirecon = 0;
            if (lastwiretimer > 0f)
                lastwiretimer = MathF.Max(0f, lastwiretimer - 1f);
        }

        if (vinesReady)
        {
            for (int ii = bgvinecount; ii < prevBgCount; ii++)
                CreateBgCut(ii);

            for (int k = fgvinecount; k < prevFgCount; k++)
                CreateFgCut(fgorder[k]);
        }
        else
        {
            vinesReady = true;
        }

        prevBgCount = bgvinecount;
        prevFgCount = fgvinecount;

        for (int i = vinecuts.Count - 1; i >= 0; i--)
        {
            SneoVineCut cut = vinecuts[i];
            cut.timer += f;

            if (cut.shakeamount > 0f)
                cut.shakeamount = MathF.Max(0f, cut.shakeamount - f);

            cut.shakeamount2 = cut.shakeamount > 0f
                ? cut.shakeamount - Main.rand.NextFloat(cut.shakeamount * 2f)
                : 0f;

            if (cut.timer > 11f && 130f - (cut.timer - 11f) * 4f < 16f)
                cut.bottomlinevisible = false;

            if (cut.timer >= 60f)
                vinecuts.RemoveAt(i);
            else
                vinecuts[i] = cut;
        }

        if (hurttimer2 > 0)
        {
            shakevar = Main.rand.NextFloat(hurttimer2) * 0.5f - hurttimer2 * 0.25f;
            float amp = hurttimer2 * 0.2f;
            fgjitter = amp - Main.rand.NextFloat(amp * 2f);
        }
        else
        {
            shakevar = 0f;
            fgjitter = 0f;
        }

        for (int ii = 0; ii < BgVineMax; ii++)
            partsiner_back[ii] += 0.5f * f;

        isAirborne = Player.velocity.Y != 0f
            || Player.oldVelocity.Y < 0f
            || Player.jump > 0
            || (Player.whoAmI == Main.myPlayer && Player.wingTimeMax > 0 && Player.wingTime < Player.wingTimeMax);

        bool flying = isAirborne && Player.controlJump && Player.jump <= 0 &&
            (Player.wingTime > 0f || (Player.whoAmI != Main.myPlayer && Player.velocity.Y < 0f));

        if (hurttimer2 > 0)
            partmode = 2;
        else if (flying)
            partmode = 21;
        else
            partmode = 1;

        shocker -= 1f * f;

        for (int i = 0; i < PartCount; i++)
        {
            if (partmode == 1)
            {
                partsiner[i] += (1f + i / 5f) * f * 2;
                partrot[i] = MathHelper.Lerp(partrot[i], MathF.Sin(partsiner[i] / 30f) * 15f, 0.25f);
                partx[i] = MathHelper.Lerp(partx[i], 0f, 0.25f * f);
                party[i] = MathHelper.Lerp(party[i], 0f, 0.25f * f);
            }
            else if (partmode == 2)
            {
                partsiner[i] += (1f + i / 5f) * f;
                partx[i] = MathF.Sin((partsiner[i] / 2f) * f);
                party[i] = MathF.Cos((partsiner[i] / 2f) * f);
                if (shocker < 0f)
                    partrot[i] = 60f - Main.rand.NextFloat(120f);
            }
            else if (partmode == 21)
            {
                partsiner[i] += 0.25f * f;
                partx[i] = MathHelper.Lerp(partx[i], 0f, 0.5f * f);
                party[i] = MathHelper.Lerp(party[i], 0f, 0.5f * f);
                idealrot[i] = MathF.Sin(partsiner[i] + i / 8f) * 20f;
                partrot[i] = MathHelper.Lerp(partrot[i], idealrot[i], 0.5f * f);
            }
        }

        if (shocker < 0f)
        {
            shocker = shocker_threshold;
            if (shocker_threshold > 2f)
                shocker_threshold -= 0.25f * f;
        }
    }
}

public class NeoWingsDrawLayer : PlayerDrawLayer
{
    private static readonly Color c_darkvine = new(0, 51, 0, 255);
    private static readonly Color c_green = new(0, 128, 0, 255);
    private static readonly Color c_orange = new(255, 160, 64, 255);
    private static readonly Rectangle pixelRect = new(0, 0, 1, 1);

    private Asset<Texture2D> winglTex;
    private Asset<Texture2D> wingrTex;

    public override Position GetDefaultPosition() => PlayerDrawLayers.BeforeFirstVanillaLayer;
    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
        if (drawInfo.headOnlyRender || drawInfo.shadow != 0f)
            return false;

        return drawInfo.drawPlayer.GetModPlayer<NeoWingsPlayer>().ShouldDrawVines();
    }

    private static void DrawVine(ref PlayerDrawSet drawInfo, Texture2D pixel, Vector2 bottom, Vector2 top, float width, Color color)
    {
        Vector2 diff = bottom - top;
        float len = diff.Length();
        if (len <= 1f)
            return;

        float rot = diff.ToRotation() - MathHelper.PiOver2;
        DrawData data = new(
            pixel,
            top,
            pixelRect,
            color,
            rot,
            new Vector2(0.5f, 0f),
            new Vector2(width, len),
            SpriteEffects.None,
            0
        )
        {
            shader = drawInfo.cWings
        };

        drawInfo.DrawDataCache.Add(data);
    }

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        Player player = drawInfo.drawPlayer;
        var sneo = player.GetModPlayer<NeoWingsPlayer>();

        Texture2D pixel = TextureAssets.MagicPixel.Value;
        int facing = player.direction;
        float grav = player.gravDir;

        Vector2 center = drawInfo.Position + new Vector2(player.width * 0.5f, player.height * 0.5f) - Main.screenPosition;
        float topY = grav > 0f ? -400f : Main.screenHeight + 400f;

        for (int ii = 0; ii < sneo.bgvinecount; ii++)
        {
            float baseX = center.X + NeoWingsPlayer.partx_back[ii] * facing;
            float sway = MathF.Sin(sneo.partsiner_back[ii] / 30f) * 2f;
            Vector2 bottom = new(baseX + sway, center.Y + NeoWingsPlayer.party_back[ii] * grav);
            Vector2 top = new(baseX, topY);
            DrawVine(ref drawInfo, pixel, bottom, top, 1f, c_darkvine);
        }

        if (sneo.lastwiretimer > 0f)
        {
            float sway = MathF.Sin(sneo.partsiner[4] / 25f) * 1.8f;
            Vector2 bottom = new(center.X + 2f * facing + sway, center.Y - 10f * grav);
            Vector2 top = new(center.X + 6f * facing, topY);

            if (sneo.lastwiretimer < 30f)
                DrawVine(ref drawInfo, pixel, bottom, top, 1f, c_darkvine);

            float t = sneo.lastwiretimer / 30f;
            DrawVine(ref drawInfo, pixel, bottom, top, 1f + t, c_green * t);
        }

        for (int k = 0; k < sneo.fgvinecount; k++)
        {
            int slot = NeoWingsPlayer.fgorder[k];
            float offset = slot - (NeoWingsPlayer.FgVineMax - 1) * 0.5f;
            float sway = MathF.Sin(sneo.partsiner[slot] / 30f) * 2f;

            Vector2 bottom = new(
                center.X + offset * 5f * facing + sway + sneo.fgjitter,
                center.Y - 10f * grav + sneo.fgjitter
            );
            Vector2 top = new(
                center.X + offset * 8f * facing,
                topY
            );

            DrawVine(ref drawInfo, pixel, bottom, top, 2f, c_green);
        }

        for (int i = 0; i < sneo.vinecuts.Count; i++)
        {
            SneoVineCut cut = sneo.vinecuts[i];
            float shake = cut.shakeamount2;

            float x1 = center.X + cut.x1;
            float y1 = center.Y + cut.y1 * grav;
            float xTop = center.X + cut.x2;
            float span = MathF.Max(1f, MathF.Abs(y1 - topY));

            if (cut.timer < 11f)
            {
                DrawVine(ref drawInfo, pixel, new Vector2(x1 + shake, y1 + shake), new Vector2(xTop + shake, topY + shake), cut.width, c_orange);
                DrawVine(ref drawInfo, pixel, new Vector2(x1, y1), new Vector2(xTop, topY), cut.width, Color.White);
            }
            else
            {
                float recoil = (cut.timer - 11f) * 4f;
                float topCut = 130f + recoil;
                float y3 = y1 - topCut * grav;

                if (grav > 0f ? topY < y3 : topY > y3)
                {
                    float x3 = MathHelper.Lerp(x1, xTop, MathHelper.Clamp(topCut / span, 0f, 1f));
                    DrawVine(ref drawInfo, pixel, new Vector2(x3, y3), new Vector2(xTop, topY), cut.width, c_orange);
                }

                if (cut.bottomlinevisible)
                {
                    float botCut = 130f - recoil;
                    float y2 = y1 - botCut * grav;
                    float x2 = MathHelper.Lerp(x1, xTop, MathHelper.Clamp(botCut / span, 0f, 1f));
                    DrawVine(ref drawInfo, pixel, new Vector2(x1 + shake, y1 + shake), new Vector2(x2 + shake, y2 + shake), cut.width, c_orange);
                }
            }
        }

        if (!sneo.ShouldDrawWings())
            return;

        winglTex ??= ModContent.Request<Texture2D>("HendecamMod/Content/Items/Accessories/NeoWings_Left");
        wingrTex ??= ModContent.Request<Texture2D>("HendecamMod/Content/Items/Accessories/NeoWings_Right");

        if (!winglTex.IsLoaded || !wingrTex.IsLoaded)
            return;

        Texture2D leftTex = winglTex.Value;
        Texture2D rightTex = wingrTex.Value;

        Vector2 winglPos = center + new Vector2(
            (4f + sneo.partx[0]) * facing + sneo.shakevar,
            (-4f + sneo.party[0]) * grav - sneo.shakevar
        );
        Vector2 wingrPos = center + new Vector2(
            (-4f + sneo.partx[7]) * facing + sneo.shakevar,
            (-4f + sneo.party[7]) * grav - sneo.shakevar
        );

        float leftRot = MathHelper.ToRadians(sneo.partrot[0]) * facing * grav;
        float rightRot = MathHelper.ToRadians(sneo.partrot[7]) * facing * grav;

        SpriteEffects fx = SpriteEffects.None;
        if (facing == -1)
            fx |= SpriteEffects.FlipHorizontally;
        if (grav < 0f)
            fx |= SpriteEffects.FlipVertically;

        Vector2 leftOrigin = new(
            facing == 1 ? leftTex.Width * 0.12f : leftTex.Width * 0.88f,
            grav > 0f ? leftTex.Height * 0.24f : leftTex.Height * 0.76f
        );

        Vector2 rightOrigin = new(
            facing == 1 ? rightTex.Width * 0.88f : rightTex.Width * 0.12f,
            grav > 0f ? rightTex.Height * 0.24f : rightTex.Height * 0.76f
        );

        DrawData winglData = new(leftTex, winglPos, null, drawInfo.colorArmorBody, leftRot, leftOrigin, 1f, fx, 0)
        {
            shader = drawInfo.cWings
        };

        DrawData wingrData = new(rightTex, wingrPos, null, drawInfo.colorArmorBody, rightRot, rightOrigin, 1f, fx, 0)
        {
            shader = drawInfo.cWings
        };

        drawInfo.DrawDataCache.Add(winglData);
        drawInfo.DrawDataCache.Add(wingrData);
    }
}