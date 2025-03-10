using System;
using UnityEngine;

namespace Script.Character.Movement.StateMachine
{
    [Serializable]
    public class PlayerStateMachine
    {
        public IState CurrentState { get; private set; }

        // reference to the state objects
        public MovingState movingState;
        public ClimbingState climbingState;
        public IdleState idleState;
        //public FallingState fallingState; //maybe we doesn't need it
        
        // event to notify other objects of the state change
        public event Action<IState> stateChanged;
        
        // pass in necessary parameters into constructor 
        public PlayerStateMachine(PlayerController player, Animator animator)
        {
            // create an instance for each state and pass in PlayerController
            this.movingState = new MovingState(player, animator);
            this.climbingState = new ClimbingState(player, animator);
            this.idleState = new IdleState(player, animator);
            //this.fallingState = new FallingState(player); //maybe we doesn't need it
        }

        // set the starting state
        public void Initialize(IState state)
        {
            CurrentState = state;
            state.Enter();

            // notify other objects that state has changed
            stateChanged?.Invoke(state);
        }

        // exit this state and enter another
        public void TransitionTo(IState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter();

            // notify other objects that state has changed
            stateChanged?.Invoke(nextState);
        }

        // allow the StateMachine to update this state
        public void Execute()
        {
            if (CurrentState != null)
            {
                CurrentState.Execute();
            }
        }
    }
}