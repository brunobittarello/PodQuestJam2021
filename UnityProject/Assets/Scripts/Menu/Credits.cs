
using UnityEngine;

class Credits : SceneLoader
{
    void Update()
    {
        TryLoadScene();
    }

    void goBackToIntro()
    {
        if (ObjectSwitcherTutorialBehaviour.gameplayTrack.HasValue)
            ObjectSwitcherTutorialBehaviour.gameplayTrack.Value.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        LoadSceneAsync(0);
    }
}