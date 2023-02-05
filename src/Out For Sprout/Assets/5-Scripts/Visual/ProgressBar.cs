using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image progressBarImage;
    [SerializeField] private GameObject timeText;

    void FixedUpdate()
    {
        progressBarImage.fillAmount = ProgressTracker.Instance.GetFullProgressPercentage();
        timeText.GetComponent<TMP_Text>().text = UIManager.Timeformat(ProgressTracker.Instance.GetTimer());
    }
}
