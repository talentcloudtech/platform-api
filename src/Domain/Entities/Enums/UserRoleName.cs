namespace Platform.Domain.Entities.Enums;

/// <summary>
/// The name of a user role. This role names are used in the application for type safety when checking roles.
/// </summary>
public class UserRole
{
    private UserRole(string name)
    {
        Name = name;
    }

    /// <summary>
    /// The name of the user role.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// A user that can log in to the platform.
    /// </summary>
    public static UserRole User
    {
        get { return new UserRole("User"); }
    }

    /// <summary>
    /// An admin user that can manage the platform.
    /// </summary>
    public static UserRole Admin
    {
        get { return new UserRole("Admin"); }
    }

    /// <summary>
    /// A customer that can create projects and manage them.
    /// </summary>
    public static UserRole Customer
    {
        get { return new UserRole("Customer"); }
    }
}
