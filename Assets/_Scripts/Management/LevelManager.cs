using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarGame.Obstacles;

namespace CarGame.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public int CurrentLevel { get; private set; }
        public int CurrentIteration { get; private set; }

        [SerializeField] private int maxIterationIndex = 8;

        [SerializeField] private List<GameObject> levelPrefabs = new();

        [SerializeField] private List<StartGoalPoint> startGoalPoints = new();
        [SerializeField] private LevelStartGoalPoints _startGoalPointClass;

        [SerializeField] private EnterExitIndex[] enterExitPoints = new EnterExitIndex[8];



        private void Awake()
        {

        }

        private void Start()
        {
            _startGoalPointClass = levelPrefabs[0].transform.GetChild(0).GetComponent<LevelStartGoalPoints>();
            for (int i = 0; i < _startGoalPointClass.startGoalPointsList.Count; i++)
            {
                startGoalPoints.Add(_startGoalPointClass.startGoalPointsList[i]);
            }
            SetStartAndExit(enterExitPoints[CurrentIteration].EnterIndex, enterExitPoints[CurrentIteration].ExitIndex);

        }
        private void SetStartAndExit(int start, int goal)
        {
            //startGoalPoints[i].startGoalPointInfo.StartOrGoal = StartOrGoal.
            for(int i = 0; i < startGoalPoints.Count; i++)
            {
                if(i == start) startGoalPoints[i].SetAsStartOrGoal(StartOrGoal.Start);
                else if (i == goal) startGoalPoints[i].SetAsStartOrGoal(StartOrGoal.Goal);
                else startGoalPoints[i].SetAsStartOrGoal(StartOrGoal.Obstacle);
            }

            GameManager.instance.Player.InitializePlayerForNextIteration
                (startGoalPoints[start].gameObject.transform.position, startGoalPoints[start].gameObject.transform.rotation);
        }

        public void LevelWon()
        {
            Debug.Log("level won at " + CurrentIteration + " iteration");
            if (CurrentIteration > maxIterationIndex-1)
            {
                NextLevel();
            }
            else
            {
                NextIteration();
            }
        }
        public void NextIteration()
        {
            CurrentIteration++;
            SetStartAndExit(enterExitPoints[CurrentIteration].EnterIndex, enterExitPoints[CurrentIteration].ExitIndex);
            GameManager.instance.ChangeState(GameState.GameAwaitingStart);
            //sonraki queueye geç, oyunu awaiting starta getir
        }
        public void NextLevel()
        {
            Debug.Log("next level");
            //tüm queueleri temizle, bir sonraki bölümü aç, oyunu awaiting starta getir
            GameManager.instance.ResetAllRecords();
            CurrentIteration = 0;
            GameManager.instance.ChangeState(GameState.GameAwaitingStart);
        }
        public void RestartIteration()
        {
            Debug.Log("restart iteration");
            SetStartAndExit(enterExitPoints[CurrentIteration].EnterIndex, enterExitPoints[CurrentIteration].ExitIndex);
            GameManager.instance.ChangeState(GameState.GameAwaitingStart);
        }
        public void RestartLevel()
        {
            Debug.Log("restart level");
            //tüm queueleri temizle, oyunu awaiting starta getir.
        }
    }
    [System.Serializable]
    public struct EnterExitIndex
    {
        public int EnterIndex;
        public int ExitIndex;
    }
}
