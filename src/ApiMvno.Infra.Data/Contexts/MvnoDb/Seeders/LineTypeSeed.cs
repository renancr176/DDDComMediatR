using ApiMvno.Domain.Attributes;
using ApiMvno.Domain.Core.Extensions;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Enums;
using ApiMvno.Domain.Interfaces.Repositories;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Seeders;

public class LineTypeSeed : ILineTypeSeed
{
    private readonly ILineTypeRepository _lineTypeRepository;

    public LineTypeSeed(ILineTypeRepository lineTypeRepository)
    {
        _lineTypeRepository = lineTypeRepository;
    }

    public async Task SeedAsync()
    {
        foreach (var lineTypeEnum in Enum.GetValues<LineTypeEnum>())
        {
            if (!await _lineTypeRepository.AnyAsync(lt => lt.Type == lineTypeEnum))
            {
                var enumAttribute = lineTypeEnum.GetAttributeOfType<LineTypeAttribute>();
                await _lineTypeRepository.InsertAsync(
                    new LineType()
                    {
                        Type = lineTypeEnum,
                        Name = enumAttribute?.Name ?? lineTypeEnum.ToString(),
                        NumasyId = enumAttribute?.NumasyId ?? 0,
                        Active = true
                    });

                await _lineTypeRepository.SaveChangesAsync();
            }
        }
    }
}