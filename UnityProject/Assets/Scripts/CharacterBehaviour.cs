using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    const float SPEED = 3;

    public static CharacterBehaviour instance;

    bool isChangingChannel;
    InstableObjectBehaviour target;
    public bool hasItem;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChangingChannel)
            ChangeChannel();
        else
            ReadInputs();
    }

    void ChangeChannel()
    {
        var channel = GetValidChannelInput();
        if (channel == 0)
            return;

        var isDone = false;
        if (channel > 0)
            isDone = target.ChangeChannel(channel);
        else
            isDone = true;

        if (isDone)
        {
            target = null;
            isChangingChannel = false;
        }
    }

    int GetValidChannelInput()
    {
        if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1)) return 1;
        if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2)) return 2;
        if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3)) return 3;
        if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4)) return 4;
        if (Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Keypad5)) return 5;
        if (Input.GetKey(KeyCode.Alpha6) || Input.GetKey(KeyCode.Keypad6)) return 6;
        if (Input.GetKey(KeyCode.Alpha7) || Input.GetKey(KeyCode.Keypad7)) return 7;
        if (Input.GetKey(KeyCode.Alpha8) || Input.GetKey(KeyCode.Keypad8)) return 8;
        if (Input.GetKey(KeyCode.Alpha9) || Input.GetKey(KeyCode.Keypad9)) return 9;
        if (Input.GetKey(KeyCode.Alpha0) || Input.GetKey(KeyCode.Keypad0)) return -1;
        return 0;
    }

    void ReadInputs()
    {
        var movement = Vector2Int.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            movement += Vector2Int.left;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            movement += Vector2Int.right;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            movement += Vector2Int.up;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            movement += Vector2Int.down;

        Move(movement);

        if (Input.GetKeyDown(KeyCode.Space))
            TryToChangeChannel();
    }

    void Move(Vector2 deltaMovement)
    {
        this.transform.position = this.transform.position + (Vector3)deltaMovement * Time.deltaTime * SPEED;
    }

    void TryToChangeChannel()
    {
        Debug.Log("Trying...");
        var hit = Physics2D.Raycast(this.transform.position, Vector2.up, float.PositiveInfinity, LayerMask.GetMask("RemoteControl"));
        if (hit.collider == null)
            return;

        Debug.Log("HIT " + hit.collider.name);
        target =  hit.collider.GetComponent<InstableObjectBehaviour>();
        if (target == null)
            return;

        Debug.Log("object found!");
        isChangingChannel = true;
    }
}
