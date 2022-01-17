public class Slash : CharacterAction
{
    public override string Name => "Slash";
    public override string Description => throw new System.NotImplementedException();
    public override int Power => 55;
    public override int Accuracy => 100;

    public override void Action(Character actor, Character target)
    {
        throw new System.NotImplementedException();
    }
}
