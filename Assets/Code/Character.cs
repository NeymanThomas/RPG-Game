
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
    #endregion

    public string Name 
    {
        get 
        {
            return _name;
        }
        set 
        {
            _name = value;
        }
    }

    public Character() 
    {
        _name = "default";
        _maxHealth = 10;
        _currentHealth = 10;
        _power = 10;
        _defense = 10;
        _stamina = 10;
        _magicPower = 10;
        _magicResistance = 10;
        _mana = 10;
        _speed = 10;
        _skill = 10;
        _luck = 10;
        _weapon = null;
    }

    public Character(string name, int maxHealth, int currHealth, int power, int defense, int stamina, int magicPWR, int magicRES, int mana, int speed, int skill, int luck) 
    {
        _name = name;
        _maxHealth = maxHealth;
        _currentHealth = currHealth;
        _power = power;
        _defense = defense;
        _stamina = stamina;
        _magicPower = magicPWR;
        _magicResistance = magicRES;
        _mana = mana;
        _speed = speed;
        _skill = skill;
        _luck = luck;
        _weapon = null;
    }
}
