namespace TemperoDaVovo.Domain.Entities;

public class VerificationCode : BaseEntity
{
    public Guid UserId { get; set; }
    public string Code { get; set; } = string.Empty;
    public VerificationCodeType Type { get; set; }
    public DateTime ExpiresAt { get; set; }
    public int Attempts { get; set; } = 0;
    public bool IsUsed { get; set; } = false;

    public User User { get; set; } = null!;

    public bool IsExpired() => DateTime.UtcNow > ExpiresAt;
    public bool IsValid() => !IsUsed && !IsExpired() && Attempts < 3;
}