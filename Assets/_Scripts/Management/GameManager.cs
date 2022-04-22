using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame.Managers
{
    [RequireComponent(typeof(LevelManager), typeof(UIManager))]
    public class GameManager : Singleton<GameManager>
    {
        public GameState CurrentState { get; private set; }

        public Controller Player
        {
            get { return player; }
            set { player = value; }

        }
        [SerializeField] private Controller player;

        private const int maxIterationIndex = 7; 

        [Tooltip("Lower this value is, smoother the animations of recorded cars will be. Too low values may cause issues.")]
        [Range(0.005f, 0.08f)]
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
            for (int i = 0; i < _carRecords.Length; i++)
            {
                _carRecords[i] = new();
            }
        }

        private void Start()
        {
            _levelManager.StartLevel(_levelManager.CurrentLevel);
            ChangeState(GameState.GameAwaitingStart);
            _uiManager.SetText("Current Iteration: " + _levelManager.CurrentIteration, "Current Level: " + _levelManager.CurrentLevel);
        }

        private IEnumerator Co_RecordCar(int currentIteration)
        {
            while (CurrentState == GameState.GameStarted)
            {
                _carRecords[currentIteration].Add(new PosRot(Player.transform.position, Player.transform.rotation));
                yield return new WaitForSecondsRealtime(recordSmoothness); //WaitForSecondsRealtime instead of Waitforseconds is because movement is in FixedUpdate.
            }
            yield break;
        }
        private IEnumerator Co_ReplayCar(int carIndex, int i)
        {
            while (CurrentState == GameState.GameStarted)
            {
                if (_carRecords[carIndex].Count == i)
                {
                    recordedCar[carIndex].SetActive(false);
                    yield break;
                }
                recordedCar[carIndex].transform.SetPositionAndRotation(_carRecords[carIndex][i].currentPosition, _carRecords[carIndex][i].currentRotation);
                i++;
                yield return new WaitForSecondsRealtime(recordSmoothness);
            }
            yield break;
        }
        public void ChangeState(GameState newState)
        {
            if (CurrentState == newState) return;

            CurrentState = newState;
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
            //Method for pre game.
            //_uiManager.GameStartText(true);
        }
        private void GameStartedState()
        {
            _uiManager.GameStartText(false);
            StartCoroutine(Co_RecordCar(_levelManager.CurrentIteration));
            if (_levelManager.CurrentIteration <= 0) return;
            for (int i = 0; i < _levelManager.CurrentIteration; i++)
            {
                recordedCar[i].SetActive(true);
                StartCoroutine(Co_ReplayCar(i, 0));
            }
        }
        private void GameWonState()
        {
            if (_levelManager.CurrentIteration >= maxIterationIndex)
            {
                ResetAllRecords();
                _levelManager.NextLevel();
                ChangeState(GameState.GameAwaitingStart);
                for(int i = 0; i < recordedCar.Length; i++)
                {
                    recordedCar[i].SetActive(false);
                }
            }
            else
            {
                _levelManager.NextIteration();
            }
            _uiManager.SetText("Current Iteration: " + _levelManager.CurrentIteration, "Current Level: " + _levelManager.CurrentLevel);
        }
        private void GameLostState()
        {
            _carRecords[_levelManager.CurrentIteration].Clear();
            _levelManager.RestartIteration();
        }
        public void ResetAllRecords()
        {
            for (int i = 0; i < _carRecords.Length; i++)
            {
                _carRecords[i].Clear();
            }
        }
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
namespace CarGame
{
    [Serializable]
    public struct PosRot
    {
        public Vector3 currentPosition;
        public Quaternion currentRotation;
        public PosRot(Vector3 currPos, Quaternion currRot)
        {
            currentPosition = currPos;
            currentRotation = currRot;
        }
    }
}
