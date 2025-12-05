using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    public static int minutes;
    public static int seconds;
    // Update is called once per frame
    void Update()
    {
        if (!Finale.finished) {
            elapsedTime += Time.deltaTime;
            minutes = Mathf.FloorToInt(elapsedTime / 60);
            seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        
    }
}
