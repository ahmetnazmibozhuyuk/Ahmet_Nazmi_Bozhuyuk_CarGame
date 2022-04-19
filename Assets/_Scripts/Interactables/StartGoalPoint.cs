using UnityEngine;
using CarGame.Managers;

namespace CarGame.Obstacles
{
    public class StartGoalPoint : MonoBehaviour,ICrash
    {
        public StartGoalPointInfo startGoalPointInfo;

        private Material _pointMat;
        private Collider _collider;

        private void Awake()
        {
            _pointMat = GetComponent<Renderer>().material;
            _collider = GetComponent<Collider>();
            startGoalPointInfo.StartGoalPointTransform = transform;

            switch (startGoalPointInfo.StartOrGoal)
            {
                case StartOrGoal.Obstacle:
                    _pointMat.color = Color.red;
                    break;
                case StartOrGoal.Start:
                    _pointMat.color = Color.blue;
                    _collider.isTrigger = true;
                    break;
                case StartOrGoal.Goal:
                    _pointMat.color = Color.green;
                    break;
            }
        }
        public void SetAsStartOrGoal(StartOrGoal startOrGoal)
        {
            startGoalPointInfo.StartOrGoal = startOrGoal;
            switch (startGoalPointInfo.StartOrGoal)
            {
                case StartOrGoal.Obstacle:
                    _pointMat.color = Color.red;
                    break;
                case StartOrGoal.Start:
                    _pointMat.color = Color.blue;
                    _collider.isTrigger = true;
                    break;
                case StartOrGoal.Goal:
                    _pointMat.color = Color.green;
                    break;
            }
        }
        public void Crash()
        {
            switch (startGoalPointInfo.StartOrGoal)
            {
                case StartOrGoal.Obstacle:
                    Debug.Log("HIT THE WRONG POINT");
                    GameManager.instance.ChangeState(GameState.GameLost);
                    break;
                case StartOrGoal.Start:
                    Debug.Log("Player is at the start point.");
                    break;
                case StartOrGoal.Goal:
                    Debug.Log("HIT THE GOAL POINT");
                    GameManager.instance.ChangeState(GameState.GameWon);
                    break;
            }
        }
    }

    [System.Serializable]
    public struct StartGoalPointInfo
    {
        public Transform StartGoalPointTransform;
        public StartOrGoal StartOrGoal;
        public StartingDirection StartingDirection;
    }
    public enum StartOrGoal { Obstacle = 0, Start = 1, Goal = 2}
    public enum StartingDirection { North = 0, West = 1, South = 2, East = 3 }
}
