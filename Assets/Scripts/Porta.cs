using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public static bool chavePorta = false;
    private bool InTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerController1.hasKey)
        {
            InTrigger = true;
        }
        
    }

    void Update()
    {
        if (InTrigger) {
            var newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, -90, 0.0f), Time.deltaTime * 200);
            transform.rotation = newRot;
        }
    }

    void OnGUI()
    {
        if (InTrigger)
        {
            if (chavePorta)
            {
                GUI.Box(new Rect(0, 0, 500, 25), "Aperte E para abrir");
            }

            else
            {
                GUI.Box(new Rect(0, 0, 200, 25), "Você precisa da chave!");
            }
                
        }
    }

}
