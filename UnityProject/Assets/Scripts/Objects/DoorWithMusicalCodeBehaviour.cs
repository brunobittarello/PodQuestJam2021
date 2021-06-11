using UnityEngine;

public class DoorWithMusicalCodeBehaviour : BaseObjectBehaviour, IRemoteControlable, IDoor
{
    public ParticleSystem funfair;
    public Collider2D remoteCollider2d;
    public Collider2D collider2d;
    public Sprite openDoorSprite;
    public PopUpBehaviour popup;
    public SpriteRenderer outline;

    public int[] code;
    int[] inputCode;
    int inputCodeIndex;
    bool isOpened;

    void Awake()
    {
        inputCode = new int[code.Length];
    }

    public void PlayerTargetStart() { }

    public void PlayerTargetExit() { }

    public bool IsOpened()
    {
        return isOpened;
    }

    public bool ChangeChannel(int channel, out bool disconnect)
    {
        disconnect = false;
        if (channel == 0)
            disconnect = true;
        else
            ReceiveInput(channel);
        return true;
    }

    bool ReceiveInput(int channel)
    {
        popup.transform.position = MusicNotePosition();
        popup.Show();
        popup.spriteRender.color = RemoteControllerUIEffect.ColorByChannel(channel);
        inputCode[inputCodeIndex] = channel;
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Instruments/Guitar/Guitar" + channel, transform.position);

        inputCodeIndex++;
        if (inputCodeIndex == code.Length)
            return VerifyCode();
        return false;

    }

    Vector3 MusicNotePosition()
    {
        if (inputCodeIndex == 0) return this.transform.position + Vector3.left * 0.5f;
        if (inputCodeIndex == 2) return this.transform.position + Vector3.right * 0.5f;
        return this.transform.position;
    }

    bool VerifyCode()
    {
        inputCodeIndex = 0;
        for (int i = 0; i < code.Length; i++)
            if (inputCode[i] != code[i])
                return false;

        OpenDoor();
        return true;
    }

    void OpenDoor()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
        remoteCollider2d.enabled = false;
        collider2d.enabled = false;
        isOpened = true;

        if (funfair)
            funfair.Play();
    }
}
