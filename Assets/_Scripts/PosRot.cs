using UnityEngine;

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
