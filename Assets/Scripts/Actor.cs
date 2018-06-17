using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor
    : MonoBehaviour
{
    public ActorModel actorModel = null;


    private void Start()
    {
        actorModel = new ActorModel();
    }
}
