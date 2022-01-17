public class Strike : CharacterAction
{
    public override string Name => "Strike";

    public override string Description => "The user strikes with their weapon dealing physical damage. The most basic of attacks.";

    public override void Action(Character actor, Character target)
    {
        float actorDamage = ((actor.CurrentHealth / actor.MaxHealth) * ((actor.Level + 5.0f) / 2.0f)) + 
        ((actor.CurrentStamina / actor.MaxStamina) * ((actor.Level + 5.0f) / 2.0f)) +
        (actor.Skill / 10.0f) + Luck.GetLuckDamage(actor.Luck) + (actor.Weapon.PrimaryDamage + actor.Weapon.SecondaryDamage);

        if (Luck.GetCrit(actor)) 
        {
            actorDamage = actorDamage * 2;
        }

        float targetResistance = ((target.CurrentHealth / target.MaxHealth) * ((target.Level + 5.0f) / 2.0f)) +
        ((target.CurrentStamina / target.MaxStamina) * ((target.Level + 5.0f) / 2.0f)) +
        (target.Skill / 10.0f) + Luck.GetLuckResistance(target.Luck);

        float outcome = actorDamage + actor.Power * (100.0f / (100.0f + targetResistance + target.Defense));

        if (outcome > 0) 
        {
            target.CurrentHealth -= (int)outcome;
        }
    }
}
