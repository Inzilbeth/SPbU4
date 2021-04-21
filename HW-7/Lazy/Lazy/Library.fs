namespace Lazy

/// Type for creating different kinds of ILazy objects
type LazyFactory =
    /// Creates thread-unsafe implementation of ILazy
    static member CreateSingleThreaded supplier =
        new SingleThreadedLazy<'t>(supplier) :> ILazy<'t>

    /// Creates thread-safe implementation of ILazy
    static member CreateMultiThreaded supplier =
        new MultiThreadedLazy<'t>(supplier) :> ILazy<'t>

    /// Creates thread-safe lock free implementation of ILazy
    static member CreateLockFree supplier =
        new LockFreeLazy<'t>(supplier) :> ILazy<'t>
