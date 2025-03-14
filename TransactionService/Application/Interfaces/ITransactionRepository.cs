﻿using TransactionService.Application.DTOs;
using TransactionService.Domain.Entities;

namespace TransactionService.Application.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<TransactionEventDTO>> GetBalanceAsync(int idCuenta);
        Task SaveTransactionAsync(TransactionEventEntity entity);
    }
}