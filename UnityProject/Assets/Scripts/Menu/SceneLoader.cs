
using UnityEngine;
using UnityEngine.SceneManagement;

abstract class SceneLoader : MonoBehaviour
{
    AsyncOperation sceneAsyncOp;
    bool isFadeoutEnded;
    protected bool IsGoing { get { return sceneAsyncOp?.allowSceneActivation ?? false; } }

    protected void TryLoadScene()
    {
        if (isFadeoutEnded && sceneAsyncOp != null && sceneAsyncOp.progress >= 0.9f)
            sceneAsyncOp.allowSceneActivation = true;
    }

    protected void LoadSceneAsync(int index)
    {
        sceneAsyncOp = SceneManager.LoadSceneAsync(index);
        OnSceneStartLoading();
    }

    protected void LoadSceneAsync(string name)
    {
        sceneAsyncOp = SceneManager.LoadSceneAsync(name);
        OnSceneStartLoading();
    }

    void OnSceneStartLoading()
    {
        if (CharacterBehaviour.instance != null)
            CharacterBehaviour.instance.IsMoveEnabled = false;
        sceneAsyncOp.allowSceneActivation = false;
        SceneTransition.StartFadeOut(OnFadeOutEnds);
    }

    void OnFadeOutEnds() => isFadeoutEnded = true;
}
