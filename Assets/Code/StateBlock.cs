using UnityEngine;

public class StateBlock
{
    public void Start_StateBlock () 
    {
        // For now, simply determine if the block goes through by comparing
        // the defense stat of each character
        if (CombatStateMachine.Instance.Enemy.Defense < CombatStateMachine.Instance.Player.Power) 
        {
            Debug.Log("They failed to block");
            CombatStateMachine.Instance.sAttack.IsBlocking = false;
            CombatStateMachine.Instance.sAttack.Start_StateAttack();
        } else 
        {
            Debug.Log("They blocked successfully");
            CombatStateMachine.Instance.sEndTurn.Start_StateEndTurn();
        }
    }

    public StateBlock () 
    {

    }
}
