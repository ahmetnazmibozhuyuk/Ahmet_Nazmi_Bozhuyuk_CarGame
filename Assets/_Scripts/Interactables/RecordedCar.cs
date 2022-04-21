using UnityEngine;
using CarGame.Managers;

namespace CarGame.Obstacles
{
    public class RecordedCar : MonoBehaviour, ICrash
    {
        public void Crash()
        {
            GameManager.instance.ChangeState(GameState.GameLost);
        }
    }
}
