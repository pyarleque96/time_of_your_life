using time_of_your_life.Infrastructure.Data.Entities;

namespace time_of_your_life.Domain.Repositories.Clock;

public interface IClockRepository
{
    Task<IEnumerable<ClockProps>> GetAllPresets();
    Task<ClockProps> GetPresetById(Guid parameter);
    Task<Guid> AddPreset(ClockProps parameters);
    Task<bool> UpdatePreset(ClockProps parameters);
}
