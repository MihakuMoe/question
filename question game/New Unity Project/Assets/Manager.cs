using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
[System.Serializable]
public class Manager : MonoBehaviour
{
    public Question[] question;
    private static List<Question> unansweredQuestions;
    private Question q;

    [SerializeField]
    private Text questionField;

    [SerializeField]
    private float transitionDelay = 0.8f;

    [SerializeField]
    private Text trueCorrect;
    [SerializeField]
    private Text falseCorrect;

    [SerializeField]
    private Animator animator;

    private void Start()
    {
       if (unansweredQuestions == null || unansweredQuestions.Count == 0)
       {
            unansweredQuestions = question.ToList<Question>(); 
       }
        GetQuestion();
        Debug.Log(q.fact + " is " + q.isTrue);
    }

    IEnumerator transitionState()
    {
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GetQuestion()
    {
        int idx = Random.Range(0, unansweredQuestions.Count);
        q = unansweredQuestions[idx];
        questionField.text = q.fact;

        unansweredQuestions.RemoveAt(idx);
        if (q.isTrue)
        {
            trueCorrect.text = "CORRECT!";
            falseCorrect.text = "INCORRECT!";
        }
        else
        {
            trueCorrect.text = "INCORRECT";
            falseCorrect.text = "CORRECT";
        }

    }

    public void selectTrue()
    {
        animator.SetTrigger("True");
        if (q.isTrue)
        {
            Debug.Log("add score");
        }
        else
        {
            Debug.Log("subtract score");
        }
        StartCoroutine(transitionState());
    }

    public void selectFalse()
    {
        animator.SetTrigger("False");
        if (!q.isTrue)
        {
            Debug.Log("add score");
        }
        else
        {
            Debug.Log("subtract score");
        }
        StartCoroutine(transitionState());
    }

}
