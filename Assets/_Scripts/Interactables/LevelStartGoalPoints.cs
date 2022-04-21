using CarGame.Obstacles;
using System.Collections.Generic;
using UnityEngine;
namespace CarGame
{
    public class LevelStartGoalPoints : MonoBehaviour
    {
        public List<StartGoalPoint> startGoalPointsList = new();
        public EnterExitIndex[] enterExitPoints = new EnterExitIndex[8];
    }
}
