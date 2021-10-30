using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private AudioClip clickSFX;

    public void PlayClickSFX()
    {
        SoundManager.Instance.PlaySound(clickSFX);
    }
}
