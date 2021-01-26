# Data Pack Checker
![Banner](./.github/Banner.webp)
|--:|
© _2021, Bertie2011_

This project is a dynamic rule-based style checker CLI for Minecraft Data Packs.

## For Data Pack Creators
1. Download the [Data Pack Checker](https://github.com/Bertie2011/DataPackChecker/releases)
2. Download .dll files, which contain the actual rules. Place them in a Rules folder in the same folder as the executable.
   > **Note that a rule can do anything to your system, so do not download rule files of untrusted sources**.  
   > Try the [rules repository](https://github.com/Bertie2011/DataPackCheckerRules) by the author of Data Pack Checker for recommended basic rules.
3. Skip to the last step if you got given a config file.
4. Read up on which rules are available by opening up a Console/Terminal window and running the software with the `--rule-list` (`-l`) and `--rule-info <identifier>` (`-i`) options.
5. Create a config file (JSON), looking like this:
```JSON
[
    {
        "rule": "com.namespace.RuleClass (= identifier)",
        "config": {
            "first": "If a rule has no config, you can omit the config key.",
            "second": "The JSON structure is rule specific, use the rule info option for more information."
        }
    },
    {
        "...": "..."
    }
]
```
6. Run the checker with `--data-pack "<path to pack>" --config "<path to config>"` (or using `-d` and `-c` respectively) 

## For Communities, Moderators and Event Organizers
1. Make a selection of .dll files that contain the required rules.
   > Try the [rules repository](https://github.com/Bertie2011/DataPackCheckerRules) by the author of Data Pack Checker for recommended basic rules.
2. Make a configuration file (see above and `--config-help`) that lists the rules and their configurations.
3. Publish a list of .dll files and the configuration file for members/participants to check their data packs with.
4. Since there is no way to trust creators in relaying the results truthfully, you'll have to check data packs yourself too.

## For Developers
In order to make new rules follow the steps below.

### Setup & Creating Rules
1. Create a new .NET Core Class Library project (and solution) in Visual Studio.
   > Each project will end up being a .dll file, so use multiple projects to avoid creating one big .dll file. That way users can pick their rules a little more precise and not waste time on loading rules they don't want.
2. Download the [Shared.dll and Shared.xml files](https://github.com/Bertie2011/DataPackChecker/releases) and place them in the solution folder.
3. Right click `Project` > `Add` > `Reference...` > `Browse...` and select the .dll file.
   > A relative path is saved, so collaboration isn't a problem.  
   > The .xml file will be detected automatically if it's in the same folder as the .dll file.
4. Create classes subclassing from `CheckerRule`.
   > Besides required overriding of abstract members, there are also virtual members you might want to explore and override.  
   > Note that each rule is executed in its own thread, so your code **must be thread-safe**. As a rule of thumb, your rule should be stateless. This means that your rule does not save any data and one run cannot be influenced by another run.
   > The **identifier** that users will specify in their configuration consists of the fully qualified class name (`This.Is.The.Namespace.Rule1`).

### Running
1. Create a new gitignored folder or symbolic link in the solution folder. Use this folder to store and access a download of the [Data Pack Checker](https://github.com/Bertie2011/DataPackChecker/releases). This folder will later be referred to as **`<DPC>`**.
2. Go to `Project` > `Properties` > `Build Events` > `Post-build event command line`, enter the following commands and make it only run if project output is updated:
```Batchfile
xcopy "$(TargetDir)\$(TargetName).dll" "..\<DPC>\Rules" /Y /D /I
```
> Note: The $(var) syntax is supported by Visual Studio and will work as-is. Only replace the `<DPC>`.
3. Create a data pack and a config file to test your rule.
4. Go to `Project` > `Properties` > `Debug` and select `Executable` in the `Launch` dropdown. Then specify `..\<DPC>\<executable>` in `Executable`, `..\<DPC>` in `Working directory` and `-f ..\Arguments.txt` in `Application arguments`.
   > It is important to enter **relative paths**, since it will end up in version control and should work for any developer.
5. Create a gitignored `Arguments.txt` file in the parent directory of `<DPC>`, which should be the solution folder. The arguments (single line) will be passed to the executable. A couple examples:
```Batchfile
-o -l
-o -i <identifier>
-o -d "datapack" -c "config.json"
```
> Do not forget the **`-o/--keep-open`** switch. It keeps the process running after finishing, allowing you to see the results.  

### Publish & Update
1. Simply publish the .dll files in the `bin/Release/netcoreapp3.1` folders of the projects after **building on Release**.
2. To update to a new version of Data Pack Checker, simply overwrite the Shared.dll and Shared.xml files with a new version, fix any warnings/errors and re-publish.
