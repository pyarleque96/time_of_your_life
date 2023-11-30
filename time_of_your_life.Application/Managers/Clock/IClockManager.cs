using time_of_your_life.Application.Contratcs.Clock;
using time_of_your_life.Infrastructure.Transport.Clock.Result;
using time_of_your_life.Infrastructure.Transport.Core.Response;

namespace time_of_your_life.Application.Managers
{
    public interface IClockManager
    {
        Task<BaseResponse<GetAllPresetsResult<ClockPropsDto>>> GetAllPresetsAsync();
        Task<BaseResponse<GetPresetByIdResult<ClockPropsDto>>> GetPresetByIdAsync(Guid request);
        Task<BaseResponse<AddPresetResult>> AddPresetAsync(ClockPropsDto request);
        Task<BaseResponse> UpdatePresetAsync(ClockPropsDto request);
    }
}
