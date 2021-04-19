namespace Tests

open RoundingWorkflow.Library
open NUnit.Framework
open FsUnit

module Tests =
    [<Test>]
    let ``Should work on a simple case`` () =
        let flow = Workflow 3 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }

        flow |> should (equalWithin 0.001) 0.048

    [<Test>]
    let ``Should work on another simple case`` () =
        let flow = Workflow 5 {
            let! a = 10.0 * 12.3456789
            let! b = 3.14
            return a / b
        }

        flow |> should (equalWithin 0.00001) 39.31745
