using UnityEngine;
using CarGame.Managers;

namespace CarGame.Obstacles
{
    public class GoalPoint : MonoBehaviour,ICrash
    {
        public void Crash()
        {

            Debug.Log("HIT THE GOAL POINT");
            GameManager.instance.ChangeState(GameState.GameWon);
        }



    }
}
