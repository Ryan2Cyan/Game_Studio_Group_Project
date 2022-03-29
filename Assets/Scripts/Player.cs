using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TDG.Entity;
using System;

public class Player : Entity
{
    private float experience;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        entityName = "Jargleblarg The Great";
        maxHealth = 100;
        currentHealth = maxHealth;
        movementSpeed = 5.0f;
        level = 0;
        classType = "Warrior";
        weapon = new Weapon();
        inventory = new Inventory();

        slider = GameObject.Find("Health bar").GetComponent<Slider>();

        slider.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Skip day");
            // SkipDay()
        }

        HighLightInteractables();

        TakeDamage();
    }

    // Currently test code to highlight shopkeeps later
    private void HighLightInteractables()
    {
        Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(transform.position + new Vector3(0,1,0), ray.direction, out hit))
        {
            var selection = hit.transform;
            var selectionRenderer = selection.GetComponent<Renderer>();
            if( selectionRenderer != null && hit.collider.gameObject.tag == "Selectable")
            {
                selectionRenderer.material = highlightMaterial;
            }
        }

        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), ray.direction * hit.distance, Color.yellow);
    }

    private void TakeDamage()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth -= 10;
            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        slider.value = currentHealth;
    }

}
