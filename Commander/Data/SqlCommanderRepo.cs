using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {

        private readonly CommanderContext _context;

        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;

        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {

            if (cmd == null)
            {
                throw new ArgumentException(nameof(cmd));
            }
            _context.Commands.Remove(cmd);  
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command cmd)
        {
            //Not needed. DB Context takes care of this.
        }
    }
}
