using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounterSoundScript : ClearCounterSoundScript {
    [SerializeField] private AudioSource failSFX;
    [SerializeField] private AudioSource successSFX;
    private DeliveryCounterEventBus deliveryCounterEventBus;
    protected override void Awake() {
        base.Awake();
        deliveryCounterEventBus = counterEventBus as DeliveryCounterEventBus;

    }
    protected override void Start() {
        base.Start();
        deliveryCounterEventBus.OnSuccessOrder += PlaySuccessSFX;
        deliveryCounterEventBus.OnFailOrder += PlayFailSFX;
    }
    protected override void OnDisable() {
        base.OnDisable();
        deliveryCounterEventBus.OnSuccessOrder -= PlaySuccessSFX;
        deliveryCounterEventBus.OnFailOrder -= PlayFailSFX;
    }
    private void PlayFailSFX(object o, EventArgs args) {
        AudioSource.PlayClipAtPoint(failSFX.clip, transform.position, volume);
        Debug.Log("FAIL SOUND");
    }
    private void PlaySuccessSFX(object o, EventArgs args) {
        AudioSource.PlayClipAtPoint(successSFX.clip, transform.position, volume);
        Debug.Log("SUCCESS SOUND");
    }
}
