using System.Collections.Generic;
using System;

public static class EnemyCombatDecisionHandler
{
    public static void CreateRandomDecision(Character enemy, List<Character> targets) 
    {
        Random rnd = new Random();
        CombatStateMachine.Instance.CurrentCharacterActionIndex = rnd.Next(0, enemy.ActionList.Count);
        int randIndex = rnd.Next(0, targets.Count);
        CombatStateMachine.Instance.TargetList.Add(CombatStateMachine.Instance.PlayerTeam[randIndex]);
    }
}
