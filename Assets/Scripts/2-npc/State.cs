using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    // a base class which each state needs to implment RunCurrentState() function 
    public abstract State RunCurrentState();
}
