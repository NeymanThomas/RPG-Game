public class Weapon
{
    private string _name;
    private int _weight;
    private int _primaryDamage;
    private int _secondaryDamage;
    private int _skill;
    private int _range;

    public string Name { get => _name; set => _name = value; }
    public int Weight { get => _weight; set => _weight = value; }
    public int PrimaryDamage { get => _primaryDamage; set => _primaryDamage = value; }
    public int SecondaryDamage { get => _secondaryDamage; set => _secondaryDamage = value; }
    public int Skill { get => _skill; set => _skill = value; }
    public int Range { get => _range; set => _range = value; }

    public Weapon (string name, int weight, int primDamage, int secDamage, int skill, int range) 
    {
        _name = name;
        _weight = weight;
        _primaryDamage = primDamage;
        _secondaryDamage = secDamage;
        _skill = skill;
        _range = range;
    }

    public Weapon () 
    {
        _name = "default";
        _weight = 10;
        _primaryDamage = 10;
        _secondaryDamage = 10;
        _skill = 10;
        _range = 10;
    }
}
