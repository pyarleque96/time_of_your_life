namespace time_of_your_life.Infrastructure.Transport.Clock.Result
{
    public class GetAllPresetsResult<TResult>
    {
        public IEnumerable<TResult> presets { get; set; }
    }
}
