namespace efatoora.service.Contracts;

public class DashboardViewModel
{
    public bool IsOnBoarded { get; set; }
    public OnboardingResult OnboardingResult { get; set; }

}

public class OnboardingResult
{
    public string BinaryToken { get; set; }
    public string PrivateKey { get; set; }
    public string Secret { get; set; }

    public string UpdatedAt { get; set; }
}
