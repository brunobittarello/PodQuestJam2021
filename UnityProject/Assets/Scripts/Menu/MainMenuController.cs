
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

class MainMenuController : MonoBehaviour
{
    const float THRESHOLD_DIST = 0.2f;

    [SerializeField]
    Transform remoteController;
    [SerializeField]
    float controllerSpeed = 4f;
    [SerializeField]
    Vector3 controllerPosStart;

    [SerializeField]
    Transform logo;
    [SerializeField]
    float logoTimer = 3f;
    [SerializeField]
    Vector3 logoPosStart;

    Vector3 controllerPosEnd;
    Vector3 logoPosEnd;

    [SerializeField]
    float fadeOutTime;

    float timer;
    State status;

    enum State
    {
        showingController,
        showingLogo,
        Waiting,
        goingNextScene,
    }

    void Awake()
    {
        status = State.showingController;
        controllerPosEnd = remoteController.transform.position;
        logoPosEnd = logo.transform.position;

        remoteController.transform.position = controllerPosStart;
        logo.transform.position = logoPosStart;
    }

    void Update()
    {
        switch (status)
        {
            case State.showingController:
                UpdateShowingController();
                break;
            case State.showingLogo:
                UpdateShowingLogo();
                break;
            case State.Waiting:
                UpdateWaiting();
                break;
            case State.goingNextScene:
                UpdateGoingNextScene();
                break;
        }
    }

    void UpdateShowingController()
    {
        remoteController.position = Vector3.MoveTowards(remoteController.position, controllerPosEnd, controllerSpeed * Time.deltaTime);
        if (Vector3.SqrMagnitude(remoteController.position - controllerPosEnd) < THRESHOLD_DIST)
        {
            remoteController.position = controllerPosEnd;
            status = State.showingLogo;
        }
    }

    void UpdateShowingLogo()
    {
        //TODO add another effect
        logo.position = Vector3.MoveTowards(logo.position, logoPosEnd, controllerSpeed * Time.deltaTime);
        if (Vector3.SqrMagnitude(logo.position - logoPosEnd) < THRESHOLD_DIST)
        {
            logo.position = logoPosEnd;
            status = State.Waiting;
        }
    }

    void UpdateWaiting()
    {
        if (Input.anyKeyDown)
            status = State.goingNextScene;
    }

    void UpdateGoingNextScene()
    {
        timer += Time.deltaTime;
        if (timer > fadeOutTime)
            SceneManager.LoadScene("Room.1");
    }
}
