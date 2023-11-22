using UnityEngine;
using UnityEngine.InputSystem;

public class NpcVrInteraction : MonoBehaviour
{
    public GameManager gameManager;
    private NpcCollision _npcCollision;
    private NpcInteraction _npcInteraction;

    // Input actions
    [SerializeField] private InputActionReference startRecordAudioActionReference;
    [SerializeField] private InputActionReference stopRecordAudioActionReference;

    
    private void Start()
    {
        // Get references to the NpcCollision and NpcInteraction scripts
        _npcCollision = gameObject.GetComponent<NpcCollision>();
        _npcInteraction = gameObject.GetComponent<NpcInteraction>();

        // Only enable the input actions if dialogue options are NOT enabled
        if (!gameManager.enableDialogueOptions)
        {
            EnableInputActions();
        }
        else
        {
            DisableInputActions();
        }
    }

    // Enable the input actions
    private void EnableInputActions()
    {
        startRecordAudioActionReference.action.Enable();
        startRecordAudioActionReference.action.performed += OnRecordAudioStarted;
        stopRecordAudioActionReference.action.Enable();
        stopRecordAudioActionReference.action.performed += OnRecordAudioStopped;
    }

    // Disable the input actions
    private void DisableInputActions()
    {
        startRecordAudioActionReference.action.Disable();
        startRecordAudioActionReference.action.performed -= OnRecordAudioStarted;
        stopRecordAudioActionReference.action.Disable();
        stopRecordAudioActionReference.action.performed -= OnRecordAudioStopped;
    }

    // Action for starting to record audio
    private void OnRecordAudioStarted(InputAction.CallbackContext context)
    {
            //Debug.Log("OnRecordAudioStarted called. Context: " + context);
        
            // Only start recording if not already recording
            if (_npcCollision.IsPlayerInProximity() && !_npcInteraction.isRecording)
            {
                _npcInteraction.StartRecording();
            }
    }

    // Action for stopping record audio
    private void OnRecordAudioStopped(InputAction.CallbackContext context)
    {
            //Debug.Log("OnRecordAudioStopped called. Context: " + context);

            // Only stop recording if currently recording
            if (_npcInteraction.isRecording)
            {
                _npcInteraction.EndRecording();
            }
    }
    
}
