namespace ItSupplyControl.Api.Modules.MasterData;

public class Site
{
	public int Id { get; set; }

	public string Name { get; set; } = null!;

	public string? Address { get; set; }

	public string? Cnpj { get; set; }

}
