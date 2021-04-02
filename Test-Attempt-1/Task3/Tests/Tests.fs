namespace Tests

open Task3
open NUnit.Framework
open FsUnit
open System.IO

module Tests =
    [<Test>]
    let ``Simple push test`` () =
        let stack = new ConcurrentStack<int>()
        stack.TryPop() |> should equal None
        stack.Push 10
        stack.TryPop() |> should equal (Some 10)

    [<Test>]
    let ``Simple pop test`` () =
        let stack = new ConcurrentStack<int>()
        stack.Push 1
        stack.Push 2

        stack.TryPop() |> should equal (Some 2)
        stack.TryPop() |> should equal (Some 1)
        stack.TryPop() |> should equal None

    // no concurrency tests :(