using UnityEngine;

public abstract class BaseObjectBehaviour : MonoBehaviour
{
    internal virtual void OnPlayerTargetStart() { }
    internal virtual void OnPlayerTargetExit() { }
    internal virtual void OnPlaced() { }
}
