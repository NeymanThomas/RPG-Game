using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    // The instance of the State Machine itself
    private static CombatStateMachine _instance;

    // States
    private StateAttack _sAttack;
    private StateEndTurn _sEndTurn;
    private StateDodge _sDodge;

    private Character _player;
    private Character _enemy;
    private int _characterTurn;

    public static CombatStateMachine Instance => _instance;
    public StateAttack sAttack => _sAttack;
    public StateEndTurn sEndTurn => _sEndTurn;
    public StateDodge sDodge => _sDodge;

    public Character Player => _player;
    public Character Enemy => _enemy;
    public int CharacterTurn 
    {
        get => _characterTurn;
        set => _characterTurn = value;
    }

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
        _sDodge = new StateDodge();
    }

    void Start()
    {
        _characterTurn = 1;

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
