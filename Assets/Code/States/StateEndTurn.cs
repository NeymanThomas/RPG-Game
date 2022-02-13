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
            CombatStateMachine.Instance.TargetList.Clear();
            CombatStateMachine.Instance.IncreaseTurnNumber();
            RegenerateStats(CombatStateMachine.Instance.CurrentCharacter);

            CombatStateMachine.Instance.UIHandler.UpdateHUDBars();
            CombatStateMachine.Instance.GoToNextCharacter();
            CombatStateMachine.Instance.AddCombatText($"It is { CombatStateMachine.Instance.CurrentCharacter.Name }'s turn!");
            CombatStateMachine.Instance.EndTurn();
        }
    }

    private void RegenerateStats(Character c) 
    {
        c.CurrentStamina += c.StaminaRegen;
        c.CurrentMana += c.ManaRegen;
        if (c.CurrentStamina > c.MaxStamina) c.CurrentStamina = c.MaxStamina;
        if (c.CurrentMana > c.MaxMana) c.CurrentMana = c.MaxMana;
    }

    public StateEndTurn() 
    {

    }
}
