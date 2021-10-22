namespace Lazy

/// Thread-UNsafe implementation of ILazy
type SingleThreadedLazy<'t> (supplier : unit -> 't) =
    let mutable result = None

    interface ILazy<'t> with
        member _.Get() =
            if result.IsNone then
                result <- Some <| supplier()
            result.Value
