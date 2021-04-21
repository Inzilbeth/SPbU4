namespace Lazy

open System.Threading

/// Thread-safe & lock free implemenation of ILazy
type LockFreeLazy<'t> (supplier : unit -> 't) =
    let mutable result = None

    interface ILazy<'t> with
        member _.Get() =
            if result.IsNone then
                let original = result
                let calculated = Some <| supplier()
                Interlocked.CompareExchange(&result, calculated, original) |> ignore
            result.Value
