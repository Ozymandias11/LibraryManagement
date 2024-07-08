using Library.Data.Library.Interfaces;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Implementations
{
    public class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
    {
        public ReservationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateReservation(Reservation reservation)
            => Create(reservation);
      
        

        public void DeleteReservation(Reservation reservation) => Delete(reservation);
        

        public async Task<IEnumerable<Reservation>> GetAllReservations(bool trackChanges)
             => await FindByCondition(r => r.DeletedDate == null, trackChanges).ToListAsync();




        public async Task<Reservation?> GetReservation(Guid id, bool trackChanges)
            => await FindByCondition(r => r.ReservationId == id, trackChanges).FirstOrDefaultAsync();
        
            
        
    }
}
