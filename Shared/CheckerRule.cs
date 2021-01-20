using DataPackChecker.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DataPackChecker.Shared {
    abstract public class CheckerRule {
        abstract public string Title { get; }
        abstract public string Description { get; }
        abstract public List<string> GoodExamples { get; }
        abstract public List<string> BadExamples { get; }
        virtual public List<string> ConfigExamples { get; } = new List<string>();

        abstract public void Run(DataPack pack, JsonElement? config, Output output);
    }
}
