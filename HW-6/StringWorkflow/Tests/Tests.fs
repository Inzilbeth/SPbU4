namespace Tests

open StringWorkflow
open NUnit.Framework
open FsUnit

module Tests =
    [<Test>]
    let ``Should work on simple case`` () =
        let flow = Workflow() {
            let! x = "1"
            let! y = "2"
            let z = x + y

            return z
        }

        flow |> should equal (Some(3))

    [<Test>]
    let ``Should work on another simple case`` () =
        let flow = Workflow() {
            let! x = "1"
            let! y = "Ъ"
            let z = x + y

            return z
        }

        flow |> should equal None