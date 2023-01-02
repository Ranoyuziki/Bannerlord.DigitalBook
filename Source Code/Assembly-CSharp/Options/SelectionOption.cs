using System;
using System.Collections.Generic;

namespace TaleWorlds.CompanionBook.Options
{
	// Token: 0x02000044 RID: 68
	public class SelectionOption : OptionBase
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00007628 File Offset: 0x00005828
		public IReadOnlyList<string> Items { get; } = new List<string>();

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00007630 File Offset: 0x00005830
		public bool LocalizeItems { get; }

		// Token: 0x06000218 RID: 536 RVA: 0x00007638 File Offset: 0x00005838
		public SelectionOption(GenericOptionType optionType, int value, int defaultValue, Action<GenericOptionType, float> onChange, IEnumerable<string> items) : base((float)value, (float)defaultValue, optionType, onChange)
		{
			List<string> list = new List<string>();
			foreach (string text in items)
			{
				list.Add(text);
			}
			this.Items = list;
			this.LocalizeItems = 0;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000076B0 File Offset: 0x000058B0
		public SelectionOption(GenericOptionType optionType, int value, int defaultValue, Action<GenericOptionType, float> onChange, int itemCount) : base((float)value, (float)defaultValue, optionType, onChange)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < itemCount; i++)
			{
				list.Add(string.Format("ui_menu_options_{0}_item_{1}", optionType.ToString().ToLower(), i));
			}
			this.Items = list;
			this.LocalizeItems = 1;
		}
	}
}
