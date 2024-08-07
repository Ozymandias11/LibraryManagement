using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Reports.Dto
{
    public record MonthlyReportDto(
           string Name,
           int January,
           int February,
           int March,
           int April,
           int May,
           int June,
           int July,
           int August,
           int September,
           int October,
           int November,
           int December,
           int TotalReservations
        );
    
    
}
