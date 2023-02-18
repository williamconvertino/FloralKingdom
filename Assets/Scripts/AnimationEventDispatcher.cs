using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityAnimationEvent : UnityEvent<string>{};
[RequireComponent(typeof(Animator))]

//Code sourced from https://gamedev.stackexchange.com/questions/117423/unity-detect-animations-end
public class AnimationEventDispatcher : MonoBehaviour
{
    public UnityAnimationEvent OnAnimationStart { set; get; }
    public UnityAnimationEvent OnAnimationComplete { set; get; }
    
    private Animator _animator;
    
    void Awake()
    {
        _animator = GetComponent<Animator>();

        OnAnimationStart = new UnityAnimationEvent();
        OnAnimationComplete = new UnityAnimationEvent();
        
        for(int i=0; i<_animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            AnimationClip clip = _animator.runtimeAnimatorController.animationClips[i];
            
            AnimationEvent animationStartEvent = new AnimationEvent();
            animationStartEvent.time = 0;
            animationStartEvent.functionName = "AnimationStartHandler";
            animationStartEvent.stringParameter = clip.name;
            
            AnimationEvent animationEndEvent = new AnimationEvent();
            animationEndEvent.time = clip.length;
            animationEndEvent.functionName = "AnimationCompleteHandler";
            animationEndEvent.stringParameter = clip.name;
            
            clip.AddEvent(animationStartEvent);
            clip.AddEvent(animationEndEvent);
        }
    }

    public void AnimationStartHandler(string animationName)
    {
        OnAnimationStart?.Invoke(animationName);
    }
    public void AnimationCompleteHandler(string animationName)
    {
        OnAnimationComplete?.Invoke(animationName);
    }
}