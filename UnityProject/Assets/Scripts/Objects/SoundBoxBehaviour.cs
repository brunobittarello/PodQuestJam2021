
using UnityEngine;

class SoundBoxBehaviour : BaseObjectBehaviour, IInterrupter
{
    const float TIME = 0.8f;

    public int[] code;
    public PopUpBehaviour musicNote;
    bool isTurnedOn;

    int current;
    float timer;

    void Update()
    {
        if (isTurnedOn == false) return;

        timer += Time.deltaTime;
        if (timer > TIME)
            NextNote();
    }

    void NextNote()
    {
        timer = 0;
        if (current < code.Length)
            ShowNote(current);

        current++;
        if (current >= code.Length + 2)
            current = 0;
    }

    void ShowNote(int index)
    {
        musicNote.Show();
        musicNote.transform.position = MusicNotePosition(index);
        musicNote.spriteRender.color = RemoteControllerUIEffect.ColorByChannel(code[index]);
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Instruments/Bass/Bass" + code[index], transform.position);
    }

    Vector3 MusicNotePosition(int index)//TODO merge with DoorWithMusicalCodeBehaviour function
    {
        if (index == 0) return this.transform.position + Vector3.left * 0.5f;
        if (index == 2) return this.transform.position + Vector3.right * 0.5f;
        return this.transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        var character = collision.collider.GetComponent<CharacterBehaviour>();
        if (character != null)
        {
            TurnOn();
            return;
        }
    }

    public void TurnOn()
    {
        isTurnedOn = true;
    }

    
}
