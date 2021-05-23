﻿namespace DataPackChecker.Shared.Data.Resources.WorldGen {
    public class ProcessorList : JsonResource {
        public override string FilePath => $"worldgen/processor_list/{Identifier}.json";
        public override string TypeString => "Processor List";
        public ProcessorList(string path, string name) : base(path, name) {}
    }
}
