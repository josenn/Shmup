/* This class is used for scrolling the background
 *
 * It has a set amount of scroll speed and ways to access the material
 *
 * And material offset of the object that it is attached to
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.5f;
    Material material;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        // This is how we are getting the material that is sitting on our background
        material = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // mainTextureOffset is how we get access to the main texture offset of the object's material
        // This allows us to make the texture move
        material.mainTextureOffset += (offset * Time.deltaTime);
    }
}
