namespace BracketsMatching.Tests

open FsUnit
open NUnit.Framework
open BracketsMatching.Checker

type Tests () =

    static member CheckerTestCases =
        [|
            "() ((())) [] {{{}}} {{(([)}])}", false
            "() ((())) [] {{{}}} {{(([}])}", false
            "", true
            "(◨) (((𓁟 ))) [] {{▟ {}}ddaa} {{((gsg[)}0157])}   ", false
            "        ", true
            "(}", false
            "[} []", false
            "{{}}}{", false
            "aaaaAaaaAa", true
            "([)]", false
            "[[()]] {{(([]))}} {} [[]] ([{}])", true
        |]

    [<TestCaseSource(nameof Tests.CheckerTestCases)>]
    member _.``Should determine correct brackets placements`` (testCase) =
        let string, expectedResult = testCase
        Checker.check string |> should equal expectedResult
