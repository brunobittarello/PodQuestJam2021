
using UnityEngine;

class BallLauncherBehaviour : TouchableObjectBehaviour
{
    [SerializeField]
    BallBehaviour ball;

    protected override void OnPlayerContact(Vector2Int dir)
    {
        ball?.Restart();
    }

    protected override Vector2 ReferencePosition() => this.transform.position;
}
