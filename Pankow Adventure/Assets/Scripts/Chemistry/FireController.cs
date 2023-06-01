using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    Animator anim;
    public RuntimeAnimatorController normalFire;
    public RuntimeAnimatorController WhiteFire;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {

        //assign anim and set to normal fire
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = normalFire;
        //unrender object
        disableFire();
    }

    public void enableFire()
    {
        //set to red and enable render
        anim.runtimeAnimatorController = normalFire;
        sr.enabled = true;
    }
    
    public void disableFire()
    {
        
       sr.enabled = false;
    }

    public void colorFire(Color c)
    {
        print("color fire");
        if (!sr.enabled) return;
        //if g is negative in c then burn out fire
        if (c.g < 0)
        {
            disableFire();
            return;
        }
        if (anim.runtimeAnimatorController != WhiteFire)
        {
            //first time

            anim.runtimeAnimatorController = WhiteFire;
            StartCoroutine(fadeColor(c));
        }
        else
        {

            StartCoroutine(fadeColor(sr.color, c));
        }
    }
    
    IEnumerator fadeColor(Color c)
    {
        Color init = new Color(1, 0.2313229f, 0);
        for(int i = 0; i < 100; i++)
        {
          sr.color = Color.Lerp(init, c, i/100f);
            yield return new WaitForSeconds(.005f);
        }
    }
    IEnumerator fadeColor(Color c1, Color c2)
    {
        for (int i = 0; i < 100; i++)
        {
           sr.color = Color.Lerp(c1, c2, i / 100f);
            yield return new WaitForSeconds(.005f);
        }
    }
}
    
    


