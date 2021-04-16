using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Types
{
    public enum ErrorCode
    {
        IdLessThen1,

        // UIElements
        UIElements_NameMaxLength,
        UIElements_NameIsEmpty,
        UIElements_NameAlreadyUsed,

        UIElements_FindByIsEmpty,
        UIElements_FindByMaxLength,

        // UITestCases
        UITestCases_NameMaxLength,        
        UITestCases_NameIsEmpty,
        UITestCases_NameAreadyUsed,

        UITestCases_StartUrlIsEmpty,        
        
        UITestCases_UIElementNotFound,
        UITestCases_UserFilesNotFound,

        UITestCases_StepsAreNotUnique,

        // UIEvent
        UIEvents_NameIsEmtpy,
        UIEvents_NameMaxLength,

        // UITestRuns
        UITestRuns_TestCasesNotFound,
        UITestRuns_TestCasesAreEmpty,        

        // UIPages
        UIPages_NameIsEmpty,
        UIPages_NameMaxLength,

        // TestCaseStatusInfo
        UITestCaseStatusInfo_StartIsEmpty,
        UITestCaseStatusInfo_EndIsEmpty,
        UITestCaseStatusInfo_EndLessThenStart,

        // HealthChecks
        HealthChecks_NameIsEmpty,
        HealthChecks_NameMaxLength,
        HealthChecks_UriIsEmpty,

        // UserFiles
        UserFiles_FileNameIsEmpty,
        UserFiles_FileSizeIsNegative,
        UserFiles_FileNameMaxLength,

        // Pagination
        PageIndexLessThan0,
        PageSizeLessThen1,

        // Deleting
        DeletionFailed
    }
}
