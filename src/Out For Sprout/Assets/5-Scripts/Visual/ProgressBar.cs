using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image progressBarImage;

    void FixedUpdate()
    {
        progressBarImage.fillAmount = ProgressTracker.Instance.GetFullProgressPercentage();
    }
}
