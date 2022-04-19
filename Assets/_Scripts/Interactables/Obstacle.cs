using UnityEngine;
using CarGame.Managers;

namespace CarGame.Obstacles
{
    public class Obstacle : MonoBehaviour,ICrash
    {
        public void Crash()
        {
            // Wall hit sfx-particle etc
            Debug.Log("HIT THE OBSTACLE!");
            GameManager.instance.ChangeState(GameState.GameLost);
        }
    }
}
