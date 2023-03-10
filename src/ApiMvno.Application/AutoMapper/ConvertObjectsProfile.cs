using AutoMapper;

namespace ApiMvno.Application.AutoMapper;

public class ConvertObjectsProfile : Profile
{

    public ConvertObjectsProfile()
    {
        //Example
        //CreateMap<SourceEnum, DestinationEnum>().ConvertUsing((value, destination) =>
        //{
        //    switch (value)
        //    {
        //        case SourceEnum.Pending:
        //            return DestinationEnum.Pending;
        //        case SourceEnum.Completed:
        //            return DestinationEnum.Completed;
        //        case SourceEnum.Error:
        //            return DestinationEnum.Error;
        //        default:
        //            throw new NotImplementedException(
        //                "Cannot convert ApiMvno.Infra.CrossCutting.Portability.Models.Enums.SourceEnum To ApiMvno.Domain.Enums.DestinationEnum.");
        //    }
        //});
    }
}