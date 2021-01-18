using System.Collections.Generic;
using System.Linq;

namespace DataPackChecker.Shared.Data.Resources {
    public class Function : Resource {
        /// <summary>
        /// Returns all commands in this function. Each command might have a NextCommand (e.g. /execute) and
        /// that command might have a NextCommand of its own. You might want to use CommandsFlat instead.
        /// </summary>
        public List<Command> Commands { get; set; } = new List<Command>();

        /// <summary>
        /// Returns all commands, including NextCommands (recursive).
        /// </summary>
        public List<Command> CommandsFlat {
            get {
                return Commands.SelectMany(c => c.Flat).ToList();
            }
        }

        public List<Function> References { get; } = new List<Function>();
        public List<Function> ReferencesFlat {
            get {
                //TODO FIX REFERENCES FLAT
                return null;
            }
        }

        public Function(string path, string name) : base(path, name) { }
    }
}
