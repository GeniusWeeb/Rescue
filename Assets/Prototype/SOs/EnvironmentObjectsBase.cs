
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnvironmentObjectsBase : ScriptableObject
{
    //TODO : should change this to addressable
    [SerializeField] private GameObject spawnEffect;
    [SerializeField] private GameObject despawnEffect;
    [SerializeField] private string thisObjectInfoText;
    private List<Action> fireEvents;
  

    //I would like object to have its own behavior stored here 
    // A Cmmon base class can be define for object class
    // which can be both interactable and an objects   
    


}
  