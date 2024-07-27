using Library.Data.RequestFeatures;
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
        Task<PagedList<Reservation>> GetAllReservations(ReservationParameters reservationParameters, bool trackChanges);
        Task<Reservation?> GetReservation(Guid id,  bool trackChanges);  
        Task<int> GetTotalNumberOfReservations();   

        void CreateReservation(Reservation reservation);    
        void DeleteReservation(Reservation reservation);    
    }
}
