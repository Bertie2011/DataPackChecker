using DataPackChecker.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DataPackChecker.Shared {
    abstract public class CheckerRule {
        abstract public string Title { get; }
        abstract public string Description { get; }
        abstract public string GoodExample { get; }
        abstract public string BadExample { get; }
        virtual public string ConfigExample { get; }

        abstract public void Run(DataPack pack, JsonElement config, Output output);
    }
}
