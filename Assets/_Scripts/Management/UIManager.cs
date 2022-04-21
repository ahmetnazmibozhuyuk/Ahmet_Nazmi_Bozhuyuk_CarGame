using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CarGame.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentIterationText;
        [SerializeField] private TextMeshProUGUI currentLevelText;

        public void SetText(string currIterationText, string currLevelText)
        {
            currentIterationText.SetText(currIterationText);
            currentLevelText.SetText(currLevelText);
        }
    }
}
