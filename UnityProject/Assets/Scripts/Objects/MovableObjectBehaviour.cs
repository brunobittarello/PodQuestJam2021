using UnityEngine;

public class MovableObjectBehaviour : MonoBehaviour
{
    const float SPEED = 4f;

    void Update()
    {
        Move();
    }

    void Move()
    {
        this.transform.position = this.transform.position + Vector3.up * Time.deltaTime * SPEED;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("RemoteControl"))
        {
            collision.collider.gameObject.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
}
