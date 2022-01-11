using UnityEngine;
using UnityEngine.UI;

public class CombatUIHandler : MonoBehaviour
{
    [SerializeField] private Text PlayerName;
    [SerializeField] private Text EnemyName;
    [SerializeField] private Text PlayerHealth;
    [SerializeField] private Text EnemyHealth;
    [SerializeField] private Text Turn;

    public void Init()
    {
        PlayerName.text = CombatStateMachine.Instance.Player.Name;
        EnemyName.text = CombatStateMachine.Instance.Enemy.Name;
        PlayerHealth.text = CombatStateMachine.Instance.Player.CurrentHealth.ToString();
        EnemyHealth.text = CombatStateMachine.Instance.Enemy.CurrentHealth.ToString();
        Turn.text = CombatStateMachine.Instance.CurrentCharacter.Name + "'s Turn";
    }

    public void UpdateStats() 
    {
        PlayerHealth.text = CombatStateMachine.Instance.Player.CurrentHealth.ToString();
        EnemyHealth.text = CombatStateMachine.Instance.Enemy.CurrentHealth.ToString();
        Turn.text = CombatStateMachine.Instance.CurrentCharacter.Name + "'s Turn";
    }
}
