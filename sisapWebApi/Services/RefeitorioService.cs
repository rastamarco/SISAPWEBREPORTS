using sisapWebApi.Context;
using sisapWebApi.Context.Interfaces;
using sisapWebApi.Models.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sisapWebApi.Services
{
    public class RefeitorioService : IRefeitorioRepo
    {
        private readonly DataContextSQLServer _context;
        public RefeitorioService(DataContextSQLServer context)
        {
            _context = context; 
        }
        public IEnumerable<Refeitorio> GetAllApontamentos()
        {
            return _context.Refeitorio.ToList();
        }
    }
}
