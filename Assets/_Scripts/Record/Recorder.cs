using UnityEngine;
namespace CarGame.Record
{
    public abstract class Recorder : MonoBehaviour
    {
        public const int maxIterationIndex = 7;
        public abstract void StartRecording(int currentIteration);
        public abstract void StartReplaying(int iteration);
        public abstract void RestartCurrentIteration(int currentIteration);
        public abstract void NextLevel();
        public abstract void ResetAllRecords();
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

