using System;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] Raycaster raycaster;

    private IInteractable currentInteractable;

    public void UpdateInteraction()
    {
        if(raycaster.TryGetTarget(out RaycastHit hit))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if(interactable != null)
            {
                currentInteractable = interactable;
            }

        }
        else
        {
            currentInteractable = null;
        }
    }  
    
    public void OnInteract()
    {
        currentInteractable?.Interact();
    }

    public void OnInteractRelease()
    {
        IInteractableRelease release = (IInteractableRelease)currentInteractable ;

        release?.InteractRelease();
    }
}


public class Door : InteractableObject
{
    private bool isOpen = false;

    protected override void Activate()
    {
        Toggle();
    }

    private void Toggle()
    {
        isOpen = !isOpen;
    }
}

public class Marchand : InteractableObject
{
    protected override void Activate()
    {
        OpenShop();
    }

    private void OpenShop()
    {

    }
}

public class NPC : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        StartDialogue();
    }

    private void StartDialogue()
    {
        
    }
}

public class Coffre : InteractableObject
{
    protected override void Activate()
    {
        OpenCoffre();
    }

    private void OpenCoffre()
    {

    }
}

public abstract class InteractableObject : MonoBehaviour, IInteractable, IInteractableRelease
{
    public float holdInputInSeconds = 5;

    private float timer = 0;
    private bool isHolding = false;

    protected abstract void Activate();

    void Update()
    {
        if (isHolding)
        {
            timer += Time.deltaTime;
            if(timer >= holdInputInSeconds)
            {
                Activate();
                isHolding = false;
            }
        }
    }

    public void Interact()
    {
        isHolding = true;
        timer = 0;
    }

    public void InteractRelease()
    {
        isHolding = false;
    }
}

public interface IInteractable
{
    public void Interact();
}

public interface IInteractableRelease
{
    public void InteractRelease();
}