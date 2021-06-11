using FMOD.Studio;
using UnityEngine;

class MainMenuController : SceneLoader
{
    [SerializeField]
    Animator animator;

    Bus master;

    private void Start()
    {
        master = FMODUnity.RuntimeManager.GetBus("bus:/");
        master.setVolume(0.3f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("IntroSpace", 1);
            animator.SetTrigger("trigContinue");
        }

        TryLoadScene();
        if (IsGoing)
            master.setVolume(1);
    }

    void OnAnimationEnds()
    {
        LoadSceneAsync("Room.1");
    }
}
