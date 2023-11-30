using System.ComponentModel.DataAnnotations;

namespace time_of_your_life.Application.Contratcs.Clock;

public class ClockPropsDto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(ErrorMessage = "The Title Text field must be less than 30 characters.")]
    [Required(ErrorMessage = "The Title Text field cannot be empty.")]
    public string TitleText { get; set; } = "The Time of Your Life";

    [Required(ErrorMessage = "The Font Family field cannot be empty.")]
    public string FontFamily { get; set; } = "courier";

    public int TitleFontSize { get; set; } = 64;

    public int ClockFontSize { get; set; } = 48;

    public bool BlinkColons { get; set; } = true;

    [Required(ErrorMessage = "The Title Font Color field cannot be empty.")]
    public string TitleFontColor { get; set; } = "black";

    [Required(ErrorMessage = "The Clock Font Color field cannot be empty.")]
    public string ClockFontColor { get; set; } = "black";
}
