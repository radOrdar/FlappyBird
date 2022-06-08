using System;
using UnityEngine;

public class Ground : MonoBehaviour {

    [SerializeField] private Transform secondPard;
    [SerializeField] private float speed;
    private float xOffset;

    private void Awake() {
        float colSizeX = GetComponent<Collider2D>().bounds.size.x;
        xOffset = -colSizeX/2 * transform.localScale.x;
        secondPard.localPosition = new Vector3(colSizeX/2, secondPard.localPosition.y, secondPard.localPosition.z);
    }

    private void Update() {
        if (Game.Instance.IsGameOver) { return; }
        var curPosition = transform.localPosition;
        float newX = curPosition.x - speed * Time.deltaTime;
        if (newX < xOffset) {
            newX = 0;
        }
        
        transform.localPosition = new Vector3(newX, curPosition.y, curPosition.z);
    }
}