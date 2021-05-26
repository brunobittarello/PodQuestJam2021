using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitcherBehaviour : MonoBehaviour
{
    const float SYNC_TIME = 0.25f;

    public BaseObjectBehaviour[] objectChannels;
    public int startChannel;

    protected SpriteRenderer sprRenderer;
    protected Collider2D collider2d;

    BaseObjectBehaviour current;
    int currentChannel;
    float timer;

    void Start()
    {
        sprRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        collider2d = this.gameObject.GetComponent<Collider2D>();
        sprRenderer.enabled = false;
        currentChannel = startChannel;
        LoadChannel();
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
        LoadChannel();
    }

    public virtual bool ChangeChannel(int channel)
    {
        if (timer > 0) return false;

        currentChannel = channel;
        sprRenderer.enabled = true;
        timer = SYNC_TIME;
        if (current != null)
            Destroy(current.gameObject);

        return false;
    }

    protected virtual void LoadChannel()
    {
        sprRenderer.enabled = false;
        current = Instantiate(objectChannels[currentChannel - 1], this.transform);
        current.transform.localPosition = Vector3.zero;
    }
}
