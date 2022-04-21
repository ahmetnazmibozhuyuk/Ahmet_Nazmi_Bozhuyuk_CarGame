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
                    _collider.isTrigger = false;
                    break;
                case StartOrGoal.Start:
                    _pointMat.color = Color.blue;
                    _collider.isTrigger = true;
                    break;
                case StartOrGoal.Goal:
                    _pointMat.color = Color.green;
                    _collider.isTrigger = false;
                    break;
            }
            switch (startGoalPointInfo.StartingDirection)
            {
                case StartingDirection.North:
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;
                case StartingDirection.West:
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    break;
                case StartingDirection.South:
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case StartingDirection.East:
                    transform.rotation = Quaternion.Euler(0, -90, 0);
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
                    _collider.isTrigger = false;
                    break;
                case StartOrGoal.Start:
                    _pointMat.color = Color.blue;
                    _collider.isTrigger = true;
                    break;
                case StartOrGoal.Goal:
                    _pointMat.color = Color.green;
                    _collider.isTrigger = false;
                    break;
            }
        }
        public void Crash()
        {
            switch (startGoalPointInfo.StartOrGoal)
            {
                case StartOrGoal.Obstacle:
                    GameManager.instance.ChangeState(GameState.GameLost);
                    break;
                case StartOrGoal.Start:
                    break;
                case StartOrGoal.Goal:
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
namespace CarGame
{
    public interface ICrash
    {
        public void Crash();
    }
}
