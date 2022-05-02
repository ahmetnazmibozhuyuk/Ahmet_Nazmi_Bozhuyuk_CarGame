using UnityEngine;

namespace CarGame.Record
{
    public abstract class Recorder: MonoBehaviour
    {
        public int maxIterationIndex = 7;
        public abstract void StartRecording(int currentIteration);
        public abstract void StartReplaying(int iteration);
        public abstract void RestartCurrentIteration(int currentIteration);
        public abstract void ResetAllRecords();
        public abstract void NextLevel();

    }
}
