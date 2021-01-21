using System.Collections.Generic;
using System.Linq;

namespace DataPackChecker.Shared.Data.Resources {
    public class Function : Resource {
        public override string FilePath => $"functions/{Identifier}.mcfunction";
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

        /// <summary>
        /// Return all uniquely referenced functions (recursive), including this one.
        /// </summary>
        public List<Function> ReferencesFlat {
            get {
                HashSet<Function> result = new HashSet<Function>();
                Queue<Function> queue = new Queue<Function>();
                queue.Enqueue(this);
                while (queue.Count > 0) {
                    var item = queue.Dequeue();
                    result.Add(item);
                    foreach (Function f in item.References) if (!result.Contains(f)) queue.Enqueue(f);
                }
                return result.ToList();
            }
        }

        /// <summary>
        /// Return all commands (including NextCommands) of all uniquely referenced functions (recursive), including this one.
        /// This is an expensive operation.
        /// </summary>
        public List<Command> CommandsFlatWithReferences {
            get {
                return ReferencesFlat.SelectMany(f => f.CommandsFlat).ToList();
            }
        }

        public Function(string path, string name) : base(path, name) { }
    }
}
