public abstract class CharacterAction
{
    private string _name;
    private string _description;
    private int _power;
    private int _accuracy;
    private int _critModifier;
    private int _energyCost;

    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract int Power { get; }
    public abstract int Accuracy { get; }
    public abstract int CritModifier { get; }
    public abstract int EnergyCost { get; }

    public abstract void Action(Character actor, Character target);
}
