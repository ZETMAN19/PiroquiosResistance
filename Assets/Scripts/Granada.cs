using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granada : MonoBehaviour {

    float delay = 3f;
    float radius = 50f;
    float forca = 20f;
    public float danoG = 10f;

    public GameObject efeitoBooom;
    public float efeitpBoom_End = 3f;

    float contagemReg;
    bool posExplo = false;

    // Use this for initialization
    void Start () {
        contagemReg = delay;
    }
	
	// Update is called once per frame
	void Update () {

        contagemReg -= Time.deltaTime;

        if (contagemReg <= 0f && !posExplo)
        {
            Boom();
            //Damage();
            posExplo = true;
        }
	}

    void Boom()
    {
        Instantiate(efeitoBooom, transform.position, transform.rotation);

        Collider[] collidersDestruido = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in collidersDestruido)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(forca, transform.position, radius);
            }

            PlayerController1 player = nearbyObject.GetComponent<PlayerController1>();
            if (player != null)
            {
                player.TakeDamagePlayer(danoG);
            }
        }

        Destroy(gameObject);
    }
}
