using System.Collections.Generic;

public class Slash : CharacterAction
{
    public override string Name => "Slash";
    public override string Description => "The user slashes skillfully at the enemy with their weapon. Higher chance to land a critical hit.";
    public override int Level => 2;
    public override int SkillLevel => 10;
    public override int WeaponLevel  => 1;
    public override List<CharacterRole> RoleRequirements => new List<CharacterRole>() 
    {
        CharacterRole.ANY
    };
    public override int Power => 55;
    public override int Accuracy => 100;
    public override int CritModifier => 10;
    public override int EnergyCost => 30;

    public override void Action(Character actor, Character target)
    {
        if(actor.CurrentStamina >= EnergyCost) 
        {
            DamageCalculator.DealPhysicalDamage(actor, target, Power, Accuracy, CritModifier);
            actor.CurrentStamina -= EnergyCost;
        }
    }
}
