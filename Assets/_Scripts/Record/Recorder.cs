using UnityEngine;
namespace CarGame.Record
{
    public interface IRecord
    {
        public int MaxIterationIndex { get; set; }
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
    [System.Serializable]
    public struct InputInfo
    {
        public InputState LeftInput;
        public InputState RightInput;
        public InputInfo(InputState leftInput, InputState rightInput)
        {
            LeftInput = leftInput;
            RightInput = rightInput;
        }
    }
    public enum InputState
    {
        InputUp = 0, InputDown = 1, InputActive = 2, InputInactive = 3
    }
}

