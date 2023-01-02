using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CompanionBook.Options;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace TaleWorlds.CompanionBook.MenuSection.Options
{
	// Token: 0x0200004D RID: 77
	public class SelectionOptionItem : OptionItem
	{
		// Token: 0x0600026C RID: 620 RVA: 0x00008DC4 File Offset: 0x00006FC4
		private void OnDestroy()
		{
			base.OnFinalize();
			this._dropdown.onValueChanged.RemoveListener(new UnityAction<int>(this.OnDropdownValueChanged));
			OptionsManager optionsManager = Game.Instance.OptionsManager;
			optionsManager.OnOptionValueChangedEvent = (Action<OptionType>)Delegate.Remove(optionsManager.OnOptionValueChangedEvent, new Action<OptionType>(this.OnOptionChanged));
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008E20 File Offset: 0x00007020
		public void Initialize(SelectionOption option)
		{
			base.OnInitialize(option, this._dropdown);
			this._selectionOption = option;
			this.RefreshItems();
			this._dropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnDropdownValueChanged));
			OptionsManager optionsManager = Game.Instance.OptionsManager;
			optionsManager.OnOptionValueChangedEvent = (Action<OptionType>)Delegate.Combine(optionsManager.OnOptionValueChangedEvent, new Action<OptionType>(this.OnOptionChanged));
			this._name.SetReference("ui_menu_options_" + option.GetOptionType().ToString().ToLower());
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008EBC File Offset: 0x000070BC
		protected override void OnOptionValueChanged(GenericOptionType optionType, float value)
		{
			this.UpdateDropdownValue();
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008EC4 File Offset: 0x000070C4
		private void OnDropdownValueChanged(int index)
		{
			this._option.TrySetValue((float)index);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00008ED3 File Offset: 0x000070D3
		private void UpdateDropdownValue()
		{
			this._dropdown.SetValueWithoutNotify(Mathf.RoundToInt(this._option.Value));
			this._dropdown.Hide();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00008EFC File Offset: 0x000070FC
		private void RefreshItems()
		{
			this._dropdown.ClearOptions();
			List<string> list = new List<string>();
			if (this._selectionOption.LocalizeItems)
			{
				list = Enumerable.ToList<string>(Enumerable.Select<string, string>(this._selectionOption.Items, (string x) => LocalizedText.CreateLocalizedString(x).GetLocalizedString()));
			}
			else
			{
				list = Enumerable.ToList<string>(this._selectionOption.Items);
			}
			this._dropdown.AddOptions(list);
			this.UpdateDropdownValue();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008F81 File Offset: 0x00007181
		private void OnOptionChanged(OptionType optionType)
		{
			if (optionType == OptionType.Language)
			{
				this.RefreshItems();
			}
		}

		// Token: 0x040001E4 RID: 484
		[SerializeField]
		private TMP_Dropdown _dropdown;

		// Token: 0x040001E5 RID: 485
		[SerializeField]
		private LocalizedText _name;

		// Token: 0x040001E6 RID: 486
		private SelectionOption _selectionOption;
	}
}
