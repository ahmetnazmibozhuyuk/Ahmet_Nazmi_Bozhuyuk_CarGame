using System;
using UnityEngine;

namespace CarGame.Managers
{
    [RequireComponent(typeof(LevelManager))]
    public class GameManager : Singleton<GameManager>
    {
        public GameState currentState { get; private set; }

        public Controller Player
        {
            get { return player; }
            set { player = value; }
        }
        [SerializeField] private Controller player;

        private LevelManager _levelManager;

        protected override void Awake()
        {
            base.Awake();
            _levelManager = GetComponent<LevelManager>();
        }
        private void Start()
        {
            ChangeState(GameState.GameAwaitingStart);


        }

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

        }
        private void GameWonState()
        {
            // if son araçsa bir sonraki bölüme geç yoksa sonraki arabaya geç
            _levelManager.LevelWon();
        }
        private void GameLostState()
        {

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
