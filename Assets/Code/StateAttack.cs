// Only here for the Debug.Log function
using UnityEngine;

public class StateAttack
{
    public void Start_StateAttack() 
    {
        // Check to see if the opponent is doing anything that complicates attacking
        /*
        if (IsBlocking) 
        {
            Debug.Log("They are blocking");
            CombatStateMachine.Instance.sBlock.Start_StateBlock();
        }
        else if (IsDodging) 
        {
            Debug.Log("They are dodging");
            CombatStateMachine.Instance.sDodge.Start_StateDodge();
        }
        else if (IsCountering) 
        {
            Debug.Log("They are countering");
        } else {
            // If the opponent wasn't doing anything, go to the attack function
            Attack();
        }
        */
    }

    private void Attack() 
    {
        Debug.Log("They Attacked");
        CombatStateMachine.Instance.sEndTurn.Start_StateEndTurn();
    }

    public StateAttack() 
    {

    }
}
