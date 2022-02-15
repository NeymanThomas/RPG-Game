using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private string currentAnimation;

    public void Init() 
    {
        animator.Play("NoCombat_Anim");
    }

    public void StartAttackAnimation() 
    {
        currentAnimation = CombatStateMachine.Instance.CurrentCharacter.ActionList[CombatStateMachine.Instance.CurrentCharacterActionIndex].Name + "_Anim";
        animator.SetBool("Mirror", true);
        animator.Play(currentAnimation);
        //animator.Play("NoCombat_Anim");
    }

    public void ChangeAnimationState(string newAnimation) 
    {
        // Stop the animation from interupting itself
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }
}
