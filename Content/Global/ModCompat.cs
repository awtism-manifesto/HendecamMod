using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework.Graphics;

namespace HendecamMod.Content.Global;

public class ModCompat : ModSystem
{
    public override void PostAddRecipes()
    {
        ModLoader.TryGetMod("CalamityMod", out Mod CalMerica);
        ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica);
        ModLoader.TryGetMod("Redemption", out Mod RedMerica);
        ModLoader.TryGetMod("Paracosm", out Mod ParaMerica);
        ModLoader.TryGetMod("Spooky", out Mod SpookMerica);
        ModLoader.TryGetMod("FargowiltasSouls", out Mod FargoMerica);
        ModLoader.TryGetMod("Fargowiltas", out Mod FargoMerica2);
        ModLoader.TryGetMod("StarsAbove", out Mod StarMerica);
        ModLoader.TryGetMod("SpiritMod", out Mod SpiritMerica);
        ModLoader.TryGetMod("SOTS", out Mod SOTSMerica);
        ModLoader.TryGetMod("MagAF", out Mod MagMerica);
        ModLoader.TryGetMod("RecipeBrowser", out Mod RecipeBrowser);
    }

    public override void PostSetupContent()
    {
        if (ModLoader.TryGetMod("RecipeBrowser", out Mod mod) && !Main.dedServ)
        {
            mod.Call("AddItemCategory", "Stupid", "Weapons", Mod.Assets.Request<Texture2D>("Content/Global/StupidIcon"), (Predicate<Item>)(item =>
            {
                if (item.damage > 0 && item.CountsAsClass<StupidDamage>() && !item.CountsAsClass<OmniDamage>())
                {
                    return true;
                }

                return false;
            }));
        }
    }
}

public class MulticlassRecipe : ModSystem
{
    public override void PostSetupContent()
    {
        if (ModLoader.TryGetMod("RecipeBrowser", out Mod mod) && !Main.dedServ)
        {
            mod.Call("AddItemCategory", "Multiclass", "Weapons", Mod.Assets.Request<Texture2D>("Content/Global/MulticlassIcon"), (Predicate<Item>)(item =>
            {
                if ((item.damage > 0) & item.CountsAsClass<MeleeStupidDamage>() || item.CountsAsClass<RangedStupidDamage>() || item.CountsAsClass<AutismDamage>()
                    || item.CountsAsClass<OmniDamage>() || item.CountsAsClass<MeleeMagicDamage>() || item.CountsAsClass<RangedMagicDamage>() || item.CountsAsClass<RangedSummonDamage>()
                    || item.CountsAsClass<MagicSummonDamage>() || item.CountsAsClass<SummonStupidDamage>() || item.CountsAsClass<MeleeRangedDamage>() || item.CountsAsClass<MeleeSummonDamage>() || item.CountsAsClass(DamageClass.Generic))
                {
                    return true;
                }

                return false;
            }));
        }
    }
}