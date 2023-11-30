using Microsoft.Extensions.Logging;
using Moq;
using time_of_your_life.Application.Contratcs.Clock;
using time_of_your_life.Application.Managers;
using time_of_your_life.Controllers;
using time_of_your_life.Infrastructure.Transport.Clock.Result;
using time_of_your_life.Infrastructure.Transport.Core.Response;

namespace time_of_your_life.Test
{
    public class ClockControllerUnitTest
    {
        [Fact]
        public async Task GetPreset_ReturnsOkObjectResult()
        {
            // Arrange
            var mockClockManager = new Mock<IClockManager>();
            var mockLogger = new Mock<ILogger<ClockController>>();

            var clockController = new ClockController(mockClockManager.Object, mockLogger.Object);

            var expectedId = new Guid("c0adc222-fe54-42be-959a-7d99f4536d31");
            var expectedResult = new GetPresetByIdResult<ClockPropsDto>
            {
                preset = new ClockPropsDto
                {
                    Id = expectedId,
                    TitleText = "The Time of Your Life 2",
                    FontFamily = "courier",
                    TitleFontSize = 64,
                    ClockFontSize = 48,
                    BlinkColons = true,
                    ClockFontColor = "black",
                    TitleFontColor = "black"
                }
            };

            // Configura el comportamiento esperado del IClockManager
            mockClockManager
                .Setup(manager => manager.GetPresetByIdAsync(expectedId))
                .ReturnsAsync(BaseResponse<GetPresetByIdResult<ClockPropsDto>>.Ok(expectedResult));

            // Act
            var result = await clockController.GetPreset(expectedId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult, result.Result);
        }
    }
}