using UnityEngine;
using TMPro;

namespace CarGame.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentIterationText;
        [SerializeField] private TextMeshProUGUI currentLevelText;

        [SerializeField] private GameObject preGameText;
        public void SetText(string currIterationText, string currLevelText)
        {
            currentIterationText.SetText(currIterationText);
            currentLevelText.SetText(currLevelText);
        }
        public void GameStartText(bool isActive)
        {
            preGameText.SetActive(isActive);
        }
    }
}
