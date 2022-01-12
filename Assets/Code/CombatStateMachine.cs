using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    // The instance of the State Machine itself
    private static CombatStateMachine _instance;

    // States
    private StateEndTurn _sEndTurn;
    private StateAttack _sAttack;
    private StateDodge _sDodge;
    private StateBlock _sBlock;

    [SerializeField] private CombatUIHandler uiHandler;

    private List<Character> _playerTeam, _enemyTeam, _turnOrder;
    private Character _currentCharacter;

    private Character _player, _enemy;
    private int _turnNumber;

    // ===================================================================

    public static CombatStateMachine Instance => _instance;
    public StateEndTurn sEndTurn => _sEndTurn;
    public StateAttack sAttack => _sAttack;
    public StateDodge sDodge => _sDodge;
    public StateBlock sBlock => _sBlock;

    public CombatUIHandler UIHandler => uiHandler;

    public List<Character> PlayerTeam => _playerTeam;
    public List<Character> EnemyTeam => _enemyTeam;
    public List<Character> TurnOrder => _turnOrder;

    public Character CurrentCharacter 
    {
        get => _currentCharacter;
        set => _currentCharacter = value;
    }

    public int TurnNumber 
    {
        get => _turnNumber;
        set => _turnNumber = value;
    }

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
        _sEndTurn = new StateEndTurn();
        _sAttack = new StateAttack();
        _sDodge = new StateDodge();
        _sBlock = new StateBlock();

        // Initialize variables and objects
        _turnNumber = 1;

        // Here there will be a method for gathering all of the player's characters
        // and adding them to the state machine. For now we create them
        Character playerCharacter_1 = new Character() {
            Name = "Player Character 1",
            MaxHealth = 100,
            CurrentHealth = 100,
            Speed = 40
        };
        Character playerCharacter_2 = new Character() {
            Name = "Player Character 2",
            MaxHealth = 250,
            CurrentHealth = 250,
            Speed = 10
        };
        Character playerCharacter_3 = new Character() {
            Name = "Player Character 3",
            MaxHealth = 75,
            CurrentHealth = 75,
            Speed = 30
        };

        // The enemies will be created based on a script elsewhere. For now we 
        // can just create them here
        Character enemyCharacter_1 = new Character() {
            Name = "Enemy Character 1",
            MaxHealth = 150,
            CurrentHealth = 150,
            Speed = 35
        };
        Character enemyCharacter_2 = new Character() {
            Name = "Enemy Character 2",
            MaxHealth = 100,
            CurrentHealth = 100,
            Speed = 45
        };
        Character enemyCharacter_3 = new Character() {
            Name = "Enemy Character 3",
            MaxHealth = 300,
            CurrentHealth = 300,
            Speed = 5
        };

        _turnOrder = new List<Character>();
        _turnOrder.Add(playerCharacter_1);
        _turnOrder.Add(playerCharacter_2);
        _turnOrder.Add(playerCharacter_3);
        _turnOrder.Add(enemyCharacter_1);
        _turnOrder.Add(enemyCharacter_2);
        _turnOrder.Add(enemyCharacter_3);
        // Sort the List now

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

        _currentCharacter = _turnOrder[0];
    }

    void Start()
    {
        uiHandler.Init();
    }

    public void GoToNextCharacter() 
    {
        for (int i = 0; i < _turnOrder.Count; i++) 
        {
            if (_currentCharacter == _turnOrder[_turnOrder.Count - 1]) 
            {
                _currentCharacter = _turnOrder[0];
                break;
            }
            if (_currentCharacter == _turnOrder[i]) 
            {
                _currentCharacter = _turnOrder[i + 1];
                break;
            }
        }
    }

    public void OnAttack() 
    {
        sAttack.Start_StateAttack();
    }

}
