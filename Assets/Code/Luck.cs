using UnityEngine;

public static class Luck
{
    public static float GetLuckDamage(int luckStat) 
    {
        return 5.0f;
    }

    public static float GetLuckResistance(int luckState) 
    {
        return 5.0f;
    }

    public static bool GetCrit(Character attacker) 
    {
        float val = (attacker.Luck + (attacker.Skill / 2.0f)) * (100.0f / (attacker.Luck + attacker.Skill + 100.0f));
        int chance = Random.Range(0, 100);
        val = Mathf.Round(val);

        if (chance <= val) 
        {
            return true;
        }
        return false;
    }
}
