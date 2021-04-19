namespace StringWorkflow

open System

type Workflow () =
    member workflow.Bind(s : string, func) =
        let result = Int32.TryParse(s)

        match result with
        | false, _ -> None
        | true, value -> func value

    member workflow.Return(v) =
        Some(v)