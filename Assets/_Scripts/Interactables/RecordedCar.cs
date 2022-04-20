using UnityEngine;
using CarGame.Managers;

namespace CarGame.Obstacles
{
    public class RecordedCar : MonoBehaviour, ICrash
    {
        public void Crash()
        {
            Debug.Log("HIT A RECORDED CAR!");
            GameManager.instance.ChangeState(GameState.GameLost);
        }
    }
}
