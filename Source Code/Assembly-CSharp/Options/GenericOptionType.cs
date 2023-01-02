using System;

namespace TaleWorlds.CompanionBook.Options
{
	// Token: 0x0200003F RID: 63
	public struct GenericOptionType
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00007112 File Offset: 0x00005312
		public readonly bool IsEngineOption { get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000711A File Offset: 0x0000531A
		public readonly object Type { get; }

		// Token: 0x060001F9 RID: 505 RVA: 0x00007122 File Offset: 0x00005322
		public GenericOptionType(EngineOptionType engineOptionType)
		{
			this.Type = engineOptionType;
			this.IsEngineOption = 1;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007137 File Offset: 0x00005337
		public GenericOptionType(OptionType optionType)
		{
			this.Type = optionType;
			this.IsEngineOption = 0;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000714C File Offset: 0x0000534C
		public static implicit operator GenericOptionType(EngineOptionType engineOptionType)
		{
			return new GenericOptionType(engineOptionType);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007154 File Offset: 0x00005354
		public static implicit operator GenericOptionType(OptionType optionType)
		{
			return new GenericOptionType(optionType);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000715C File Offset: 0x0000535C
		public override string ToString()
		{
			return this.Type.ToString();
		}
	}
}
