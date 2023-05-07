using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerControl p_script;

    void Start()
    {
        p_script = GetComponentInParent<PlayerControl>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Ground" || other.gameObject.name == "SandPlatform")
        {
            p_script.p_grounded = true;

            if (p_script.p_collected_Jump)
            {
                p_script.p_extra_jump = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Ground" || other.gameObject.name == "SandPlatform")
        {
            p_script.p_grounded = false;
        }
    }
}
