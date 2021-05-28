using UnityEngine;

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
        return isRight;
    }

    protected override void LoadChannel()
    {
        base.LoadChannel();
        if (isRight && funfair != null)
            funfair.Play();
    }
}
