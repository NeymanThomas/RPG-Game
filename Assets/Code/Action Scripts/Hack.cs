public class Hack : CharacterAction
{
    public override string Name => "Hack";
    public override string Description => "The user uses extra force attacking with their weapon. There is a chance the enemy will be stunned.";
    public override int Power => 75;
    public override int Accuracy => 80;
    public override int CritModifier => 0;
    public override int EnergyCost => 30;

    public override void Action(Character actor, Character target)
    {
        if(actor.CurrentStamina >= EnergyCost) 
        {
            DamageCalculator.DealPhysicalDamage(actor, target, Power, Accuracy, CritModifier);
            actor.CurrentStamina -= EnergyCost;

            if (Luck.GetRandomChanceResult(actor, 10)) 
            {
                target.IsStunned = true;
            }
        }
    }
}
