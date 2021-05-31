using UnityEngine;

public class BalanceBehaviour : BaseObjectBehaviour, IReady
{
    public ParticleSystem funfair;
    public ObjectSwitcherBehaviour leftSwitch;
    public ObjectSwitcherBehaviour rightSwitch;
    public GameObject weightIndicator;

    internal float weight;
    State status;

    enum State
    {
        leftIsHeavier,
        rightIsHeavier,
        equal
    }

    void Update()
    {
        PropBalance();
    }

    void PropBalance()
    {
        var left = int.MaxValue;
        if (leftSwitch.current is PropObjectBehaviour propLeft)
            left = propLeft.Weight;

        var right = int.MinValue;
        if (rightSwitch.current is PropObjectBehaviour propRight)
            right = propRight.Weight;

        var status = Messure(left, right);

        if (this.status == status)
            return;

        this.status = status;
        UpdateWeightIndicator();
        if (status == State.equal)
            funfair?.Play();
    }

    State Messure(int leftWeight, int rightWeight)
    {
        if (leftWeight == rightWeight)
            return State.equal;
        if (leftWeight > rightWeight)
            return State.rightIsHeavier;
        return State.leftIsHeavier;
    }

    void UpdateWeightIndicator()
    {
        if (status == State.equal)
        {
            weightIndicator.gameObject.SetActive(false);
            return;
        }

        weightIndicator.gameObject.SetActive(true);
        var vertialOffset = Vector3.up * 0.2f;
        weightIndicator.transform.position = this.transform.position + Vector3.left * 0.5f * (status == State.leftIsHeavier ? -1 : 1) + vertialOffset;
    }

    public bool IsReady()
    {
        return status == State.equal;
    }
}
