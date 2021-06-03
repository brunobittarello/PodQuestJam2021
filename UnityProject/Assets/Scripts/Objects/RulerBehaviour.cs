
using UnityEngine;

class RulerBehaviour : ReadyBehaviour
{
    public PropObjectBehaviour biggest;
    public PropObjectBehaviour lowest;
    public ObjectSwitcherBehaviour leftSwitch;
    public ObjectSwitcherBehaviour rightSwitch;

    int leftHeight;
    int rightHeight;

    void Update()
    {
        Messure();
    }

    void Messure()
    {
        leftHeight = int.MaxValue;
        if (leftSwitch.current is PropObjectBehaviour propLeft)
            leftHeight = propLeft.Height;

        rightHeight = int.MaxValue;
        if (rightSwitch.current is PropObjectBehaviour propRight)
            rightHeight = propRight.Height;
    }

    public override bool IsReady()
    {
        return biggest.Height > leftHeight && leftHeight > rightHeight && rightHeight > lowest.Height;
    }
}
