namespace Lazy

open System

/// Thread-safe implementation of ILazy
type MultiThreadedLazy<'t> (supplier : unit -> 't) =
    let mutable result = None
    let obj = new Object()

    interface ILazy<'t> with
        member _.Get() =
            if result.IsNone then
                lock obj (fun () ->
                    if result.IsNone then
                        result <- Some <| supplier())
            result.Value
