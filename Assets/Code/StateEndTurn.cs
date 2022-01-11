// Only here for Debug.Log function
using UnityEngine;

public class StateEndTurn
{
    public void Start_StateEndTurn() 
    {
        Debug.Log("The turn is now ending.");
        ChangeTurn();
        CombatStateMachine.Instance.UIHandler.UpdateStats();
        Debug.Log("It is now turn " + CombatStateMachine.Instance.TurnNumber);
        Debug.Log("It is " + CombatStateMachine.Instance.CurrentCharacter + "'s Turn");
    }

    private void ChangeTurn() 
    {
        CombatStateMachine.Instance.TurnNumber += 1;
        CombatStateMachine.Instance.NextCharacterTurn();
    }

    public StateEndTurn() 
    {

    }
}
