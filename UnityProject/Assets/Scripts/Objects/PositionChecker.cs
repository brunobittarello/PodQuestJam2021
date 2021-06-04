
using UnityEngine;

class PositionChecker : ReadyBehaviour
{
    [SerializeField]
    ObjectSwitcherBehaviour[] props;
    [SerializeField]
    Vector2Int[] positions;
    [SerializeField]
    int[] channels;

    bool isRight;

    void Update()
    {
        isRight = CheckPositions();
    }

    bool CheckPositions()
    {
        for (int i = 0; i < props.Length; i++)
            if (channels[i] != props[i].CurrentChannel || positions[i] != Vector2Int.RoundToInt((Vector2)props[i].transform.position))
                return false;
        return true;
    }

    public override bool IsReady()
    {
        return isRight;
    }
}
