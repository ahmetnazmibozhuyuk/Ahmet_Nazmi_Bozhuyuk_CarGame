using System.Collections.Generic;
using UnityEngine;
using CarGame.Managers;

namespace CarGame.Record
{

    // todo : refactor fixed recorder; segregate IRecord interface if necessary
    public class FixedRecorder : MonoBehaviour, IRecord
    {
        public int MaxIterationIndex
        {
            get { return maxIterationIndex; }
            set { maxIterationIndex = value; }
        }
        [SerializeField] private int maxIterationIndex;

        [SerializeField] private GameObject[] recordedCar;

        private List<PosRot>[] _carRecords = new List<PosRot>[8];


        private int[] j = new int[8];
        private void Awake()
        {
            for (int i = 0; i < _carRecords.Length; i++)
            {
                _carRecords[i] = new();
            }

        }
        private void FixedUpdate()
        {
            RecordCar();
            ReplayCar();
        }

        public void StartRecording(int currentIteration)
        {
            for(int i = 0; i < j.Length; i++)
            {
                j[i] = 0;
            }
        }

        public void StartReplaying(int iteration)
        {
            //throw new System.NotImplementedException();

        }
        private void RecordCar()
        {
            if (GameManager.instance.CurrentState != GameState.GameStarted) return;
            _carRecords[GameManager.instance.CurrentIteration()].Add(new PosRot(GameManager.instance.Player.transform.position, GameManager.instance.Player.transform.rotation));
        }
        private void ReplayCar()
        {
            if (GameManager.instance.CurrentState != GameState.GameStarted) return;

            for (int i = 0; i < GameManager.instance.CurrentIteration(); i++)
            {
                recordedCar[i].SetActive(true);
                MoveReplayCar(i);
            }
        }
        private void MoveReplayCar(int i)
        {
            if (_carRecords[i].Count < j[i] + 1)
            {
                recordedCar[i].SetActive(false);
                return;
            }
            recordedCar[i].transform.SetPositionAndRotation(_carRecords[i][j[i]].currentPosition, _carRecords[i][j[i]].currentRotation);
            j[i]++;
        }
        public void RestartCurrentIteration(int currentIteration)
        {
            for (int i = 0; i < recordedCar.Length; i++)
            {
                recordedCar[i].SetActive(false);
                j[i] = 0;
            }
            _carRecords[currentIteration].Clear();
        }
        public void NextLevel()
        {
            ResetAllRecords();
            for (int i = 0; i < recordedCar.Length; i++)
            {
                recordedCar[i].SetActive(false);
            }
        }
        public void ResetAllRecords()
        {
            for (int i = 0; i < _carRecords.Length; i++)
            {
                _carRecords[i].Clear();
            }
        }
    }
}