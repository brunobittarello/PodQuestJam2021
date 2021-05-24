using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstableObjectBehaviour : MonoBehaviour
{
    const float SYNC_TIME = 0.25f;

    public Sprite spriteNoise;

    public Sprite[] spriteChannels;
    public int correctChannel;
    public bool isRight;

    public ParticleSystem funfair;
    public CollectableBehaviour collectable;

    protected SpriteRenderer sprRenderer;
    protected Collider2D collider2d;
    int currentChannel;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        sprRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        collider2d = this.gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        if (timer > 0)
            SyncSprite();
    }

    public void SyncSprite()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
            return;

        timer = 0;
        sprRenderer.sprite = spriteChannels[currentChannel - 1];
        if (isRight)
            funfair.Play();
    }

    public bool ChangeChannel(int channel)
    {
        if (timer > 0) return false;
        if (isRight) return true;

        isRight = channel == correctChannel;
        currentChannel = channel;
        sprRenderer.sprite = spriteNoise;
        timer = SYNC_TIME;

        if (isRight)
            EnableObjectFunction();
        return isRight;
    }

    protected virtual void EnableObjectFunction()
    {
        collider2d.enabled = false;
        if (collectable != null)
        {
            collectable.Show();
            CharacterBehaviour.instance.hasItem = true;
        }
    }
}
