namespace MapFilterFold.OddCountingTests

open FsCheck
open FsUnit
open NUnit.Framework
open MapFilterFold.OddCounters

[<TestFixture>]
module Tests =

    [<Test>]
    let ``Should work on simple input`` () =
        let list = [1; -2; 3; 4; -5; 6]
        OddCounters.mapOddCounter list |> should equal 3
        OddCounters.filterOddCounter list |> should equal 3
        OddCounters.foldOddCounter list |> should equal 3

    [<Test>]
    let ``Should be nonnegative`` () =
        let resultMustBeNonnegative list =
            OddCounters.mapOddCounter list >= 0 &&
            OddCounters.filterOddCounter list >= 0 &&
            OddCounters.foldOddCounter list >= 0

        Check.QuickThrowOnFailure resultMustBeNonnegative

    [<Test>]
    let ``Should have same output`` () =
        let oddCountersHaveSameOutputs list =
            let mapResult = OddCounters.mapOddCounter list
            let filterResult = OddCounters.filterOddCounter list
            let foldResult = OddCounters.foldOddCounter list
            (mapResult = filterResult) && (filterResult = foldResult)

        Check.QuickThrowOnFailure oddCountersHaveSameOutputs