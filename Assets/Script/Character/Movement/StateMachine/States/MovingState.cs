using Script.Character.Movement.StateMachine;
using UnityEngine;

namespace Script.Character.Movement
{
    public class MovingState : IState
    {
        private PlayerController player;
        private Animator animator;

        public MovingState(PlayerController player, Animator animator)
        {
            this.player = player;
            this.animator = animator;
        }

        public void Enter()
        {
            // code that runs when we first enter the state
            animator.SetBool("IsMoving", true);
        }

        public void Execute()
        {
            //play idleAnim
            if (player.CharController.velocity.x < Mathf.Epsilon && player.CharController.velocity.x > -Mathf.Epsilon)
            {
                //is not moving
                player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.idleState);
            }

            if (player.CharController.velocity.y > Mathf.Epsilon || player.CharController.velocity.y < -Mathf.Epsilon)
            {
                //climbing
                player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.climbingState);
            }
            
        }

        public void Exit()
        {
            // code that runs when we exit the state
        }
    }
}