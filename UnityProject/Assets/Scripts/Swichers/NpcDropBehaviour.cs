using UnityEngine;

public class NpcDropBehaviour : MonoBehaviour, IRemoteControlable
{
    public int correctChannel = 9;

    public ParticleSystem Funfair;

    public Color[] ChannelColors;

    public GameObject ObjectToDrop;

    public bool Dropped = false;

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
        Debug.Log($"Channel#{channel}");

        if (channel == correctChannel)
        {
            if (!Dropped && ObjectToDrop)
            {
                if (Funfair != null)
                {
                    Funfair.Play();
                }
                Dropped = true;
                Instantiate(ObjectToDrop, this.transform.position, Quaternion.identity);
            }
        }
        else if (channel == 0)
        {
            // Does not change color
        }
        else
        {
            // Changes color according to channel number
            var colorIndex = (channel - 1) % ChannelColors.Length;
            m_SpriteRenderer.color = ChannelColors[colorIndex];
        }
        return true;
    }
}
