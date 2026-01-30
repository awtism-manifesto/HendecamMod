namespace HendecamMod.Content.Rarities;

public class BurntOrange : ModRarity //for cross compat items
{
    public override Color RarityColor => new Color(224, 89, 0);

    public override int GetPrefixedRarity(int offset, float valueMult)
    {
        if (offset < 0)
        {
            return ItemRarityID.Purple;
        }

        return Type;
    }
}