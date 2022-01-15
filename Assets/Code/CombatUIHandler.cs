using UnityEngine;
using UnityEngine.UI;

public class CombatUIHandler : MonoBehaviour
{
    [SerializeField] private Text PlayerInfo;
    [SerializeField] private Text EnemyInfo;
    [SerializeField] private Text Turn;
    [SerializeField] private GameObject Targets;

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

    public void OnAttack() 
    {
        Targets.SetActive(true);
    }

    #endregion
}
