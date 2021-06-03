
using UnityEngine;

class PositionChecker : ReadyBehaviour
{
    [SerializeField]
    PropObjectBehaviour[] props;
    [SerializeField]
    Vector2Int[] positions;
    bool isRight;

    void Update()
    {
        isRight = CheckPositions();
    }

    bool CheckPositions()
    {
        for (int i = 0; i < props.Length; i++)
            if (positions[i] != Vector2Int.RoundToInt((Vector2)props[i].transform.position))
                return false;
        return true;
    }

    public override bool IsReady()
    {
        return isRight;
    }
}
