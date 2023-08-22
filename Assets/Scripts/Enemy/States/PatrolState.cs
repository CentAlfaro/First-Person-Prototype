using UnityEngine;

namespace Enemy.States
{
    public class PatrolState : BaseState
    {
        private int _waypointIndex; // tracks which waypoint we are currently targeting
        private float _waitTimer;
        public override void Enter()
        {
        }

        public override void Perform()
        {
            PatrolCycle();
            if (Enemy.CanSeePlayer())
            {
                StateMachine.ChangeState(new AttackState());
            }
        }

        public override void Exit()
        {
        }

        public void PatrolCycle()
        {
            if (Enemy.Agent.remainingDistance < 0.2f)
            {
                _waitTimer += Time.deltaTime;
                if (_waitTimer > 3)
                {
                    if (_waypointIndex < Enemy.path.waypoints.Count - 1)
                    {
                        _waypointIndex++;
                    }

                    else
                    {
                        _waypointIndex = 0;
                    }

                    Enemy.Agent.SetDestination(Enemy.path.waypoints[_waypointIndex].position);
                    _waitTimer = 0;
                }
            }
        }
    }
}