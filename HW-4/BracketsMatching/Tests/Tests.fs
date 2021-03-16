namespace BracketsMatching.Tests

open System
open FsUnit
open NUnit.Framework
open BracketsMatching.Checker

type Tests () =

    static member CheckerTestCases =
        [|
            "() ((())) [] {{{}}} {{(([)}])}", true
            "() ((())) [] {{{}}} {{(([}])}", false
            "", true
            "(◨) (((𓁟 ))) [] {{▟ {}}ddaa} {{((gsg[)}0157])}   ", true
            "        ", true
            "(}", false
            "[} []", false
            "{{}}}{", false
            "aaaaAaaaAa", true
        |]

    [<TestCaseSource(nameof Tests.CheckerTestCases)>]
    member this.``Should determine correct brackets placements`` (testCase) =
        let string, expectedResult = testCase
        Checker.check string |> should equal expectedResult
