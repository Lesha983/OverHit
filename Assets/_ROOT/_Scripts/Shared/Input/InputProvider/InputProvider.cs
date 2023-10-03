namespace Chillplay.Input
{
    using System;
    using Drags;
    using Settings;
    using Swipes;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Zenject;

    public class InputProvider : IInitializable, IInputProvider
    {
        [Inject]
        public CustomInputSettings CustomInputSettings { get; set; }
        
        public event Action<Vector2> OnTap;

        public event Action<Vector2> OnPointerDown;
        public event Action<Vector2> OnPointerMove;
        public event Action<Vector2> OnPointerUp;

        public event Action<Drag> OnDragStart;
        public event Action<Drag> OnDrag;
        public event Action<Drag> OnDragEnd;

        public event Action<Swipe> OnSwipe;

        public Vector2 PointerPosition => Pointer.current.position.ReadValue();
        
        private InputActions input;
        private DragProcessor dragProcessor;
        private SwipeProcessor swipeProcessor;
        
        public void Initialize()
        {
            input = new InputActions();
            dragProcessor = new DragProcessor(CustomInputSettings, input.Gameplay);
            swipeProcessor = new SwipeProcessor(CustomInputSettings, dragProcessor);
            BindInputActionsEvents();
            Enable();
        }
        
        private void BindInputActionsEvents()
        {
            var actions = input.Gameplay;

            actions.Tap.performed += _ => OnTap?.Invoke(PointerPosition);

            actions.Hold.performed += _ => OnPointerDown?.Invoke(PointerPosition);
            actions.Hold.canceled += _ => OnPointerUp?.Invoke(PointerPosition);
            actions.PointerMove.performed += context => OnPointerMove?.Invoke(context.ReadValue<Vector2>());

            dragProcessor.OnDragStart += drag => OnDragStart?.Invoke(drag);
            dragProcessor.OnDrag += drag => OnDrag?.Invoke(drag);
            dragProcessor.OnDragEnd += drag => OnDragEnd?.Invoke(drag);

            swipeProcessor.OnSwipe += swipe => OnSwipe?.Invoke(swipe);
        }
        
        public void Enable()
        {
            input.Enable();
        }

        public void Disable()
        {
            input.Disable();
        }
    }
}