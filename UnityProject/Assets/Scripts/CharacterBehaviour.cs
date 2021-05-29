using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    const float SPEED = 2.5f;
    const float ANIMATION_SPEED = 0.2f;

    public static CharacterBehaviour instance;

    bool isChangingChannel;
    IRemoteControlable target;
    public bool hasItem;

    public bool CanBreak = false;

    public Sprite[] sprites;

    SpriteRenderer sprRenderer;
    Vector2Int lastMovement;
    float animationTimer;
    int movingSpriteIndex;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        instance = this;
        sprRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        lastMovement = Vector2Int.up;
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
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) return 1;
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) return 2;
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) return 3;
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) return 4;
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) return 5;
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) return 6;
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)) return 7;
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8)) return 8;
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9)) return 9;
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0)) return -1;
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
        Animate(movement);
        lastMovement = movement;

        if (Input.GetKeyDown(KeyCode.Space))
            TryToChangeChannel();
    }

    void Move(Vector2 deltaMovement)
    {
        this.transform.position = this.transform.position + (Vector3)deltaMovement * Time.deltaTime * SPEED;
    }

    void Animate(Vector2Int deltaMovement)
    {
        if (deltaMovement != Vector2Int.zero) {
            animationTimer += Time.deltaTime;
            if (animationTimer > ANIMATION_SPEED)
            {
                sprRenderer.sprite = GetMovingSprite(deltaMovement);
                animationTimer = 0;
            }
            return;
        }
        else if (lastMovement == Vector2Int.zero)
            return;

        sprRenderer.sprite = GetIdleSprite(lastMovement);
        animationTimer = ANIMATION_SPEED;
    }

    protected virtual Sprite GetMovingSprite(Vector2Int direction)
    {
        sprRenderer.flipX = false;
        movingSpriteIndex++;
        if (movingSpriteIndex == 4)
            movingSpriteIndex = 0;

        if (direction.y > 0)
            return GetMovingSprite(0);
        if (direction.y < 0)
            return GetMovingSprite(1);

        if (direction.x > 0)
            sprRenderer.flipX = true;

        return GetMovingSprite(2);
    }

    Sprite GetMovingSprite(int offset)
    {
        switch (movingSpriteIndex)
        {
            case 0:
            case 2: return sprites[0 + offset];
            case 1: return sprites[3 + offset * 2];
            case 3: return sprites[4 + offset * 2];
        }
        return null;
    }

    protected virtual Sprite GetIdleSprite(Vector2Int direction)
    {
        sprRenderer.flipX = false;
        if (direction.y > 0)
            return sprites[0];
        if (direction.y < 0)
            return sprites[1];
        if (direction.x > 0)
            sprRenderer.flipX = true;

        return sprites[2];
    }

    void TryToChangeChannel()
    {
        Debug.Log("Trying...");
        var hit = Physics2D.Raycast(this.transform.position, Vector2.up, float.PositiveInfinity, LayerMask.GetMask("RemoteControl"));
        if (hit.collider == null)
            return;

        Debug.Log("HIT " + hit.collider.name);
        target = hit.collider.GetComponent<IRemoteControlable>();
        if (target == null)
            return;

        Debug.Log("object found!");
        isChangingChannel = true;
    }
}
