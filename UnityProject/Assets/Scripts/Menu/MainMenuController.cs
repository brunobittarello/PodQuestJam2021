
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

class MainMenuController : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("IntroSpace", 1);
            animator.SetTrigger("trigContinue");
        }
    }

    void OnAnimationEnds()
    {
        SceneManager.LoadScene("Room.1");
        //Camera.main.enabled = false;
    }
}
