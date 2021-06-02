
using UnityEngine;

class BallBehaviour : MonoBehaviour
{
    [SerializeField]
    float speed;

    Vector3 direction;

    private void Start()
    {
        direction = Vector3.down;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        this.transform.position = this.transform.position + direction * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        direction *= -1;
    }
}