using Commander.Models;
using System.Collections.Generic;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id = 0,HowTo = "What?",Line = "Line 1",Platform = "Platforma 1"},
                new Command{Id = 1,HowTo = "Where?",Line = "Line 2",Platform = "Platforma 2"},
                new Command{Id = 2,HowTo = "When?",Line = "Line 3",Platform = "Platforma 3"}

        };
            return commands;

        }

        public Command GetCommandById(int id)
        {
            return new Command
            {
                Id = 0,
                HowTo = "What?",
                Line = "Line 1",
                Platform = "Platforma 1"
            };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}
