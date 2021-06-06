
using UnityEngine;

class InputsTutorial : MonoBehaviour
{
    [SerializeField]
    Sprite[] move;

    [SerializeField]
    Sprite[] remote;

    [SerializeField]
    Sprite[] numbers;

    CyclicAnimation cyclicAnim;
    SpriteRenderer spriteRenderer;
    int index;
    State status;

    enum State
    {
        move, remote, numbers, done
    }

    void Awake()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        cyclicAnim = this.gameObject.GetComponent<CyclicAnimation>();
        cyclicAnim.SetSprites(move);
        status = State.move;
    }

    void Update()
    {
        if (status == State.move) MoveUpdate();
        else if (status == State.remote && CharacterBehaviour.instance.HasRemoteController) RemoteUpdate();
        else if (status == State.numbers && CharacterBehaviour.instance.Target != null) NumbersUpdate();
    }

    void MoveUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)
            || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)
            || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            status = State.remote;
            cyclicAnim.SetSprites(remote);
            spriteRenderer.enabled = false;
            cyclicAnim.enabled = false;
        }
    }

    void RemoteUpdate()
    {
        spriteRenderer.enabled = true;//TODO otimizar
        cyclicAnim.enabled = true;
        if (Input.GetKey(KeyCode.Space))
        {
            status = State.numbers;
            cyclicAnim.SetSprites(numbers);
            spriteRenderer.enabled = false;
            cyclicAnim.enabled = false;
        }
    }

    void NumbersUpdate()
    {
        spriteRenderer.enabled = true;
        cyclicAnim.enabled = true;
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)
            || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)
            || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)
            || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)
            || Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)
            || Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)
            || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)
            || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8)
            || Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9)
            || Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            status = State.done;
            Destroy(this.gameObject);
        }
    }
}
