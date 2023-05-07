using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    public PlayerControl p_script;

    void Start()
    {
        p_script = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    void OnTriggerStay(Collider col)
    {
        if (p_script.p_attack_input_check)
        {
            if (col.transform.name == "Switch Holder")
            {
                if (!p_script.p_single_attack_check)
                {
                    p_script.p_single_attack_check = true;
                    Destroy(col.gameObject.transform.Find("Door").gameObject);
                    Destroy(col.gameObject);
                }
            }

            if (col.transform.name == "Big Slime" || col.transform.name == "Medium Slime" || col.transform.name == "Small Slime")
            {
                if (!p_script.p_single_attack_check)
                {
                    col.gameObject.GetComponent<SlimeScript>().slime_health -= 1;
                    p_script.p_single_attack_check = true;
                }
            }

        }


    }
}


