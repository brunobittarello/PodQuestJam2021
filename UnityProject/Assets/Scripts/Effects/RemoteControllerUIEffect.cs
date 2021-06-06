
using UnityEngine;

class RemoteControllerUIEffect : MonoBehaviour
{
    const float SPEED = 12f;
    const float HIDE_Y = -4.5f;

    [SerializeField]
    Sprite[] sprites;

    static internal RemoteControllerUIEffect instance; 

    SpriteRenderer sprRender;
    float timer;
    Status state;

    Vector3 posShow;
    Vector3 posHide;

    enum Status
    {
        Entering,
        Showing,
        Exiting,
        Out,
    }

    void Awake()
    {
        instance = this;
        Out();

        sprRender = this.gameObject.GetComponent<SpriteRenderer>();
        var posX = this.transform.position.x;
        posShow = new Vector3(posX, 0, 0);
        posHide = new Vector3(posX, HIDE_Y, 0);
        this.transform.position = posHide;
    }

    void Update()
    {
        if (state == Status.Out || state == Status.Showing)
            return;

        if (state == Status.Entering)
            UpdateEntering();
        if (state == Status.Exiting)
            UpdateExiting();
    }

    public void Show()
    {
        state = Status.Entering;
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        state = Status.Exiting;
    }

    public void ControllerButtonPressed(int button)
    {
        if (button == -1)
            button = sprites.Length - 1;
        sprRender.sprite = sprites[button];
    }

    void UpdateEntering()
    {
        if (MoveTowards(posShow))
            state = Status.Showing;
    }

    void UpdateExiting()
    {
        if (MoveTowards(posHide))
            Out();
    }

    void Out()
    {
        state = Status.Out;
        this.gameObject.SetActive(false);
    }

    bool MoveTowards(Vector3 position)
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, position, SPEED * Time.deltaTime);
        if (Vector3.SqrMagnitude(this.transform.position - position) < 0.1f)
        {
            this.transform.position = position;
            return true;
        }
        return false;
    }

    public static Color ColorByChannel(int channel)
    {
        if (channel == 1) return new Color32(223, 113, 38, 255);//orange
        if (channel == 2) return new Color32(213, 123, 186, 255);//pink
        if (channel == 3) return new Color32(153, 229, 80, 255);//lime
        if (channel == 4) return new Color32(143, 151, 74, 255);//gold
        if (channel == 5) return new Color32(118, 66, 138, 255);//purple
        if (channel == 6) return new Color32(55, 148, 110, 255);//green
        if (channel == 7) return new Color32(251, 242, 54, 255);//yellow
        if (channel == 8) return new Color32(91, 110, 225, 255);//lilac 
        return new Color32(172, 50, 50, 255);//lilac 
    }
}
