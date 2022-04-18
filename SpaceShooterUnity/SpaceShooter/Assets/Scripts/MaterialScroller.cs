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

public class MaterialScroller : MonoBehaviour
{

    /***VARIABLES***/
    public Vector2 scrollSpeed = new Vector2(0, 0f); //x and y speed for scroll

    private Renderer goRenderer; //game object's renderer component
    private Material goMat; //the game object's material

    private Vector2 offset; //the offset of scroll

    // Start is called before the first frame update
    private void Start()
    {
        goRenderer = GetComponent<Renderer>();
        goMat = goRenderer.material;
    } //end Start()

    // Update is called once per frame
    void Update()
    {
        offset = (scrollSpeed * Time.deltaTime);
        goMat.mainTextureOffset += offset;
    }// end Update()
}
