using UnityEngine;

class ColorMatcherBehaviour : ReadyBehaviour
{
    [SerializeField]
    int rightChannel;

    [SerializeField]
    bool isMached;

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var npc = collision.GetComponent<NpcColorSwitcherBehaviour>();
        if (npc == null || npc.Channel != rightChannel)
            return;

        isMached = true;
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/NPC/ColorConfirm", transform.position);
    }

    public override bool IsReady() => isMached;
}
