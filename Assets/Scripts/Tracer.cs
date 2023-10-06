using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Tracer : NetworkBehaviour
{
    private TrailRenderer trail;
    public void Travel(Vector3 destination)
    {
        StartCoroutine(TravelRoutine(destination));
    }
    IEnumerator TravelRoutine(Vector3 destination)
    {
        Vector3 origin = trail.transform.position;
        float time = 0;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(origin, destination, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        trail.transform.position = destination;

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Awake()
    {
        trail = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
