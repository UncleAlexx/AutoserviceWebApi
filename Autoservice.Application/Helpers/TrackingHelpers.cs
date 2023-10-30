namespace Autoservice.Application.Helpers;

internal static class TrackingHelpers
{
    public static void TrackVariable<T>(ref T trackingVariable, in T value) => trackingVariable = value;
}