using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoRoutines : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool inMotion;
    private IEnumerator MoveToPositionNoRotation(Transform o, Vector3 start, Vector3 targetLocation, float time)
    {

        inMotion = true;
        float t = 0;
        while (t < 1)
        {
            o.position = Vector3.Lerp(start, targetLocation, t);
            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        o.position = targetLocation;
        inMotion = false;
        yield return null;
    }

    private IEnumerator MoveToPositionWithRotation(Transform o, Vector3 start, Vector3 targetLocation, Quaternion startRot, Quaternion targetRot, float time)
    {

        inMotion = true;
        float t = 0;
        while (t < 1)
        {
            o.SetPositionAndRotation(Vector3.Lerp(start, targetLocation, t), Quaternion.Lerp(startRot, targetRot, t));
            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        o.SetPositionAndRotation(targetLocation, targetRot);
        inMotion = false;
        yield return null;
    }
}
