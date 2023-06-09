using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flint : MonoBehaviour
{
    bool following = false;
    Vector3 offset; int attempts, maxAttempts;
   public GameObject fire; bool cooling = false;
    AudioSource aud; public AudioClip flint;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        //set max attempts to num between 2 and 7
        maxAttempts = Random.Range(2, 7);
        //lock cursor on screen 
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {

        //click and drag stuff
        if (!Input.GetMouseButton(0))
        {
            following = false;
            offset = Vector3.zero;
        }
            if (following)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x + offset.x, mousePos.y + offset.y, gameObject.transform.position.z); ;
            return;
        }
        
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                following = true;
                offset = gameObject.transform.position - (Vector3)mousePos;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("on trigga");
        if(other.gameObject.name == "strikeZone" && !cooling)
        {
            StartCoroutine(cooldown());
            //play sound
            aud.PlayOneShot(flint);
            attempts++;
            if(attempts == maxAttempts)
            {
                //play fire lighting sound
                fire.GetComponent<FireController>().enableFire();
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator cooldown()
    {
        cooling = true;
        //wait 1.5 sec then set cooldown to false
        yield return new WaitForSeconds(0.5f);
        cooling = false;
    }
}

