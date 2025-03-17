using MediatR;
using System.Net;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Application.Queries.Logs;

namespace UserService.Application.Queries.Logs.Handlers
{
    public class GetUserLogsHandler : IRequestHandler<GetLogByUserIdQuery, ResponseDto<List<UserLogsDto>>>
    {
        private readonly IUserLogRepository _repository;

        public GetUserLogsHandler(IUserLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDto<List<UserLogsDto>>> Handle(GetLogByUserIdQuery request, CancellationToken cancellationToken)
        {
            var logs = await _repository.GetLogsByUserIdAsync(request.UserId);
            var logDTOs = logs.Select(log => new UserLogsDto { Action = log.Action }).ToList();
            return new ResponseDto<List<UserLogsDto>>(true, "Logs encontrados", logDTOs, (int)HttpStatusCode.OK);
        }
    }
}
