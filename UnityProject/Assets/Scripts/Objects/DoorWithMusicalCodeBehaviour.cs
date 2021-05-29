using UnityEngine;

public class DoorWithMusicalCodeBehaviour : BaseObjectBehaviour, IRemoteControlable
{
    public ParticleSystem funfair;
    public Collider2D remoteCollider2d;
    public Collider2D collider2d;
    public Sprite openDoorSprite;
    public PopUpBehaviour popup;

    public int[] code;
    int[] inputCode;
    int inputCodeIndex;

    void Awake()
    {
        inputCode = new int[code.Length];
    }

    public bool ChangeChannel(int channel)
    {
        if (channel == 0)
            return true;
        ReceiveInput(channel);
        return false;
    }

    bool ReceiveInput(int channel)
    {
        popup.transform.position = MusicNotePosition();
        popup.Show();
        popup.spriteRender.color = SoundBoxBehaviour.NoteColorByChannel(channel);
        inputCode[inputCodeIndex] = channel;

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

        if (funfair)
            funfair.Play();
    }
}
