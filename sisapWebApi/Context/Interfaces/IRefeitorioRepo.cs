using System.Collections.Generic;
using sisapWebApi.Models.SQLServer;

namespace sisapWebApi.Context.Interfaces
{
    public interface IRefeitorioRepo
    {
        IEnumerable<Refeitorio> GetAllApontamentos();
    }
}
