using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeScript : MonoBehaviour
{
    private Renderer renderer; 
    public Material white;
    public Material blue;
    public Material green;
    public Material yellow;
    public Material red;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        renderer.material = blue;
    }

    private void OnMouseExit()
    {
        renderer.material= white;
    }

    
}
