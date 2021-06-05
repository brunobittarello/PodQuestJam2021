using UnityEngine;

class BallDetectorBehaviour : ReadyBehaviour
{
    bool gotBallHit;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallBehaviour>() != null)
        {
            gotBallHit = true;
            Destroy(collision.gameObject);
        }
    }

    public override bool IsReady() => gotBallHit;
}
