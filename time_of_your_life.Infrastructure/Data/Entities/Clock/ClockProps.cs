namespace time_of_your_life.Infrastructure.Data.Entities;
public class ClockProps
{
    public Guid Id { get; set; }
    public string TitleText { get; set; }
    public string FontFamily { get; set; }
    public int TitleFontSize { get; set; }
    public int ClockFontSize { get; set; }
    public bool BlinkColons { get; set; }
    public string TitleFontColor { get; set; }
    public string ClockFontColor { get; set; }
}
