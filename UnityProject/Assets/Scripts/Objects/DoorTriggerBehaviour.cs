using UnityEngine;

public class DoorTriggerBehaviour : BaseObjectBehaviour
{
    public BalanceBehaviour[] triggers;
    public ParticleSystem funfair;
    public Collider2D collider2d;
    public Sprite openDoorSprite;

    Sprite startSprite;
    bool isDoorOpened;

    private void Start()
    {
        startSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    void Update()
    {
        CheckDoor();
    }

    void CheckDoor()
    {
        var readyToOpen = CheckTriggers();
        if (isDoorOpened == readyToOpen)
            return;

        isDoorOpened = readyToOpen;
        if (readyToOpen)
            OpenDoor();
        else
            CloseDoor();
    }

    bool CheckTriggers()
    {
        foreach (var trigger in triggers)
            if (!trigger.IsReady())
                return false;
        return true;
    }

    void OpenDoor()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
        collider2d.enabled = false;
        if (funfair)
            funfair.Play();
    }

    void CloseDoor( )
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = startSprite;
        collider2d.enabled = true;
    }
}
