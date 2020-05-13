using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hook.NTC
{
    public class AnimationController : MonoBehaviour
    {
        #region Properties

        private Animator _animator;
        private AnimatorClipInfo[] _clips;
        
        #endregion
        
        #region MonoBehaviour

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animator.SetTrigger("dancer-pose");            
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                var clips = _animator.GetCurrentAnimatorClipInfo(0);
                Debug.Log("Number of clips: " + clips.Length);
                foreach (var clip in _animator.runtimeAnimatorController.animationClips)
                {
                    Debug.LogFormat("[ name: {0}, events: {1} ]", clip.name, clip.events.Length);
                    if (clip.events.Length > 0)
                    {
                        foreach(var clipEvent in clip.events)
                        {
                            Debug.LogFormat("\t[ param: {0}, time: {1} ]", clipEvent.stringParameter, clipEvent.time);
                        }
                    }
                }
            }
        }
        
        #endregion
        
        #region Class Methods

        public void CompletedAnimationEvent(string clipId)
        {
            
        }
        
        #endregion
    }
}