using HendecamMod.Content.NPCs.Bosses;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace HendecamMod.Content.Global;

// Very simple drop condition: drop during daytime
public class MichaelDrop : IItemDropRuleCondition
    {
    private static LocalizedText Description;

    public MichaelDrop()
        {
        Description ??= Language.GetOrRegister("Drops after defeating moon lord");
        }

    public bool CanDrop(DropAttemptInfo info)
        {
        return !NPC.AnyNPCs(ModContent.NPCType<EyesOfGabriel>()) && !NPC.AnyNPCs(ModContent.NPCType<EyesOfRaphael>());
        }

    public bool CanShowItemDropInUI()
        {
        return true;
        }

    public string GetConditionDescription()
        {
        return Description.Value;
        }
    }
public class GabrielDrop : IItemDropRuleCondition
    {
    private static LocalizedText Description;

    public GabrielDrop()
        {
        Description ??= Language.GetOrRegister("Drops after defeating moon lord");
        }

    public bool CanDrop(DropAttemptInfo info)
        {
        return !NPC.AnyNPCs(ModContent.NPCType<EyesOfMichael>()) && !NPC.AnyNPCs(ModContent.NPCType<EyesOfRaphael>());
        }

    public bool CanShowItemDropInUI()
        {
        return true;
        }

    public string GetConditionDescription()
        {
        return Description.Value;
        }
    }
public class RaphaelDrop : IItemDropRuleCondition
    {
    private static LocalizedText Description;

    public RaphaelDrop()
        {
        Description ??= Language.GetOrRegister("Drops after defeating moon lord");
        }

    public bool CanDrop(DropAttemptInfo info)
        {
        return !NPC.AnyNPCs(ModContent.NPCType<EyesOfGabriel>()) && !NPC.AnyNPCs(ModContent.NPCType<EyesOfMichael>());
        }

    public bool CanShowItemDropInUI()
        {
        return true;
        }

    public string GetConditionDescription()
        {
        return Description.Value;
        }
    }