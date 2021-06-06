using UnityEngine;

public class DropBehaviour : MonoBehaviour
{
    public ParticleSystem Funfair;
 
    public GameObject ObjectToDrop;

    public bool Dropped = false;

    [SerializeField]
    ReadyBehaviour[] triggers;

    void Update()
    {
        if (Dropped)
            return;

        for (int i = 0; i < triggers.Length; i++)
            if (!triggers[i].IsReady())
                return;

        Drop();
    }

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
            Instantiate(ObjectToDrop, Vector3Int.RoundToInt(this.transform.position), Quaternion.identity);
        }
    }
}
