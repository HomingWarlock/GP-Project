using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAction : MonoBehaviour
{
    private MeshRenderer p_rend;
    private BoxCollider p_box;

    [SerializeField] private float s_size;
    [SerializeField] private bool s_shrinking;
    [SerializeField] private bool s_growing;

    private void Start()
    {
        p_rend = GetComponent<MeshRenderer>();
        p_box = GetComponent<BoxCollider>();

        s_size = 1;
        s_shrinking = false;
        s_growing = false;
    }

    private void Update()
    {
        if (s_shrinking && s_size > 0)
        {
            this.transform.localScale = new Vector3(5, s_size, 5);
            s_size -= Time.deltaTime;
        }
        else if (s_shrinking && s_size <= 0.05)
        {
            p_rend.enabled = false;
            p_box.enabled = false;
            s_shrinking = false;
            StartCoroutine(Respawn_Delay());
        }

        if (s_growing && s_size < 1)
        {
            this.transform.localScale = new Vector3(5, s_size, 5);
            s_size += Time.deltaTime;
        }
        else if (s_growing && s_size <= 1)
        {
            s_size = 1;
            s_growing = false;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            if (this.gameObject.name == "SandPlatform" && !s_shrinking)
            {
                s_shrinking = true;
            }
        }
    }

    private IEnumerator Respawn_Delay()
    {
        yield return new WaitForSeconds(1);
        p_rend.enabled = true;
        p_box.enabled = true;

        s_growing = true;
    }
}
