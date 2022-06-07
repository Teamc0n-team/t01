using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSystem : MonoBehaviour
{
    [SerializeField]
    public GameObject[] WayPoint;

    [SerializeField]
    public Transform[] Parts;
    public int PosInx = 0;


    [SerializeField]
    private int WayPointInx = 0;

    public float minDist;
    public float Speed;

    public bool Rand = false;
    public bool go = false;

    void Start()
    {

        foreach (var Part in Parts)
        {
            Part.rotation.SetEulerRotation(0, 90, 0);
        }
    }
    void MoveParts()
    {
        float sqrDistance = Mathf.Sqrt(80f);
        Vector3 PrevPos = transform.position;
        
        Debug.Log(sqrDistance);

        foreach (var Part in Parts)
        {
            if ((Part.position - PrevPos).sqrMagnitude > sqrDistance)
            {
                Part.LookAt(PrevPos);
                Vector3 currentPos = Part.position;
                Quaternion CurrentRotation = Part.rotation;
                Part.position = Vector3.Lerp(Part.position, PrevPos, Time.deltaTime * Speed);
                PrevPos = currentPos;
            }
            else
            {
                break;
            }
        }
    }
    void Move()
    {
        MoveParts();

        gameObject.transform.LookAt(WayPoint[WayPointInx].transform.position);
        gameObject.transform.position += gameObject.transform.forward * Speed * Time.deltaTime;
    }

    private void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, WayPoint[WayPointInx].transform.position);

        if (go)
        {
            if (dist > minDist)
            {
                Move();
            }
            else
            {
                if (!Rand)
                {
                    if (WayPointInx + 1 == WayPoint.Length)
                    {
                        WayPointInx = 0;
                    }
                    else
                    {
                        WayPointInx++;
                    }
                }
                else
                {
                    WayPointInx = Random.Range(0, WayPoint.Length);
                }
            }
        }
    }

    // Update is called once per frame
}
