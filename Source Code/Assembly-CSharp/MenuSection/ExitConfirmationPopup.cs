using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.MenuSection
{
	// Token: 0x02000046 RID: 70
	public class ExitConfirmationPopup : MonoBehaviour
	{
		// Token: 0x06000224 RID: 548 RVA: 0x0000790B File Offset: 0x00005B0B
		private void Awake()
		{
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007910 File Offset: 0x00005B10
		private void Start()
		{
			PlayerActions.UIActions ui = Game.Instance.PlayerActions.UI;
			this._submitAction = ui.Submit;
			this._cancelAction = ui.Cancel;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007947 File Offset: 0x00005B47
		private void OnEnable()
		{
			this._yesButton.onClick.AddListener(new UnityAction(this.ExecuteYes));
			this._noButton.onClick.AddListener(new UnityAction(this.ExecuteNo));
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007981 File Offset: 0x00005B81
		private void OnDisable()
		{
			this._yesButton.onClick.RemoveListener(new UnityAction(this.ExecuteYes));
			this._noButton.onClick.RemoveListener(new UnityAction(this.ExecuteNo));
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000079BB File Offset: 0x00005BBB
		private void Update()
		{
			if (this._submitAction.WasPressedThisFrame())
			{
				this.ExecuteYes();
				return;
			}
			if (this._cancelAction.WasPressedThisFrame())
			{
				this.ExecuteNo();
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000079E4 File Offset: 0x00005BE4
		private void ExecuteYes()
		{
			Action onConfirm = this.OnConfirm;
			if (onConfirm == null)
			{
				return;
			}
			onConfirm.Invoke();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000079F6 File Offset: 0x00005BF6
		private void ExecuteNo()
		{
			Action onCancel = this.OnCancel;
			if (onCancel == null)
			{
				return;
			}
			onCancel.Invoke();
		}

		// Token: 0x040001A5 RID: 421
		public Action OnConfirm;

		// Token: 0x040001A6 RID: 422
		public Action OnCancel;

		// Token: 0x040001A7 RID: 423
		[SerializeField]
		private Button _yesButton;

		// Token: 0x040001A8 RID: 424
		[SerializeField]
		private Button _noButton;

		// Token: 0x040001A9 RID: 425
		private InputAction _submitAction;

		// Token: 0x040001AA RID: 426
		private InputAction _cancelAction;
	}
}
