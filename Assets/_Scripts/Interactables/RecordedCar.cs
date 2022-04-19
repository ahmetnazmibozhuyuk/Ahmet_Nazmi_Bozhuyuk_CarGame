using UnityEngine;
using CarGame.Managers;

namespace CarGame.Obstacles
{
    public class RecordedCar : MonoBehaviour, ICrash
    {
        // Sekiz araç da hazır açık olacak, her biri kendine atanmış queue'ya erişim sağlıyor olacak sırası gelen başlangıç pozisyonuna
        // gelip aktive olup hareket edecek.


        public void Crash()
        {
            Debug.Log("HIT A RECORDED CAR!");
            GameManager.instance.ChangeState(GameState.GameLost);
        }


        private void Start()
        {
        
        }


        private void Update()
        {
        
        }
    }
}
