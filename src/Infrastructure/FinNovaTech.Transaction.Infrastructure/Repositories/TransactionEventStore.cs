using FinNovaTech.Transaction.Application.Interfaces;
using FinNovaTech.Transaction.Domain.Entities;
using FinNovaTech.Transaction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinNovaTech.Transaction.Infrastructure.Repositories
{
    public class TransactionEventStore : ITransactionEventStore
    {
        private readonly ApplicationDbContext _context;

        public TransactionEventStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(TransactionEvent transactionEvent)
        {
            _context.TransactionEvents.Add(transactionEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransactionEvent>> GetEventsByAccountIdAsync(int accountId)
        {
            return await _context.TransactionEvents.Where(e => e.AccountId == accountId).OrderBy(e => e.Timestamp).ToListAsync();
        }
    }
}