using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] RawImage _rawImage;
    [SerializeField] float xCoor;
    [SerializeField] float yCoor;

    // Update is called once per frame
    void Update()
    {
        _rawImage.uvRect = new Rect(_rawImage.uvRect.position + new Vector2(xCoor, yCoor) * Time.deltaTime, _rawImage.uvRect.size);
    }
}
