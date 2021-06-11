using UnityEngine;
using System.Collections;

public class ObjectSwitcherTutorialBehaviour : ObjectSwitcherBehaviour
{
    public int correctChannel;
    public bool isRight;
    public ParticleSystem funfair;
    public bool playMusic;

    public override bool ChangeChannel(int channel, out bool disconnect)
    {
        disconnect = true;
        if (isRight) return true;

        if (!base.ChangeChannel(channel, out disconnect))
            return false;

        isRight = disconnect = channel == correctChannel;

        if (isRight)
            collider2d.enabled = false;            
        return true;
    }

    protected override void LoadChannel()
    {
        base.LoadChannel();
        if (isRight && funfair != null)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Ray/RayFunfair", transform.position);
            funfair.Play();
            if (playMusic) { 
                var fmodInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Gameplay");
                fmodInstance.start();
            }
        }
    }
}
