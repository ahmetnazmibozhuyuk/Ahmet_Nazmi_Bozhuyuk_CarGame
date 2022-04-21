using UnityEngine;
using CarGame.Managers;

namespace CarGame.Obstacles
{
    public class Obstacle : MonoBehaviour,ICrash
    {
        public void Crash()
        {
            // Wall hit sfx-particle etc
            GameManager.instance.ChangeState(GameState.GameLost);
        }
    }
}
