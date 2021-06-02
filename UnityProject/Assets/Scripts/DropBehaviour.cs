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
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Ray/RayFunfair", transform.position);
            }
            Dropped = true;
            Instantiate(ObjectToDrop, this.transform.position, Quaternion.identity);
        }
    }
}
