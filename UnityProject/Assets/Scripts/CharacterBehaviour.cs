using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterBehaviour : MonoBehaviour
{
    const float SPEED = 2.5f;
    const float ANIMATION_SPEED = 0.07f;
    const int WALKING_NUM_SPRITES = 8;
    const float TRANSITION_TIME = 1f;
    internal const float INTRO_TIME = TRANSITION_TIME + 0.5f;

    public static CharacterBehaviour instance;
    public static bool HasAxe;

    bool isChangingChannel;
    public IRemoteControlable Target { get; private set; }
    public bool hasItem;
    internal bool HasRemoteController;

    public bool CanBreak = false;

    public Sprite[] sprites;
    public InfraredRayEffect infrared;

    SpriteRenderer sprRenderer;
    Collider2D collider2d;
    Vector2Int lastMovement;
    Vector2Int direction;
    float animationTimer;
    int movingSpriteIndex;
    Vector3 hitpoint;
    float timer;
    bool isIntro;
    public bool IsMoveEnabled { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;        
        HasRemoteController = true;
        IsMoveEnabled = false;
    }

    protected virtual void Start()
    {
        sprRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        collider2d = this.gameObject.GetComponent<Collider2D>();
        direction = lastMovement = Vector2Int.up;
        StartIntro();
    }

    void StartIntro()
    {
        isIntro = true;
        this.transform.position = new Vector2(4, -1);
        collider2d.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCheatsInputs();//TODO commment

        if (isIntro)
        {
            UpdateIntro();
            return;
        }
        if (isChangingChannel)
            ChangeChannel();
        else
            ReadInputs();
    }

    void CheckCheatsInputs()
    {
        if (Input.GetKeyDown(KeyCode.Home))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.End))
            HasAxe = !HasAxe;

        if (Input.GetKeyDown(KeyCode.PageDown))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        if (Input.GetKeyDown(KeyCode.PageUp))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void UpdateIntro()
    {
        timer += Time.deltaTime;
        if (timer < TRANSITION_TIME) return;

        Move(Vector2Int.up);
        if (timer > INTRO_TIME)
        {
            isIntro = false;
            IsMoveEnabled = true;
            collider2d.enabled = true;
        }
    }

    void ChangeChannel()
    {
        var channel = GetValidChannelInput();

        if (channel == 0)
            return;

        bool disconnect;
        if (channel < 0)
            disconnect = true;
        else if (!Target.ChangeChannel(channel, out disconnect))
            return;

        ShowInfraredRay();
        RemoteControllerUIEffect.instance?.ControllerButtonPressed(channel);
        if (disconnect)
            ClearTarget();
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
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Escape)) return -1;
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

        if (HasRemoteController && Input.GetKeyDown(KeyCode.Space))
            TryToChangeChannel();
    }

    void Move(Vector2Int deltaMovement)
    {
        if (!isIntro && !IsMoveEnabled) return;

        this.transform.position = this.transform.position + (Vector3)(Vector2)deltaMovement * Time.deltaTime * SPEED;
        Animate(deltaMovement);
        lastMovement = deltaMovement;
    }

    void Animate(Vector2Int deltaMovement)
    {
        if (deltaMovement != Vector2Int.zero)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Character/Steps", transform.position);
            direction = deltaMovement;
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
        if (movingSpriteIndex == WALKING_NUM_SPRITES)
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
        return sprites[WALKING_NUM_SPRITES * offset + 3 + movingSpriteIndex];
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
        var hit = Physics2D.Raycast(this.transform.position, direction, float.PositiveInfinity, LayerMask.GetMask("RemoteControl"));
        if (hit.collider == null)
            return;

        Debug.Log("HIT " + hit.collider.name);
        Target = hit.collider.GetComponent<IRemoteControlable>();
        if (Target == null)
            return;

        Debug.Log("object found!");
        Target.PlayerTargetStart();
        hitpoint = hit.point;
        RemoteControllerUIEffect.instance?.Show();
        ShowInfraredRay();
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Ray/RayEngage", transform.position);
        isChangingChannel = true;
    }

    void ClearTarget()
    {
        RemoteControllerUIEffect.instance?.Hide();
        Target.PlayerTargetExit();
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Ray/RayDisEngage", transform.position);
        Target = null;
        isChangingChannel = false;
    }

    void ShowInfraredRay()
    {
        infrared.Show(this.transform.position, hitpoint);
    }
}
