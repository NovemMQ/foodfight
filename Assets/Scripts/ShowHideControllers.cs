using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR.Avatars.Controllers;
using Liminal.SDK.VR.Pointers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideControllers : MonoBehaviour
{
    public static ShowHideControllers Instance;

    public VRAvatarController PrimaryController;
    public VRAvatarController SecondaryController;
    public bool ShowControllers;
    public EDisplayLaserPointer ShowPrimaryLaserPointer;
    public EDisplayLaserPointer ShowSecondaryLaserPointer;

    private VRControllerVisual _primaryVisual;
    private VRControllerVisual _secondaryVisual;
    private LaserPointerVisual _primaryLaserVisual;
    private LaserPointerVisual _secondaryLaserVisual;
    private GameObject _primaryModel;
    private GameObject _secondaryModel;
    private float _baseMaxDistance;

    public enum EDisplayLaserPointer 
    {
        Never,
        Always,
        OnPointerHit,
    }

    public void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (_primaryVisual == null)
        {
            _primaryVisual = PrimaryController.GetComponentInChildren<VRControllerVisual>();
            return;
        }

        if (_secondaryVisual == null)
        {
            _secondaryVisual = SecondaryController.GetComponentInChildren<VRControllerVisual>();
            return;
        }

        if (_primaryVisual != null)
        {
            UpdateLaserPointerVisuals(ref _primaryVisual, ref _primaryLaserVisual, ShowPrimaryLaserPointer);
            UpdateControllerModelVisibility(ref _primaryModel, ref _primaryVisual);
        }

        if (_secondaryVisual != null) 
        {
            UpdateLaserPointerVisuals(ref _secondaryVisual, ref _secondaryLaserVisual, ShowSecondaryLaserPointer);
            UpdateControllerModelVisibility(ref _secondaryModel, ref _secondaryVisual);
        }
    }

    private void UpdateLaserPointerVisuals(ref VRControllerVisual controllerVisual, ref LaserPointerVisual pointerVisual, EDisplayLaserPointer showPointer)
    {
        if (controllerVisual == null || controllerVisual.PointerVisual == null)
            return;

        if (pointerVisual == null)
        {
            pointerVisual = controllerVisual?.PointerVisual?.Pointer?.Transform.GetComponent<LaserPointerVisual>();
            _baseMaxDistance = pointerVisual.MaxDistance;
            return;
        }

        if (showPointer == EDisplayLaserPointer.Always && pointerVisual.MaxDistance != _baseMaxDistance)
        {
            pointerVisual.MaxDistance = _baseMaxDistance;
            return;
        }
        else if (showPointer == EDisplayLaserPointer.Never && pointerVisual.MaxDistance != 0)
        {
            pointerVisual.MaxDistance = 0;
            return;
        }
        else if (showPointer == EDisplayLaserPointer.OnPointerHit)
        {
            if (controllerVisual.PointerVisual.Pointer.CurrentRaycastResult.gameObject == null)
                pointerVisual.MaxDistance = 0;
            else
                pointerVisual.MaxDistance = _baseMaxDistance;
        }
    }

    private void UpdateControllerModelVisibility(ref GameObject model, ref VRControllerVisual controllerVisual)
    {
        if (model == null)
        {
            foreach (Transform child in controllerVisual.transform)
            {
                if (!child.gameObject.activeSelf || child.gameObject == controllerVisual?.PointerVisual?.transform.gameObject)
                    continue;

                model = child.gameObject;
            }

            return;
        }

        model.SetActive(ShowControllers);
    }

    public void SetShowControllers(bool state)
    {
        ShowControllers = state;
    }
}
