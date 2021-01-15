using DataPackChecker.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace DataPackChecker.Rules {
    class RuleRegistry {
        public List<CheckerRule> Rules { get; } = new List<CheckerRule>();

        static public (RuleRegistry Registry, List<Exception> Errors) FromDirectory(string path) {
            RuleRegistry registry = new RuleRegistry();
            List<Exception> errors = new List<Exception>();

            if (!Directory.Exists(path)) return (registry, errors);

            foreach (string library in Directory.EnumerateFiles(path, "*")) {
                if (Path.GetExtension(library) != ".dll") continue;
                try {
                    var ass = Assembly.LoadFrom(library);
                    foreach (var type in ass.GetTypes()) {
                        if (typeof(CheckerRule).IsAssignableFrom(type)) {
                            try {
                                ConstructorInfo constructor = type.GetConstructor(new Type[0]);
                                if (constructor == null) throw new InvalidOperationException("No visible default constructor on " + type.AssemblyQualifiedName);
                                CheckerRule rule = (CheckerRule)constructor.Invoke(new object[0]);
                                registry.Rules.Add(rule);
                            } catch (Exception e) {
                                errors.Add(new TypeLoadException("Could not load " + type.AssemblyQualifiedName, e));
                            }
                        }
                    }
                } catch (Exception e) {
                    errors.Add(new IOException("Could not load " + Path.GetFileName(library), e));
                }
            }

            registry.Rules.Sort((CheckerRule a, CheckerRule b) => string.Compare(a.GetType().FullName, b.GetType().FullName));

            return (registry, errors);
        }
    }
}
