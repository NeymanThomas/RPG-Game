using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    // The instance of the State Machine itself
    private static CombatStateMachine _instance;

    // States
    private StateAttack _sAttack;
    private StateEndTurn _sEndTurn;

    private Character _player;
    private Character _enemy;
    private int CharacterTurn;

    public static CombatStateMachine Instance => _instance;
    public StateAttack sAttack => _sAttack;
    public StateEndTurn sEndTurn => _sEndTurn;
    public Character Player => _player;
    public Character Enemy => _enemy;

    void Awake() 
    {
        // Initialize Singleton for the CombatStateMachine
        if (_instance != null && _instance != this) 
        {
            Destroy(this.gameObject);
        } else 
        {
            _instance = this;
        }

        // Initialize all of the states that will be used for combat
        _sAttack = new StateAttack();
        _sEndTurn = new StateEndTurn();
    }

    void Start()
    {
        CharacterTurn = 1;

        _player = new Character() {
            MaxHealth = 100,
            CurrentHealth = 100
        };
        _enemy = new Character() {
            MaxHealth = 100,
            CurrentHealth = 100
        };
    }

    public void OnAttack() 
    {
        sAttack.Start_StateAttack();
    }

}
