﻿using System.Collections.Generic;
using Animators;
using Characters;
using UnityEngine;

namespace Animations {
    public class AnimationStateBase : StateMachineBehaviour {
        private IAnimatorFacade animatorFacade;
        private IAnimatorStateFacade animatorStateFacade;
        [SerializeField] private List<AnimationStateData> animationStatesDatas = new List<AnimationStateData>();


        public IAnimatorFacade getAnimatorFacade(Animator animator) {
            return animatorFacade ?? (animatorFacade = animator.GetComponentInParent<ICharacter>().getAnimatorFacade());
        }

        public IAnimatorStateFacade getAnimatorStateFacade(Animator animator) {
            return animatorStateFacade ?? (animatorStateFacade = animator.GetComponent<IAnimatorStateFacade>());
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            enterAll(animator);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            updateAll(animator);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            exitAll(animator);
        }

        public void updateAll(Animator animator) {
            animationStatesDatas.ForEach(state => state.update(this, animator));
        }

        public void enterAll(Animator animator) {
            animationStatesDatas.ForEach(state => state.enter(this, animator));
        }

        public void exitAll(Animator animator) {
            animationStatesDatas.ForEach(state => state.exit(this, animator));
        }
    }
}