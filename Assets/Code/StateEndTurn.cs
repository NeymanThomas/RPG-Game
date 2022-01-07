// Only here for Debug.Log function
using UnityEngine;

public class StateEndTurn
{
    public void Start_StateEndTurn() 
    {
        Debug.Log("The turn is now ending.");
        ChangeTurn();
        Debug.Log("It is now turn " + CombatStateMachine.Instance.CharacterTurn);
    }

    private void ChangeTurn() 
    {
        CombatStateMachine.Instance.CharacterTurn += 1;
    }
}
