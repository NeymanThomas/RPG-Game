public abstract class CharacterAction
{
    private string _name;
    private string _description;

    public abstract string Name { get; }
    public abstract string Description { get; }

    public abstract void Action();
}
