using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingReciver : MonoBehaviour
{
    //arraylist of objects that are in the mixing area
    public ArrayList mixingItems = new ArrayList();
    
    public void addItem(string name)
    {
        //add item to mixingItems
        mixingItems.Add(name);

        //go through every item and do what is nessecary
        foreach (string itemname in mixingItems)
        {
            //do something

            //example! check for colors and mix them
            Color currentColor = gameObject.GetComponent<SpriteRenderer>().color;
            float lerpAmount = 0.5f; // You can adjust this value to control the amount of mixing

            switch (itemname)
            {
                case "red":
                    Color redColor = Color.red;
                    Color mixedRed = Color.Lerp(currentColor, redColor, lerpAmount);
                    gameObject.GetComponent<SpriteRenderer>().color = mixedRed;
                    print("red");
                    break;

                case "blue":
                    Color blueColor = Color.blue;
                    Color mixedBlue = Color.Lerp(currentColor, blueColor, lerpAmount);
                    gameObject.GetComponent<SpriteRenderer>().color = mixedBlue;
                    print("blue");
                    break;

                case "yellow":
                    Color yellowColor = Color.yellow;
                    Color mixedYellow = Color.Lerp(currentColor, yellowColor, lerpAmount);
                    gameObject.GetComponent<SpriteRenderer>().color = mixedYellow;
                    print("yellow");
                    break;

                case "green":
                    Color greenColor = Color.green;
                    Color mixedGreen = Color.Lerp(currentColor, greenColor, lerpAmount);
                    gameObject.GetComponent<SpriteRenderer>().color = mixedGreen;
                    print("green");
                    break;

                case "purple":
                    Color purpleColor = Color.magenta;
                    Color mixedPurple = Color.Lerp(currentColor, purpleColor, lerpAmount);
                    gameObject.GetComponent<SpriteRenderer>().color = mixedPurple;
                    print("purple");
                    break;

                case "orange":
                    Color orangeColor = Color.Lerp(Color.red, Color.yellow, 0.5f); // Mixing red and yellow equally
                    Color mixedOrange = Color.Lerp(currentColor, orangeColor, lerpAmount);
                    gameObject.GetComponent<SpriteRenderer>().color = mixedOrange;
                    print("orange");
                    break;
            }


        }
    }
}

