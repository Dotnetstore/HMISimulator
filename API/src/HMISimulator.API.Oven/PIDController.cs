namespace HMISimulator.API.Oven;

public class PIDController
{
    private double Kp;  // Proportional gain
    private double Ki;  // Integral gain
    private double Kd;  // Derivative gain

    private double integralSum;  // Sum of previous errors for integral calculation
    private double previousError;  // Last error value

    public PIDController(double kp, double ki, double kd)
    {
        Kp = kp;
        Ki = ki;
        Kd = kd;
        integralSum = 0.0;
        previousError = 0.0;
    }

    public double Compute(double setPoint, double currentTemperature, double deltaTime)
    {
        // Error between the target (setPoint) and current temperature
        var error = setPoint - currentTemperature;

        // Proportional term
        var proportional = Kp * error;

        // Integral term (sum of errors over time)
        integralSum += error * deltaTime;
        var integral = Ki * integralSum;

        // Derivative term (rate of error change)
        var derivative = Kd * (error - previousError) / deltaTime;

        // Save error for next derivative calculation
        previousError = error;

        // Compute the control signal (output to the heating element)
        return proportional + integral + derivative;
    }
}