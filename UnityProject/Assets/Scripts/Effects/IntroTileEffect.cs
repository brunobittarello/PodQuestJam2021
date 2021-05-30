
using UnityEngine;

class IntroTileEffect : MonoBehaviour
{
    const float FADE_TIME = 0.35f;
    const float DESTROY_TIME = CharacterBehaviour.INTRO_TIME + FADE_TIME;
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < CharacterBehaviour.INTRO_TIME)
            return;
        if (timer > DESTROY_TIME)
            Destroy(this.gameObject);
        var x = 1 - Mathf.InverseLerp(CharacterBehaviour.INTRO_TIME, DESTROY_TIME, timer);
        this.transform.localScale = new Vector3(x, 1, 1);            
    }
}
