using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestChecker : MonoBehaviour
{
    //[SerializeField] private GameObject dialogueBox, textFinished, textUnfinished;
    [SerializeField] private int questGoal = 1;
    [SerializeField] private int nextLevel;

    //private Animator anim;
    private bool levelIsLoading = false;

    private void Start()
    {
        //anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //dialogueBox.SetActive(true);
            if (collision.GetComponent<PlayerMovement>().keyCount >= questGoal)
            {
                Debug.Log("keycount: " + collision.GetComponent<PlayerMovement>().keyCount);
                //textFinished.SetActive(true);
                //anim.SetTrigger("Door");
                Invoke("LoadNextLevel", 1.5f);
                levelIsLoading = true;
                
            }
            else
            {
                //textUnfinished.SetActive(true);
            }
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !levelIsLoading)
        {
            textFinished.SetActive(false);
            textUnfinished.SetActive(false);
            dialogueBox.SetActive(false);
        }
    }
    */
}
