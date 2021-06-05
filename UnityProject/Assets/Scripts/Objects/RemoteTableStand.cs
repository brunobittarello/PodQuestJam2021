
using UnityEngine;

class RemoteTableStand : TouchableObjectBehaviour
{
    void Start()
    {
        CharacterBehaviour.instance.HasRemoteController = false;
    }

    protected override void OnPlayerContact(Vector2Int dir)
    {
        if (CharacterBehaviour.instance.HasRemoteController)
            return;

        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Key/KeyGet", transform.position);
        CharacterBehaviour.instance.HasRemoteController = true;
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    protected override Vector2 ReferencePosition() => this.transform.position;
}