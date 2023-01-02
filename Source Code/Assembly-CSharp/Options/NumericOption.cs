using System;
using UnityEngine;

namespace TaleWorlds.CompanionBook.Options
{
	// Token: 0x02000040 RID: 64
	public class NumericOption : OptionBase
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00007169 File Offset: 0x00005369
		public Vector2 Range { get; }

		// Token: 0x060001FF RID: 511 RVA: 0x00007171 File Offset: 0x00005371
		public NumericOption(GenericOptionType optionType, float value, float defaultValue, Vector2 range, Action<GenericOptionType, float> onChange) : base(value, defaultValue, optionType, onChange)
		{
			this.Range = range;
		}
	}
}
