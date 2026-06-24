using Spectacles.Platform;

namespace Spectacles.Tests.Platform;

public sealed class PresentationTests
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
}
