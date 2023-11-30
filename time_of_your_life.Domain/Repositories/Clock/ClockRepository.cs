using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using time_of_your_life.Infrastructure.Data.Context;
using time_of_your_life.Infrastructure.Data.Entities;
using time_of_your_life.Infrastructure.ExceptionHandler;

namespace time_of_your_life.Domain.Repositories.Clock;

public class ClockRepository : IClockRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ClockRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<ClockProps>> GetAllPresets()
    {
        try
        {
            var result = await _applicationDbContext.ClockPresets.ToListAsync();

            return result;
        }
        catch (Exception ex)
        {
            throw new DomainException(ex.Message, ex.InnerException ?? ex);
        }
    }

    public async Task<ClockProps> GetPresetById(Guid id)
    {
        try
        {
            var result = await _applicationDbContext.ClockPresets
                                                     .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        catch (Exception ex)
        {
            throw new DomainException(ex.Message, ex.InnerException ?? ex);
        }

    }

    public async Task<Guid> AddPreset(ClockProps parameters)
    {
        try
        {
            var obj = new ClockProps
            {
                TitleText = parameters.TitleText,
                FontFamily = parameters.FontFamily,
                TitleFontSize = parameters.TitleFontSize,
                ClockFontSize = parameters.ClockFontSize,
                BlinkColons = parameters.BlinkColons,
                TitleFontColor = parameters.TitleFontColor,
                ClockFontColor = parameters.ClockFontColor
            };

            await _applicationDbContext.ClockPresets.AddAsync(obj);
            _applicationDbContext.SaveChanges();

            return obj.Id;
        }
        catch (Exception ex)
        {
            throw new DomainException(ex.Message, ex.InnerException ?? ex);
        }
    }

    public async Task<bool> UpdatePreset(ClockProps parameters)
    {
        try
        {
            var obj = _applicationDbContext.ClockPresets.FirstOrDefault(x => x.Id == parameters.Id);

            if (obj == null)
            {
                throw new DomainException("Edit: Preset no encontrado.");
            }

            obj.TitleText = parameters.TitleText;
            obj.FontFamily = parameters.FontFamily;
            obj.TitleFontSize = parameters.TitleFontSize;
            obj.ClockFontSize = parameters.ClockFontSize;
            obj.BlinkColons = parameters.BlinkColons;
            obj.TitleFontColor = parameters.TitleFontColor;
            obj.ClockFontColor = parameters.ClockFontColor;

            _applicationDbContext.ClockPresets.Update(obj);
            _applicationDbContext.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            throw new DomainException(ex.Message, ex.InnerException ?? ex);
        }
    }
}
