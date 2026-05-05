using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaXRHeadBlocking : MonoBehaviour
{

    [SerializeField] public GameObject player = null;
    [SerializeField] private LayerMask _collisionLayers = 1 << 0;
    [SerializeField] private float _collisionRadius = 0.2f;

    private Vector3 prevHeadPos;

    private void Start()
    {
        prevHeadPos = transform.position;
    }

    private bool DetectHit(Vector3 loc)
    {
        Collider[] objs = new Collider[10];
        int size = Physics.OverlapSphereNonAlloc(loc, _collisionRadius, objs,
                   _collisionLayers, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < size; i++)
        {
            if (objs[i].tag != "Player")
            {
                return true;
            }
        }
        return false;
    }

    public void Update()
    {
        if (player != null)
        {

            bool collision = DetectHit(transform.position);

            // No collision
            if (!collision) prevHeadPos = transform.position;

            // Collision
            else
            {
                Vector3 headDiff = transform.position - prevHeadPos;
                Vector3 adjHeadPos = new Vector3(player.transform.position.x - headDiff.x,
                                                 player.transform.position.y,
                                                 player.transform.position.z - headDiff.z);
                player.transform.SetPositionAndRotation(adjHeadPos, player.transform.rotation);
            }
        }
    }
}

/*
 * 
 * CREDIT: https://github.com/MetaAnomie/GameResources/blob/main/UnityScripts/MetaXRHeadBlocking.cs
 * 
 */