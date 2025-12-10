using System.Collections.Generic;
using UnityEngine;

public class capacitorPower : MonoBehaviour
{
    [SerializeField] private GameObject[] itemList;
    public bool isPowered;

    public GameManager gm;

    // Track which LED GameObjects we've already counted so we only increment once per LED
    

    private void Update()
    {
        if (isPowered == true)
        {
            foreach (GameObject item in itemList)
            {

                //Checks tag and changes wire colors
                if (item.CompareTag("Wire"))
                {
                    SpriteRenderer srWire = item.GetComponent<SpriteRenderer>();
                    srWire.color = Color.yellow;
                }

                //same for the LED
                if (item.CompareTag("LED"))
                {
                    SpriteRenderer srLED = item.GetComponent<SpriteRenderer>();
                    srLED.color = Color.green;
                    if (gm != null && !gm.countedLEDs.Contains(item))
                    {
                        gm.lightsOn++;
                        gm.countedLEDs.Add(item);
                    }
                }
            }
        }
        else
        {
            foreach (GameObject item in itemList)
            {
                // Changes it back
                if (item.CompareTag("Wire"))
                {
                    SpriteRenderer srWire = item.GetComponent<SpriteRenderer>();
                    srWire.color = Color.gray;
                }
                //Same for the LED
                if (item.CompareTag("LED"))
                {
                    SpriteRenderer srLED = item.GetComponent<SpriteRenderer>();
                    srLED.color = Color.white;

                    //This only happens when the Game manager script is not null
                    if (gm != null && gm.countedLEDs.Contains(item))
                    {
                        gm.lightsOn--;
                        gm.countedLEDs.Remove(item);

                        // safety: prevent negative counts
                        if (gm.lightsOn < 0)
                            gm.lightsOn = 0;
                    }
                }
            }
        }
    }
}
