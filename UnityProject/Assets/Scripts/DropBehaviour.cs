using UnityEngine;

public class DropBehaviour : MonoBehaviour
{
    public ParticleSystem Funfair;
 
    public GameObject ObjectToDrop;

    public bool Dropped = false;

    public void Drop()
    {
        if (!Dropped && ObjectToDrop != null)
        {
            if (Funfair != null)
            {
                Funfair.Play();
            }
            Dropped = true;
            Instantiate(ObjectToDrop, this.transform.position, Quaternion.identity);
        }
    }
}
