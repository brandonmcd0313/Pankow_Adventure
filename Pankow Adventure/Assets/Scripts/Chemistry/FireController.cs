using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    Animator anim;
    public RuntimeAnimatorController normalFire;
    public RuntimeAnimatorController WhiteFire;

    // Start is called before the first frame update
    void Start()
    {
        
        //assign anim and set to normal fire
        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = normalFire;
        //unrender object
        disableFire();
    }

    public void enableFire()
    {
        //set to red and enable render
        anim.runtimeAnimatorController = normalFire;
        GetComponent<SpriteRenderer>().enabled = true;
    }
    
    public void disableFire()
    {
        
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void colorFire(Color c)
    {
        //enable render and set to white if not already
        GetComponent<SpriteRenderer>().enabled = true;
        if (anim.runtimeAnimatorController != WhiteFire)
        {
            anim.runtimeAnimatorController = WhiteFire;
        }
        GetComponent<SpriteRenderer>().color = c;
    }
}

