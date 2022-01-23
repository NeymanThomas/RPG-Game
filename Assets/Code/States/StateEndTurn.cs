// Only here for Debug.Log function
using UnityEngine;

public class StateEndTurn
{
    public void Start_StateEndTurn() 
    {
        CombatStateMachine.Instance.CheckForCharacterDeath();
        // If the character targetted mutliple enemies, then it needs to go back
        if (CombatStateMachine.Instance.TargetList.Count > 1) 
        {
            CombatStateMachine.Instance.TargetList.RemoveAt(0);
            CombatStateMachine.Instance.sAttack.Start_StateAttack();
        } 
        else 
        {
            // change the turn basically
            CombatStateMachine.Instance.TargetList.Clear();
            CombatStateMachine.Instance.IncreaseTurnNumber();
            CombatStateMachine.Instance.GoToNextCharacter();
            CombatStateMachine.Instance.UIHandler.UpdateHUDBars();
            CombatStateMachine.Instance.UIHandler.UpdateCombatText("base");
        }
    }

    public StateEndTurn() 
    {

    }
}
