using System;

namespace TaleWorlds.CompanionBook.Options
{
	// Token: 0x02000041 RID: 65
	public abstract class OptionBase
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00007186 File Offset: 0x00005386
		public float Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000718E File Offset: 0x0000538E
		public OptionBase(float value, float defaultValue, GenericOptionType optionType, Action<GenericOptionType, float> onChange)
		{
			this._value = value;
			this._defaultValue = defaultValue;
			this._optionType = optionType;
			this.OnChange = onChange;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000071B3 File Offset: 0x000053B3
		public void TrySetValue(float value)
		{
			if (this._value.CompareTo(value) != 0)
			{
				this._value = value;
				Action<GenericOptionType, float> onChange = this.OnChange;
				if (onChange == null)
				{
					return;
				}
				onChange.Invoke(this._optionType, value);
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000071E1 File Offset: 0x000053E1
		public void Reset()
		{
			this.TrySetValue(this._defaultValue);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000071EF File Offset: 0x000053EF
		public GenericOptionType GetOptionType()
		{
			return this._optionType;
		}

		// Token: 0x0400018D RID: 397
		public Action<GenericOptionType, float> OnChange;

		// Token: 0x0400018E RID: 398
		private float _value;

		// Token: 0x0400018F RID: 399
		private readonly float _defaultValue;

		// Token: 0x04000190 RID: 400
		private readonly GenericOptionType _optionType;
	}
}
