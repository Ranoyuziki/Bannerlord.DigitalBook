using System;
using System.Collections.Generic;
using TaleWorlds.CompanionBook.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.MenuSection.Options
{
	// Token: 0x0200004C RID: 76
	public class OptionsPanel : MonoBehaviour
	{
		// Token: 0x06000261 RID: 609 RVA: 0x000089A4 File Offset: 0x00006BA4
		private void Awake()
		{
			this._optionsManager = Game.Instance.OptionsManager;
			PlayerActions playerActions = Game.Instance.PlayerActions;
			this._doneAction = playerActions.MenuActions.OptionsConfirmAction;
			this._resetAction = playerActions.MenuActions.OptionsResetAction;
			this._cancelAction = playerActions.UI.Cancel;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00008A08 File Offset: 0x00006C08
		private void OnEnable()
		{
			this._doneButton.onClick.AddListener(new UnityAction(this.ExecuteDone));
			this._resetButton.onClick.AddListener(new UnityAction(this.ExecuteReset));
			this._cancelButton.onClick.AddListener(new UnityAction(this.ExecuteCancel));
			this.AddItems();
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Combine(instance.OnControlSchemeChanged, new Action(this.RefreshNavigation));
			this.RefreshNavigation();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00008A9C File Offset: 0x00006C9C
		private void OnDisable()
		{
			this.DestroyAllItems();
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Remove(instance.OnControlSchemeChanged, new Action(this.RefreshNavigation));
			this._doneButton.onClick.RemoveListener(new UnityAction(this.ExecuteDone));
			this._resetButton.onClick.RemoveListener(new UnityAction(this.ExecuteReset));
			this._cancelButton.onClick.RemoveListener(new UnityAction(this.ExecuteCancel));
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00008B2C File Offset: 0x00006D2C
		private void Update()
		{
			this._navigationStatusProvider.Update();
			if (this._doneAction.WasPressedThisFrame())
			{
				this.ExecuteDone();
				return;
			}
			if (this._resetAction.WasPressedThisFrame())
			{
				this.ExecuteReset();
				return;
			}
			if (this._cancelAction.WasPressedThisFrame() && !this._navigationStatusProvider.IsOnDropdownItem && !this._navigationStatusProvider.WasOnDropdownItemPrevFrame)
			{
				this.ExecuteCancel();
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00008B9C File Offset: 0x00006D9C
		private void AddItems()
		{
			this.DestroyAllItems();
			foreach (OptionBase optionBase in this._optionsManager.GetAvailableOptions())
			{
				NumericOption numericOption = optionBase as NumericOption;
				if (numericOption != null)
				{
					NumericOptionItem numericOptionItem = Object.Instantiate<NumericOptionItem>(this._numericOptionPrefab, this._optionItemsParent);
					numericOptionItem.Initialize(numericOption);
					this._items.Add(numericOptionItem);
				}
				else
				{
					SelectionOption selectionOption = optionBase as SelectionOption;
					if (selectionOption != null)
					{
						SelectionOptionItem selectionOptionItem = Object.Instantiate<SelectionOptionItem>(this._selectionOptionPrefab, this._optionItemsParent);
						selectionOptionItem.Initialize(selectionOption);
						this._items.Add(selectionOptionItem);
					}
				}
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00008C54 File Offset: 0x00006E54
		private void RefreshNavigation()
		{
			if (Game.Instance.IsCurrentSchemeGamepad && this._items.Count > 0 && this._items[0].Selectable != null)
			{
				EventSystem.current.SetSelectedGameObject(this._items[0].Selectable.gameObject);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00008CB4 File Offset: 0x00006EB4
		private void DestroyAllItems()
		{
			this._items.Clear();
			foreach (object obj in this._optionItemsParent)
			{
				Object.Destroy(((Transform)obj).gameObject);
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00008D1C File Offset: 0x00006F1C
		private void ExecuteDone()
		{
			Game.Instance.OptionsManager.SaveConfig();
			Action onClose = this.OnClose;
			if (onClose == null)
			{
				return;
			}
			onClose.Invoke();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00008D3D File Offset: 0x00006F3D
		private void ExecuteReset()
		{
			this._items.ForEach(delegate(OptionItem x)
			{
				x.ResetToDefaultValue();
			});
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00008D69 File Offset: 0x00006F69
		private void ExecuteCancel()
		{
			this._items.ForEach(delegate(OptionItem x)
			{
				x.ResetToInitialValue();
			});
			Action onClose = this.OnClose;
			if (onClose == null)
			{
				return;
			}
			onClose.Invoke();
		}

		// Token: 0x040001D7 RID: 471
		public Action OnClose;

		// Token: 0x040001D8 RID: 472
		[SerializeField]
		private NumericOptionItem _numericOptionPrefab;

		// Token: 0x040001D9 RID: 473
		[SerializeField]
		private SelectionOptionItem _selectionOptionPrefab;

		// Token: 0x040001DA RID: 474
		[SerializeField]
		private Transform _optionItemsParent;

		// Token: 0x040001DB RID: 475
		[SerializeField]
		private Button _doneButton;

		// Token: 0x040001DC RID: 476
		[SerializeField]
		private Button _resetButton;

		// Token: 0x040001DD RID: 477
		[SerializeField]
		private Button _cancelButton;

		// Token: 0x040001DE RID: 478
		private InputAction _doneAction;

		// Token: 0x040001DF RID: 479
		private InputAction _resetAction;

		// Token: 0x040001E0 RID: 480
		private InputAction _cancelAction;

		// Token: 0x040001E1 RID: 481
		private NavigationStatusProvider _navigationStatusProvider = new NavigationStatusProvider();

		// Token: 0x040001E2 RID: 482
		private OptionsManager _optionsManager;

		// Token: 0x040001E3 RID: 483
		private List<OptionItem> _items = new List<OptionItem>();
	}
}
