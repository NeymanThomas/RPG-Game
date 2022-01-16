using System;

public static class Luck
{
    public static int GetLuckDamage(int luckStat) 
    {
        return 5;
    }

    public static int GetLuckResistance(int luckState) 
    {
        return 5;
    }

    public static bool GetCrit(Character attacker) 
    {
        Random rnd = new Random();
        int val = (attacker.Luck + (attacker.Skill / 2)) * (100 / (attacker.Luck + attacker.Skill + 100));
        int chance = rnd.Next(0, 101);

        if (val <= chance) 
        {
            return true;
        }
        return false;
    }
}
