using UnityEngine;

public class StateAttack
{
    /// <summary>
    /// This is the entry point for the Attack State. The function checks to see if the first
    /// Character in the Target List is Blocking, Dodging, or Countering. If they are doing
    /// any of those things, the action will then go to the corresponding state. Otherwise, 
    /// the attack function is called and damage is dealt.
    /// </summary>
    public void Start_StateAttack() 
    {
        if (CombatStateMachine.Instance.TargetList[0].IsBlocking) 
        {
            Debug.Log("They are blocking");
            CombatStateMachine.Instance.sBlock.Start_StateBlock();
        }
        else if (CombatStateMachine.Instance.TargetList[0].IsDodging) 
        {
            Debug.Log("They are dodging");
            CombatStateMachine.Instance.sDodge.Start_StateDodge();
        }
        else if (CombatStateMachine.Instance.TargetList[0].IsCountering) 
        {
            Debug.Log("They are countering");
        } else {
            Attack();
        }
    }

    private void Attack() 
    {
        CombatStateMachine.Instance.CurrentCharacter.ActionList[CombatStateMachine.Instance.CurrentCharacterActionIndex].Action(CombatStateMachine.Instance.CurrentCharacter, CombatStateMachine.Instance.TargetList[0]);
        Debug.Log(CombatStateMachine.Instance.CurrentCharacter.Name + " Attacked");
        CombatStateMachine.Instance.sEndTurn.Start_StateEndTurn();
    }

    public StateAttack() 
    {

    }
}
