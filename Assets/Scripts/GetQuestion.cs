using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GetQuestion : MonoBehaviour
{
    [Header("Question Variables")]
    [SerializeField] TextMeshProUGUI Question_Text;
    [SerializeField] List<QuestionMO> questions= new List<QuestionMO>() ;
    QuestionMO question;
    [Header("Answer Variables")]
    [SerializeField] GameObject[] ans;
    [Header("Button Colors")]
    [SerializeField] Sprite correct;
    [SerializeField] Sprite default_button_sprite;
    [Header("Timer Variables")]
    [SerializeField] Image timer_image;
    bool hasAnsweredEarly;
    Timer timer;
    
    [SerializeField] GameObject t;
    int score;
    [Header("Progress Bar")]
    [SerializeField]  Slider progress;
    [Header("Game Over")]
    [SerializeField] GameObject gb;
    [SerializeField] GameObject gb2;
    [SerializeField] TextMeshProUGUI game_over;
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        Load_next_question();
        change_button(true);
        load_ques();
        score = 0;
        progress.maxValue = questions.Count;
        progress.minValue = 0;
        
    }
    void load_ques()
    {
        Question_Text.text = question.GetQues();
        for (int i = 0; i < 4; ++i)
        {
            TextMeshProUGUI button = ans[i].GetComponentInChildren<TextMeshProUGUI>();
            button.text = question.GetAns(i);
        }
    }


    private void Update()
    {
        game_over.text = "Your Total Score was " +((score/progress.maxValue)*100) +"%";
        t.GetComponent<TextMeshProUGUI>().text= "Score: " + score;
        timer_image.fillAmount =  timer.get_fill_fraction();
        if(timer.load_next_question)
        {
            hasAnsweredEarly = false;
            
            if(questions.Count<=0)
            {
                Debug.Log("game over!!");
                gb.SetActive(true);
                gb2.SetActive(false);
            }
            else
            {
                progress.value++;
                Load_next_question();
            }
            timer.load_next_question = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnswering)
        {
            DisplayAnswer(-1);
            change_button(false);
        }

        
        //else if(has)
    }


    public void DisplayAnswer(int index)
    {
        if (index == question.GetCorAns())
        {
            Question_Text.text = "Correct";
            Image c = ans[index].GetComponent<Image>();
            c.sprite = correct;
            score++;
            Debug.Log(score);
        }
        else
        {
            Question_Text.text = question.GetAns(question.GetCorAns());
            Image c = ans[question.GetCorAns()].GetComponent<Image>();
            c.sprite = correct;
        }
    }
    public void post_answer(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        change_button(false);
        timer.CancelTimer();
        
    }



    void Load_next_question()
    {
        change_button(true);
        for(int i=0;i<4;i++)
        {
            Image img = ans[i].GetComponent<Image>();
            img.sprite = default_button_sprite;
        }
        select_random();
        load_ques();
    }

    void select_random()
    {
        int i = Random.Range(0, questions.Count);
        question = questions[i];
        if(questions.Contains(question))
        {
            questions.Remove(question);
        }

    }
    void change_button(bool state)
    {
        for(int i=0;i<4;i++)
        {
            Button but = ans[i].GetComponent<Button>();
            but.interactable = state;
        }
    }

    public void replay()
    {
        SceneManager.LoadScene(0);
    }


}
