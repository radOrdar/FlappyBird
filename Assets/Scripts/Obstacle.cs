using System;
using DefaultNamespace;
using UnityEngine;

public class Obstacle : MonoBehaviour, IPooled<Obstacle> {

    [SerializeField] private Transform pipeDown;
    [SerializeField] private Transform pipeUp;
    [SerializeField] private float gap;
    [SerializeField] private float lifeTime = 3;
    [SerializeField] private float speed;

    public IPool<Obstacle> OriginFactory { get; set; }
    
    private bool isCheckedOut;
    private float elapsedTime;

    private void OnValidate() {
        gap = Mathf.Abs(gap);
        pipeDown.localPosition = -Vector2.up * gap/2;
        pipeUp.localPosition = Vector2.up * gap/2;
    }

    private void Update()
    {
        if (Game.Instance.IsGameOver) { return; }
        
        elapsedTime += Time.deltaTime;
        if (elapsedTime > lifeTime)
        {
            OriginFactory.Reclaim(this);
        }
        
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (isCheckedOut) { return; }
        if (Physics2D.Raycast((Vector2)pipeUp.position + Vector2.right * transform.localScale.x/2, Vector2.down, gap - Mathf.Epsilon)) {
            Game.Instance.CheckPointEnter();
            AudioManager.Instance.PlaySound(AudioManager.ClipName.Point);
            isCheckedOut = true;
        }
    }
    
    public void OnClaim()
    {
        elapsedTime = 0;
        isCheckedOut = false;
    }
}