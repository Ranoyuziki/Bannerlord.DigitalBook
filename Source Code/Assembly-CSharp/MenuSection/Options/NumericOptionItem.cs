using System;
using TaleWorlds.CompanionBook.Options;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.MenuSection.Options
{
	// Token: 0x0200004A RID: 74
	public class NumericOptionItem : OptionItem
	{
		// Token: 0x06000253 RID: 595 RVA: 0x000087C5 File Offset: 0x000069C5
		private void OnDestroy()
		{
			base.OnFinalize();
			this._slider.onValueChanged.RemoveListener(new UnityAction<float>(this.OnSliderValueChanged));
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000087EC File Offset: 0x000069EC
		public void Initialize(NumericOption option)
		{
			base.OnInitialize(option, this._slider);
			this._range = option.Range;
			this._slider.onValueChanged.AddListener(new UnityAction<float>(this.OnSliderValueChanged));
			this._name.SetReference("ui_menu_options_" + option.GetOptionType().ToString().ToLower());
			this.UpdateValue();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00008862 File Offset: 0x00006A62
		protected override void OnOptionValueChanged(GenericOptionType optionType, float value)
		{
			this.UpdateValue();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000886A File Offset: 0x00006A6A
		private void OnSliderValueChanged(float value)
		{
			this._option.TrySetValue(value);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00008878 File Offset: 0x00006A78
		private void UpdateValue()
		{
			this._slider.SetValueWithoutNotify(this._option.Value);
			float num = (this._range.y - this._range.x) * this._option.Value + this._range.x;
			this._valueText.text = Mathf.RoundToInt(num).ToString();
		}

		// Token: 0x040001D0 RID: 464
		[SerializeField]
		private Slider _slider;

		// Token: 0x040001D1 RID: 465
		[SerializeField]
		private LocalizedText _name;

		// Token: 0x040001D2 RID: 466
		[SerializeField]
		private TMP_Text _valueText;

		// Token: 0x040001D3 RID: 467
		private Vector2 _range;
	}
}
