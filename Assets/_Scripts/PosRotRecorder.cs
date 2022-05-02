using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarGame.Managers;

namespace CarGame.Record
{
    public class PosRotRecorder : Recorder
    {

        [Tooltip("Lower this value is, smoother the animations of recorded cars will be.")]
        [Range(0.005f, 0.08f)]
        [SerializeField] private float recordSmoothness;

        [SerializeField] private GameObject[] recordedCar = new GameObject[7];

        private List<PosRot>[] _carRecords = new List<PosRot>[8];

        private void Awake()
        {
            for (int i = 0; i < _carRecords.Length; i++)
            {
                _carRecords[i] = new();
            }
        }
        public override void StartRecording(int currentIteration)
        {
            StartCoroutine(Co_RecordCar(currentIteration));
        }
        public override void StartReplaying(int iteration)
        {
            for (int i = 0; i < iteration; i++)
            {
                recordedCar[i].SetActive(true);
                StartCoroutine(Co_ReplayCar(i, 0));
            }
        }
        private IEnumerator Co_RecordCar(int currentIteration)
        {
            var recordSmoothnessRealtime = new WaitForSecondsRealtime(recordSmoothness);
            while (GameManager.instance.CurrentState == GameState.GameStarted)
            {
                _carRecords[currentIteration].Add(new PosRot(GameManager.instance.Player.transform.position, GameManager.instance.Player.transform.rotation));
                yield return recordSmoothnessRealtime;
            }
            yield break;
        }
        private IEnumerator Co_ReplayCar(int carIndex, int i)
        {
            var recordSmoothnessRealtime = new WaitForSecondsRealtime(recordSmoothness);
            while (GameManager.instance.CurrentState == GameState.GameStarted)
            {
                if (_carRecords[carIndex].Count == i)
                {
                    recordedCar[carIndex].SetActive(false);
                    yield break;
                }
                recordedCar[carIndex].transform.SetPositionAndRotation(_carRecords[carIndex][i].currentPosition, _carRecords[carIndex][i].currentRotation);
                i++;
                yield return recordSmoothnessRealtime;
            }
            yield break;
        }
        public override void RestartCurrentIteration(int currentIteration)
        {
            _carRecords[currentIteration].Clear();
        }
        public override void NextLevel()
        {
            ResetAllRecords();
            for (int i = 0; i < recordedCar.Length; i++)
            {
                recordedCar[i].SetActive(false);
            }
        }
        public override void ResetAllRecords()
        {
            for (int i = 0; i < _carRecords.Length; i++)
            {
                _carRecords[i].Clear();
            }
        }
    }
}
namespace CarGame
{
    [System.Serializable]
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
