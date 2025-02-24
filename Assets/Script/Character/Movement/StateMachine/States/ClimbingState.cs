using Script.Character.Movement.StateMachine;
using UnityEngine;

namespace Script.Character.Movement
{
    public class ClimbingState : IState
    {
        private PlayerController player;
        private Animator animator;

        public ClimbingState(PlayerController player, Animator animator)
        {
            this.player = player;
            this.animator = animator;
        }

        public void Enter()
        {
            // code that runs when we first enter the state
            //animator.SetBool("Climbing", true);
        }

        public void Execute()
        {
            
            if (player.CharController.velocity.y < Mathf.Epsilon && player.CharController.velocity.y > -Mathf.Epsilon)
            {
                //not moving while climbing
                player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.idleState);
                
            }
        }

        public void Exit()
        {
            // code that runs when we exit the state
        }
    }
}