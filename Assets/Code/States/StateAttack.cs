public class StateAttack
{
    public void CreateAttackMessage() 
    {
        // Before anything, we need to output what attack is being executed
        // to the UI combat Text
        CombatStateMachine.Instance.AddCombatText(CombatStateMachine.Instance.CurrentCharacter.Name + " used " + 
        CombatStateMachine.Instance.CurrentCharacter.ActionList[CombatStateMachine.Instance.CurrentCharacterActionIndex]);
        CombatStateMachine.Instance.StartCombatText();
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
            CombatStateMachine.Instance.AddCombatText($"{CombatStateMachine.Instance.TargetList[0].Name} is blocking the attack!");
            CombatStateMachine.Instance.ModifyTextState(CombatTextState.Blocking);
            CombatStateMachine.Instance.AdvanceCombat();
        }
        else if (CombatStateMachine.Instance.TargetList[0].IsDodging) 
        {
            CombatStateMachine.Instance.AddCombatText($"{CombatStateMachine.Instance.TargetList[0].Name} is dodging the attack!");
            CombatStateMachine.Instance.ModifyTextState(CombatTextState.Dodging);
            CombatStateMachine.Instance.AdvanceCombat();
        }
        else if (CombatStateMachine.Instance.TargetList[0].IsCountering) 
        {
            CombatStateMachine.Instance.AddCombatText($"{CombatStateMachine.Instance.TargetList[0].Name} is countering the attack!");
            CombatStateMachine.Instance.ModifyTextState(CombatTextState.Countering);
            CombatStateMachine.Instance.AdvanceCombat();
        } 
        else 
        {
            // Continue with the attack
            CombatStateMachine.Instance.CurrentCharacter.ActionList[CombatStateMachine.Instance.CurrentCharacterActionIndex].Action(CombatStateMachine.Instance.CurrentCharacter, CombatStateMachine.Instance.TargetList[0]);
            CombatStateMachine.Instance.AdvanceCombat();
        }
    }

    public StateAttack() 
    {

    }
}
