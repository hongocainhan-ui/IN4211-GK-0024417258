namespace EduHub.Domain.Entities;

public class Member : BaseEntity
{
    public string Email { get; private set; }

    private Member(string email)
    {
        Email = email;
    }

    public static Member Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
        {
            throw new ArgumentException("Email must not be empty and must contain '@'.", nameof(email));
        }

        return new Member(email.Trim());
    }
}
