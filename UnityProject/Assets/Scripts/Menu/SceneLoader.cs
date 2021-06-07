
using UnityEngine;
using UnityEngine.SceneManagement;

abstract class SceneLoader : MonoBehaviour
{
    AsyncOperation sceneAsyncOp;
    bool isFadeoutEnded;

    protected void TryLoadScene()
    {
        if (isFadeoutEnded && sceneAsyncOp != null && sceneAsyncOp.progress >= 0.9f)
            sceneAsyncOp.allowSceneActivation = true;
    }

    protected void LoadSceneAsync(int index)
    {
        sceneAsyncOp = SceneManager.LoadSceneAsync(index);
        sceneAsyncOp.allowSceneActivation = false;
        SceneTransition.StartFadeOut(OnFadeOutEnds);
    }

    protected void LoadSceneAsync(string name)
    {
        sceneAsyncOp = SceneManager.LoadSceneAsync(name);
        sceneAsyncOp.allowSceneActivation = false;
        SceneTransition.StartFadeOut(OnFadeOutEnds);
    }

    void OnFadeOutEnds() => isFadeoutEnded = true;
}
