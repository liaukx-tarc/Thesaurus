using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public RawImage instruction;
    [SerializeField] private Text timeText;
    public int seconds, minutes;
    void Start()
    {
        StartCoroutine(delay(3));
       
    }
   
 
 IEnumerator delay(int secs)
    {
        
        yield return new WaitForSeconds(secs);
        instruction.gameObject.SetActive(false);
        AddToSecond();
    }
    private void AddToSecond()
    {
        seconds++;
        if (seconds > 59)
        {
            minutes++;
            seconds = 0;
        }
        timeText.text = (minutes < 10 ? "0" : "") + minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
        Invoke(nameof(AddToSecond), 1);
    }

    public void StopTimer()
    {
        CancelInvoke(nameof(AddToSecond));
        timeText.color = Color.yellow;
       // timeText.gameObject.SetActive(false);
    }
}
