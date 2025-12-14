using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lightsOn = 0;
    public int neededLights;
    public Rigidbody2D CPUrb;
    public HashSet<GameObject> countedLEDs = new HashSet<GameObject>();
    public float pushForce = 200f;

    public GameObject cpuSocketTxt;
    public GameObject hintTxt;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip pop;

    [SerializeField] float pitchVariance = 0.05f;


    private void Start()
    {
        // Starts the game with a static CPU
        CPUrb.bodyType = RigidbodyType2D.Static;

        // Turns off the cpu socket text on start

        if (cpuSocketTxt != null)
        { 
            cpuSocketTxt.SetActive(false);
        }
    }
    private void Update()
    {
        if(lightsOn >= neededLights)
        {
            // Changes the CPU rb type from Static to Dynamic
            CPUrb.bodyType = RigidbodyType2D.Dynamic;

            // manages text when the 
            if (hintTxt != null)
            {
                hintTxt.SetActive(false);
            }

            if (cpuSocketTxt != null)
            { 
                cpuSocketTxt.SetActive(true);
            }

            // Shoots the cpu away
            Vector2 impulse = Random.insideUnitCircle.normalized * Random.Range(pushForce * 0.5f, pushForce);
            CPUrb.AddForce(impulse, ForceMode2D.Impulse);

            float randomPitch = Random.Range(1f - pitchVariance, 1f + pitchVariance);

            source.pitch = randomPitch;
            source.PlayOneShot(pop);

            //adds one to the neededlights, because else you can shoot the CPU away multiple times
            neededLights++;
        }
    }
}
