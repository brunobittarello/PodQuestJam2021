
using UnityEngine;

class BallBehaviour : MonoBehaviour
{
    const float COLISION_COOLDOWN = 0.4f;

    [SerializeField]
    float speed;

    Collider2D colider2d;
    Vector3Int direction;
    Vector3Int startPos;
    float timer;

    private void Start()
    {
        colider2d = this.gameObject.GetComponent<Collider2D>();
        direction = Vector3Int.down;
        startPos = Vector3Int.RoundToInt(this.transform.position);
    }

    void Update()
    {
        Move();
        ColisionCooldown();
    }

    internal void Restart()
    {
        colider2d.enabled = true;
        direction = Vector3Int.down;
        transform.position = startPos;
    }

    void Move()
    {
        this.transform.position = this.transform.position + (Vector3)direction * speed * Time.deltaTime;
    }

    void ColisionCooldown()
    {
        if (timer == 0)
            return;

        timer = Mathf.Clamp(timer - Time.deltaTime, 0, COLISION_COOLDOWN);

        if (timer == 0)
            colider2d.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Contains("Tapume"))
        {
            direction *= -1;
            return;
        }

        direction = Do90Turn(!collision.gameObject.name.Contains("1"));
        this.transform.position = Vector3Int.RoundToInt(collision.transform.position);
        timer = COLISION_COOLDOWN;
        //colider2d.enabled = false;
    }

    Vector3Int Do90Turn(bool inverse)
    {
        if (direction == Vector3Int.down)
            return inverse ? Vector3Int.left : Vector3Int.right;
        if (direction == Vector3Int.left)
            return inverse ? Vector3Int.down : Vector3Int.up;
        if (direction == Vector3Int.up)
            return inverse ? Vector3Int.right : Vector3Int.left;
        
        return inverse ? Vector3Int.up : Vector3Int.down;
    }
}