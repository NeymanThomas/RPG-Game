public class StateAttack
{
    public void CreateAttackMessage() 
    {
        // Before anything, we need to output what attack is being executed
        // to the UI combat Text
        CombatStateMachine.Instance.UIHandler.combatTextState = CombatTextState.Attacking;
        CombatStateMachine.Instance.UIHandler.AddCombatText(CombatStateMachine.Instance.CurrentCharacter.Name + " used " + 
        CombatStateMachine.Instance.CurrentCharacter.ActionList[CombatStateMachine.Instance.CurrentCharacterActionIndex]);
        CombatStateMachine.Instance.UIHandler.StartCombatText();
    }

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
            CombatStateMachine.Instance.sBlock.Start_StateBlock();
        }
        else if (CombatStateMachine.Instance.TargetList[0].IsDodging) 
        {
            CombatStateMachine.Instance.sDodge.Start_StateDodge();
        }
        else if (CombatStateMachine.Instance.TargetList[0].IsCountering) 
        {

        } 
        else 
        {
            Attack();
        }
    }

    private void Attack() 
    {
        CombatStateMachine.Instance.CurrentCharacter.ActionList[CombatStateMachine.Instance.CurrentCharacterActionIndex].Action(CombatStateMachine.Instance.CurrentCharacter, CombatStateMachine.Instance.TargetList[0]);
        CombatStateMachine.Instance.sEndTurn.Start_StateEndTurn();
    }

    public StateAttack() 
    {

    }
}
