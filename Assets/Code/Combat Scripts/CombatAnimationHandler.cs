using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator AttackAnimator;
    [SerializeField] private Animator CharacterAnimator;

    public void Init() 
    {
        AttackAnimator.Play("NoCombat_Anim");
    }

    public void StopAttackAnimation()
    {
        AttackAnimator.Play("NoCombat_Anim");
    }

    public void StartAttackAnimation(string anim) 
    {
        anim += "_Anim";
        float waitTime = 0;
        foreach(AnimationClip c in AttackAnimator.runtimeAnimatorController.animationClips) 
        {
            if (anim == c.name)
            {
                waitTime = c.length;
            }
        }
        StartCoroutine(PlayAttackAnimation(waitTime, anim));
    }

    private IEnumerator PlayAttackAnimation(float wait, string anim)
    {
        AttackAnimator.Play(anim);
        yield return new WaitForSeconds(wait + 0.1f);
        StopAttackAnimation();
    }
}
