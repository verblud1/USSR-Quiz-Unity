using UnityEngine;
using DG.Tweening;

public class StartMenuAnimationButton : MonoBehaviour
{
    private Transform ButtonTransform;
    public float hoverScale = 1.1f
    void Start()
    {
        ButtonTransform.DOScale(new Vector3(2f,2f,2f),1f);
    }
}
