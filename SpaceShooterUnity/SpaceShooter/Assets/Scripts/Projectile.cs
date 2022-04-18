/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 23, 2022
 * 
 * Last Edited by: Tyrese Peoples
 * Last Edited: April 6, 2022
 * 
 * Description: Behavior for projectile
****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //VARIABLES
    private BoundsCheck bndCheck;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bndCheck.offUp)
        {
            gameObject.SetActive(false);
            bndCheck.offUp = false;
        }
    }// end Update()

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }// end Awake()
}
