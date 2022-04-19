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
        private List<PosRot>[] _carRecords = new List<PosRot>[8];


        // burada queue oluşsun her seferinde diğer tüm araçlara gitsin diğer araçlarda uzunluk sıfırsa disable bu queue'yu doldurmayı dene değilse enable olup 
        //bu queue'yu doldurup yola çıksın

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
            startGoalPoints[start].SetAsStartOrGoal(StartOrGoal.Start);
            startGoalPoints[goal].SetAsStartOrGoal(StartOrGoal.Goal);


            GameManager.instance.Player.InitializePlayerForNextIteration
                (startGoalPoints[start].gameObject.transform.position, startGoalPoints[start].gameObject.transform.rotation);
        }

        public void LevelWon()
        {
            if (CurrentIteration > maxIterationIndex)
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
            Debug.Log("next iteration");
            CurrentIteration++;
            SetStartAndExit(enterExitPoints[CurrentIteration].EnterIndex, enterExitPoints[CurrentIteration].ExitIndex);
            GameManager.instance.ChangeState(GameState.GameAwaitingStart);
            //sonraki queueye geç, oyunu awaiting starta getir
        }
        public void NextLevel()
        {
            Debug.Log("next level");
            //tüm queueleri temizle, bir sonraki bölümü aç, oyunu awaiting starta getir
        }
        public void RestartIteration()
        {
            Debug.Log("restart iteration");
            //aktif queue temizle, oyunu awaiting starta getir.
        }
        public void RestartLevel()
        {
            Debug.Log("restart level");
            //tüm queueleri temizle, oyunu awaiting starta getir.
        }
        private void PrepareForNextIteration()
        {

        }
    }
    [System.Serializable]
    public struct EnterExitIndex
    {
        public int EnterIndex;
        public int ExitIndex;
    }
}
