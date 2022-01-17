using UnityEngine;

public static class DamageCalculator
{
    public static void DealPhysicalDamage(Character attacker, Character defender, int actionPower, int accuracy, int critModifier) 
    {
        if (Luck.CheckIfAttackHits(accuracy)) 
        {
            float attackerStats = ((attacker.CurrentHealth / attacker.MaxHealth) * ((attacker.Level + 5.0f) / 2)) +
            ((attacker.CurrentStamina / attacker.MaxStamina) * ((attacker.Level + 5.0f) / 2)) +
            (attacker.Skill / 10.0f) + Luck.GetLuckDamage(attacker.Luck) + 
            (attacker.Weapon.PrimaryDamage + attacker.Weapon.SecondaryDamage);

            float defenderStats = ((defender.CurrentHealth / defender.MaxHealth) * ((defender.Level + 5.0f) / 2)) +
            ((defender.CurrentStamina / defender.MaxStamina) * ((defender.Level + 5.0f) / 2)) + 
            (defender.Skill / 10.0f) + Luck.GetLuckResistance(defender.Luck);

            if (Luck.GetCrit(attacker, critModifier)) 
            {
                attackerStats = attackerStats * 2.0f;
            }

            float damage = ((actionPower + attacker.Power + attackerStats) * (100.0f / (150.0f + defender.Defense + defenderStats)));
            damage = Mathf.RoundToInt(damage);

            if (damage > 0) 
            {
                defender.CurrentHealth -= (int)damage;
            }
        }
    }

    public static void DealMagicDamage(Character attacker, Character defender, int actionPower, int accuracy) 
    {

    }
}
