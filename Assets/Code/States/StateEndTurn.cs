public class StateEndTurn
{
    public void Start_StateEndTurn() 
    {
        CombatStateMachine.Instance.CheckForCharacterDeath();
        // If the character targetted mutliple enemies, then it needs to go back
        if (CombatStateMachine.Instance.TargetList.Count > 1) 
        {
            CombatStateMachine.Instance.TargetList.RemoveAt(0);
            CombatStateMachine.Instance.ModifyTextState(CombatTextState.AttackMessage);
            CombatStateMachine.Instance.sAttack.CreateAttackMessage();
        } 
        else 
        {
            // change the turn basically
            CombatStateMachine.Instance.TargetList.Clear();
            CombatStateMachine.Instance.IncreaseTurnNumber();
            CombatStateMachine.Instance.UIHandler.UpdateHUDBars();
            CombatStateMachine.Instance.GoToNextCharacter();
            CombatStateMachine.Instance.AddCombatText($"It is { CombatStateMachine.Instance.CurrentCharacter.Name }'s turn!");
            CombatStateMachine.Instance.EndTurn();
        }
    }

    public StateEndTurn() 
    {

    }
}
