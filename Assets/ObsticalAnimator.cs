using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticalAnimator : MonoBehaviour
{

    private Animator animator;
    private Renderer renderer;

    private const string WARP_OUT_NAME = "WarpOut";
    private const string WARP_IN_NAME = "WarpIn";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.Play(WARP_OUT_NAME);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.Play(WARP_IN_NAME);
        }
    }

    public void Disable()
    {
        renderer.enabled = false;
    }
    public void Enable()
    {
        renderer.enabled = true;
    }
}
