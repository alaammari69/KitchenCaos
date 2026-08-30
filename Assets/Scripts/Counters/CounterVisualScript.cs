using System;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private CounterEventBus counterEventBus;
    [SerializeField] private GameObject selectedCounter;
    private MeshRenderer meshRenderer;
    void Awake()
    {
        counterEventBus = GetComponent<CounterEventBus>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void OnEnable()
    {
        counterEventBus.OnPlayerInRange += SwapToSelectedCounterMaterial;
        counterEventBus.OnPlayerOutOfRange += SwapToDefaultMaterial;
    }
    void OnDisable()
    {
        counterEventBus.OnPlayerInRange -= SwapToSelectedCounterMaterial;
        counterEventBus.OnPlayerOutOfRange -= SwapToDefaultMaterial;
    }
    void Start()
    {
        selectedCounter.SetActive(false);
    }

    private void SwapToDefaultMaterial(object o, PlayerController player)
    {
        selectedCounter.SetActive(false);
    }
    private void SwapToSelectedCounterMaterial(object o, PlayerController player)
    {
        selectedCounter.SetActive(true);
    }
}
