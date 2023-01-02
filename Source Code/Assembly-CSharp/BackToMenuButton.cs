using System;
using TaleWorlds.CompanionBook.ScreenSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000006 RID: 6
	public class BackToMenuButton : MonoBehaviour
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002509 File Offset: 0x00000709
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002511 File Offset: 0x00000711
		public bool IsBindingBlocked
		{
			get
			{
				return this._isBindingBlocked;
			}
			set
			{
				if (value != this._isBindingBlocked)
				{
					this._isBindingBlocked = value;
					this._bindingCanvasGroup.alpha = (value ? 0f : 1f);
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002540 File Offset: 0x00000740
		private void Awake()
		{
			this._cancelAction = Game.Instance.PlayerActions.UI.Cancel;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000256A File Offset: 0x0000076A
		private void OnEnable()
		{
			this._button.onClick.AddListener(new UnityAction(this.ExecuteBackToMenu));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002588 File Offset: 0x00000788
		private void OnDisable()
		{
			this._button.onClick.RemoveListener(new UnityAction(this.ExecuteBackToMenu));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025A6 File Offset: 0x000007A6
		public void CheckActions()
		{
			if (this._cancelAction.WasPressedThisFrame())
			{
				this.ExecuteBackToMenu();
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025BB File Offset: 0x000007BB
		private void ExecuteBackToMenu()
		{
			Game.Instance.AudioManager.PlayAudioClip(this._clip);
			Game.Instance.ScreenManager.SwitchToScreen(ScreenType.MainMenu, Array.Empty<object>());
		}

		// Token: 0x0400001B RID: 27
		[SerializeField]
		private CanvasGroup _bindingCanvasGroup;

		// Token: 0x0400001C RID: 28
		[SerializeField]
		private AudioClip _clip;

		// Token: 0x0400001D RID: 29
		[SerializeField]
		private Button _button;

		// Token: 0x0400001E RID: 30
		private InputAction _cancelAction;

		// Token: 0x0400001F RID: 31
		private bool _isBindingBlocked;
	}
}
