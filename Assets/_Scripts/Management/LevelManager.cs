using System.Collections.Generic;
using UnityEngine;
using CarGame.Obstacles;

namespace CarGame.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public int CurrentLevel { get; private set; }
        public int CurrentIteration { get; private set; }

        [SerializeField] private List<GameObject> levelPrefabs = new();

        private List<StartGoalPoint> _startGoalPoints = new();
        private LevelStartGoalPoints _startGoalPointClass;

        private GameObject instantiatedLevel;

        public void StartLevel(int levelIndex)
        {
            if (instantiatedLevel != null) Destroy(instantiatedLevel);
            instantiatedLevel = Instantiate(levelPrefabs[levelIndex], new Vector3(0, 0, 0), transform.rotation);
            _startGoalPointClass = instantiatedLevel.transform.GetChild(0).GetComponent<LevelStartGoalPoints>();
            for (int i = 0; i < _startGoalPointClass.startGoalPointsList.Count; i++)
            {
                _startGoalPoints.Add(_startGoalPointClass.startGoalPointsList[i]);
            }
            SetStartAndExit(_startGoalPointClass.enterExitPoints[CurrentIteration].EnterIndex,
                _startGoalPointClass.enterExitPoints[CurrentIteration].ExitIndex);
        }
        private void SetStartAndExit(int start, int goal)
        {
            for(int i = 0; i < _startGoalPoints.Count; i++)
            {
                if(i == start) _startGoalPoints[i].SetAsStartOrGoal(StartOrGoal.Start);
                else if (i == goal) _startGoalPoints[i].SetAsStartOrGoal(StartOrGoal.Goal);
                else _startGoalPoints[i].SetAsStartOrGoal(StartOrGoal.Obstacle);
            }

            GameManager.instance.Player.InitializePlayerForNextIteration
                (_startGoalPoints[start].gameObject.transform.position, _startGoalPoints[start].gameObject.transform.rotation);
        }
        public void NextIteration()
        {
            CurrentIteration++;
            SetStartAndExit(_startGoalPointClass.enterExitPoints[CurrentIteration].EnterIndex, _startGoalPointClass.enterExitPoints[CurrentIteration].ExitIndex);
            GameManager.instance.ChangeState(GameState.GameAwaitingStart);
        }
        public void NextLevel()
        {
            if (CurrentLevel < levelPrefabs.Count-1)
            {
                CurrentIteration = 0;
                CurrentLevel++;
                _startGoalPoints.Clear();
                StartLevel(CurrentLevel);
            }
            else
            {
                Debug.Log("YOU WON");
            }
        }
        public void RestartIteration()
        {
            SetStartAndExit(_startGoalPointClass.enterExitPoints[CurrentIteration].EnterIndex,
                _startGoalPointClass.enterExitPoints[CurrentIteration].ExitIndex);
            GameManager.instance.ChangeState(GameState.GameAwaitingStart);
        }
    }
}
namespace CarGame
{
    [System.Serializable]
    public struct EnterExitIndex
    {
        public int EnterIndex;
        public int ExitIndex;
    }
}
