using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.DTOs;
using FinNovaTech.User.Application.Interfaces;
using MediatR;
using System.Net;

namespace FinNovaTech.User.Application.Queries.Logs.Handler
{
    public class GetUserLogsHandler(IUserLogRepository repository) : IRequestHandler<GetLogByUserIdQuery, Response<List<UserLogsDto>>>
    {
        private readonly IUserLogRepository _repository = repository;

        public async Task<Response<List<UserLogsDto>>> Handle(GetLogByUserIdQuery request, CancellationToken cancellationToken)
        {
            var logs = await _repository.GetLogsByUserIdAsync(request.UserId);
            var logDTOs = logs.Select(log => new UserLogsDto { Action = log.Action }).ToList();
            return new Response<List<UserLogsDto>>(true, "Logs encontrados", logDTOs, (int)HttpStatusCode.OK);
        }
    }
}