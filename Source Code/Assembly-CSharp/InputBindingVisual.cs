using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000011 RID: 17
	[RequireComponent(typeof(Image))]
	public class InputBindingVisual : MonoBehaviour
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003590 File Offset: 0x00001790
		private void Awake()
		{
			this._image = base.GetComponent<Image>();
			this.ChangeVisibility(false);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000035A5 File Offset: 0x000017A5
		private void Start()
		{
			this.Refresh();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000035B0 File Offset: 0x000017B0
		private void OnEnable()
		{
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Combine(instance.OnControlSchemeChanged, new Action(this.OnControlSchemeChanged));
			Game instance2 = Game.Instance;
			instance2.OnActionBindingChanged = (Action<Guid>)Delegate.Combine(instance2.OnActionBindingChanged, new Action<Guid>(this.OnActionBindingChanged));
			this.RefreshVisualID();
			this.Refresh();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003618 File Offset: 0x00001818
		private void OnDisable()
		{
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Remove(instance.OnControlSchemeChanged, new Action(this.OnControlSchemeChanged));
			Game instance2 = Game.Instance;
			instance2.OnActionBindingChanged = (Action<Guid>)Delegate.Remove(instance2.OnActionBindingChanged, new Action<Guid>(this.OnActionBindingChanged));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003671 File Offset: 0x00001871
		private void OnControlSchemeChanged()
		{
			this.Refresh();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000367C File Offset: 0x0000187C
		private void Refresh()
		{
			PlayerInput playerInput = (Game.Instance != null) ? Game.Instance.PlayerInput : null;
			if (playerInput != null && this._image != null)
			{
				PlayerActions playerActions = Game.Instance.PlayerActions;
				string currentControlScheme = playerInput.currentControlScheme;
				if (currentControlScheme == playerActions.KeyboardMouseScheme.name || currentControlScheme == playerActions.GamepadScheme.name)
				{
					string text = (currentControlScheme == playerActions.KeyboardMouseScheme.name) ? this._keyboardMouseVisualID : this._gamepadVisualID;
					Sprite sprite = Game.Instance.ResourceProvider.BindingsAtlas.GetSprite(text);
					this._image.sprite = sprite;
					this.ChangeVisibility(sprite != null);
				}
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003758 File Offset: 0x00001958
		private static string GetVisualID(string inputBindingPath)
		{
			string text = string.Empty;
			if (!string.IsNullOrEmpty(inputBindingPath) && inputBindingPath.Length > 2)
			{
				string text2 = inputBindingPath.Replace('/', '_');
				int num = text2.IndexOf("<");
				int num2 = text2.IndexOf(">");
				if (num == 0 && num < num2)
				{
					string text3 = text2.Substring(num + 1, num2 - num - 1);
					string text4 = text2.Substring(num2 + 1);
					text = text3 + text4;
					if (Application.platform == 38 || Application.platform == 25)
					{
						if (Application.platform == 25 && (text == "Gamepad_select" || text == "Gamepad_start"))
						{
							text += "_ps4";
						}
						else
						{
							text += "_ps";
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003821 File Offset: 0x00001A21
		private void ChangeVisibility(bool isVisible)
		{
			this._image.enabled = isVisible;
			if (this._iconToControl != null)
			{
				this._iconToControl.enabled = isVisible;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000384C File Offset: 0x00001A4C
		private void RefreshVisualID()
		{
			InputAction inputAction = Game.Instance.PlayerInput.actions.FindAction(this._action.action.id);
			if (inputAction != null)
			{
				InputBinding inputBinding = Enumerable.FirstOrDefault<InputBinding>(inputAction.bindings, (InputBinding x) => x.id.ToString() == this._keyboardMouseBinding);
				InputBinding inputBinding2 = Enumerable.FirstOrDefault<InputBinding>(inputAction.bindings, (InputBinding x) => x.id.ToString() == this._gamepadBinding);
				this._keyboardMouseVisualID = InputBindingVisual.GetVisualID(inputBinding.effectivePath);
				this._gamepadVisualID = InputBindingVisual.GetVisualID(inputBinding2.effectivePath);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000038DF File Offset: 0x00001ADF
		private void OnActionBindingChanged(Guid actionID)
		{
			if (this._action.action.id == actionID)
			{
				this.RefreshVisualID();
				this.Refresh();
			}
		}

		// Token: 0x04000053 RID: 83
		[SerializeField]
		private Image _iconToControl;

		// Token: 0x04000054 RID: 84
		[SerializeField]
		private InputActionReference _action;

		// Token: 0x04000055 RID: 85
		[SerializeField]
		private string _keyboardMouseBinding;

		// Token: 0x04000056 RID: 86
		[SerializeField]
		private string _gamepadBinding;

		// Token: 0x04000057 RID: 87
		private Image _image;

		// Token: 0x04000058 RID: 88
		private string _keyboardMouseVisualID = string.Empty;

		// Token: 0x04000059 RID: 89
		private string _gamepadVisualID = string.Empty;
	}
}
