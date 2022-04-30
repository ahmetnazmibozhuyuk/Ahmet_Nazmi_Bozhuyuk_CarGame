using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarGame.Managers;
using CarGame.Control;


namespace CarGame.Record
{
    public class InputRecorder : InputAbstract, IRecord
    {
        [SerializeField] private InputDrivenCar[] recordedCar = new InputDrivenCar[7];


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
        public void StartRecording(int currentIteration)
        {
            Debug.Log(currentIteration);
            StartCoroutine(Co_RecordCar(currentIteration));
        }
        public void StartReplaying(int iteration)
        {
            for (int i = 0; i < iteration; i++)
            {
                recordedCar[i].gameObject.SetActive(true);
                StartCoroutine(Co_ReplayCar(i, 0));
            }
        }
        private IEnumerator Co_RecordCar(int currentIteration)
        {
            while (GameManager.instance.CurrentState == GameState.GameStarted)
            {
                _inputInfo[currentIteration].Add(new InputInfo(leftInput, rightInput));
                yield return new WaitForEndOfFrame();
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
                //recordedCar[carIndex].transform.SetPositionAndRotation(_carRecords[carIndex][i].currentPosition, _carRecords[carIndex][i].currentRotation);
                recordedCar[carIndex].AssignInput(_inputInfo[carIndex][i]);
                i++;
                yield return new WaitForEndOfFrame();
            }
            yield break;
        }
        public void RestartCurrentIteration(int currentIteration)
        {
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
