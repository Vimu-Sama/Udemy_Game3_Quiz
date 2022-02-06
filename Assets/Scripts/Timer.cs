using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float total_time = 30f;
    [SerializeField] float show_answer = 10f;
    float time_remaining;
    [SerializeField] float fill_fraction;
    public bool  load_next_question;
    public bool isAnswering ;
    
    bool showing_answer;

    private void Start()
    {
        time_remaining = total_time;
        isAnswering = true;
        load_next_question = false;
    }

    private void Update()
    {
        UpdateTimer();
    }

    public float get_fill_fraction()
    {
        return fill_fraction;
    }
    public void CancelTimer()
    {
        time_remaining = 0;
    }
    void UpdateTimer()
    {
        time_remaining -= Time.deltaTime;
        if (isAnswering)
        {
            if (time_remaining > 0)
            {
                fill_fraction = time_remaining / (total_time);
            }
            else
            {
                isAnswering = false;
                time_remaining = show_answer;
            }
        }
        else
        {
            if (time_remaining > 0)
            {
                fill_fraction = time_remaining / show_answer;
            }
            else
            {
                isAnswering = true;
                load_next_question = true;
                time_remaining = total_time;
            }
        }
    }
}
