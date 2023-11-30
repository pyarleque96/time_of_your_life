using AutoMapper;
using time_of_your_life.Application.Contratcs.Clock;
using time_of_your_life.Domain.Repositories.Clock;
using time_of_your_life.Infrastructure.Data.Entities;
using time_of_your_life.Infrastructure.Transport.Clock.Result;
using time_of_your_life.Infrastructure.Transport.Core.Response;

namespace time_of_your_life.Application.Managers
{
    public class ClockManager : IClockManager
    {
        private readonly IClockRepository _clockRepository;
        private readonly IMapper _mapper;

        public ClockManager(IClockRepository clockRepository,
                            IMapper mapper)
        {
            _clockRepository = clockRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<GetAllPresetsResult<ClockPropsDto>>> GetAllPresetsAsync()
        {
            var presets = _mapper.Map<IEnumerable<ClockPropsDto>>(await _clockRepository.GetAllPresets());

            var response = new GetAllPresetsResult<ClockPropsDto>
            {
                presets = presets
            };

            return BaseResponse<GetAllPresetsResult<ClockPropsDto>>.Complete(response);
        }

        public async Task<BaseResponse<GetPresetByIdResult<ClockPropsDto>>> GetPresetByIdAsync(Guid id)
        {
            var response = new GetPresetByIdResult<ClockPropsDto>
            {
                preset = _mapper.Map<ClockPropsDto>(await _clockRepository.GetPresetById(id))
            };

            return BaseResponse<GetPresetByIdResult<ClockPropsDto>>.Complete(response);
        }

        public async Task<BaseResponse<AddPresetResult>> AddPresetAsync(ClockPropsDto request)
        {
            return BaseResponse<AddPresetResult>.Complete(new AddPresetResult
            {
                PresetId = await _clockRepository.AddPreset(_mapper.Map<ClockProps>(request))
            });
        }

        public async Task<BaseResponse> UpdatePresetAsync(ClockPropsDto request)
        {
            return BaseResponse.Complete(await _clockRepository.UpdatePreset(_mapper.Map<ClockProps>(request)));
        }
    }
}
