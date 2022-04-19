using CarGame.Obstacles;
using UnityEngine;

namespace CarGame
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelPieceScriptableObject", order = 1)]
    public class LevelPiece : ScriptableObject
    {
        public GameObject LevelPrefab;

    }
}
