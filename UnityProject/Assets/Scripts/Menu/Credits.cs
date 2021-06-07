
class Credits : SceneLoader
{
    private void Update()
    {
        TryLoadScene();
    }

    void goBackToIntro()
    {
        LoadSceneAsync(0);
    }
}