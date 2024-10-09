namespace HMISimulator.API.SDK.Oven.Requests;

public record struct AddErrorRequest(OvenErrorType ErrorType);