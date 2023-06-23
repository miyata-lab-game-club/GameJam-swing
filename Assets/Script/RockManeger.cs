using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManeger : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 200f;

    [SerializeField] private GameObject breakEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject eff = Instantiate(breakEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
