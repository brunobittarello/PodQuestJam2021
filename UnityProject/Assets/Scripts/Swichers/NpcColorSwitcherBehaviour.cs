using UnityEngine;

public class NpcColorSwitcherBehaviour : MonoBehaviour, IRemoteControlable
{
    MovingBehaviour m_Npc;
    SpriteRenderer m_SpriteRenderer;
    public int Channel { get; private set; }

    void Start()
    {
        m_Npc = GetComponent<MovingBehaviour>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayerTargetStart()
    {
        m_Npc.CanMove = false;

    }

    public void PlayerTargetExit()
    {
        m_Npc.CanMove = true;
    }

    public bool ChangeChannel(int channel, out bool disconnect)
    {
        disconnect = false;
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Ray/RayStatic", transform.position);
        if (channel < 1)
            disconnect = true;
        else
        {
            Channel = channel;
            m_SpriteRenderer.color = RemoteControllerUIEffect.ColorByChannel(channel);
        }
        return true;
    }
}
