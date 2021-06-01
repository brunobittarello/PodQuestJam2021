using UnityEngine;

public class MovingBehaviour : MonoBehaviour
{
    const float SPEED = 2.5f;
    const float ANIMATION_SPEED = 0.2f;

    public Sprite[] sprites;

    public GameObject[] Route;

    public float WaitOnRoutePoint = 0f;

    public bool CanMove = true;

    SpriteRenderer sprRenderer;

    Vector3 lastMovement;
    float animationTimer;
    int movingSpriteIndex;

    private float m_WaitUntilMove = 0f;
    private int m_NextRoutePointIndex;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        sprRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove && Time.time >= m_WaitUntilMove)
        {
            Move();
        }
    }

    void Move()
    {
        if (Route == null || Route.Length < 1)
        {
            return;
        }

        Vector3 movement = Vector3.zero;

        Vector3 to = Route[m_NextRoutePointIndex].transform.position;

        Vector3 from = this.transform.position;

        float distance = Vector3.Distance(from, to);

        if (distance > 0.1f)
        {
            Vector2 delta = to - from;

            if (delta.x < 0)
                movement += Vector3.left;
            if (delta.x > 0)
                movement += Vector3.right;
            if (delta.y < 0)
                movement += Vector3.down;
            if (delta.y > 0)
                movement += Vector3.up;


            Move(movement);
        }
        else
        {
            this.transform.position = to;
            Animate(movement);

            m_NextRoutePointIndex++;

            if (WaitOnRoutePoint > 0)
            {
                m_WaitUntilMove = Time.time + WaitOnRoutePoint;
            }

            if (m_NextRoutePointIndex == Route.Length)
            {
                m_NextRoutePointIndex = 0;
            }
        }

    }

    void Move(Vector3 deltaMovement)
    {
        this.transform.position = this.transform.position + deltaMovement * Time.deltaTime * SPEED;
        Animate(deltaMovement);
        lastMovement = deltaMovement;
    }

    void Animate(Vector3 deltaMovement)
    {
        if (deltaMovement != Vector3.zero)
        {
            animationTimer += Time.deltaTime;
            if (animationTimer > ANIMATION_SPEED)
            {
                sprRenderer.sprite = GetMovingSprite(deltaMovement);
                animationTimer = 0;
            }
            return;
        }
        else if (lastMovement == Vector3.zero)
            return;

        sprRenderer.sprite = GetIdleSprite(lastMovement);
        animationTimer = ANIMATION_SPEED;
    }

    protected virtual Sprite GetMovingSprite(Vector3 direction)
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

    protected virtual Sprite GetIdleSprite(Vector3 direction)
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

}
