using UnityEngine;
using UnityEngine.UI;

public class CombatUIHandler : MonoBehaviour
{
    [SerializeField] private Text PlayerInfo;
    [SerializeField] private Text EnemyInfo;
    [SerializeField] private Text Turn;
    [SerializeField] private GameObject Targets;
    [SerializeField] private GameObject Actions;

    public void Init()
    {
        PlayerInfo.text = CombatStateMachine.Instance.PlayerTeam[0].Name + ": " + CombatStateMachine.Instance.PlayerTeam[0].CurrentHealth
        + "\r\n" + CombatStateMachine.Instance.PlayerTeam[1].Name + ": " + CombatStateMachine.Instance.PlayerTeam[1].CurrentHealth 
        + "\r\n" + CombatStateMachine.Instance.PlayerTeam[2].Name + ": " + CombatStateMachine.Instance.PlayerTeam[2].CurrentHealth;
        EnemyInfo.text = CombatStateMachine.Instance.EnemyTeam[0].Name + ": " + CombatStateMachine.Instance.EnemyTeam[0].CurrentHealth
        + "\r\n" + CombatStateMachine.Instance.EnemyTeam[1].Name + ": " + CombatStateMachine.Instance.EnemyTeam[1].CurrentHealth 
        + "\r\n" + CombatStateMachine.Instance.EnemyTeam[2].Name + ": " + CombatStateMachine.Instance.EnemyTeam[2].CurrentHealth;
        Turn.text = CombatStateMachine.Instance.CurrentCharacter.Name + "'s Turn";

        Targets.SetActive(false);
        Actions.SetActive(false);
    }

    public void UpdateStats() 
    {
        PlayerInfo.text = CombatStateMachine.Instance.PlayerTeam[0].Name + ": " + CombatStateMachine.Instance.PlayerTeam[0].CurrentHealth
        + "\r\n" + CombatStateMachine.Instance.PlayerTeam[1].Name + ": " + CombatStateMachine.Instance.PlayerTeam[1].CurrentHealth 
        + "\r\n" + CombatStateMachine.Instance.PlayerTeam[2].Name + ": " + CombatStateMachine.Instance.PlayerTeam[2].CurrentHealth;
        EnemyInfo.text = CombatStateMachine.Instance.EnemyTeam[0].Name + ": " + CombatStateMachine.Instance.EnemyTeam[0].CurrentHealth
        + "\r\n" + CombatStateMachine.Instance.EnemyTeam[1].Name + ": " + CombatStateMachine.Instance.EnemyTeam[1].CurrentHealth 
        + "\r\n" + CombatStateMachine.Instance.EnemyTeam[2].Name + ": " + CombatStateMachine.Instance.EnemyTeam[2].CurrentHealth;
        Turn.text = CombatStateMachine.Instance.CurrentCharacter.Name + "'s Turn";
    }

    #region Click Events

    public void OnSelectTarget1() 
    {
        Targets.SetActive(false);
        CombatStateMachine.Instance.TargetList.Add(CombatStateMachine.Instance.EnemyTeam[0]);
        CombatStateMachine.Instance.sAttack.Start_StateAttack();
    }

    public void OnSelectTarget2() 
    {
        Targets.SetActive(false);
        CombatStateMachine.Instance.TargetList.Add(CombatStateMachine.Instance.EnemyTeam[1]);
        CombatStateMachine.Instance.sAttack.Start_StateAttack();
    }

    public void OnSelectTarget3() 
    {
        Targets.SetActive(false);
        CombatStateMachine.Instance.TargetList.Add(CombatStateMachine.Instance.EnemyTeam[2]);
        CombatStateMachine.Instance.sAttack.Start_StateAttack();
    }

    public void OnSelectAction1() 
    {
        Debug.Log(CombatStateMachine.Instance.CurrentCharacter.ActionList[0].Name);
        CombatStateMachine.Instance.CurrentCharacterActionIndex = 0;
        Actions.SetActive(false);
        Targets.SetActive(true);
    }

    public void OnSelectAction2() 
    {
        Debug.Log(CombatStateMachine.Instance.CurrentCharacter.ActionList[1].Name);
        CombatStateMachine.Instance.CurrentCharacterActionIndex = 1;
        Actions.SetActive(false);
        Targets.SetActive(true);
    }

    public void OnSelectAction3() 
    {
        Debug.Log(CombatStateMachine.Instance.CurrentCharacter.ActionList[2].Name);
        CombatStateMachine.Instance.CurrentCharacterActionIndex = 2;
        Actions.SetActive(false);
        Targets.SetActive(true);
    }

    // Instead of just showing targets, this function should show a list of all the actions the
    // Character can use, THEN after choosing an action the player chooses a target to use the
    // Action on. Since the list of actions will be 0 - whatever, the elements can have the 
    // same value, so if they choose their 0th action, pass a 0 on to the action class so it
    // knows to use the CurrentCharacter's 0th action from their ActionList. boom
    public void OnAttack() 
    {
        Actions.SetActive(true);
    }

    #endregion
}
