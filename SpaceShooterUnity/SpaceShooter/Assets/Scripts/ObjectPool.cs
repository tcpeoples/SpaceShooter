/**** 
 * Created by: Akram Taghavi-Burris
 * Date Created: April 6 2022
 * 
 * Last Edited by: Tyrese Peoples
 * Last Edited: April 11, 2022
 * 
 * Description: 
****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //VARIABLES
    static public ObjectPool Pool;
    #region Pool Singleton

    void CheckPoolIsInScene()
    {
        if(Pool == null)
        {
            Pool = this;
        }
        else
        {
            Debug.LogError("Pool.Awake() - attempted to assign a second ObjectPool.Pool");

        }
    } // end CheckPoolIsInScene()

    #endregion

    private Queue<GameObject> projectiles = new Queue<GameObject>();

    [Header("Pool Settings")]
    public GameObject projectilePrefab;
    public int poolStartSize = 5;

    private void Awake()
    {
        CheckPoolIsInScene();
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < poolStartSize; i++)
        {
            GameObject projectileGO = Instantiate(projectilePrefab);
            projectiles.Enqueue(projectileGO);
            projectileGO.SetActive(false);
        }
    }


    public GameObject GetObject()
    {
        if(projectiles.Count > 0)
        {
            GameObject gObject = projectiles.Dequeue();
            gObject.SetActive(true); //enable
            return gObject; //return object
        }
        else
        {
            Debug.LogWarning("Out of objects, reloading...");
            return null; //return null
        }//end if(projecitles.Count > 0)
    }// end GetObject()

    public void ReturnObject(GameObject gObject)
    {
        projectiles.Enqueue(gObject);
        gObject.SetActive(false);
    }
}
