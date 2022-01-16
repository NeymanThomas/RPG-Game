public class Strike : CharacterAction
{
    public override string Name => "Strike";

    public override string Description => "The user strikes with their weapon dealing physical damage. The most basic of attacks.";

    public override void Action(Character actor, Character target)
    {
        int actorDamage = ((actor.CurrentHealth / actor.MaxHealth) * ((actor.Level + 5) / 2)) + 
        ((actor.CurrentStamina / actor.MaxStamina) * ((actor.Level + 5) / 2)) +
        (actor.Skill / 10) + Luck.GetLuckDamage(actor.Luck) + (actor.Weapon.PrimaryDamaga + actor.Weapon.SecondaryDamage) +
        AbilityEffect();

        if (Luck.GetCrit(actor)) 
        {
            actorDamage = actorDamage * 2;
        }

        int targetResistance = ((target.CurrentHealth / target.MaxHealth) * ((target.Level + 10) / 2)) +
        ((target.CurrentStamina / target.MaxStamina) * ((target.Level + 10) / 2)) +
        ((target.Skill / 10) + 5) + Luck.GetLuckResistance(target.Luck);

        int outcome = actorDamage + actor.Power * (100 / (100 + targetResistance + target.Defense));

        if (outcome > 0) 
        {
            target.CurrentHealth -= outcome;
        }
    }

    // There is none for this action
    private int AbilityEffect() 
    {
        return 0;
    }
}
