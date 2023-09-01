using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterRenderer : MonoBehaviour
{
    public static readonly string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public static readonly string[] runDirections = {"Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE"};

    Animator animator;
    int lastDirection;
    
    private void Awake()
    {
        //cache the animator component
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
