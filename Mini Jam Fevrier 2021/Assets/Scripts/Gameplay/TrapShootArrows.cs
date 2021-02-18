﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapShootArrows : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public bool isFiring = true;
    public float countdown = 7.5f;

    public Transform[] waypoints;
    private Transform target;

    public float speed;
    private int destPoint;

    public TrapShootBonusBloc trapShootBonusBloc;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // Si l'ennemi est quasiment arrivé à sa destination
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];

        }

        if (isFiring == true && trapShootBonusBloc.isActive)
        {
            StartCoroutine(Shoot());
            isFiring = false;
        }
    }

    IEnumerator Shoot()
    {
        // shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //AudioManager.instance.PlaySFX(4);
        yield return new WaitForSeconds(countdown);
        isFiring = true;
    }
}