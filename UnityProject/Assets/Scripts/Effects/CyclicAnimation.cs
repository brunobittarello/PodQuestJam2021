
using UnityEngine;

class CyclicAnimation : MonoBehaviour
{
    [SerializeField]
    Sprite[] spritesAnimation;
    [SerializeField]
    float speed;

    SpriteRenderer spriteRenderer;
    float timer;
    int index;

    void Awake()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (spritesAnimation == null || spritesAnimation.Length == 0)
            return;

        timer += Time.deltaTime;
        if (timer < speed)
            return;

        timer = 0;
        spriteRenderer.sprite = spritesAnimation[index];
        index++;
        if (index == spritesAnimation.Length)
            index = 0;
    }

    public void SetSprites(Sprite[] sprites)
    {
        spritesAnimation = sprites;
        index = 0;
    }
}