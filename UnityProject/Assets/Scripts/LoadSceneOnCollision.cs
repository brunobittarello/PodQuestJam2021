using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnCollision : MonoBehaviour
{

    public string PlayerTag = "Player";
    public string SceneName;

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!string.IsNullOrWhiteSpace(SceneName) && collision.CompareTag(PlayerTag))
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
