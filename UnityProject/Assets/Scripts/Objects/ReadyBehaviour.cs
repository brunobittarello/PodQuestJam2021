
using UnityEngine;

abstract class ReadyBehaviour : MonoBehaviour, IReady
{
    public abstract bool IsReady();
}
