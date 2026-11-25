using System.ComponentModel.DataAnnotations;
using ItSupplyControl.Api.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ItSupplyControl.Api.Modules.MasterData;

// DTO de entrada. Os atributos são validados antes de o endpoint rodar
// se falharem, a API responde 400 com estas mensagens
public record CreateSiteRequest(
	[Required(ErrorMessage = "Informe o nome do site.")]
	[MaxLength(64, ErrorMessage = "O nome pode ter no máximo 64 caracteres.")]
	string Name,
	[MaxLength(128, ErrorMessage = "O endereço pode ter no máximo 128 caracteres.")]
	string? Address,
	[MaxLength(18, ErrorMessage = "CNPJ Inválido.")]
	string? Cnpj
);

// DTO de saída
public record SiteResponse(int Id, string Name, string? Address, string? Cnpj);

public static class SiteEndpoints
{
	public static IEndpointRouteBuilder MapSiteEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/api/sites").WithTags("Sites");
		group.MapGet("/", ListSites);
		group.MapGet("/{id:int}", GetSite).WithName(nameof(GetSite));
		group.MapPost("/", CreateSite);


		return app;
	}

	static async Task<Ok<List<SiteResponse>>> ListSites(AppDbContext db)
	{
		var sites = await db.Sites
			.OrderBy(s => s.Name)
			.Select(s => new SiteResponse(s.Id, s.Name, s.Address, s.Cnpj))
			.ToListAsync();

		return TypedResults.Ok(sites);
	}

	static async Task<Results<Ok<SiteResponse>, NotFound>> GetSite(int id, AppDbContext db)
	{
		var site = await db.Sites.FindAsync(id);

		return site is null
			? TypedResults.NotFound()
			: TypedResults.Ok(new SiteResponse(site.Id, site.Name, site.Address, site.Cnpj));
	}

	static async Task<Results<CreatedAtRoute<SiteResponse>, ValidationProblem, ProblemHttpResult>> CreateSite(CreateSiteRequest request, AppDbContext db)
	{
		var name = request.Name.Trim();
		var address = string.IsNullOrWhiteSpace(request.Address) ? null : request.Address.Trim();
		var cnpj = NormalizeCnpj(request.Cnpj);

		if (cnpj is not null && cnpj.Length != 14)
		{
			return TypedResults.ValidationProblem(new Dictionary<string, string[]>
			{ ["cnpj"] = ["O CNPJ deve ter 14 caracteres, sem contar a pontuação."] });
		}

		if (await db.Sites.AnyAsync(s => s.Name == name))
		{
			return TypedResults.Problem(
				detail: $"Já existe um site chamado \"{name}\".",
				statusCode: StatusCodes.Status409Conflict);
		}

		var site = new Site { Name = name, Address = address, Cnpj = cnpj };
		db.Sites.Add(site);
		await db.SaveChangesAsync();

		return TypedResults.CreatedAtRoute(
			new SiteResponse(site.Id, site.Name, site.Address, site.Cnpj),
			nameof(GetSite),
			new { id = site.Id }
		);

		static string? NormalizeCnpj(string? value) =>
			string.IsNullOrWhiteSpace(value)
			? null
			: new string(value.Where(char.IsLetterOrDigit).ToArray()).ToUpperInvariant();
	}

};
