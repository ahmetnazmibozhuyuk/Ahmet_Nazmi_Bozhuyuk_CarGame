using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame.Managers
{
    [RequireComponent(typeof(LevelManager),typeof(UIManager))]
    public class GameManager : Singleton<GameManager>
    {
        public GameState currentState { get; private set; }

        public Controller Player
        {
            get { return player; }
            set { player = value; }

        }
        [SerializeField] private Controller player;

        [Tooltip("Lower this value is, smoother the animations of recorded cars will be. Too low values may cause issues.")]
        [Range(0.01f,0.15f)]
        [SerializeField] private float recordSmoothness;

        private List<PosRot>[] _carRecords = new List<PosRot>[8];

        private LevelManager _levelManager;
        private UIManager _uiManager;

        [SerializeField] private GameObject[] recordedCar = new GameObject[7];

        protected override void Awake()
        {
            base.Awake();
            _levelManager = GetComponent<LevelManager>();
            _uiManager = GetComponent<UIManager>();
            for(int i = 0; i < _carRecords.Length; i++)
            {
                _carRecords[i] = new();
            }
        }

        private void Start()
        {
            ChangeState(GameState.GameAwaitingStart);
            _uiManager.SetText("Current Iteration: " + _levelManager.CurrentIteration, "Current Level: " + _levelManager.CurrentLevel);

        }

        private IEnumerator Co_RecordCar(int currentIteration)
        {
                _carRecords[currentIteration].Add(new PosRot(Player.transform.position, Player.transform.rotation));

                if (currentState == GameState.GameStarted)
                {
                    yield return new WaitForSeconds(recordSmoothness);
                    StartCoroutine(Co_RecordCar(currentIteration));
                }
                yield break;
        }
        private IEnumerator Co_ReplayCar(int carIndex, int i)
        {

            if (_carRecords[carIndex].Count == i)
            {
                recordedCar[carIndex].SetActive(false);
                yield break;
            }
            //Debug.Log(carIndex + " is running");

            recordedCar[carIndex].transform.SetPositionAndRotation(_carRecords[carIndex][i].currentPosition, _carRecords[carIndex][i].currentRotation);
            i++;
            if (currentState == GameState.GameStarted)
            {
                yield return new WaitForSeconds(recordSmoothness);
                StartCoroutine(Co_ReplayCar(carIndex,i));
            }
            yield break;
        }
        private void RecordCar()
        {

        }
        //private void ReplayCar(int carIndex)
        //{
        //    StartCoroutine(Co_ReplayCar(carIndex));
        //}

        public void ChangeState(GameState newState)
        {
            if (currentState == newState) return;

            currentState = newState;
            switch (newState)
            {
                case GameState.GameAwaitingStart:
                    GameAwaitingStartState();
                    break;
                case GameState.GameStarted:
                    GameStartedState();
                    break;
                case GameState.GameWon:
                    GameWonState();
                    break;
                case GameState.GameLost:
                    GameLostState();
                    break;
                default:
                    throw new ArgumentException("Invalid game state selection.");
            }
        }

        private void GameAwaitingStartState()
        {

        }
        private void GameStartedState()
        {
            //StartCoroutine(RecordPosRot());
            StartCoroutine(Co_RecordCar(_levelManager.CurrentIteration));
            if (_levelManager.CurrentIteration <= 0) return;
            for(int i = 0; i < _levelManager.CurrentIteration; i++)
            {
                recordedCar[i].SetActive(true);
                StartCoroutine(Co_ReplayCar(i,0));
            }

        }
        private void GameWonState()
        {
            // if son araçsa bir sonraki bölüme geç yoksa sonraki arabaya geç
            _levelManager.LevelWon();
            _uiManager.SetText("Current Iteration: " + _levelManager.CurrentIteration, "Current Level: " + _levelManager.CurrentLevel);
        }
        private void GameLostState()
        {
            _carRecords[_levelManager.CurrentIteration].Clear();
            _levelManager.RestartIteration();
        }
public void ResetAllRecords()
        {
            Debug.Log("reset all iterations");
            for(int i = 0; i < _carRecords.Length; i++)
            {
                _carRecords[i].Clear();
            }
        }
        //HEP AYNI ARACI KONTROL ET (controller), KAÇ ARABA OLUŞACAKSA O KADARINI AKTİF OLMAZ ŞEKİLDE YEDEKTE TUT. HERKESİN SCRİPTİ OYUN BAŞLAMADAN HAZIR OLSUN.
    }
    public enum GameState
    {
        GamePreStart = 0,
        GameAwaitingStart = 1,
        GameStarted = 2,
        GameWon = 4,
        GameLost = 5,
    }
}
