using UnityEngine;

public class NpcColorSwitcherBehaviour : MonoBehaviour, IRemoteControlable
{
    public Color[] ChannelColors;

    private MovingBehaviour m_Npc;
    private SpriteRenderer m_SpriteRenderer;

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

    public bool ChangeChannel(int channel)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Ray/RayStatic", transform.position);
        if (channel != 0)
        {
            var colorIndex = (channel - 1) % ChannelColors.Length;
            m_SpriteRenderer.color = ChannelColors[colorIndex];
        }
        return true;
    }
}
