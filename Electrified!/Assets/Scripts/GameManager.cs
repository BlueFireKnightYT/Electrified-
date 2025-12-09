using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lightsOn = 0;
    public int neededLights;
    public Rigidbody2D CPUrb;
    public HashSet<GameObject> countedLEDs = new HashSet<GameObject>();
    public float pushForce = 200f;

    private void Start()
    {
        CPUrb.bodyType = RigidbodyType2D.Static;
    }
    private void Update()
    {
        if(lightsOn >= neededLights)
        {
            CPUrb.bodyType = RigidbodyType2D.Dynamic;

            
            Vector2 impulse = Random.insideUnitCircle.normalized * Random.Range(pushForce * 0.5f, pushForce);
            CPUrb.AddForce(impulse, ForceMode2D.Impulse);
            neededLights++;
        }
    }
}
