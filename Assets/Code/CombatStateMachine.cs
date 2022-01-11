using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    // The instance of the State Machine itself
    private static CombatStateMachine _instance;

    // States
    private StateAttack _sAttack;
    private StateEndTurn _sEndTurn;
    private StateDodge _sDodge;
    private StateBlock _sBlock;

    [SerializeField] private CombatUIHandler uiHandler;

    private Character _player;
    private Character _enemy;
    private int _turnNumber;
    private Character _characterTurn;

    // ===================================================================

    public static CombatStateMachine Instance => _instance;
    public StateAttack sAttack => _sAttack;
    public StateEndTurn sEndTurn => _sEndTurn;
    public StateDodge sDodge => _sDodge;
    public StateBlock sBlock => _sBlock;

    public CombatUIHandler UIHandler => uiHandler;

    public Character Player => _player;
    public Character Enemy => _enemy;
    public int TurnNumber 
    {
        get => _turnNumber;
        set => _turnNumber = value;
    }
    public Character CharacterTurn 
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
        _sBlock = new StateBlock();
    }

    void Start()
    {
        _turnNumber = 1;

        _player = new Character() {
            Name = "Player",
            MaxHealth = 100,
            CurrentHealth = 100,
            Speed = 25
        };
        _enemy = new Character() {
            Name = "Enemy",
            MaxHealth = 100,
            CurrentHealth = 100,
            Speed = 15
        };

        if (_player.Speed >= _enemy.Speed) 
        {
            _characterTurn = _player;
        }

        uiHandler.Init();
    }

    public void OnAttack() 
    {
        sAttack.Start_StateAttack();
    }

}
