using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittlePowder : MonoBehaviour
{
    bool following; Vector2 offset; int speed;
    static Color fireColor = new Color(0, 1, 1, 1);
    // Start is called before the first frame update
    void Start()
    {
        following = true;
        speed = Random.Range(20, 60);
    }

    // Update is called once per frame
    void Update()
    {
        //if not following add roatational momentum
        if (!following)
        {
            gameObject.transform.Rotate(0, 0,speed  * Time.deltaTime);
            //if off screen destroy it
            if(transform.position.y < -10)
            {
                Destroy(gameObject);
            }
        }
        //click and drag stuff
        if (!Input.GetMouseButton(0))
        {
            if(following)
            {
                //reset velocity
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            following = false;
            offset = Vector3.zero;
            
        }
        if (following)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x + offset.x, mousePos.y + offset.y, gameObject.transform.position.z); ;
            return;
        }
        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        print("running ontrigger");
        if (col.gameObject.name == "strikeZone")
        {
            if (!GameObject.Find("fire").GetComponent<SpriteRenderer>().enabled)
            {

                print("no fire");
                return;
            }

            if (following)
            {
                print("still holding");
                return; }

            print("ran");
            GameObject.Find("fire").GetComponent<FireController>().colorFire(fireColor);
            fireColor = new Color(fireColor.r, fireColor.g - .15f, fireColor.b, 1);
            Destroy(gameObject);
        }
    }
}
