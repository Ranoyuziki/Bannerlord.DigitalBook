using System;

namespace TaleWorlds.CompanionBook.ResourceSystem.CreditsResources
{
	// Token: 0x02000034 RID: 52
	public class CreditsNode
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000069FB File Offset: 0x00004BFB
		public string Value { get; } = string.Empty;

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00006A03 File Offset: 0x00004C03
		public CreditsNodeType Type { get; }

		// Token: 0x060001C6 RID: 454 RVA: 0x00006A0B File Offset: 0x00004C0B
		public CreditsNode(string value, CreditsNodeType type)
		{
			this.Value = value;
			this.Type = type;
		}
	}
}
