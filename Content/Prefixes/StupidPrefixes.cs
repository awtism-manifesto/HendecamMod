using HendecamMod.Content.DamageClasses;

namespace HendecamMod.Content.Prefixes;

public class Tough : ModPrefix
    {
    public virtual float Power => 1f;
    public override PrefixCategory Category => PrefixCategory.AnyWeapon;
    public override float RollChance(Item item)
        {
        return 4.5f;
        }
    public override bool CanRoll(Item item)
        {
        if (item.DamageType == ModContent.GetInstance<UnarmedDamage>())
            {
            return true;
            }
        return false;
        }
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
        damageMult *= 1f + 0.075f * Power;
        }
    public override void ModifyValue(ref float valueMult)
        {
        valueMult *= 0f + 0.945f * Power;
        }
    public override void Apply(Item item)
        {
        }
    }