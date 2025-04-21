using UnityEngine;
using UnityEngine.UI;


public class CircularTimer : MonoBehaviour
{
    public Image timerImage;
    public float duration = 3f;
    private float timeLeft;
    private bool isRunning = false;

    public void StartTimer()
    {
        timeLeft = duration;
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            timeLeft -= Time.deltaTime;
            timerImage.fillAmount = timeLeft / duration;

            if (timeLeft <= 0f)
            {
                isRunning = false;
                timerImage.fillAmount = 0f;
            }
        }
    }
}
