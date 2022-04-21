using System.Collections.Generic;
using UnityEngine;

namespace CarGame.Obstacles
{
    public class LevelStartGoalPoints : MonoBehaviour
    {
        public List<StartGoalPoint> startGoalPointsList = new();
        public EnterExitIndex[] enterExitPoints = new EnterExitIndex[8];
    }
}
