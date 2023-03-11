using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public Animator animator;
    public AudioManager audioManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")PlayEndLevel();
    }
    public void PlayEndLevel()
    {animator.SetTrigger("End");
        audioManager.PlaySound("EndLevel");
    }
    public void EndLevel()
    {SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);}
}
