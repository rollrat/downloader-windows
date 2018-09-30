﻿/***

   Copyright (C) 2018. dc-koromo. All Rights Reserved.

   Author: Koromo Copy Developer

***/

using Koromo_Copy.Interface;
using Koromo_Copy.Utility;
using System.Collections.Generic;

namespace Koromo_Copy.Console.Utility
{
    /// <summary>
    /// Run 콘솔 옵션입니다.
    /// </summary>
    public class TestConsoleOption : IConsoleOption
    {
        [CommandLine("--help", CommandType.OPTION, Default = true)]
        public bool Help;

        [CommandLine("--cmd", CommandType.ARGUMENTS, DefaultArgument = true)]
        public string[] Commands;
    }
    
    class TestConsole : IConsole
    {
        /// <summary>
        /// Test 콘솔 리다이렉트
        /// </summary>
        static bool Redirect(string[] arguments, string contents)
        {
            arguments = CommandLineUtil.SplitCombinedOptions(arguments);
            arguments = CommandLineUtil.InsertWeirdArguments<TestConsoleOption>(arguments, true, "--cmd");
            TestConsoleOption option = CommandLineParser<TestConsoleOption>.Parse(arguments);

            if (option.Error)
            {
                Console.Instance.WriteLine(option.ErrorMessage);
                if (option.HelpMessage != null)
                    Console.Instance.WriteLine(option.HelpMessage);
                return false;
            }
            else if (option.Help)
            {
                PrintHelp();
            }
            else if (option.Commands != null)
            {
                ProcessTest(option.Commands);
            }

            return true;
        }

        bool IConsole.Redirect(string[] arguments, string contents)
        {
            return Redirect(arguments, contents);
        }

        static void PrintHelp()
        {
            Console.Instance.WriteLine(
                "Test Console\r\n"
                );
        }
        
        static void ProcessTest(string[] args)
        {
            switch (args[0])
            {
                case "version":
                    Version.ExportVersion();
                    break;
            }
        }
    }
}
