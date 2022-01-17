public class Strike : CharacterAction
{
    public override string Name => "Strike";
    public override string Description => "The user strikes with their weapon dealing physical damage. The most basic of attacks.";
    public override int Power => 40;
    public override int Accuracy => 95;
    public override int CritModifier => 0;
    public override int EnergyCost => 10;

    public override void Action(Character actor, Character target)
    {
        if(actor.CurrentStamina >= EnergyCost) 
        {
            DamageCalculator.DealPhysicalDamage(actor, target, Power, Accuracy, CritModifier);
            actor.CurrentStamina -= EnergyCost;
        }
    }
}
