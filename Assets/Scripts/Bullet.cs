/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage = 2;

    // Update is called once per frame
    void Update()
    {
        RaycastHit pontoDeColisao;
        if (Physics.Raycast(transform.position, transform.forward, out pontoDeColisao))
        {
            if (pontoDeColisao.transform.gameObject.tag == "Enemy")
            {
                pontoDeColisao.transform.gameObject.GetComponent<PlayerController>().DamagePlayer(damage);
            }
        }
    }
}
*/