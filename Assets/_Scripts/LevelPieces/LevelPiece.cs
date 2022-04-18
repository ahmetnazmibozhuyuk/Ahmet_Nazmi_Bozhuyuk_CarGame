using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelPieceScriptableObject", order = 1)]
    public class LevelPiece : ScriptableObject
    {
        public GameObject LevelPrefab;
        public Vector3[] StartingPoints = new Vector3[8];
        public Vector3[] EndingPoints = new Vector3[8];
    }
}
