using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Interfaces
{
   public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllReservations(int page, int pageSize, bool trackChanges);
        Task<Reservation?> GetReservation(Guid id,  bool trackChanges);  
        Task<int> GetTotalNumberOfReservations();   

        void CreateReservation(Reservation reservation);    
        void DeleteReservation(Reservation reservation);    
    }
}
