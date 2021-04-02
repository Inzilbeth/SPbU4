namespace Tests

open Task2
open FsUnit
open NUnit.Framework

module Tests =
    [<Test>]
    let ``Simple test 1`` () =
        Lib.supermap (fun x -> [x + 1; x * 5]) [1; 2; 3]
            |> should equal [2; 5; 3; 10; 4; 15]

    [<Test>]
    let ``Simple test 2`` () =
        Lib.supermap (fun x -> [x; x]) ["a"; "b"]
            |> should equal ["a"; "a"; "b"; "b"]