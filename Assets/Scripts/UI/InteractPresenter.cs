using TMPro;
using UnityEngine;

public class InteractPresenter : MonoBehaviour
{
    public string InteractText = "Press E to Interact";
    public string MeowText = "Press E to Meow";
    public string DropText = "Press E to Drop";
    private TMP_Text _textElement;
    
    public PlayerInteractionCheck _playerInteractionCheck;

    private void Awake()
    {
        _textElement = GetComponent<TMP_Text>();
    }
    
    void Start()
    {
        if (_playerInteractionCheck == null)
        {
            _playerInteractionCheck = FindObjectOfType<PlayerInteractionCheck>();
        }
        _playerInteractionCheck.OnInteractUpdated += UpdateText;
    }

    private void UpdateText(InteractMode obj)
    {
        _textElement.text = obj switch
        {
            InteractMode.CanMeow => MeowText,
            InteractMode.CanInteract => InteractText,
            InteractMode.CanDrop => DropText,
            _ => _textElement.text
        };
    }
}
