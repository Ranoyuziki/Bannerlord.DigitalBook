using System;
using TaleWorlds.CompanionBook.Options;
using UnityEngine;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.MenuSection.Options
{
	// Token: 0x0200004B RID: 75
	public abstract class OptionItem : MonoBehaviour
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000259 RID: 601 RVA: 0x000088EC File Offset: 0x00006AEC
		// (set) Token: 0x0600025A RID: 602 RVA: 0x000088F4 File Offset: 0x00006AF4
		public Selectable Selectable { get; private set; }

		// Token: 0x0600025B RID: 603 RVA: 0x000088FD File Offset: 0x00006AFD
		public void ResetToInitialValue()
		{
			this._option.TrySetValue(this._initialValue);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00008910 File Offset: 0x00006B10
		public void ResetToDefaultValue()
		{
			this._option.Reset();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00008920 File Offset: 0x00006B20
		protected void OnInitialize(OptionBase option, Selectable selectable)
		{
			this._option = option;
			OptionBase option2 = this._option;
			option2.OnChange = (Action<GenericOptionType, float>)Delegate.Combine(option2.OnChange, new Action<GenericOptionType, float>(this.OnOptionValueChanged));
			this._initialValue = option.Value;
			this.Selectable = selectable;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000896F File Offset: 0x00006B6F
		protected void OnFinalize()
		{
			OptionBase option = this._option;
			option.OnChange = (Action<GenericOptionType, float>)Delegate.Remove(option.OnChange, new Action<GenericOptionType, float>(this.OnOptionValueChanged));
		}

		// Token: 0x0600025F RID: 607
		protected abstract void OnOptionValueChanged(GenericOptionType optionType, float value);

		// Token: 0x040001D5 RID: 469
		protected OptionBase _option;

		// Token: 0x040001D6 RID: 470
		private float _initialValue;
	}
}
