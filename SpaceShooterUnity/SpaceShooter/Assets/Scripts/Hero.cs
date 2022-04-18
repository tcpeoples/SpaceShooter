/**** 
 * Created by: Akram Taghavi-Burris
 * Date Created: March 16, 2022
 * 
 * Last Edited by: Tyrese Peoples
 * Last Edited: April 11, 2022
 * 
 * Description: Hero ship controller
****/

/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase] //forces selection of parent object
public class Hero : MonoBehaviour
{
    /*** VARIABLES ***/

    #region PlayerShip Singleton
    static public Hero SHIP; //refence GameManager


   
    //Check to make sure only one gm of the GameManager is in the scene
    void CheckSHIPIsInScene()
    {

        //Check if instnace is null
        if (SHIP == null)
        {
            SHIP = this; //set SHIP to this game object
        }
        else //else if SHIP is not null send an error
        {
            Debug.LogError("Hero.Awake() - Attempeeted to assign second Hero.SHIP");
        }
    }//end CheckGameManagerIsInScene()
    #endregion

    GameManager gm; //reference to game manager

    ObjectPool pool;

    [Header("Ship Movement")]
    public float speed = 10;
    public float rollMult = -45;
    public float pitchMult = 30;



    [Space(10)]

    [Header("Projectile Settings")]
    //public GameObject projectilePrefab;
    public float projectileSpeed = 40;
    public AudioClip projectSound; //sound clip of projectile
    private AudioSource audioSource; // the audio source of the game object

    private GameObject lastTriggerGo; //reference to the last triggering game object
   
    [SerializeField] //show in inspector
    private float _shieldLevel = 1; //level for shields
    public int maxShield = 4; //maximum shield level
    
    //method that acts as a field (property), if the property falls below zero the game object is desotryed
    public float shieldLevel
    {
        get { return (_shieldLevel); }
        set
        {
            _shieldLevel = Mathf.Min(value, maxShield); //Min returns the smallest of the values, therby making max sheilds 4

            //if the sheild is going to be set to less than zero
            if (value < 0)
            {
                Destroy(this.gameObject);
                Debug.Log(gm.name);
                gm.LostLife();
                
            }

        }
    }

    /*** MEHTODS ***/

    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        CheckSHIPIsInScene(); //check for Hero SHIP
    }//end Awake()


    //Start is called once before the update
    private void Start()
    {
        gm = GameManager.GM; //find the game manager
        pool = ObjectPool.Pool;
        audioSource = GetComponent<AudioSource>(); //get reference to audio source
    }//end Start()



    // Update is called once per frame (page 551)
    void Update()
    {
        //player input
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        //Change the transform based on the axis
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        //Rotate the ship to make it feel more dynamic
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        //check for spacebar (fire)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();

            //if there is an audio source
            if(audioSource != null)
            {
                audioSource.PlayOneShot(projectSound); //play projectSound
            }
        }

    }//end Update()



    //Taking Damage
    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        //tranform root returens the topmost transfrom in the hierarchy (i.e. parent)
        GameObject go = rootT.gameObject; //game object of parent transform

        if (go == lastTriggerGo) 
        {
            return; 
        } //don't do anything if it's the same object we last collied with
            
        lastTriggerGo = go; //set the triger to the last trigger
        
        if (go.tag == "Enemy")
        {
            Debug.Log("Triggered by Enemy " + other.gameObject.name);
            shieldLevel--;
            Destroy(go);
        }

        else
        {
            Debug.Log("Triggered by non-Enemy " + other.gameObject.name);
        }
    }//end OnTriggerEnter()

    void FireProjectile()
    {
        GameObject projectile = pool.GetObject();
        //if there is a projectile object
        if (projectile != null)
        {
            projectile.transform.position = transform.position; //set position to the ships position
            Rigidbody rb = projectile.GetComponent<Rigidbody>(); //get rigidbody of the projectile
            rb.velocity = Vector3.up * projectileSpeed; //use velocity to move projectile
        }//end if(projectile != null)
    }

}
