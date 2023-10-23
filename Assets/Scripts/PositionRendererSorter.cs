using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    [SerializeField]
    private int sortingOrderBase = 5000;
    [SerializeField]
    private float offset = 0;
    [SerializeField]
    private SpriteRenderer[] myRenderer;

    [SerializeField]
    private bool RunOnce = true;
    

    void Awake()
    {
        if(myRenderer.Length == 0)
        {
            myRenderer = new SpriteRenderer[1];
            myRenderer[0] = gameObject.GetComponent<SpriteRenderer>();

        }
    }

    void LateUpdate()
    {
        foreach(SpriteRenderer renderer in myRenderer)
        {
            renderer.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset);
        }
        if(RunOnce)
            Destroy(this);
    }
}
