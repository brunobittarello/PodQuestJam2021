
using UnityEngine;

class RemoteControllerUIEffect : MonoBehaviour
{
    const float SPEED = 12f;
    const float HIDE_Y = -4.5f;

    static internal RemoteControllerUIEffect instance; 

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
        state = Status.Out;

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
    }

    public void Hide()
    {
        state = Status.Exiting;
    }

    public void ControllerButtonPressed(int button)
    {

    }

    void UpdateEntering()
    {
        if (MoveTowards(posShow))
            state = Status.Showing;
    }

    void UpdateExiting()
    {
        if (MoveTowards(posHide))
            state = Status.Showing;
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
}
