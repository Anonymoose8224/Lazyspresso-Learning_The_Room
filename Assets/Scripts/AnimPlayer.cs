using UnityEngine;

public class AnimPlayer : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float animationSpeed = 1f;
    [SerializeField] private string AnimationTriggerName;
    [SerializeField] private string AnimationNbrsName;

    public void PlayAnimation(int noteIndex)
    {
        if (anim == null) return;

        anim.speed = animationSpeed;
        anim.SetInteger(AnimationNbrsName, noteIndex);
        anim.SetTrigger(AnimationTriggerName);
    }
}
