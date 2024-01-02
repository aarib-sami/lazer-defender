using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.5f;
    Material myMaterial;
    Vector2 offset;
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
