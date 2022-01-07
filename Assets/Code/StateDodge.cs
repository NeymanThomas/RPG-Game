using UnityEngine;

public class StateDodge
{
    public void Start_StateDodge() 
    {
        // For now lets keep the logic simple. If the opponent is faster than the 
        // character attacking, the dodge is successful and the attack does not happen
        if (CombatStateMachine.Instance.Player.Speed < CombatStateMachine.Instance.Enemy.Speed) 
        {
            Debug.Log("They dodged successfully");
            CombatStateMachine.Instance.sEndTurn.Start_StateEndTurn();
        } else 
        {
            Debug.Log("They failed to dodge");
            CombatStateMachine.Instance.sAttack.IsDodging = false;
            CombatStateMachine.Instance.sAttack.Start_StateAttack();
        }
    }
}
