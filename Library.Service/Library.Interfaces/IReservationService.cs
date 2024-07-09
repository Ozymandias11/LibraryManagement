﻿using FluentResults;
using Library.Service.Dto.Library.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Interfaces
{
   public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllReservations(bool trackChanges);
        Task<Result<ReservationDto>> GetReservation(Guid id, bool trackChanges);
        Task<Result> CreateReservation(CreateReservationDto createReservationDto);
    }
}