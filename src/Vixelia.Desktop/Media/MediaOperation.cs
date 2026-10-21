namespace Vixelia.Domain.Media;

public enum MediaOperation
{
    Import = 1,
    Generate = 2,

    Enhance = 10,
    Upscale = 11,
    RemoveBackground = 12,
    Edit = 13,

    Crop = 20,
    Resize = 21,
    Rotate = 22,
    Flip = 23,
    Adjust = 24
}