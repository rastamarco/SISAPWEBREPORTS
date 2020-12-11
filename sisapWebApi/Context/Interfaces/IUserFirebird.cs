using sisapWebApi.Models.FirebirdEntitty;
using System.Collections.Generic;

namespace sisapWebApi.Context.Interfaces
{
    public interface IUserFirebird
    {
        IEnumerable<UserFirebird> GetAllUsers();
    }
}
