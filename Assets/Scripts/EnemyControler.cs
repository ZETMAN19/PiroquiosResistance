using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyControler : MonoBehaviour
{
    Animator anim;
    Rigidbody[] rdbs;
    public GameObject brain;
    public GameObject waypoints;
    Transform[] ways;
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

    int walkStateHash = Animator.StringToHash("Base Layer.Walk");


    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rdbs = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rdb in rdbs)
        {
            rdb.isKinematic = true;
        }
        ways = waypoints.GetComponentsInChildren<Transform>();

        wayindex = Random.Range(1, ways.Length);
        agent.SetDestination(ways[wayindex].position);

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
        Vector3 dir = ways[wayindex].position - transform.position;
        if (dir.magnitude < 3)
        {
            wayindex = Random.Range(1, ways.Length);
     
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

        }
    }

    public void TakeDamageEnemy(float amount)
    {
        healthEnemy -= amount;
        if (healthEnemy <= 0f)
        {
            DieEnemy();
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
}