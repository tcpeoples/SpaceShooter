/**** 
 * Created by: Akram Taghavi-Burris
 * Date Created: April 11, 2022
 * 
 * Last Edited by: Tyrese Peoples
 * Last Edited: April 11, 2022
 * 
 * Description:
****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolReturn : MonoBehaviour
{

    private ObjectPool pool;
    // Start is called before the first frame update
    private void Start()
    {
        pool = ObjectPool.Pool;
    }// end Start()

    
    private void OnDisable()
    {
        //if pool is not empty
        if(pool != null)
        {
            pool.ReturnObject(this.gameObject); //return to pool
        } // end if

    }// end OnDisable()
}
