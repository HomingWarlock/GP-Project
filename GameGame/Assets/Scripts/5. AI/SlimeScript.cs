using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public int slime_health;
    [SerializeField] private GameObject slime_object;
    private Transform slime_spawn1;
    private Transform slime_spawn2;
    private bool slime_single_death;

    void Awake()
    {
        slime_single_death = false;

        if (this.transform.name == "Big Slime")
        {
            slime_health = 3;
            transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            slime_spawn1 = transform.Find("SpawnPos1");
            slime_spawn2 = transform.Find("SpawnPos2");
        }
    }

    void Update()
    {
        if (slime_health <= 0 && !slime_single_death)
        {
            slime_single_death = true;

            if (this.transform.name == "Big Slime")
            {
                GameObject smaller_slime1 = Instantiate(slime_object, new Vector3(slime_spawn1.position.x, slime_spawn1.position.y, slime_spawn1.position.z), Quaternion.identity) as GameObject;
                SlimeScript smaller_script1 = smaller_slime1.GetComponent<SlimeScript>() as SlimeScript;
                smaller_slime1.transform.name = "Medium Slime";
                smaller_slime1.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                smaller_script1.slime_health = 2;
                smaller_script1.slime_spawn1 = smaller_script1.transform.Find("SpawnPos1");
                smaller_script1.slime_spawn2 = smaller_script1.transform.Find("SpawnPos2");

                GameObject smaller_slime2 = Instantiate(slime_object, new Vector3(slime_spawn2.position.x, slime_spawn2.position.y, slime_spawn2.position.z), Quaternion.identity) as GameObject;
                SlimeScript smaller_script2 = smaller_slime2.GetComponent<SlimeScript>() as SlimeScript;
                smaller_slime2.transform.name = "Medium Slime";
                smaller_slime2.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                smaller_script2.slime_health = 2;
                smaller_script2.slime_spawn2 = smaller_script2.transform.Find("SpawnPos1");
                smaller_script2.slime_spawn2 = smaller_script2.transform.Find("SpawnPos2");

                Destroy(this.gameObject);
            }

            if (this.transform.name == "Medium Slime")
            {
                GameObject smaller_slime1 = Instantiate(slime_object, new Vector3(slime_spawn1.position.x, slime_spawn1.position.y, slime_spawn1.position.z), Quaternion.identity) as GameObject;
                SlimeScript smaller_script1 = smaller_slime1.GetComponent<SlimeScript>() as SlimeScript;
                smaller_slime1.transform.name = "Small Slime";
                smaller_slime1.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                smaller_script1.slime_health = 1;

                GameObject smaller_slime2 = Instantiate(slime_object, new Vector3(slime_spawn2.position.x, slime_spawn2.position.y, slime_spawn2.position.z), Quaternion.identity) as GameObject;
                SlimeScript smaller_script2 = smaller_slime2.GetComponent<SlimeScript>() as SlimeScript;
                smaller_slime2.transform.name = "Small Slime";
                smaller_slime2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                smaller_script2.slime_health = 1;

                Destroy(this.gameObject);
            }

            if (this.transform.name == "Small Slime")
            {
                Destroy(this.gameObject);
            }
        }
    }


}
