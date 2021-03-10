namespace MapForTrees.Tests

open FsCheck
open FsUnit
open NUnit.Framework
open MapForTrees.TreeMap

[<TestFixture>]
module Tests =
    [<Test>]
    let ``Should work on simple case`` () =
         Tree(1, Tree(-9, Tip -8, Tree(0, Tip 15, Tip 5 )), Tree(7, Tip 45, Tip 10))
         |> TreeMap.mapTree (fun x -> x * 2)
         |> should equal
         <| Tree(2, Tree(-18, Tip -16, Tree(0, Tip 30, Tip 10 )), Tree(14, Tip 90, Tip 20))

    [<Test>]
    let ``Should not return null`` () =
        let mapTreeReturnsTree func tree =
            TreeMap.mapTree func tree |> should not' (be null)
        Check.QuickThrowOnFailure mapTreeReturnsTree

    [<Test>]
    let ``Map should be correctly applied in any case`` () =
        let checkIfMapAppliedCorrectly func tree =
            let traverse (tree : Tree<'a>) =
                let rec loop (tree : Tree<'a>) = seq {
                    match tree with
                    | Tip (value) ->
                        yield value
                    | Tree (value, left, right) ->
                        yield value
                        yield! loop left
                        yield! loop right
                }
                loop tree

            let original = traverse tree
            let result = traverse (TreeMap.mapTree func tree)

            original |> Seq.map(func) |> should equal result

        Check.QuickThrowOnFailure checkIfMapAppliedCorrectly
