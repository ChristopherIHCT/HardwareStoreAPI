using AutoMapper;
using HardwareStore.Dto.Request;
using HardwareStore.Dto.Response;
using HardwareStore.Entities;
using System.Globalization;

namespace HardwareStore.Services.Implementations
{
    public class InvoiceProfile : Profile
    {
        private static readonly CultureInfo Culture = new("es-HN");

        public InvoiceProfile()
        {
            CreateMap<InvoiceDtoRequest, Invoices>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignorar mapeo de Id ya que es autogenerado
                .ForMember(dest => dest.DocNum, opt => opt.Ignore()) // Ignorar mapeo de DocNum ya que se genera automáticamente
                .ForMember(dest => dest.Total, opt => opt.Ignore()) // Ignorar mapeo de Total inicialmente
                          .ForMember(d => d.SaleDate, o => o.MapFrom(x => x.SaleDate.ToString("dd/MM/yyyy HH:mm", Culture)));


            CreateMap<Invoices, InvoiceDtoResponse>();
        }
    }
}
