using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Add Question", fileName = "New Question")]
public class QuestionMO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string ques = "Type Your Question Here" ;
    [SerializeField] string[] ans = new string[4];
    [SerializeField] int correctans;
    public string GetQues()
    {
        return ques;
    }

    public string GetAns(int ind)
    {
        return ans[ind];
    }

    public int GetCorAns()
    {
        return correctans;
    }

}
