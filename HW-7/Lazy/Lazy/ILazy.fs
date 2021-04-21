namespace Lazy

/// Interface for objects with lazy initialization.
type ILazy<'t> =

    /// Returns the value, calculating it only once on the first call.
    abstract member Get : unit -> 't
