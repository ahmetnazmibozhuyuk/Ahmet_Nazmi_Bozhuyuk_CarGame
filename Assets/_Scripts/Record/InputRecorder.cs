using System.Collections.Generic;
using UnityEngine;
using CarGame.Managers;
using CarGame.Control;

namespace CarGame.Record
{
    public class InputRecorder : InputAbstract, IRecord
    {
        [SerializeField] private GameObject[] recordedCar = new GameObject[7];

        private List<InputInfo>[] _inputInfo = new List<InputInfo>[8];


        private void Awake()
        {
            for (int i = 0; i < _inputInfo.Length; i++)
            {
                _inputInfo[i] = new();
            }
        }

        protected override void Update()
        {
            base.Update();
        }

        public void NextLevel()
        {
            throw new System.NotImplementedException();
        }

        public void ResetAllRecords()
        {
            throw new System.NotImplementedException();
        }

        public void RestartCurrentIteration(int currentIteration)
        {
            throw new System.NotImplementedException();
        }

        public void StartRecording(int currentIteration)
        {
            while(GameManager.instance.CurrentState == GameState.GameStarted)
            {

            }
        }

        public void StartReplaying(int iteration)
        {
            throw new System.NotImplementedException();
        }
    }
}
