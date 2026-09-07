using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterSoundScript : ClearCounterSoundScript {
    [SerializeField] private AudioClip sliceSFX;

    protected override void Start() {
        base.Start();
        counterEventBus.OnKitchenObjectSliced += PlaySliceSFX;
    }
    private void PlaySliceSFX(object o, EventArgs args) {
        AudioSource.PlayClipAtPoint(sliceSFX, transform.position, volume);
    }
}
