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

        /// <summary>
        /// All Functions referenced by commands in this function.
        /// This includes any functions listed in referenced tags. If referenced tags contain more tags, each
        /// (indirectly) referenced tag is searched for more functions.
        /// </summary>
        public List<Function> References { get; } = new List<Function>();

        /// <summary>
        /// All referenced functions (recursive), including this one.
        /// This means that any function that is (indirectly) referenced through function commands
        /// or tags will be listed here.
        /// </summary>
        public List<Function> ReferencesFlat {
            get {
                HashSet<Function> result = new HashSet<Function>();
                Queue<Function> queue = new Queue<Function>();
                result.Add(this);
                queue.Enqueue(this);
                while (queue.Count > 0) {
                    var item = queue.Dequeue();
                    foreach (Function f in item.References) if (!result.Contains(f)) {
                        result.Add(item);
                        queue.Enqueue(f);
                    }
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
