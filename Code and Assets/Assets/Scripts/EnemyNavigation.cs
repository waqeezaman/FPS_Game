using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField]
    private  List<Vector3> Nodes;

   void Awake()
    {
        foreach (Transform Child in transform)
        {
            Nodes.Add(Child.position);
        }                             
    }

    public  Vector3 GetNode()
    {
        return Nodes[Random.Range(0, Nodes.Count)];
    }
}
