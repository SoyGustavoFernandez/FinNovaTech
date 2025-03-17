using Microsoft.EntityFrameworkCore;
using TransactionService.Application.DTOs;
using TransactionService.Application.Interfaces;
using TransactionService.Domain.Entities;
using TransactionService.Domain.Enums;
using TransactionService.Infrastructure.Data;

namespace TransactionService.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveTransactionAsync(TransactionEventEntity entity)
        {
            _context.TransactionEvents.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TransactionEventDto>> GetBalanceAsync(int idCuenta)
        {
            return await _context.TransactionEvents.Where(x => x.AccountId == idCuenta).OrderByDescending(x => x.Timestamp)
                .Select(x => new TransactionEventDto(x.AccountId, Enum.Parse<TransactionType>(x.Type), x.Amount)).ToListAsync();
        }
    }
}