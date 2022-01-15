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
    public Character CurrentCharacter => _currentCharacter;
    public int TurnNumber => _turnNumber;

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
        InitializeCharacters();

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
    /// THIS NEEDS TO BE UPDATED LATER, but for now this function simply creates all of
    /// the characters that are used for combat. It then places the player's characters
    /// into their team, the enemy characters into the enemy's team, then puts all 
    /// characters into the turn order list.
    /// </summary>
    private void InitializeCharacters() 
    {
        Character playerCharacter_1 = new Character() {
            Name = "Player Character 1",
            MaxHealth = 100,
            CurrentHealth = 100,
            Power = 30,
            Defense = 30,
            CurrentStamina = 100,
            MaxStamina = 100,
            MagicPower = 10,
            MagicResistance = 15,
            MaxMana = 50,
            CurrentMana = 50,
            Skill = 5,
            Luck = 10,
            Speed = 40,
            Weapon = new Weapon() {
                Name = "Basic Sword",
                Weight = 5,
                PrimaryDamaga = 10,
                SecondaryDamage = 0,
                Skill = 3,
                Range = 5
            }
        };
        Character playerCharacter_2 = new Character() {
            Name = "Player Character 2",
            MaxHealth = 250,
            CurrentHealth = 250,
            Power = 65,
            Defense = 50,
            MaxStamina = 150,
            CurrentStamina = 150,
            MagicPower = 10,
            MagicResistance = 30,
            MaxMana = 50,
            CurrentMana = 50,
            Skill = 5,
            Luck = 10,
            Speed = 10,
            Weapon = new Weapon() {
                Name = "Battle Axe",
                Weight = 10,
                PrimaryDamaga = 20,
                SecondaryDamage = 0,
                Skill = 3,
                Range = 4
            }
        };
        Character playerCharacter_3 = new Character() {
            Name = "Player Character 3",
            MaxHealth = 75,
            CurrentHealth = 75,
            Power = 5,
            Defense = 5,
            MaxStamina = 60,
            CurrentStamina = 60,
            MagicPower = 50,
            MagicResistance = 75,
            MaxMana = 200,
            CurrentMana = 200,
            Skill = 10,
            Luck = 20,
            Speed = 30,
            Weapon = new Weapon() {
                Name = "Spell Book",
                Weight = 1,
                PrimaryDamaga = 20,
                SecondaryDamage = 0,
                Skill = 10,
                Range = 20
            }
        };

        Strike strike = new Strike();
        Hack hack = new Hack();
        Slash slash = new Slash();

        playerCharacter_1.ActionList.Add(strike);
        playerCharacter_1.ActionList.Add(hack);
        playerCharacter_1.ActionList.Add(slash);
        playerCharacter_2.ActionList.Add(strike);
        playerCharacter_2.ActionList.Add(hack);
        playerCharacter_2.ActionList.Add(slash);
        playerCharacter_3.ActionList.Add(strike);
        playerCharacter_3.ActionList.Add(hack);
        playerCharacter_3.ActionList.Add(slash);

        Character enemyCharacter_1 = new Character() {
            Name = "Enemy Character 1",
            MaxHealth = 150,
            CurrentHealth = 150,
            Power = 50,
            Defense = 75,
            MaxStamina = 150,
            CurrentStamina = 150,
            MagicPower = 10,
            MagicResistance = 10,
            MaxMana = 15,
            CurrentMana = 15,
            Skill = 3,
            Luck = 5,
            Speed = 35,
            Weapon = new Weapon() {
                Name = "Dagger",
                Weight = 1,
                PrimaryDamaga = 5,
                SecondaryDamage = 0,
                Skill = 2,
                Range = 1
            }
        };
        Character enemyCharacter_2 = new Character() {
            Name = "Enemy Character 2",
            MaxHealth = 100,
            CurrentHealth = 100,
            Power = 30,
            Defense = 40,
            MaxStamina = 200,
            CurrentStamina = 200,
            MagicPower = 20,
            MagicResistance = 40,
            MaxMana = 100,
            CurrentMana = 100,
            Skill = 5,
            Luck = 15,
            Speed = 45,
            Weapon = new Weapon() {
                Name = "Dagger",
                Weight = 1,
                PrimaryDamaga = 5,
                SecondaryDamage = 0,
                Skill = 2,
                Range = 1
            }
        };
        Character enemyCharacter_3 = new Character() {
            Name = "Enemy Character 3",
            MaxHealth = 300,
            CurrentHealth = 300,
            Power = 100,
            Defense = 50,
            MaxStamina = 100,
            CurrentStamina = 100,
            MagicPower = 5,
            MagicResistance = 50,
            MaxMana = 50,
            CurrentMana = 50,
            Skill = 7,
            Luck = 8,
            Speed = 5,
            Weapon = new Weapon() {
                Name = "Dagger",
                Weight = 1,
                PrimaryDamaga = 5,
                SecondaryDamage = 0,
                Skill = 2,
                Range = 1
            }
        };

        enemyCharacter_1.ActionList.Add(strike);
        enemyCharacter_1.ActionList.Add(hack);
        enemyCharacter_1.ActionList.Add(slash);
        enemyCharacter_2.ActionList.Add(strike);
        enemyCharacter_2.ActionList.Add(hack);
        enemyCharacter_2.ActionList.Add(slash);
        enemyCharacter_3.ActionList.Add(strike);
        enemyCharacter_3.ActionList.Add(hack);
        enemyCharacter_3.ActionList.Add(slash);

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
    /// This function simply increases the turn number when the turn is ending. The <c>StateEndTurn</c>
    /// class calls this function when changing turns.
    /// </summary>
    public void IncreaseTurnNumber() 
    {
        _turnNumber++;
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
}
