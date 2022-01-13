// Only here for Debug.Log function
using UnityEngine;

public class StateEndTurn
{
    public void Start_StateEndTurn() 
    {
        // If the character targetted mutliple enemies, then it needs to go back
        if (CombatStateMachine.Instance.TargetList.Count > 1) 
        {
            CombatStateMachine.Instance.TargetList.RemoveAt(0);
            CombatStateMachine.Instance.sAttack.Start_StateAttack();
        } 
        else 
        {
            Debug.Log("The turn is now ending.");
            ChangeTurn();
            CombatStateMachine.Instance.UIHandler.UpdateStats();
            Debug.Log("It is now turn " + CombatStateMachine.Instance.TurnNumber);
            Debug.Log("It is " + CombatStateMachine.Instance.CurrentCharacter.Name + "'s Turn");
        }
    }

    private void ChangeTurn() 
    {
        CombatStateMachine.Instance.TargetList.Clear();
        CombatStateMachine.Instance.TurnNumber += 1;
        CombatStateMachine.Instance.GoToNextCharacter();
    }

    public StateEndTurn() 
    {

    }
}
