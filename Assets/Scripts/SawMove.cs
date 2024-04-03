using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SawMove : MonoBehaviour
{
    [SerializeField] GameObject firstPoint;
    [SerializeField] GameObject secondPoint;
    [SerializeField] float speed;
    [SerializeField] float startWaitTime;
    Vector3 targetPoint;
    float waitTime;
    bool isInFirstPoint;

    void Start()
    {
        isInFirstPoint = true;
        targetPoint = secondPoint.transform.position;
        waitTime = startWaitTime;
    }
    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint) < 0.2f)
        {
            if (waitTime <= 0)
            {
                isInFirstPoint = !isInFirstPoint;
                if (isInFirstPoint)
                    targetPoint = firstPoint.transform.position;
                else
                    targetPoint = secondPoint.transform.position;
                waitTime = startWaitTime;
            }
            else
                waitTime -= Time.deltaTime;
        }
    }
}
