using edw.CharacterStats;
using UnityEngine;

public enum EquipmentRank
{
    RankN,
    RankD,
    RankC,
    RankB,
    RankA,
    RankS
}

[CreateAssetMenu(menuName = "Items/Equippable Item")]
public class EquippableItem : Item
{
    [Space]
    public EquipmentItemSlot EquipmentType;
    [Space]
    public EquipmentRank Rank; // N, D, C, B, A, S, S+, S++
    [Range(0, 15)]
    public int EnchantLvl; // Enchant

    [Space]
    public int Strength;
    public int Agility;
    public int Intelligence;
    public int Vitality;
    [Space]
    public int StrengthPercentBonus;
    public int AgilityPercentBonus;
    public int IntelligencePercentBonus;
    public int VitalityPercentBonus;


    public override Item GetCopy()
    {
        return this;
    }

    public override void Destroy()
    {
        Destroy();
    }

    public void Equip(Character c)
    {
        if (Strength != 0)
            c.Strength.AddModifier(new StatModifier(Strength, StatModType.Flat, this));
        if (Agility != 0)
            c.Agility.AddModifier(new StatModifier(Agility, StatModType.Flat, this));
        if (Intelligence != 0)
            c.Intelligence.AddModifier(new StatModifier(Intelligence, StatModType.Flat, this));
        if (Vitality != 0)
            c.Vitality.AddModifier(new StatModifier(Vitality, StatModType.Flat, this));

        if (StrengthPercentBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrengthPercentBonus, StatModType.PercentMult, this));
        if (AgilityPercentBonus != 0)
            c.Agility.AddModifier(new StatModifier(AgilityPercentBonus, StatModType.PercentMult, this));
        if (IntelligencePercentBonus != 0)
            c.Intelligence.AddModifier(new StatModifier(IntelligencePercentBonus, StatModType.PercentMult, this));
        if (VitalityPercentBonus != 0)
            c.Vitality.AddModifier(new StatModifier(VitalityPercentBonus, StatModType.PercentMult, this));
    }

    public void Unequip(Character c)
    {
        c.Strength.RemoveAllModifiersFromSource(this);
        c.Agility.RemoveAllModifiersFromSource(this);
        c.Intelligence.RemoveAllModifiersFromSource(this);
        c.Vitality.RemoveAllModifiersFromSource(this);
    }

    public override string GetItemType()
    {
        return EquipmentType.ToString();
    }

    public override string GetDescription()
    {
        sb.Length = 0;
        AddStat(Strength, "Strength");
        AddStat(Agility, "Agility");
        AddStat(Intelligence, "Intelligence");
        AddStat(Vitality, "Vitality");

        AddStat(StrengthPercentBonus, "Strength", true);
        AddStat(AgilityPercentBonus, "Agility", true);
        AddStat(IntelligencePercentBonus, "Intelligence", true);
        AddStat(VitalityPercentBonus, "Vitality", true);
        return sb.ToString();
    }

    private void AddStat(float value, string statName, bool isPercent = false)
    {
        if (value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
                sb.Append("+");

            if (isPercent)
            {
                sb.Append(value * 100);
                sb.Append("% ");
            }
            else
            {
                sb.Append(value);
                sb.Append(" ");
            }

            sb.Append(statName);
        }
    }
}
