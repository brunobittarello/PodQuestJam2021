using UnityEngine;
using System.Collections;

public class ObjectSwitcherBehaviour : MonoBehaviour, IRemoteControlable
{
    const float SYNC_TIME = 0.25f;

    public BaseObjectBehaviour[] objectChannels;
    public int startChannel;
    public SpriteRenderer outline;

    protected SpriteRenderer sprRenderer;
    protected Collider2D collider2d;

    internal BaseObjectBehaviour current;
    int currentChannel;
    float timer;
    bool enableProxy;

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

    public void PlayerTargetStart()
    {
        outline.enabled = true;
        current.OnPlayerTargetStart();
        if (current is IRemoteControlable)
        {
            enableProxy = true;
        }
    }

    public void PlayerTargetExit()
    {
        outline.enabled = false;
        enableProxy = false;
        current.OnPlayerTargetExit();

        if (current is IDoor door)
           collider2d.enabled = !door.IsOpened();
    }

    public virtual bool ChangeChannel(int channel)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Ray/RayStatic", transform.position);
        if (timer > 0) return false;

        if (enableProxy && current is IRemoteControlable remoteControlable)
            return remoteControlable.ChangeChannel(channel);

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
        current.OnPlaced();
    }
}
