﻿
public class Character
{
    #region properties
    private string _name;
    private int _maxHealth;
    private int _currentHealth;
    private int _power;
    private int _defense;
    private int _stamina;
    private int _magicPower;
    private int _magicResistance; 
    private int _mana;
    private int _speed;
    private int _skill;
    private int _luck;
    private object _weapon;
    private bool _isAlive;
    private bool _isDodging;
    private bool _isBlocking;
    private bool _isCountering;

    public string Name { get => _name; set => _name = value; }
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public int Power { get => _power; set => _power = value; }
    public int Defense { get => _defense; set => _defense = value; }
    public int Stamina { get => _stamina; set => _stamina = value; }
    public int MagicPower { get => _magicPower; set => _magicPower = value; }
    public int MagicResistance { get => _magicResistance; set => _magicResistance = value; }
    public int Mana { get => _mana; set => _mana = value; }
    public int Speed { get => _speed; set => _speed = value; }
    public int Skill { get => _skill; set => _skill = value; }
    public int Luck { get => _luck; set => _luck = value; }
    public object Weapon { get => _weapon; set => _weapon = value; }
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

    #endregion

    public Character() 
    {
        _name = "default";
        MaxHealth = 10;
        CurrentHealth = 10;
        Power = 10;
        Defense = 10;
        Stamina = 10;
        MagicPower = 10;
        MagicResistance = 10;
        Mana = 10;
        Speed = 10;
        Skill = 10;
        Luck = 10;
        Weapon = null;
    }

    public Character(Character c) 
    {

    }

    public Character(string name, int maxHealth, int currHealth, int power, int defense, int stamina, int magicPWR, int magicRES, int mana, int speed, int skill, int luck) 
    {
        _name = name;
        MaxHealth = maxHealth;
        CurrentHealth = currHealth;
        Power = power;
        Defense = defense;
        Stamina = stamina;
        MagicPower = magicPWR;
        MagicResistance = magicRES;
        Mana = mana;
        Speed = speed;
        Skill = skill;
        Luck = luck;
        Weapon = null;
    }
}
