using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudarArma : MonoBehaviour
{
    public int selecionarArma = 0;

    void Start()
    {
        SelecionarArma();
    }

    void Update()
    {

        int proximaArma = selecionarArma;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selecionarArma >= transform.childCount - 1)
                selecionarArma = 0;

            else
                selecionarArma++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selecionarArma <= 0)
                selecionarArma = transform.childCount - 1;
            else
            selecionarArma--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selecionarArma = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >=2)
        {
            selecionarArma = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selecionarArma = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selecionarArma = 3;
        }

        if (proximaArma != selecionarArma)
        {
            SelecionarArma();
        }
    }

    void SelecionarArma()
    {
        int i = 0;

        foreach (Transform arma in transform)
        {
            if(i == selecionarArma)
            arma.gameObject.SetActive(true);

            else
            arma.gameObject.SetActive(false);

            i++;
        }
    }
}
