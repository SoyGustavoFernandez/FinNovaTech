using Microsoft.EntityFrameworkCore;
using TransactionService.Application.Interfaces;
using TransactionService.Domain.Entities;
using TransactionService.Infrastructure.Data;

namespace TransactionService.Infrastructure.Repositories
{
    public class TransactionEventStore : ITransactionEventStore
    {
        private readonly ApplicationDbContext _context;

        public TransactionEventStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(TransactionEventEntity transactionEvent)
        {
            _context.TransactionEvents.Add(transactionEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransactionEventEntity>> GetEventsByAccountIdAsync(int accountId)
        {
            return await _context.TransactionEvents.Where(x => x.AccountId == accountId).OrderBy(x => x.Timestamp).AsNoTracking().ToListAsync();
        }
    }
}