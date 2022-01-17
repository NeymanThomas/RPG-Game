public class Hack : CharacterAction
{
    public override string Name => "Hack";
    public override string Description => throw new System.NotImplementedException();
    public override int Power => 75;
    public override int Accuracy => 80;

    public override void Action(Character actor, Character target)
    {
        throw new System.NotImplementedException();
    }
}
