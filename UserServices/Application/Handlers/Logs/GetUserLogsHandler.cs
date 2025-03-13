using MediatR;
using System.Net;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Application.Queries.Logs;

namespace UserService.Application.Handlers.Logs
{
    public class GetUserLogsHandler : IRequestHandler<GetLogByUserIdQuery, ResponseDTO<List<UserLogsDTO>>>
    {
        private readonly IUserLogRepository _repository;

        public GetUserLogsHandler(IUserLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDTO<List<UserLogsDTO>>> Handle(GetLogByUserIdQuery request, CancellationToken cancellationToken)
        {
            var logs = await _repository.GetLogsByUserIdAsync(request.UserId);
            var logDTOs = logs.Select(log => new UserLogsDTO { Action = log.Action }).ToList();
            return new ResponseDTO<List<UserLogsDTO>>(true, "Logs encontrados", logDTOs, (int)HttpStatusCode.OK);
        }
    }
}
