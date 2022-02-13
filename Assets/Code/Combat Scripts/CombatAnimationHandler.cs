using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private string currentState;

    public void Init() 
    {
        animator.Play("NoCombat_Anim");
    }

    public void ChangeAnimationState(string newState) 
    {
        // Stop the animation from interupting itself
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }
}
