using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScript : MonoBehaviour
{


    private Animator animator;

    private const string WARP_CAM_OUT_NAME = "WarpCameraOut";
    private const string WARP_CAM_IN_NAME = "WarpCameraIn";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.Play(WARP_CAM_OUT_NAME);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.Play(WARP_CAM_IN_NAME);
        }
    }


}
