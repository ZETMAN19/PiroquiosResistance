using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BossControler : MonoBehaviour
{
    Animator anim;
    Rigidbody[] rdbs;
    public GameObject brain;
    public GameObject wayboss;
    Transform[] waysB;
    int wayindex = 1;
    NavMeshAgent agent;
    public enum Zstate { Patrol, Berserk, Attack, Dead };
    public Zstate zstate;
    public float healthEnemy = 100f;
    public ParticleSystem muzzleFlashEnemy;
    public GameObject impactEnemy;
    public GameObject weaponEnemy;
    public float damage = 10f;
    public float range = 100f;
    public SphereCollider head;
    public AudioSource audioshoot;
    public float forcaLancamentoGranada = 50f;
    public GameObject granadaPrefab;
    public float radius = 5f;

    float velocidade = 1f;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rdbs = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rdb in rdbs)
        {
            rdb.isKinematic = true;
        }
        waysB = wayboss.GetComponentsInChildren<Transform>();

        wayindex = Random.Range(1, waysB.Length);
        agent.SetDestination(waysB[wayindex].position);

    }


    void FixedUpdate()
    {
        switch (zstate)
        {
            case (Zstate.Patrol):
                Patrol();
                break;
            case (Zstate.Berserk):
                Berserk();
                break;
            case (Zstate.Attack):
                Attack();
                break;
            case (Zstate.Dead):

                break;

        }
    }
    void Patrol()
    {
        Vector3 dir = waysB[wayindex].position - transform.position;
        if (dir.magnitude < 3)
        {
            wayindex = Random.Range(1, waysB.Length);
            agent.SetDestination(waysB[wayindex].position);

            anim.SetFloat("Walk_IP", velocidade);
        }
    }
    void Attack()
    {
        Vector3 dir = brain.transform.position - transform.position;
        if (dir.magnitude > 2)
        {
            zstate = Zstate.Berserk;
           
        }
    }
    void Berserk()
    {
        if (brain)
        {
            agent.SetDestination(brain.transform.position);
            Vector3 dir = brain.transform.position - transform.position;
            if (dir.magnitude < 2)
            {
                zstate = Zstate.Attack;
                
            }
        }
    }

    public void KillMe()
    {
        anim.enabled = false;
        zstate = Zstate.Dead;
        agent.enabled = false;
        foreach (Rigidbody rdb in rdbs)
        {
            rdb.isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            brain = other.gameObject;
            zstate = Zstate.Berserk;
            ShootPlayer();
            audioshoot.Play();
            muzzleFlashEnemy.Play();
            LancarGranada();
        }
    }

    public void TakeDamageEnemy(float amount)
    {
        healthEnemy -= amount;
        if (healthEnemy <= 0f)
        {
            DieEnemy();
            SceneManager.LoadScene("Menu");
        }
    }

    void DieEnemy()
    {
        Destroy(gameObject);
    }

    void ShootPlayer()
    {

        muzzleFlashEnemy.Play();

        RaycastHit hit;
        if (Physics.Raycast(head.transform.position, head.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            PlayerController1 targetP = hit.transform.GetComponent<PlayerController1>();

            if (targetP != null)
            {
                targetP.TakeDamagePlayer(damage);
            }

            if (damage <= 0 )
            {
                SceneManager.LoadScene("Game_1");
            }

            GameObject impactGO = Instantiate(impactEnemy, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.5f);
        }
    }

    void LancarGranada()
    {
        RaycastHit hit;
        if (Physics.SphereCast(head.transform.position, range, head.transform.forward, out hit))
        {

            GameObject granada = Instantiate(granadaPrefab, transform.position, transform.rotation);
            Rigidbody rb = granada.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * forcaLancamentoGranada, ForceMode.VelocityChange);

            GameObject impactGO = Instantiate(impactEnemy, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.5f);
        }
    }
  
}