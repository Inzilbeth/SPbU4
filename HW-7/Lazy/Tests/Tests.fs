namespace Tests

open Lazy
open FsUnit
open NUnit.Framework
open System.Threading.Tasks
open System.Collections.Generic

type Tests () =
    static let mutable computeCount = 0

    static let incrementCount () = computeCount <- computeCount + 1

    static let intSupplier () = incrementCount(); 333 + 444
    static let listSupplier () = incrementCount(); new List<int>()

    static let checkValue (obj: ILazy<'t>) (value: 't) =
         obj.Get() |> should equal value
         obj.Get() |> should equal value
         obj.Get() |> should equal value

    static let checkSingleThreadedComputationCount (obj: ILazy<'t>) =
        computeCount <- 0

        obj.Get() |> ignore
        obj.Get() |> ignore
        obj.Get() |> ignore

        computeCount |> should equal 1

    static let checkMultiThreadedComputationCount (obj: ILazy<'t>) =
        computeCount <- 0
        Parallel.For(0, 16, (fun _ -> obj.Get() |> ignore)) |> ignore
        computeCount |> should equal 1

    static member AllLazyCases =
        [|
            LazyFactory.CreateSingleThreaded(intSupplier)
            LazyFactory.CreateMultiThreaded(intSupplier)
            LazyFactory.CreateLockFree(intSupplier)
        |]

    static member ConcurrentLazyCases =
        [|
            LazyFactory.CreateMultiThreaded(listSupplier)
            LazyFactory.CreateLockFree(listSupplier)
        |]

    [<TestCaseSource(nameof Tests.AllLazyCases)>]
    member _.``Should work in a simple case`` (case: ILazy<int>) =
        let obj = case
        checkValue obj 777

    [<TestCaseSource(nameof Tests.AllLazyCases)>]
    member _.``Should calculate the value only once`` (case: ILazy<int>) =
        let obj = case
        checkSingleThreadedComputationCount obj

    [<TestCaseSource(nameof Tests.ConcurrentLazyCases)>]
    member _.``Should return original value`` (case: ILazy<List<int>>) =
        let obj = case

        let result = obj.Get()
        result.Add(777)

        Parallel.For(0, 16, (fun _ -> obj.Get() |> should equal result)) |> ignore

    [<Test>]
    member _.``Should compute original value only once`` () =
        let obj = LazyFactory.CreateMultiThreaded(listSupplier)
        checkMultiThreadedComputationCount obj
