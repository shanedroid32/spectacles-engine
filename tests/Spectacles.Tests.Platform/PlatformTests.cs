using System.Net.WebSockets;
using Spectacles.Platform;

namespace Spectacles.Tests.Platform;

public sealed class PlatformTests
{
    [Fact]
    public void Constructor_FitsInternalResolutionIntoWindow()
    {
        var presentation = new Presentation(new Presentation.PresentationConfig(), 1280, 720);
        Assert.Equal(new RectI(0, 0, 1280, 720), presentation.PresentationRect);
    }

    [Fact]
    public void Resize_CentersIntegerScaledImageInsideAwkwardWindow()
    {
        var presentation = new Presentation(new Presentation.PresentationConfig(), 1280, 720);
        presentation.Resize(1000, 700);
        Assert.Equal(new RectI(20, 80, 960, 540), presentation.PresentationRect);
    }

    [Fact]
    public void Constructor_CalculatesFixedDeltaFromHertz()
    {
        var clocks = new Clocks(fixedStepHz: 120);
        Assert.Equal(1f / 120f, clocks.FixedDeltaSeconds);
    }

    [Fact]
    public void BeginFrame_ReturnsOneStepForSixteenMilliseconds()
    {
        var clocks = new Clocks(fixedStepHz: 120);
        var steps = clocks.BeginFrame(0.016f);
        Assert.Equal(1, steps);
    }

    [Fact]
    public void BeginFrame_CapsFixedStepsPerFrame()
    {
        var clocks = new Clocks(fixedStepHz: 120, maxFixedStepsPerFrame: 8);

        var steps = clocks.BeginFrame(1.0f);

        Assert.Equal(8, steps);
    }
}
