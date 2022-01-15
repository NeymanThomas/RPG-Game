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
        /*
        Character c1 = CombatStateMachine.Instance.CurrentCharacter;
        Character c2 = CombatStateMachine.Instance.TargetList[0];
        bool crit;
        int critDamage = 5;
        int abilityDamage = 5 + c1.Skill;

        int PlayerDamage = ((c1.CurrentHealth / c1.MaxHealth) * ((c1.Level + 5) / 2)) + ((c1.CurrentStamina / c1.MaxStamina) * ((c1.Level + 5) / 2)) + (c1.Skill / 10) + 
        (int)Luck.GetLuckDamage(c1.Luck) + (c1.Weapon.PrimaryDamaga + c1.Weapon.SecondaryDamage) + abilityDamage + critDamage;

        */
        int passedInParam = 0;
        CombatStateMachine.Instance.CurrentCharacter.ActionList[passedInParam].Action();
        CombatStateMachine.Instance.TargetList[0].CurrentHealth -= CombatStateMachine.Instance.CurrentCharacter.Power;
        Debug.Log(CombatStateMachine.Instance.CurrentCharacter.Name + " Attacked");
        CombatStateMachine.Instance.sEndTurn.Start_StateEndTurn();
    }

    public StateAttack() 
    {

    }
}
