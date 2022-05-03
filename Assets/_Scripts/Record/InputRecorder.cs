using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarGame.Managers;
using CarGame.Control;

namespace CarGame.Record
{
    public class InputRecorder : InputAbstract, IRecord
    {
        public int MaxIterationIndex
        {
            get { return maxIterationIndex; }
            set { maxIterationIndex = value; }
        }
        [SerializeField] private int maxIterationIndex;

        [SerializeField] private InputDrivenCar[] recordedCar;

        private PosRot[] _carInitialPosRot = new PosRot[8];

        private List<InputInfo>[] _inputInfo = new List<InputInfo>[8];

        private void Awake()
        {
            for (int i = 0; i < _inputInfo.Length; i++)
            {
                _inputInfo[i] = new();
            }

            Application.targetFrameRate = -1; // todo : inconsistent frame rates may make recorded cars move out of sync and FixedUpdate method may consume inputs  
                                               // so for input recorder and controller to work consistently target frame rate needs to be 30
        }
        public void StartRecording(int currentIteration)
        {
            StartCoroutine(Co_RecordCar(currentIteration));
        }
        public void StartReplaying(int iteration)
        {
            for(int i = 0; i < recordedCar.Length; i++)
            {
                recordedCar[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < iteration; i++)
            {
                recordedCar[i].transform.SetPositionAndRotation(_carInitialPosRot[i].currentPosition, _carInitialPosRot[i].currentRotation);
                recordedCar[i].gameObject.SetActive(true);
                StartCoroutine(Co_ReplayCar(i, 0));
            }
        }
        private IEnumerator Co_RecordCar(int currentIteration)
        {
            _carInitialPosRot[currentIteration] = new PosRot(GameManager.instance.Player.transform.position, GameManager.instance.Player.transform.rotation);
            while (GameManager.instance.CurrentState == GameState.GameStarted)
            {
                _inputInfo[currentIteration].Add(new InputInfo(leftInput, rightInput));
                yield return null;
            }
            yield break;
        }
        private IEnumerator Co_ReplayCar(int carIndex, int i)
        {
            while (GameManager.instance.CurrentState == GameState.GameStarted)
            {
                if (_inputInfo[carIndex].Count == i)
                {
                    recordedCar[carIndex].gameObject.SetActive(false);
                    yield break;
                }
                recordedCar[carIndex].AssignInput(_inputInfo[carIndex][i]);
                i++;
                yield return null;
            }
            yield break;
        }
        public void RestartCurrentIteration(int currentIteration)
        {
            for(int i = 0; i < recordedCar.Length; i++)
            {
                recordedCar[i].gameObject.SetActive(false);
            }
            _inputInfo[currentIteration].Clear();
        }
        public void NextLevel()
        {
            ResetAllRecords();
            for (int i = 0; i < recordedCar.Length; i++)
            {
                recordedCar[i].gameObject.SetActive(false);
            }
        }
        public void ResetAllRecords()
        {
            for (int i = 0; i < _inputInfo.Length; i++)
            {
                _inputInfo[i].Clear();
            }
        }
    }
}
