using UnityEngine;

public interface IBoostable
{
    public void StartBooster();

    public void CancelBoost();

    public void ActivateTimer();

    public void DeactivateTimer();

    public void StopBoost();

    public void ContinueBoost();

    public Sprite GetBoosterImage();

    public string GetBoosterDescription();
}
