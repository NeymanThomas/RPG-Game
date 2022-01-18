using System.Collections.Generic;

public abstract class CharacterAction
{
    private string _name;
    private string _description;
    private int _level;
    private int _skillLevel;
    private int _weaponLevel;
    private List<CharacterRole> _roleRequirements;
    private int _power;
    private int _accuracy;
    private int _critModifier;
    private int _energyCost;

    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract int Level { get; }
    public abstract int SkillLevel { get; }
    public abstract int WeaponLevel { get; }
    public abstract List<CharacterRole>  RoleRequirements { get; }
    public abstract int Power { get; }
    public abstract int Accuracy { get; }
    public abstract int CritModifier { get; }
    public abstract int EnergyCost { get; }

    public abstract void Action(Character actor, Character target);
}
