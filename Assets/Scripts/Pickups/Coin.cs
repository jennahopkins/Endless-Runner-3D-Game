using UnityEngine;

public class Coin : Pickup
{
    [SerializeField] int scoreAmount = 100;
    [SerializeField] AudioSource coinSound;
    ScoreManager scoreManager;

    public void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }

    protected override void OnPickup()
    {
        // increase score and play sound on pickup
        scoreManager.IncreaseScore(scoreAmount);
        AudioSource.PlayClipAtPoint(coinSound.clip, transform.position);
    }
}
