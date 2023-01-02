using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.MenuSection
{
	// Token: 0x02000045 RID: 69
	public class BookQueryPopup : MonoBehaviour
	{
		// Token: 0x0600021A RID: 538 RVA: 0x0000771E File Offset: 0x0000591E
		private void Awake()
		{
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007720 File Offset: 0x00005920
		private void Start()
		{
			PlayerActions playerActions = Game.Instance.PlayerActions;
			PlayerActions.MenuActionsActions menuActions = playerActions.MenuActions;
			this._submitAction = playerActions.UI.Submit;
			this._restartAction = menuActions.BookRestartAction;
			this._cancelAction = playerActions.UI.Cancel;
			this.UpdateContinueText();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000777C File Offset: 0x0000597C
		private void OnEnable()
		{
			this._continueButton.onClick.AddListener(new UnityAction(this.ExecuteContinue));
			this._restartButton.onClick.AddListener(new UnityAction(this.ExecuteRestart));
			this._cancelButton.onClick.AddListener(new UnityAction(this.ExecuteCancel));
			this.UpdateContinueText();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000077E4 File Offset: 0x000059E4
		private void OnDisable()
		{
			this._continueButton.onClick.RemoveListener(new UnityAction(this.ExecuteContinue));
			this._restartButton.onClick.RemoveListener(new UnityAction(this.ExecuteRestart));
			this._cancelButton.onClick.RemoveListener(new UnityAction(this.ExecuteCancel));
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007845 File Offset: 0x00005A45
		private void Update()
		{
			if (this._submitAction.WasPressedThisFrame())
			{
				this.ExecuteContinue();
				return;
			}
			if (this._restartAction.WasPressedThisFrame())
			{
				this.ExecuteRestart();
				return;
			}
			if (this._cancelAction.WasPressedThisFrame())
			{
				this.ExecuteCancel();
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007882 File Offset: 0x00005A82
		private void ExecuteContinue()
		{
			Action onContinue = this.OnContinue;
			if (onContinue == null)
			{
				return;
			}
			onContinue.Invoke();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007894 File Offset: 0x00005A94
		private void ExecuteRestart()
		{
			Action onRestart = this.OnRestart;
			if (onRestart == null)
			{
				return;
			}
			onRestart.Invoke();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000078A6 File Offset: 0x00005AA6
		private void ExecuteCancel()
		{
			Action onCancel = this.OnCancel;
			if (onCancel == null)
			{
				return;
			}
			onCancel.Invoke();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000078B8 File Offset: 0x00005AB8
		private void UpdateContinueText()
		{
			LocalizedString localizedString = LocalizedText.CreateLocalizedString("ui_menu_book_query_continue");
			localizedString.Add("CHAPTER", new StringVariable
			{
				Value = Helper.ToRoman(Game.Instance.ActiveBookSessionChapter + 1)
			});
			this._continueText.SetReference(localizedString);
		}

		// Token: 0x0400019B RID: 411
		public Action OnContinue;

		// Token: 0x0400019C RID: 412
		public Action OnRestart;

		// Token: 0x0400019D RID: 413
		public Action OnCancel;

		// Token: 0x0400019E RID: 414
		[SerializeField]
		private Button _continueButton;

		// Token: 0x0400019F RID: 415
		[SerializeField]
		private Button _restartButton;

		// Token: 0x040001A0 RID: 416
		[SerializeField]
		private Button _cancelButton;

		// Token: 0x040001A1 RID: 417
		[SerializeField]
		private LocalizedText _continueText;

		// Token: 0x040001A2 RID: 418
		private InputAction _submitAction;

		// Token: 0x040001A3 RID: 419
		private InputAction _restartAction;

		// Token: 0x040001A4 RID: 420
		private InputAction _cancelAction;
	}
}
