using HendecamMod.Common.Systems;
using HendecamMod.Common.Utils;
using System.Collections.Generic;
using System.IO;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using static HendecamMod.Content.NPCs.Bosses.PromethiumPlasmoid.PromethiumPlasmoid;

namespace HendecamMod.Content.NPCs.Bosses.PromethiumPlasmoid;

// !!!! This boss is split up into different files for organization !!!!
// This file contains fields and methods related to behavior/AI

public partial class PromethiumPlasmoid : ModNPC
{
    #region Attack fields

    /// <summary>
    /// Timer that is incremented while the boss is alive.
    /// </summary>
    ref float AITimer => ref NPC.ai[0];

    /// <summary>
    /// The current attack being executed.
    /// </summary>
    ref float Attack => ref NPC.ai[1];

    // Counts down from attackDurations[(int)Attack].
    ref float AttackTimer => ref NPC.ai[2];

    /// <summary>
    /// Used for counting whatever during attacks.
    /// Also used for misc purposes in certain attacks like remembering values or acting as a flag.
    /// </summary>
    ref float AttackCount => ref NPC.ai[3];

    /// <summary>
    /// Additional field with the same purpose as AttackCount.
    /// </summary>
    float AttackCount2 = 0;

    /// <summary>
    /// Attack duration of the current attack being executed.
    /// Counts down and switches attacks when equal to 0.
    /// </summary>
    float attackDuration = 0;

    /// <summary>
    /// Attack durations for the first phase in ticks, indexed by the Attack field.
    /// When switching attacks, attackDuration is set to attackDurations[Attack], and counts down to zero from there.
    /// </summary>
    readonly int[] attackDurations = [600, 1080, 1080, 1080, 1320];

    /// <summary>
    /// Attacks that can be performed (order matters).
    /// </summary>
    public enum Attacks
    {
        // placeholders
        Dashing,
        Projectiling
    }

    /// <summary>
    /// Used to store a position.
    /// Synced, so if you need to store a random Vector2, assign it to this.
    /// </summary>
    Vector2 targetPosition = Vector2.Zero;

    /// <summary>
    /// The currently targeted player.
    /// </summary>
    public Player Player { get; private set; }

    // Syncing
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(AttackCount2);
        writer.Write(attackDuration);
        writer.WriteVector2(targetPosition);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        AttackCount2 = reader.ReadSingle();
        attackDuration = reader.ReadInt32();
        targetPosition = reader.ReadVector2();
    }

    #endregion

    public override void AI()
    {
        // Change targets if current target is dead or inactive
        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
        {
            NPC.TargetClosest(false);
        }

        // Despawn if no targets
        if (!NPC.HasValidTarget)
        {
            NPC.active = false;
            NPC.life = 0;
            NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
            return;
        }

        Player = Main.player[NPC.target];

        AITimer++;
    }

    /// <summary>
    /// // Perform attack based on Attack value and decrease attackDuration, switching attacks when it reaches 0.
    /// </summary>
    void AttackControl()
    {
        // This should be tweaked if the boss has multiple phases
        switch (Attack)
        {
            case (int)Attacks.Dashing:
                //Attack_Dashing();
                break;
        }

        // Counting down to switching attacks
        attackDuration--;
        if (attackDuration <= 0)
        {
            SwitchAttacks();
        }
    }

    void SwitchAttacks()
    {
        // Next attack, loop to start if all attacks are performed
        Attack++;
        if (Attack >= attackDurations.Length)
        {
            Attack = 0;
        }

        attackDuration = attackDurations[(int)Attack];

        // Reset variables
        AttackCount = 0;
        AttackCount2 = 0;
        AttackTimer = 0;
        NPC.Opacity = 1f;

        NPC.netUpdate = true;
    }
}