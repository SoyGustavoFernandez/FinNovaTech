using FinNovaTech.Transaction.Application.DTOs;
using FinNovaTech.Transaction.Application.Interfaces;
using FinNovaTech.Transaction.Domain.Entities;
using FinNovaTech.Transaction.Domain.Enums;
using FinNovaTech.Transaction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinNovaTech.Transaction.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveTransactionAsync(TransactionEvent entity)
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