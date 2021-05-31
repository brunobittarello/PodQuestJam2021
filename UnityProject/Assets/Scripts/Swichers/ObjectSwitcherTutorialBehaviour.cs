using UnityEngine;
using System.Collections;

public class ObjectSwitcherTutorialBehaviour : ObjectSwitcherBehaviour
{
    public int correctChannel;
    public bool isRight;
    public ParticleSystem funfair;

    public override bool ChangeChannel(int channel)
    {
        if (isRight) return true;
        base.ChangeChannel(channel);

        isRight = channel == correctChannel;

        if (isRight)
            collider2d.enabled = false;
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Ray/RayStatic", transform.position);
        return isRight;
    }

    protected override void LoadChannel()
    {
        base.LoadChannel();
        if (isRight && funfair != null)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Ray/RayFunfair", transform.position);
            funfair.Play();
        }
    }
}
