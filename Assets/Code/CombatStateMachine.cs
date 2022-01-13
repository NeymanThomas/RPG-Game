using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    #region Properties

    // The instance of the State Machine itself
    private static CombatStateMachine _instance;

    // States
    private StateEndTurn _sEndTurn;
    private StateAttack _sAttack;
    private StateDodge _sDodge;
    private StateBlock _sBlock;

    [SerializeField] private CombatUIHandler uiHandler;

    private List<Character> _playerTeam, _enemyTeam, _turnOrder, _targetList;
    private Character _currentCharacter;
    private int _turnNumber;

    // Public Properties
    public static CombatStateMachine Instance => _instance;
    public StateEndTurn sEndTurn => _sEndTurn;
    public StateAttack sAttack => _sAttack;
    public StateDodge sDodge => _sDodge;
    public StateBlock sBlock => _sBlock;

    public CombatUIHandler UIHandler => uiHandler;

    public List<Character> PlayerTeam => _playerTeam;
    public List<Character> EnemyTeam => _enemyTeam;
    public List<Character> TurnOrder => _turnOrder;
    public List<Character> TargetList => _targetList;

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

    #endregion

    /// <summary>
    /// The Awake function handles all of the initialization of the <c>CombatStateMachine</c> class.
    /// This is handled by the <c>Awake</c> function so that any other class that needs to access
    /// the <c>CombatStateMachine</c> will not be trying to access something that hasn't been 
    /// initialized yet.
    /// <list type="bullet">
    ///     <item>
    ///         <term>Singleton</term>
    ///         <description>initialized first to ensure all other functionality can be derived</description>
    ///     </item>
    ///     <item>
    ///         <term>States</term>
    ///         <description>All of the states used by the state machine are initialized next</description>
    ///     </item>
    ///     <item>
    ///         <term>Variables</term>
    ///         <description>The variables like the <c>TurnNumber</c> are initialized next. These are used
    ///         for storing data the state machine needs</description>
    ///     </item>
    ///     <item>
    ///         <term>Characters</term>
    ///         <description>The characters that will be used in combat are then added. The _playerTeam
    ///         is filled with the player's characters, the _enemyTeam is filled with the enemy
    ///         characters, all characters are added to _turnOrder, and the _currentCharacter is set
    ///         to the first character in the _turnOrder list.</description>
    ///     </item>
    /// </list>
    /// </summary>
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
        _targetList = new List<Character>();

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

        _playerTeam = new List<Character>() 
        {
            playerCharacter_1,
            playerCharacter_2,
            playerCharacter_3
        };

        _enemyTeam = new List<Character>() 
        {
            enemyCharacter_1,
            enemyCharacter_2,
            enemyCharacter_3
        };

        _turnOrder = new List<Character>() 
        {
            playerCharacter_1,
            playerCharacter_2,
            playerCharacter_3,
            enemyCharacter_1,
            enemyCharacter_2,
            enemyCharacter_3
        };

        // Sort the List now
        SortOrder();

        // Finally set the current character to whoever is going first
        _currentCharacter = _turnOrder[0];
    }

    /// <summary>
    /// The <c>Start</c> method goes after <c>Awake</c>, so all things that need to be 
    /// initialized outside of the <c>CombatStateMachine</c> are handled here.
    /// </summary>
    void Start()
    {
        uiHandler.Init();
    }

    /// <summary>
    /// The <c>GoToNextCharacter</c> function is called when a character has finished their turn.
    /// It is called from the <c>StateEndTurn</c> class and changes the _currentCharacter variable
    /// to the next character in the _turnOrder list. When the _currentCharacter is the last 
    /// character in the _turnOrder list, the _currentCharacter is set back to the first character
    /// in the _turnOrder list.
    /// </summary>
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

    /// <summary>
    /// The <c>SortOrder()</c> function is specific to the <c>CombatStateMachine</c> class. When Called,
    /// the _turnOrder List is sorted based on the speeds of the character object in the list. The
    /// structure for the sorting algorithm is a simple bubble sort. 
    /// <see>https://www.geeksforgeeks.org/bubble-sort/ </see>
    /// </summary>
    private void SortOrder() 
    {
        int n = _turnOrder.Count;
        for (int i = 0; i < n - 1; i++) 
        {
            for (int j = 0; j < n - i - 1; j++) 
            {
                if (_turnOrder[j].Speed < _turnOrder[j + 1].Speed) 
                {
                    Character temp = _turnOrder[j];
                    _turnOrder[j] = _turnOrder[j + 1];
                    _turnOrder[j + 1] = temp;
                }
            }
        }
    }

    #region OnClickFunctions

    public void OnSelectTargets() 
    {

    }

    public void OnAttack() 
    {
        _targetList.Add(_enemyTeam[0]);
        sAttack.Start_StateAttack();
    }

    #endregion

}
