
using UnityEngine;

public class InfraredRayEffect : MonoBehaviour
{
    [SerializeField]
    float speed;

    LineRenderer line;
    Vector3 startPoint;
    Vector3 midPoint;
    Vector3 endPoint;

    private void Awake()
    {
        line = this.gameObject.GetComponent<LineRenderer>();
    }

    void Update()
    {
        startPoint = Vector3.MoveTowards(startPoint, endPoint, speed * Time.deltaTime);
        if (midPoint != endPoint)
            midPoint = Vector3.MoveTowards(midPoint, endPoint, speed * Time.deltaTime);

        line.SetPosition(0, startPoint);
        line.SetPosition(1, midPoint);

        if (Vector3.SqrMagnitude(endPoint - startPoint) < 0.2)
            this.gameObject.SetActive(false);

        if (Vector3.SqrMagnitude(endPoint - midPoint) < 0.2)
            midPoint = endPoint;
    }

    public void Show(Vector3 startPoint, Vector3 endPoint)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        this.midPoint = Vector3.Lerp(startPoint, endPoint, 0.4f);
        //line.SetPositions(new Vector3[2]{ startPoint, endPoint });
        this.gameObject.SetActive(true);
    }
}
