using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    GameObject cam;
    LineRenderer line;
    Color startc, endc;
    // Use this for initialization
    void Start()
    {
        cam = Camera.main.gameObject;
        line = GetComponent<LineRenderer>();
        if (line)
        {
            for (int i = 0; i < line.positionCount; i++)
            {

                line.SetPosition(i, new Vector3(
                    Mathf.Sin((i * 10) * Mathf.Deg2Rad),
                    0,
                    Mathf.Cos((i * 10) * Mathf.Deg2Rad)) *
                transform.position.magnitude
                );
                startc = line.startColor;
                endc = line.endColor;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = transform.position - cam.transform.position;
        transform.rotation = Quaternion.LookRotation(dir);


        if (dir.magnitude < 1000)
        {
            transform.localScale = Vector3.zero;
            line.startColor = Color.Lerp(line.startColor, new Color(0, 0, 0, 0), Time.deltaTime);
            line.endColor = Color.Lerp(line.startColor, new Color(0, 0, 0, 0), Time.deltaTime);
        }
        else
        {
            transform.localScale = Vector3.one * (0.001f * dir.magnitude);
            line.startColor = Color.Lerp(line.startColor, startc, Time.deltaTime);
            line.endColor = Color.Lerp(line.startColor, endc, Time.deltaTime);
        }

    }
}