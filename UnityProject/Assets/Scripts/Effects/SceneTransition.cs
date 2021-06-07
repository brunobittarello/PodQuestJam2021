
using System;
using UnityEngine;

class SceneTransition : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    float speed;

    static SceneTransition instance;

    SpriteRenderer spriteRenderer;
    int index;
    bool isDone;
    bool isFadeOut;
    float timer;
    Action onFadeOutEnds;

    void Awake()
    {
        instance = this;
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        timer = float.PositiveInfinity;
    }

    void Update()
    {
        if (isDone) return;

        Animate();
    }

    void Animate()
    {
        timer += Time.deltaTime;
        if (timer < speed)
            return;

        timer = 0;

        spriteRenderer.sprite = sprites[isFadeOut ? index : sprites.Length - 1 - index];
        index++;
        if (index == sprites.Length)
        {
            isDone = true;
            gameObject.SetActive(isFadeOut);
            if (isFadeOut)
                onFadeOutEnds?.Invoke();
        }
    }

    internal static void StartFadeOut(Action onFadeOutEnds)
    {
        if (instance == null)
            return;

        instance.timer = float.PositiveInfinity;
        instance.index = 0;
        instance.isDone = false;
        instance.isFadeOut = true;
        instance.spriteRenderer.flipX = false;
        instance.onFadeOutEnds = onFadeOutEnds;
        instance.gameObject.SetActive(true);
    }
}