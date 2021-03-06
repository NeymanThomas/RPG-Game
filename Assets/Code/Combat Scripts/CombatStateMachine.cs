using System.Collections.Generic;
using UnityEngine;
using System.Collections;

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
    [SerializeField] private CombatAnimationHandler animationHandler;

    // Properties that keep track of character information
    private List<Character> _playerTeam, _enemyTeam, _turnOrder, _targetList;
    private Character _currentCharacter;
    private int _currentCharacterActionIndex;
    private int _turnNumber;

    // Properties for the combat text
    private Queue<string> combatTextQueue;
    private CombatTextState textState;

    // Public Properties
    public static CombatStateMachine Instance => _instance;
    public StateEndTurn sEndTurn => _sEndTurn;
    public StateAttack sAttack => _sAttack;
    public StateDodge sDodge => _sDodge;
    public StateBlock sBlock => _sBlock;

    public CombatUIHandler UIHandler => uiHandler;
    public CombatAnimationHandler AnimationHandler => animationHandler;

    public List<Character> PlayerTeam => _playerTeam;
    public List<Character> EnemyTeam => _enemyTeam;
    public List<Character> TurnOrder => _turnOrder;
    public List<Character> TargetList => _targetList;
    public Character CurrentCharacter => _currentCharacter;
    public int CurrentCharacterActionIndex { get => _currentCharacterActionIndex; set => _currentCharacterActionIndex = value; }
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
        combatTextQueue = new Queue<string>();
        uiHandler.Init();
        animationHandler.Init();
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
            StaminaRegen = 30,
            MagicPower = 10,
            MagicResistance = 15,
            MaxMana = 50,
            CurrentMana = 50,
            ManaRegen = 30,
            Skill = 5,
            Luck = 10,
            Speed = 40,
            Weapon = new Weapon() {
                Name = "Basic Sword",
                Weight = 5,
                PrimaryDamage = 10,
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
            StaminaRegen = 30,
            MagicPower = 10,
            MagicResistance = 30,
            MaxMana = 50,
            CurrentMana = 50,
            ManaRegen = 30,
            Skill = 5,
            Luck = 10,
            Speed = 10,
            Weapon = new Weapon() {
                Name = "Battle Axe",
                Weight = 10,
                PrimaryDamage = 20,
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
            StaminaRegen = 30,
            MagicPower = 50,
            MagicResistance = 75,
            MaxMana = 200,
            CurrentMana = 200,
            ManaRegen = 30,
            Skill = 10,
            Luck = 20,
            Speed = 30,
            Weapon = new Weapon() {
                Name = "Spell Book",
                Weight = 1,
                PrimaryDamage = 20,
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
        playerCharacter_2.ActionList.Add(strike);
        playerCharacter_2.ActionList.Add(strike);
        playerCharacter_3.ActionList.Add(hack);
        playerCharacter_3.ActionList.Add(hack);
        playerCharacter_3.ActionList.Add(hack);

        Character enemyCharacter_1 = new Character() {
            Name = "Enemy Character 1",
            MaxHealth = 150,
            CurrentHealth = 150,
            Power = 50,
            Defense = 75,
            MaxStamina = 150,
            CurrentStamina = 150,
            StaminaRegen = 30,
            MagicPower = 10,
            MagicResistance = 10,
            MaxMana = 15,
            CurrentMana = 15,
            ManaRegen = 30,
            Skill = 3,
            Luck = 5,
            Speed = 35,
            Weapon = new Weapon() {
                Name = "Dagger",
                Weight = 1,
                PrimaryDamage = 5,
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
            StaminaRegen = 30,
            MagicPower = 20,
            MagicResistance = 40,
            MaxMana = 100,
            CurrentMana = 100,
            ManaRegen = 30,
            Skill = 5,
            Luck = 15,
            Speed = 45,
            Weapon = new Weapon() {
                Name = "Dagger",
                Weight = 1,
                PrimaryDamage = 5,
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
            StaminaRegen = 30,
            MagicPower = 5,
            MagicResistance = 50,
            MaxMana = 50,
            CurrentMana = 50,
            ManaRegen = 30,
            Skill = 7,
            Luck = 8,
            Speed = 5,
            Weapon = new Weapon() {
                Name = "Dagger",
                Weight = 1,
                PrimaryDamage = 5,
                SecondaryDamage = 0,
                Skill = 2,
                Range = 1
            }
        };

        enemyCharacter_1.ActionList.Add(slash);
        enemyCharacter_1.ActionList.Add(slash);
        enemyCharacter_1.ActionList.Add(slash);
        enemyCharacter_2.ActionList.Add(strike);
        enemyCharacter_2.ActionList.Add(strike);
        enemyCharacter_2.ActionList.Add(slash);
        enemyCharacter_3.ActionList.Add(hack);
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
    /// For now the enemy is simply choosing a random target and random action
    /// </summary>
    public void EnemyDecision() 
    {
        var decision = EnemyCombatDecisionHandler.CreateRandomDecision(_currentCharacter, _playerTeam);
        _currentCharacterActionIndex = decision.action;
        _targetList.Add(_playerTeam[decision.target]);
    }

    public void AttemptToFlee() 
    {
        // Implement method to determine whether or not you can flee from battle
        // if it's an improtant battle, you won't be able to flee no matter what
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
    /// This function is called by the <c>StateEndTurn</c> class and checks to see if any character has
    /// died after a turn has ended. It will then remove the dead character from the turnOrder list as
    /// well as either the enemy or player team.
    /// </summary>
    public void CheckForCharacterDeath()
    {
        // Unfortunately, can't use a foreach in this situation because if the loop
        // Gets modified when removing a character, the loop breaks
        for (int i = 0; i < _turnOrder.Count; i++) 
        {
            if (!(_turnOrder[i].IsAlive)) 
            {
                AddCombatText($"{ _turnOrder[i].Name } has died!");
                _turnOrder.Remove(_turnOrder[i]);
            }
        }
    }

    /// <summary>
    /// Checks the currently assigned action index of the current character to see if the character has enough
    /// energy to execute the action at the desired index. 
    /// </summary>
    /// <returns>true if the character has enough energy to execute their action, false otherwise</returns>
    public bool CheckForEnoughEnergy() 
    {
        if (_currentCharacter.ActionList[_currentCharacterActionIndex].EnergyType == CharacterAction.ActionEnergyType.Stamina) 
        {
            if (_currentCharacter.ActionList[_currentCharacterActionIndex].EnergyCost > _currentCharacter.CurrentStamina) 
            {
                return false;
            }
            return true;
        }
        else if (_currentCharacter.ActionList[_currentCharacterActionIndex].EnergyType == CharacterAction.ActionEnergyType.Mana) 
        {
            if (_currentCharacter.ActionList[_currentCharacterActionIndex].EnergyCost > _currentCharacter.CurrentMana) 
            {
                return false;
            }
            return true;
        }
        else 
        {
            return true;
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

    #region CombatTextFunctions

    /// <summary>
    /// This function is called when some sort of message from combat is added. 
    /// The text is simply enqueued to the queue.
    /// </summary>
    /// <param name="text">The text message added to the Queue</param>
    public void AddCombatText(string text) 
    {
        combatTextQueue.Enqueue(text);
    }

    /// <summary>
    /// Stops all running coroutines in order to make sure text will not overlap when being output.
    /// The function then Dequeues the next item in the combatTextQueue and sends the string to
    /// the uiHandler to print the string to the UI.
    /// </summary>
    public void StartCombatText() 
    {
        StopAllCoroutines();
        if (combatTextQueue.Count > 0) 
        {
            string text = combatTextQueue.Dequeue();
            StartCoroutine(uiHandler.PrintCombatText(text));
        }
    }

    /// <summary>
    /// Changes the textState manually
    /// </summary>
    public void ModifyTextState(CombatTextState state) 
    {
        textState = state;
    }

    /// <summary>
    /// Executes the next state of combat depending on what state of combat text was executed last.
    /// </summary>
    public void AdvanceCombat() 
    {
        if (combatTextQueue.Count > 1) 
        {
            StartCombatText();
            return;
        }

        switch(textState) 
        {
            case CombatTextState.PlayerDecision:
                textState = CombatTextState.AttackMessage;
                sAttack.CreateAttackMessage();
                break;
            case CombatTextState.EnemyDecision:
                textState = CombatTextState.AttackMessage;
                EnemyDecision();
                sAttack.CreateAttackMessage();
                break;
            case CombatTextState.AttackMessage:
                textState = CombatTextState.Attacking;
                sAttack.Start_StateAttack();
                break;
            case CombatTextState.Attacking:
                textState = CombatTextState.Ending;
                sEndTurn.Start_StateEndTurn();
                break;
            case CombatTextState.Dodging:
                sDodge.Start_StateDodge();
                break;
            case CombatTextState.Blocking:
                sBlock.Start_StateBlock();
                break;
            case CombatTextState.Countering:
                break;
            case CombatTextState.Ending:
                EndTurn();
                break;
            case CombatTextState.NotEnoughEnergy:
                textState = CombatTextState.PlayerDecision;
                AddCombatText($"It is { _currentCharacter.Name }'s turn!");
                StartCombatText();
                uiHandler.OnAttack();
                break;
        }
    }

    public void EndTurn() 
    {
        StartCombatText();
        if (combatTextQueue.Count > 0) 
        {
            return;
        }

        if (_enemyTeam.Contains(_currentCharacter)) 
        {
            textState = CombatTextState.EnemyDecision;
        }
        else 
        {
            textState = CombatTextState.PlayerDecision;
            uiHandler.SetupPlayerDecision();
        }
    }

    #endregion
}

public enum CombatTextState 
{
    PlayerDecision,
    EnemyDecision,
    AttackMessage,
    Attacking,
    Blocking,
    Dodging,
    Countering,
    Ending, 
    NotEnoughEnergy
}