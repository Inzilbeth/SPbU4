namespace Task3

open System

type ConcurrentStack<'t> () =
    let mutable data : List<'t> = []
    let obj = new Object()

    member this.Push value =
        lock obj (fun () ->
            data <- value::data)

    member this.TryPop() =
        lock obj (fun () ->
            match data with
            | head::tail ->
                data <- tail
                Some head
            | [] -> None)
