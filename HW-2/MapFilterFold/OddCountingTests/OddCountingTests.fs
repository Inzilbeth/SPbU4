namespace MapFilterFold.OddCountingTests

open FsCheck
open FsUnit
open NUnit.Framework
open MapFilterFold.EvenCounters

[<TestFixture>]
module Tests =

    [<Test>]
    let ``Should work on simple input`` () =
        let list = [1; -2; 3; 4; -5; 6]
        EvenCounters.mapEvenCounter list |> should equal 3
        EvenCounters.filterEvenCounter list |> should equal 3
        EvenCounters.foldEvenCounter list |> should equal 3

    [<Test>]
    let ``Should be nonnegative`` () =
        let resultMustBeNonnegative list =
            EvenCounters.mapEvenCounter list >= 0 &&
            EvenCounters.filterEvenCounter list >= 0 &&
            EvenCounters.foldEvenCounter list >= 0

        Check.QuickThrowOnFailure resultMustBeNonnegative

    [<Test>]
    let ``Should have same output`` () =
        let evenCountersHaveSameOutputs list =
            let mapResult = EvenCounters.mapEvenCounter list
            let filterResult = EvenCounters.filterEvenCounter list
            let foldResult = EvenCounters.foldEvenCounter list
            (mapResult = filterResult) && (filterResult = foldResult)

        Check.QuickThrowOnFailure evenCountersHaveSameOutputs