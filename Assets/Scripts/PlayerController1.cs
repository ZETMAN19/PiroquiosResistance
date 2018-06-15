using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController1 : MonoBehaviour
{
    public float damage = 10f;
    public float damageB = 15f;
    public float range = 100f;
    public CharacterController charcontroller;
    public Vector3 movement;
    public GameObject head;
    public GameObject weapon;
    float headangle;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float fireRate = 3f;
    public AudioSource audioshoot;
    public float healthPlayer = 100f;
    public GameObject granada;
    public static bool hasKey;

    private float nextTimeToFire = 0f;

    public int maxMun = 30;
    private int currentAmmo;
    public float TempoRecarga = 1f;
    private bool isReloading = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        if (currentAmmo == -1)
            currentAmmo = maxMun;
    }

    void Shoot()
    {

        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit;
        if ( Physics.Raycast(head.transform.position, head.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            EnemyControler target = hit.transform.GetComponent<EnemyControler>();
            BossControler target2 = hit.transform.GetComponent<BossControler>();

            if (target != null)
            {
                target.TakeDamageEnemy(damage);

            }
            
            else 
            {
                target2.TakeDamageEnemy(damageB);
                
            }
            
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.5f);
        }
    }

    IEnumerator Recarregar ()
    {
        isReloading = true;
        Debug.Log("Recarregando...");

        yield return new WaitForSeconds(TempoRecarga);

        currentAmmo = maxMun;
        isReloading = false;
    }

    void Control()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), -1, Input.GetAxis("Vertical"));
        /*
        if (Input.GetButton("Jump"))
        {
            movement = Vector3.up * 2;
        }
        */

        charcontroller.Move(transform.TransformDirection(movement) * 10 * Time.deltaTime);
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));


        headangle -= Input.GetAxis("Mouse Y");
        headangle = Mathf.Clamp(headangle, -50, 50);
        head.transform.localRotation = Quaternion.Euler(headangle, 0, 0);
    }

    public void TakeDamagePlayer(float amount)
    {
        healthPlayer -= amount;
        if (healthPlayer <= 0f)
        {
            DiePlayer();
            
        }
    }

    void DiePlayer()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Game_1");
    }

    void Update()
    {

        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Recarregar());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time * 11f / fireRate;
            Shoot();
            audioshoot.Play();
        }

        Control();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Granada"))
        {
            healthPlayer -= granada.GetComponent<Granada>().danoG;
            TakeDamagePlayer(damage);
        }

        if (damage <= 0)
        {
            SceneManager.LoadScene("Game_1");
        }
    }
}