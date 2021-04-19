namespace Tests

open FsCheck
open FsUnit
open NUnit.Framework
open PointFree.Library

[<TestFixture>]
module Tests =

    [<Test>]
    let ``test`` () =
        let funcsAreEqual x list =
            let a = Library.func x list
            let b = Library.func01 x list
            let c = Library.func02 x list
            let d = Library.func03() x list

            a = b && b = c && c = d
        Check.QuickThrowOnFailure funcsAreEqual

