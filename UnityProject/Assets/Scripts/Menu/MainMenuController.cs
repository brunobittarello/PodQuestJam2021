using UnityEngine;

class MainMenuController : SceneLoader
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

        TryLoadScene();
    }

    void OnAnimationEnds()
    {
        LoadSceneAsync("Room.1");
    }
}
