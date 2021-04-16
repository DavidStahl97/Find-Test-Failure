using System;

namespace TestFramework.Contract
{
    public static class Contracts
    {
        public static class UIElements
        {
            public const int NameMaxLength = 50;
        }

        public static class UIPages
        {
            public const int NameMaxLength = 255;
        }

        public static class UITestCases
        {
            public const int NameMaxLength = 50;
        }

        public static class HealthChecks
        {
            public const int NameMaxLength = 50;
        }

        public static class UserFiles
        {
            public const int FileNameMaxLength = 100;
        }

        public static class UIEvents
        {
            public const int NameMaxLength = 255;

            public const int InputMaxLength = 16000;
        }
    }
}
