using System;
using Script.Character;
using Script.Character.Movement;
using UnityEngine;
using UnityEngine.UI;

public class MovementUIScript : MonoBehaviour
{
    [SerializeField]
    private Button _leftArrow;
    [SerializeField]
    private Button _rightArrow;
    [SerializeField]
    private Button _upArrow;
    [SerializeField]
    private Button _downButton;
    [SerializeField]
    private Button _shootButton;
    [SerializeField]
    private PlayerInput _playerInput;
    [SerializeField]
    private CharacterShoot _playerShoot;
    
    private void Start()
    {
        SetupButton(_leftArrow, _playerInput.MoveLeft, () => _playerInput.StopMoveLeft());
        SetupButton(_rightArrow, _playerInput.MoveRight, () => _playerInput.StopMoveRight());
        SetupButton(_upArrow, _playerInput.MoveUp, () => _playerInput.StopMoveUp());
        SetupButton(_downButton, _playerInput.MoveDown, () => _playerInput.StopMoveDown());
        SetupButton(_shootButton, _playerShoot.Shoot, () => {});
    }

    private void SetupButton(Button button, Action onPress, Action onRelease)
    {
        if (button == null) return;

        var handler = button.gameObject.AddComponent<UIButtonPressHandler>();
        handler.OnPress = onPress;
        handler.OnRelease = onRelease;
    }
}
