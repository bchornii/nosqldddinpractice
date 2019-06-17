using AutoMapper;
using NoSqlDddInPractice.Api.Models;
using NoSqlDddInPractice.Commands.Commands;
using NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate;
using NoSqlDddInPractice.Domain.ReadModels;
using NoSqlDddInPractice.Domain.ReadModels.SnackMachine;

namespace NoSqlDddInPractice.Api.Infrastructure.MappingProfiles
{
    public class SnackMachineMappingProfile : Profile
    {
        public SnackMachineMappingProfile()
        {
            CreateMap<InitializeSnakMachineDto, InitializeSnakMachineCommand>();

            CreateMap<AddSnackMachineSlotDto, AddSnackMachineSlotCommand>();

            CreateMap<BuySnackDto, BuySnackCommand>();

            CreateMap<SnackType, SnakTypeReadModel>();

            CreateMap<Slot, SlotReadModel>()
                .ForMember(d => d.ItemPrice, s => s.MapFrom(opt => opt.GetItemPrice()))
                .ForMember(d => d.ItemQuantity, s => s.MapFrom(opt => opt.GetItemsQuantity()))
                .ForMember(d => d.Position, s => s.MapFrom(opt => opt.GetPosition()))
                .ForMember(d => d.SnakType, s => s.MapFrom(opt => opt.SnackType));

            CreateMap<SnackMachine, SnackMachineReadModel>()
                .ForMember(d => d.HasMoneyInside, s => s.MapFrom(opt => opt.MoneyInside.Amount > 0))
                .ForMember(d => d.Slots, s => s.MapFrom(opt => opt.Slots));
        }
    }
}
