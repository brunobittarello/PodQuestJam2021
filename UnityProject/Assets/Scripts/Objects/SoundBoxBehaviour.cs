
using UnityEngine;

class SoundBoxBehaviour : BaseObjectBehaviour, IInterrupter
{
    const float TIME = 0.6f;

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
        musicNote.spriteRender.color = NoteColorByChannel(code[index]);
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

    public static Color NoteColorByChannel(int channel)
    {
        if (channel == 1) return Color.white;
        if (channel == 2) return Color.grey;
        if (channel == 3) return Color.green;
        if (channel == 4) return Color.red;
        if (channel == 5) return Color.cyan;
        if (channel == 6) return Color.magenta;
        if (channel == 7) return Color.yellow;
        if (channel == 8) return new Color(1, .3f, 0);
        return new Color(0, .3f, 1);
    }
}
