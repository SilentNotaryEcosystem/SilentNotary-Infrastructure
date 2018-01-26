namespace SmartDotNet.ApsNet.Identity.Interfaces
{
    public interface IExternalPreRegistration
    {
        string Provider { get; set; }

        string Sid { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }
    }
}
