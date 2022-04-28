using System;
using UnityEngine;
using CarGame.Record;

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

        private LevelManager _levelManager;
        private UIManager _uiManager;
        [SerializeField] private Recorder recorder;


        protected override void Awake()
        {
            base.Awake();
            _levelManager = GetComponent<LevelManager>();
            _uiManager = GetComponent<UIManager>();
        }

        private void Start()
        {
            _levelManager.StartLevel(_levelManager.CurrentLevel);
            ChangeState(GameState.GameAwaitingStart);
            _uiManager.SetText("Current Iteration: " + _levelManager.CurrentIteration, "Current Level: " + _levelManager.CurrentLevel);
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
            recorder.StartRecording(_levelManager.CurrentIteration);
            if (_levelManager.CurrentIteration <= 0) return;
            recorder.StartReplaying(_levelManager.CurrentIteration);
        }
        private void GameWonState()
        {
            if (_levelManager.CurrentIteration >= Recorder.maxIterationIndex)
            {
                recorder.NextLevel();
                _levelManager.NextLevel();
                ChangeState(GameState.GameAwaitingStart);

            }
            else
            {
                _levelManager.NextIteration();
            }
            _uiManager.SetText("Current Iteration: " + _levelManager.CurrentIteration, "Current Level: " + _levelManager.CurrentLevel);
        }
        private void GameLostState()
        {
            recorder.RestartCurrentIteration(_levelManager.CurrentIteration);
            _levelManager.RestartIteration();
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