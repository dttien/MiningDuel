﻿using MD.Diggable.Projectile;
using MD.UI;
using Mirror;
using UnityEngine;

namespace MD.Character
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField]
        private NetworkAnimator networkAnimator = null;

        private Animator animator;

        private float lastX, lastY;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            if (!networkAnimator.isLocalPlayer) return;
            var eventManager = EventSystems.EventManager.Instance;
            eventManager.StartListening<JoystickDragData>(SetMovementState);
            eventManager.StartListening<DigInvokeData>(InvokeDig);
            eventManager.StartListening<ProjectileObtainData>(SetHoldState);
            eventManager.StartListening<ThrowInvokeData>(RevertToIdleState);
        }

        private void OnDestroy()
        {
            if (!networkAnimator.isLocalPlayer) return;

            var eventManager = EventSystems.EventManager.Instance;
            eventManager.StopListening<JoystickDragData>(SetMovementState);
            eventManager.StopListening<DigInvokeData>(InvokeDig);
            eventManager.StopListening<ProjectileObtainData>(SetHoldState);
            eventManager.StopListening<ThrowInvokeData>(RevertToIdleState);
        }

        private void RevertToIdleState(ThrowInvokeData obj)
        {
            animator.SetBool(AnimatorConstants.IS_HOLDING, false);
        }

        private void SetHoldState(ProjectileObtainData obj)
        {
            animator.SetBool(AnimatorConstants.IS_HOLDING, true);
        }

        private void InvokeDig(DigInvokeData obj)
        {
            //animator.SetTrigger(AnimatorConstants.INVOKE_DIG);
            networkAnimator.SetTrigger(AnimatorConstants.INVOKE_DIG);
        }

        private void SetMovementState(JoystickDragData dragData)
        {
            var speed = dragData.InputDirection.sqrMagnitude;
            animator.SetFloat(AnimatorConstants.HORIZONTAL, dragData.InputDirection.x);
            animator.SetFloat(AnimatorConstants.VERTICAL, dragData.InputDirection.y);
            animator.SetFloat(AnimatorConstants.SPEED, speed);

            if (speed.IsEqual(0f))
            {
                PlayIdle();
                return;
            }

            BindLastMoveStats(dragData.InputDirection.x, dragData.InputDirection.y);
        }

        private void PlayIdle()
        {
            animator.SetFloat(AnimatorConstants.LAST_X, lastX);
            animator.SetFloat(AnimatorConstants.LAST_Y, lastY);
        }

        private void BindLastMoveStats(float lastX, float lastY)
        {
            this.lastX = lastX;
            this.lastY = lastY;
        }        
    }
}