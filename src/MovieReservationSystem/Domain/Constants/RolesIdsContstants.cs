namespace MovieReservationSystem.Domain.Constants;

public class RolesIdsConstants
{
    /// <summary>
    /// Идентификатор системной роли "Администратор".
    /// </summary>
    public readonly Guid AdminRoleIdConstant = Guid.Parse("4650f036-ed7f-4397-a4d6-496bdf9f1872");
    
    /// <summary>
    /// Идентификатор системной роли "Пользователь".
    /// </summary>
    public readonly Guid UserRoleIdConstant = Guid.Parse("064778ca-9179-4535-8d27-eb05ed4cfd1d");
}