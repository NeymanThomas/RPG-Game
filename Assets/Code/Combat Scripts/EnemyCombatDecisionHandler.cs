using System.Collections.Generic;
using System;

public static class EnemyCombatDecisionHandler
{
    /// <summary>
    /// Being a completely random decision maker, this function simply checks the enemy's ActionList
    /// Count and chooses a random action based on that. Then a random character on the player's
    /// team is chosen based on its count. This random action and random target are then returned 
    /// as a tuple literal.
    /// </summary>
    /// <param name="enemy">The enemy character that is performing an action</param>
    /// <param name="targets">The list of characters on the player's team</param>
    /// <returns>A tuple literal of the action and target indexes</returns>
    public static (int action, int target) CreateRandomDecision(Character enemy, List<Character> targets) 
    {
        Random rnd = new Random();
        int randAction = rnd.Next(0, enemy.ActionList.Count);
        int randCharacter = rnd.Next(0, targets.Count);
        while (!(targets[randCharacter].IsAlive)) 
        {
            randCharacter = rnd.Next(0, targets.Count);
        }

        return (randAction, randCharacter);
    }

    public static (int action, int target) AttackLowestHealthPlayerRandom(Character enemy, List<Character> targets) 
    {
        return (1, 1);
    }
}
