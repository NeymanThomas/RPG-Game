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

    public void ShowTargets() 
    {
        Targets.SetActive(true);
    }

    public void HideTargets() 
    {
        Targets.SetActive(false);
    }
}
