using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClockController : MonoBehaviour
{
    public float duration;
    public UnityEvent onTimerEnd;
    private Image timerImage;
    private float currentTime;

    private void Start()
    {
        timerImage = GetComponent<Image>();
        timerImage.type = Image.Type.Filled; // Set the image type to Filled
        timerImage.fillMethod = Image.FillMethod.Radial360; // Set the fill method to Radial360
        timerImage.fillOrigin = (int)Image.Origin360.Top; // Set the fill origin to the top

        currentTime = duration;
    }

    private void Update()
    {
        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            timerImage.fillAmount = 1 - (currentTime / duration);  
        }
        else
        {
            // Timer has reached zero, invoke the method or action
            onTimerEnd.Invoke();
        }
    }
}
