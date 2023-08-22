using System;
using Enemy.States;
using UnityEngine;

namespace Enemy
{
    public class StateMachine : MonoBehaviour
    {
        public  BaseState ActiveState;
        public void Initialize()
        {
            ChangeState(new PatrolState());
        }

        private void Update()
        {
            if (ActiveState != null)
            {
                ActiveState.Perform();
            }
        }

        public void ChangeState(BaseState newState)
        {
            if (ActiveState != null)
            {
                ActiveState.Exit(); // run cleanup on Active State  
            }

            ActiveState = newState; // change to a new state

            if (ActiveState != null)
            {
                ActiveState.StateMachine = this; // setup new state
                ActiveState.Enemy = GetComponent<Enemy>();
                ActiveState.Enter(); // assign state enemy class
            }
        }
    }
}