namespace HMISimulator.API.SDK.Oven.Responses;

public record struct GetOvenStatusResponse(
    double CurrentTemperature, 
    bool HeatingElementOn);