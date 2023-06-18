using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewind : Ability
{
    //private bool Available;
    //public Text CountdownText;
    //public float CooldownTime;

    public float RewindTime;
    private List<Vector3> Positions = new List<Vector3>();
    public float RecordingInterval;

    public KeyCode RewindKey;
    // Start is called before the first frame update
    void Start()
    {
        Available = true;
        Positions.Add(transform.root.position );
        StartCoroutine(RecordPositions ());
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(RewindKey) && Available)
        {
            StartCoroutine(CountdownDisplay ());
            transform.root.position = Positions[0];
         
        }
    }
    private IEnumerator RecordPositions()
    {
        yield return new WaitForSeconds(RewindTime);

        while (true)
        {
            Positions.Add(transform.root.position );
            if (Positions .Count >RewindTime/RecordingInterval)
            {
                Positions.RemoveAt(0);
            }
            yield return new WaitForSeconds(RecordingInterval);

        }
    }

    
}
