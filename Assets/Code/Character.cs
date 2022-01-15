using System.Collections.Generic;

public class Character
{
    #region properties
    private string _name;
    private int _level;
    private int _maxHealth;
    private int _currentHealth;
    private int _power;
    private int _defense;
    private int _maxStamina;
    private int _currentStamina;
    private int _magicPower;
    private int _magicResistance; 
    private int _maxMana;
    private int _currentMana;
    private int _speed;
    private int _skill;
    private int _luck;
    private Weapon _weapon;
    private bool _isAlive;
    private bool _isDodging;
    private bool _isBlocking;
    private bool _isCountering;

    private List<CharacterAction> _actionList;

    public string Name { get => _name; set => _name = value; }
    public int Level { get => _level; set => _level = value; }
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public int Power { get => _power; set => _power = value; }
    public int Defense { get => _defense; set => _defense = value; }
    public int MaxStamina { get => _maxStamina; set => _maxStamina = value; }
    public int CurrentStamina { get => _currentStamina; set => _currentStamina = value; }
    public int MagicPower { get => _magicPower; set => _magicPower = value; }
    public int MagicResistance { get => _magicResistance; set => _magicResistance = value; }
    public int MaxMana { get => _maxMana; set => _maxMana = value; }
    public int CurrentMana { get => _currentMana; set => _currentMana = value; }
    public int Speed { get => _speed; set => _speed = value; }
    public int Skill { get => _skill; set => _skill = value; }
    public int Luck { get => _luck; set => _luck = value; }
    public Weapon Weapon { get => _weapon; set => _weapon = value; }
    public bool IsAlive 
    {
        get 
        {
            if (CurrentHealth <= 0) 
            {
                return false;
            } 
            else 
            {
                return true;
            }
        }
    }
    public bool IsDodging { get => _isDodging; set => _isDodging = value; }
    public bool IsBlocking { get => _isBlocking; set => _isBlocking = value; }
    public bool IsCountering { get => _isCountering; set => _isCountering = value; }
    public List<CharacterAction> ActionList { get => _actionList; set => _actionList = value; }

    #endregion

    public Character() 
    {
        _name = "default";
        _level = 1;
        _maxHealth = 10;
        _currentHealth = 10;
        _power = 10;
        _defense = 10;
        _maxStamina = 10;
        _currentStamina = 10;
        _magicPower = 10;
        _magicResistance = 10;
        _maxMana = 10;
        _currentMana = 10;
        _speed = 10;
        _skill = 10;
        _luck = 10;
        _weapon = null;
        _isBlocking = false;
        _isCountering = false;
        _isDodging = false;
        _actionList = new List<CharacterAction>();
    }

    public Character(Character c) 
    {

    }

    public Character(string name,int level, int maxHealth, int currHealth, int power, int defense, int stamina, int magicPWR, int magicRES, int mana, int speed, int skill, int luck) 
    {
        _name = name;
        _level = level;
        _maxHealth = maxHealth;
        _currentHealth = currHealth;
        _power = power;
        _defense = defense;
        _maxStamina = stamina;
        _currentStamina = stamina;
        _magicPower = magicPWR;
        _magicResistance = magicRES;
        _maxMana = mana;
        _currentMana = mana;
        _speed = speed;
        _skill = skill;
        _luck = luck;
        _weapon = null;
        _isBlocking = false;
        _isCountering = false;
        _isDodging = false;
        _actionList = new List<CharacterAction>();
    }
}
