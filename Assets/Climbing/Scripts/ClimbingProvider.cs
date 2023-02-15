using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// We average the velocity of the controllers and move the origin via a Character Controller or transform.
/// </summary>
public class ClimbingProvider : LocomotionProvider
{
    [SerializeField] private CharacterController characterController;
    private bool isClimbing = false;
    private List<VelocityContainer> activeVelocities = new List<VelocityContainer>();
    
    protected override void Awake()
    {
        base.Awake();
        FindCharacterController();
    }

    private void FindCharacterController()
    {
        if(!characterController)
            characterController = system.xrOrigin.GetComponent<CharacterController>();
    }

    public void AddProvider(VelocityContainer provider)
    {
        if(!activeVelocities.Contains(provider))
            activeVelocities.Add(provider);
    }

    public void RemoveProvider(VelocityContainer provider)
    {
        if(activeVelocities.Contains(provider))
            activeVelocities.Remove(provider);
    }

    private void Update()
    {
        TryBeginClimb();

        if(isClimbing)
            ApplyVelocity();
        
        TryEndClimb();
    }

    private void TryBeginClimb()
    {
        if(CanClimb() && BeginLocomotion())
            isClimbing = true;
    }

    private void TryEndClimb()
    {
        if(!CanClimb() && EndLocomotion())
            isClimbing = false;
    }

    private bool CanClimb()
    {
        return activeVelocities.Count != 0;
    }

    private void ApplyVelocity()
    {
        Vector3 velocity = CollectControllerVelocity();
        Transform origin = system.xrOrigin.transform;

        velocity = origin.TransformDirection(velocity);
        velocity *= Time.deltaTime;

        if(characterController){
            characterController.Move(-velocity);
        }else{
            origin.position -= velocity;
        }

    }

    private Vector3 CollectControllerVelocity()
    {
        Vector3 totalVelocity = Vector3.zero;

        foreach(VelocityContainer container in activeVelocities)
            totalVelocity += container.Velocity;

        return totalVelocity;
    }
}
